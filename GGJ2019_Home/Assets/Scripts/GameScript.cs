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
    public Dictionary<string, int> collected = new Dictionary<string, int>();

    //  Timer variables.
    public Text timerText;
    public float gameTime = 60;
    private bool gameStarted = false;

    public HeartScript heart;
    #region audio
    public AudioSource introMusic;
    public AudioClip intro;
    public AudioClip end;
    private float audioStartTime;
    private bool audioFading = false;
    private float audioDuration = 1.0f;
    private float minVol = 0f;
    private float maxVol = 1f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll("Items", typeof(Sprite));
    }

    // Update is called once per frame
    void Update()
    {
        if(audioFading)
        {
            float t = (Time.time - audioStartTime) / audioDuration;  
            if(t>=1)
            {
                audioFading = false;
            }
            introMusic.volume = Mathf.SmoothStep(minVol, maxVol, t); 
        } 

        if(gameStarted)
        {
            //  Spawns a new instance whenever the time in spawnTimer is greater then spawnDelay.
            //  spawnTimer holds a time value in milliseconds.
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                //  Instantiate new objects.
                float spawnPointX = Random.Range(Camera.main.ScreenToWorldPoint(Vector3.zero).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
                float spawnPointY = Random.Range(Camera.main.ScreenToWorldPoint(Vector3.zero).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
                //  Save a copy of the instatiated object so we can modify it.
                GameObject collectible = Instantiate(prefab, new Vector2(spawnPointX, spawnPointY), Quaternion.identity) as GameObject;
                //  Get the sprite renderer.
                SpriteRenderer spriteR = collectible.GetComponent<SpriteRenderer>();
                //  Set the sprite.
                spriteR.sprite = (Sprite)sprites[Random.Range(0, sprites.Length)];
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

    public void StartGame(){
        if(gameStarted == false)
        {
            AudioFadeOut();
            gameTime = 60;
            timerText.text = "60";
            gameStarted = true;
        }
    }

    void EndGame(){
        //show scoreboard stuff
        timerText.text = "";
        heart.FadeOut();
        heart.AudioFadeOut();
        introMusic.clip = end;
        AudioFadeIn();
    }
#region audioFunctions
    public void AudioFadeOut(){
        audioStartTime = Time.time;
        audioFading = true;
        minVol = 1f;
        maxVol = 0f;
    }

    public void AudioFadeIn(){
        introMusic.Play();
        audioStartTime = Time.time;
        audioFading = true;
        minVol = 0f;
        maxVol = 1f;
    }
#endregion
}
