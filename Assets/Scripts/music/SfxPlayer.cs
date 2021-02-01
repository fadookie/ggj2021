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
    public AudioClip DialogueAdvance;
    public AudioClip DialogueLineUpdate;

    private AudioSource player;

    public enum Sound
    {
        Damage,
        Heal,
        Death,
        DialogueAdvance,
        DialogueLineUpdate,
    }

    void Awake() {
        if (Services.instance.Get<SfxPlayer>() != null) {
            gameObject.SetActive(false);
            Destroy(this);
            return;
        }
        
        Services.instance.Set(this);
        player = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
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
            case Sound.DialogueAdvance:
                PlayClip(DialogueAdvance);
                break;
            case Sound.DialogueLineUpdate:
                PlayClip(DialogueLineUpdate);
                break;
            default:
                throw new ArgumentException($"Unknown sound type: {sound}");
        }
    }

    private void PlayClip(AudioClip clip) {
        player.PlayOneShot(clip);
    }
}