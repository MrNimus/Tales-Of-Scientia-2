using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profundidad : MonoBehaviour
{
    public bool FixFrame;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingLayerName = "Player";
        sprite.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (FixFrame)
        {
            sprite.sortingOrder = Mathf.RoundToInt(-transform.position.y * 100);
        }
    }
}
