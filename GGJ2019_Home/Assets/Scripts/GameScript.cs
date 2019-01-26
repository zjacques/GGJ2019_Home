using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    public Object[] sprites;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll("Items", typeof(Sprite));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
