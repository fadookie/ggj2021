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
    private Subject<Unit> dialogueLineUpdate = new Subject<Unit>();

    public enum Sound
    {
        Damage,
        Heal,
        Death,
    }

    void Awake() {
        Services.instance.Set(this);
        player = GetComponent<AudioSource>();
        dialogueLineUpdate
            .ThrottleFirst(new TimeSpan(TimeSpan.TicksPerSecond / 15))
            .Subscribe(_ => PlayClip(DialogueLineUpdate))
            .AddTo(this);
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

    public void OnDialogueLineUpdate() {
        dialogueLineUpdate.OnNext(Unit.Default);
    }

    public void OnDialogueAdvance() {
        PlayClip(DialogueAdvance);
    }
}