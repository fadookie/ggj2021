using System;
using System.Collections;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NavStar : MonoBehaviour
{
    const int numOpacityLevels = 4;
    public Sprite[] Star1 = new Sprite[numOpacityLevels];
    public Sprite[] Star2 = new Sprite[numOpacityLevels];
    public Sprite[] Star3 = new Sprite[numOpacityLevels];
//    public Sprite[] Star4 = new Sprite[numOpacityLevels]; // I accidentally forgot to put sprites here and don't have time to fix the numbering now
    public Sprite[] Star5 = new Sprite[numOpacityLevels];
    public Sprite[] Star6 = new Sprite[numOpacityLevels];
    public Sprite[] Star7 = new Sprite[numOpacityLevels];
    public Sprite[] Star8 = new Sprite[numOpacityLevels];
    public Sprite[] Star9 = new Sprite[numOpacityLevels];
    public Sprite[] Star10 = new Sprite[numOpacityLevels];
    public Sprite[] Star11 = new Sprite[numOpacityLevels];
    public Sprite[] Star12 = new Sprite[numOpacityLevels];
    public Sprite[] Star13 = new Sprite[numOpacityLevels];
    public Sprite[] Star14 = new Sprite[numOpacityLevels];
    public Sprite[] Star15 = new Sprite[numOpacityLevels];
    public Sprite[] Star16 = new Sprite[numOpacityLevels];
    public Sprite[] Star17 = new Sprite[numOpacityLevels];


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
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        var tempoManager = Services.instance.Get<TempoManager>();
        var accuracy = tempoManager.getAccuracyTime();
        var failAccuracy = tempoManager.mediocreAccuracy + 0.25f;
        var accuracyRamp = Mathf.RoundToInt(Mathf.Lerp(0, numOpacityLevels, accuracy / failAccuracy));
        Debug.LogWarning($"starIdx:{starIdx} accuracyRamp:{accuracyRamp}");
        
        spriteRenderer.sprite = activeStar[accuracyRamp];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
