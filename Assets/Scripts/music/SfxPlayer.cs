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
    public AudioClip DeathSound;

    private AudioSource player;

    public enum Sound
    {
        Damage,
        Heal,
        Death,
    }

    void Awake() {
        Services.instance.Set(this);
        player = GetComponent<AudioSource>();
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
            case Sound.Death:
                PlayClip(DeathSound);
                break;
            default:
                throw new ArgumentException($"Unknown sound type: {sound}");
        }
    }

    private void PlayClip(AudioClip clip) {
        player.PlayOneShot(clip);
    }
}