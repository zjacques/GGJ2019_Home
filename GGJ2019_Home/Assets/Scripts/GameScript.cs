using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public GameObject prefab;
    public Object[] sprites;
    public Text timerText;
    public float gameTime = 60;
    public bool gameStarted = true;

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

    void StartGame(){
        gameStarted = false;
        gameTime = 60;
        timerText.text = "60";
    }

    void EndGame(){
        //show scoreboard stuff
        timerText.text = "";
    }
}
