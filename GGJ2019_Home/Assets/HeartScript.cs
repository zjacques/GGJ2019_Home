 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class HeartScript : MonoBehaviour {
 
     public GameScript gameController;

     // Grow parameters
     public float approachSpeed = 0.02f;
     public float growthBound = 2f;
     public float shrinkBound = 0.5f;
     private float currentRatio = 0.1f;
     private float originalFontSize;

    //Fade parameters
    public float minimum = 0.0f;
    public float maximum = 1f;
    public float duration = 5.0f;
    private float startTime;
    public SpriteRenderer sprite;
    private bool fading;
    private Color targetColor;
 
     // And something to do the manipulating
     private Coroutine routine;
     private bool keepGoing = true;
 
     // Attach the coroutine
     void Awake () 
     {
        startTime = Time.time;
        fading = true;
        targetColor = new Color(1f, 1f, 1f, 1f);
         // Then start the routine
        this.routine = StartCoroutine(this.Pulse());
     }

    void Update() {
        if(fading)
        {
            float t = (Time.time - startTime) / duration;  
            if(t>=1)
            {
                fading = false;
            }
            sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(minimum, maximum, t));  
        }    
    }

    void OnMouseDown()
    {
        gameController.StartGame();
    }

    public void FadeOut(){
        startTime = Time.time;
        fading = true;
        targetColor = new Color(1f, 1f, 1f, 0f);
    }

    public void FadeIn(){
        startTime = Time.time;
        fading = true;
        targetColor = new Color(1f, 1f, 1f, 1f);
    }
 
     IEnumerator Pulse()
     {
         // Run this indefinitely
         while (keepGoing)
         {
             // Get bigger for a few seconds
             while (this.currentRatio != this.growthBound)
             {
                 // Determine the new ratio to use
                 currentRatio = Mathf.MoveTowards( currentRatio, growthBound, approachSpeed);
 
                 gameObject.transform.localScale = Vector3.one * currentRatio;
 
                 yield return new WaitForEndOfFrame();
             }
 
             // Shrink for a few seconds
             while (this.currentRatio != this.shrinkBound)
             {
                 // Determine the new ratio to use
                 currentRatio = Mathf.MoveTowards( currentRatio, shrinkBound, approachSpeed);
 
                 gameObject.transform.localScale = Vector3.one * currentRatio;
 
                 yield return new WaitForEndOfFrame();
             }
         }
     }
 }