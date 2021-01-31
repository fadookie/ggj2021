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
        if (health.Value <= 0) {
            Debug.LogError("Player died!");
        }
    }
    
    public void Heal() {
        if (health.Value < maxHealth) {
            health.Value += 10;
        }
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
