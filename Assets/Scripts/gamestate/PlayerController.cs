using System;
using System.Collections;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerController : MonoBehaviour
{
    private const int maxHealth = 100;
    Subject<Unit> onHeal = new Subject<Unit>();
    public IObservable<Unit> OnHeal => onHeal;
    public IntReactiveProperty health = new IntReactiveProperty(maxHealth);
    public Image healthBar;
    public GameObject failPanel;

    private void Awake() {
        Services.instance.Set(this);
    }
    
    // Start is called before the first frame update
    void Start() {
        health.Value = Mathf.Min(health.Value, maxHealth);
        Debug.LogWarning($"Set healh to {health.Value} max:{maxHealth}");
        
        health.Subscribe(newHealth => {
            Debug.LogWarning($"health change to {health} pct:{newHealth / (float)maxHealth}");
            healthBar.fillAmount = newHealth / (float)maxHealth;
        }).AddTo(this);
        Services.instance.Get<InputController>().MouseClicks.Subscribe(OnClick).AddTo(this);
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
            onHeal.OnNext(Unit.Default);
        }
    }

    public void OnShipDeathFinished() {
        failPanel.SetActive(true);
    }

    public void OnClick(Vector3 mousePos) {
        var accuracy = Services.instance.Get<TempoManager>().getAccuracy();
        if (accuracy == TempoManager.Accuracy.Great) {
            Heal();
        }
    }
}
