using System.Collections;
using System.Collections.Generic;
using System;
using com.eliotlash.core.service;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(MusicController))]
public class TempoManager : MonoBehaviour
{
	AudioSource source;
    public float bpm;
    private uint _beatsSinceSync = 0;
    private BehaviorSubject<Unit> subject;
    public IObservable<Unit> beats;

    void Awake() {
        subject = new BehaviorSubject<Unit>(Unit.Default);
        beats = subject;
        Services.instance.Set(this);
    }

    // Start is called before the first frame update
    void Start() {
        source = GetComponent<MusicController>().source;
    }

    // Update is called once per frame
    void Update()
    {
        //Check beat timer and trigger beat if neccessary
        if (source.time > beatsPerMinuteToDelay(bpm) * _beatsSinceSync) {
            Debug.Log(string.Format("BEAT - bpm={0} bpmToDelay={1} beatsSinceSync={2} nextBeatTime={3} > time={4}", bpm, beatsPerMinuteToDelay(bpm), _beatsSinceSync, beatsPerMinuteToDelay(bpm) * _beatsSinceSync, Time.time)); 
            beat();
        }
    }
    
    public static float beatsPerMinuteToDelay(float beatsPerMinute) {
        //beats per second = beatsPerMinute / 60
        return 1.0f / (beatsPerMinute / 60.0f);
    }


    void beat() {
        _beatsSinceSync++;
//        ReactiveManager.Instance.beatEvent(tempoEventChannel, BPM);
        subject.OnNext(Unit.Default);
    }
}
