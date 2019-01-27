using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    //  Collectible variables.
    public GameObject prefab;
    private Object[] sprites;
    private float spawnTimer = 0.0f;
    public float spawnDelay;
    private float amountToSpawn;
    public Dictionary<string, int> collected = new Dictionary<string, int>();

    //  Timer variables.
    public Text timerText;
    public float gameTime = 60;
    public bool gameStarted = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll("Items", typeof(Sprite));
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStarted)
        {
            //  Spawns a new instance whenever the time in spawnTimer is greater then spawnDelay.
            //  spawnTimer holds a time value in milliseconds.
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                amountToSpawn = ((gameTime % 6) + 1) * 2;

                for (int i = 0; i < amountToSpawn; i++)
                {
                    //  Instantiate new objects.
                    //  Save a copy of the instatiated object so we can modify it.
                    GameObject collectible = Instantiate(prefab, Vector2.zero, Quaternion.identity) as GameObject;
                    //  Get the sprite renderer.
                    SpriteRenderer spriteR = collectible.GetComponent<SpriteRenderer>();
                    //  Set the sprite.
                    spriteR.sprite = (Sprite)sprites[Random.Range(0, sprites.Length)];
                }                
                //  Reset the timer for spawning.
                spawnTimer = 0;
            }

            //  Timer stuff.
            gameTime -= Time.deltaTime;
            if(gameTime <= 0)
            {
                gameTime = 0;
                gameStarted = false;
            }
            timerText.text = Mathf.Floor(gameTime).ToString();
            if(gameStarted == false)
            {
                EndGame();
            }
        }
    }

    void StartGame()
    {
        gameStarted = false;
        gameTime = 60;
        timerText.text = "60";
    }

    void EndGame()
    {
        gameOver = true;
        // Show scoreboard stuff
        timerText.text = "";
        Vector2 spawnPoint = new Vector2(1, 1);
        foreach (string key in collected.Keys)
        {            
            int spriteIndex = IndexOf(key);
            for (int i = 0; i < collected[key]; i++)
            {
                //  Instantiate new objects.
                //  Save a copy of the instatiated object so we can modify it.
                GameObject collectible = Instantiate(prefab, Vector2.zero, Quaternion.identity) as GameObject;
                //  Get the sprite renderer.
                SpriteRenderer spriteR = collectible.GetComponent<SpriteRenderer>();
                //  Set the sprite.
                spriteR.sprite = (Sprite)sprites[spriteIndex];
                collectible.transform.position = new Vector2(spawnPoint.x * spriteR.bounds.size.x, spawnPoint.y * spriteR.bounds.size.y);
                
                spawnPoint.x++;  
                if (spawnPoint.x * spriteR.bounds.size.x > Screen.width)
                {
                    spawnPoint.x = 1;
                    spawnPoint.y++;
                } 
            }                     
        }
    }

    //  Finds the key of the key in the sprite array.
    int IndexOf(string key)
    {
        int index = -1;
        int i = 0;
        while (i < sprites.Length && index == -1)
        {
            if (sprites[i].name == key)
            {
                index = i;                
            }
            i++;
        }
        return index;
    }
}
