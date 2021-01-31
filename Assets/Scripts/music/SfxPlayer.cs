using System;
using System.Collections;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    public AudioClip DamageSound;
    public AudioClip HealSound;

    private AudioSource player;

    public enum Sound
    {
        Damage,
        Heal,
    }

    void Awake() {
        Services.instance.Set(this);
        player = GetComponent<AudioSource>();
    }

    void Start() {
    }

    public void PlaySound(Sound sound) {
//        Debug.LogWarning($"PlaySound: {sound}");
        switch (sound) {
            case Sound.Damage:
                PlayClip(DamageSound);
                break;
            case Sound.Heal:
                PlayClip(HealSound);
                break;
            default:
                throw new ArgumentException($"Unknown sound type: {sound}");
        }
    }

    private void PlayClip(AudioClip clip) {
        player.PlayOneShot(clip);
    }
}