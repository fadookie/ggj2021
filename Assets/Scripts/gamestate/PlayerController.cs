using System.Collections;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerController : MonoBehaviour
{
    private const int maxHealth = 100;
    public IntReactiveProperty health = new IntReactiveProperty(maxHealth);
    public Image healthBar;

    private void Awake() {
        Services.instance.Set(this);
    }

    public void Damage() {
        health.Value -= 10;
    }
    
    public void Heal() {
        health.Value += 10;
    }

    // Start is called before the first frame update
    void Start() {
        health.Subscribe(newHealth => {
            Debug.LogWarning($"health change to {health} pct:{newHealth / (float)maxHealth}");
            healthBar.fillAmount = newHealth / (float)maxHealth;
        }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
