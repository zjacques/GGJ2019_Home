using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the dragging of the collectible objects.
/// </summary>
public class CollectibleScript : MonoBehaviour
{
    //  Initialise class variables
    private Vector3 newPosition;
    public float timeToLive;
    public Vector3 scale;
    private SpriteRenderer spriteR;  
    private bool beingDragged = false;

    private GameObject gameScriptObject;

    //  Timing
    bool growing = true;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        transform.localScale = new Vector3(0, 0, 0);
        gameScriptObject = GameObject.Find("GameScript");
    }

    // Update is called once per frame
    void Update()
    {
        //  Make the collectible grow.
        if (transform.localScale.x < scale.x && transform.localScale.y < scale.y && growing == true)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime);
        }
        
        //  Update the collectibles position so it continues to lerp.
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * 2);

        //  Shrink the collectible after it reaches max size. It stops shrinking if it is being dragged.  
        if ((Vector3.Magnitude(scale - transform.localScale) < 0.1 || growing == false) && !beingDragged)
        {
            growing = false;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime);
        }

        //  Delete the collectible after it shrinks.
        if (Vector3.Magnitude(transform.localScale) < 0.1 && growing == false)
        {
            Destroy(gameObject);
        }
    }

    void OnMouseDrag()
    {
        if (!beingDragged)
        {
            beingDragged = true;
        }
        Vector3 mousePosition = Input.mousePosition;
        newPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));                
    }

    private void OnMouseUp() {
        beingDragged = false;    
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Heart")
        {
            //  Get access to the dict of collected objects.
            GameScript gameScript = gameScriptObject.transform.gameObject.GetComponent<GameScript>();
            //  Get the name of the sprite so we know exactly what we collected.
            string spriteName = gameObject.GetComponent<SpriteRenderer>().sprite.name;

            //  Add the type of the collectible to the hearts storage dict here.
            if (!gameScript.collected.ContainsKey(spriteName))
            {
                gameScript.collected[spriteName] = 1;
            }
            else
            {
                gameScript.collected[spriteName] += 1;
            }            
            //  Destroy the collectible.
            Destroy(gameObject);      
        }                
    }
}
