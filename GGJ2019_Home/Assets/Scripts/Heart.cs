using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public Dictionary<string, int> collected;

    // Start is called before the first frame update
    void Start()
    {
        collected.Add("Art", 0);
        collected.Add("Food", 0);
        collected.Add("Music", 0);
        collected.Add("Nature", 0);
        collected.Add("People", 0);
        collected.Add("Science", 0);
        collected.Add("Shelter", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
