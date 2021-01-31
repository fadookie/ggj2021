using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Obstacle : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] PossibleSprites;
    // Start is called before the first frame update
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteIdx = new System.Random().Next(0, PossibleSprites.Length);
        var sprite = PossibleSprites[spriteIdx];
        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
