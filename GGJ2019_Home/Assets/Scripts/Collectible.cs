using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the dragging of the collectible objects.
/// </summary>
public class Collectible : MonoBehaviour
{
    //  Initialise class variables
    private Vector3 newPosition;
    public float timeToLive;
    public Vector3 scale;
    private SpriteRenderer spriteR;  

    public enum ItemType
    {
        Art,
        Food,
        Music,
        Nature,
        People,
        Science,
        Shelter
    }  

    ItemType type;

    // Start is called before the first frame update
    void Start()
    {
        newPosition = transform.position;
        transform.localScale = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < scale.x && transform.localScale.y < scale.y){
            transform.localScale = Vector3.Lerp(transform.localScale, scale, Time.deltaTime);
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);
    }

    void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        newPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));                
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "heart")
        {

        }
        Heart otherParent = other.transform.gameObject.GetComponent<Heart>();
        switch (type)
        {
            case ItemType.Art:
                otherParent.collected["Art"] += 1;
                break;
            case ItemType.Food:
                otherParent.collected["Food"] += 1;
                break;
            case ItemType.Music:
                otherParent.collected["Music"] += 1;
                break;
            case ItemType.Nature:
                otherParent.collected["Nature"] += 1;
                break;
            case ItemType.People:
                otherParent.collected["People"] += 1;
                break;
            case ItemType.Science:
                otherParent.collected["Science"] += 1;
                break;
            case ItemType.Shelter:
                otherParent.collected["Shelter"] += 1;
                break;
            default:

                break;
        }        

        Destroy(gameObject);
    }
}
