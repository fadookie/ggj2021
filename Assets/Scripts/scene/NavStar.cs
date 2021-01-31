using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NavStar : MonoBehaviour
{
    public Sprite[] Star1 = new Sprite[4];
    public Sprite[] Star2 = new Sprite[4];
    public Sprite[] Star3 = new Sprite[4];
//    public Sprite[] Star4 = new Sprite[4]; // I accidentally forgot to put sprites here and don't have time to fix the numbering now
    public Sprite[] Star5 = new Sprite[4];
    public Sprite[] Star6 = new Sprite[4];
    public Sprite[] Star7 = new Sprite[4];
    public Sprite[] Star8 = new Sprite[4];
    public Sprite[] Star9 = new Sprite[4];
    public Sprite[] Star10 = new Sprite[4];
    public Sprite[] Star11 = new Sprite[4];
    public Sprite[] Star12 = new Sprite[4];
    public Sprite[] Star13 = new Sprite[4];
    public Sprite[] Star14 = new Sprite[4];
    public Sprite[] Star15 = new Sprite[4];
    public Sprite[] Star16 = new Sprite[4];
    public Sprite[] Star17 = new Sprite[4];


    private Sprite[][] stars;
    private Sprite[] activeStar;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        stars = new[] {
            Star1, Star2, Star3, /*Star4,*/ Star5, Star6, Star7, Star8, Star9, Star10, Star11, Star12, Star13, Star14, Star15,
            Star16, Star17
        };
        var starIdx = new System.Random().Next(0, stars.Length);
        activeStar = stars[starIdx];
        Debug.LogWarning($"starIdx:{starIdx} activeStar:{activeStar}");
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = activeStar[0]; // TODO opacity selection based on accuracy
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
