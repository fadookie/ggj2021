﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using com.eliotlash.core.service;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(MusicController))]
public class TempoManager : MonoBehaviour
{
    public struct TempoInfo
    {
        public int TotalBeats;
        public int TotalMeasures;
        public int RelativeBeat;

        public override string ToString() {
            return $"{nameof(TotalBeats)}: {TotalBeats}, {nameof(TotalMeasures)}: {TotalMeasures}, {nameof(RelativeBeat)}: {RelativeBeat}";
        }
    }
    
	AudioSource source;
    public float bpm;
    public int beatsPerMeasure = 4;
    private BehaviorSubject<int> totalBeats;
    private BehaviorSubject<TempoInfo> tempo;
    private int totalMeasures = -1; // Lord forgive me for this off-by-one error
    private int relativeBeat = 1; // Lord forgive me
    public IObservable<int> TotalBeats => totalBeats;
    public IObservable<TempoInfo> Tempo => tempo;

    void Awake() {
        totalBeats = new BehaviorSubject<int>(0);
        tempo = new BehaviorSubject<TempoInfo>(new TempoInfo { TotalBeats = totalBeats.Value, TotalMeasures = totalMeasures, RelativeBeat = 0} );
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
        if (source.time > beatsPerMinuteToDelay(bpm) * totalBeats.Value) {
            Debug.Log(string.Format("BEAT - bpm={0} bpmToDelay={1} beatsSinceSync={2} nextBeatTime={3} > time={4}", bpm, beatsPerMinuteToDelay(bpm), totalBeats.Value, beatsPerMinuteToDelay(bpm) * totalBeats.Value, Time.time)); 
            beat();
        }
    }

    private float _lastLogTime;
    public float percentElapsedToNextBeat() {
        var beatDelay = beatsPerMinuteToDelay(bpm);
        var numerator = (beatDelay * (totalBeats.Value + 1) - source.time) - beatDelay;
        var res =  1 - (numerator / beatDelay);
        if (source.time - _lastLogTime > 0.5f) {
//            Debug.Log($"percentElapsedToNextBeat delay:{beatDelay} time:{source.time} nextBeat:{beatDelay * (totalBeats.Value + 1)} numerator:{numerator} pct:{res}");
            _lastLogTime = source.time;
        }
        return res;
    }
    
    public float percentElapsedToNextMeasure() {
        var measureDelay = measuresPerMinuteToDelay(bpm, beatsPerMeasure);
        var lastMeasureTime = measureDelay * (totalMeasures - 0);
        var nextMeasureTime = measureDelay * (totalMeasures - 0 + 1);
//        var numerator = (measureDelay * (_totalMeasures + 1) - source.time) - measureDelay;
        var res = source.time - lastMeasureTime / nextMeasureTime - lastMeasureTime;
        if (source.time - _lastLogTime > 0.5f) {
//                Debug.Log($"percentElapsedToNextMeasure totalMeasures:{totalMeasures.Value} delay:{measureDelay} time:{source.time} lastMeasure:{lastMeasureTime} nextMeasure:{nextMeasureTime} equation:{source.time - lastMeasureTime} / {nextMeasureTime - lastMeasureTime} = {res}");
            _lastLogTime = source.time;
        }
        return res;
    }
    
    public static float beatsPerMinuteToDelay(float beatsPerMinute) {
        //beats per second = beatsPerMinute / 60
        return 1.0f / (beatsPerMinute / 60.0f);
    }
    
    public static float measuresPerMinuteToDelay(float beatsPerMinute, int beatsPerMeasure) {
        var beatDelay = beatsPerMinuteToDelay(beatsPerMinute);
        return beatDelay * beatsPerMeasure;
    }


    void beat() {
        totalBeats.OnNext(totalBeats.Value + 1);
        ++relativeBeat;
        if ((totalBeats.Value - 1) % beatsPerMeasure == 0) {
            ++totalMeasures;
            relativeBeat = 1;
        }
        var beats = totalBeats.Value;
//        Debug.Log($"Tempo (totalBeats:{beats}) % {beatsPerMeasure} = {(totalBeats.Value) % beatsPerMeasure}");
        var totalBeatsOfCurrentMeasure = totalMeasures * beatsPerMeasure;
//        var relativeBeat = beatsPerMeasure - (totalBeatsOfCurrentMeasure - beats);
        Debug.Log($"Tempo measures:{totalMeasures}, eq: beatsPerMeasure:{beatsPerMeasure} - (totalBeatsOfCurrentMeasure:{totalBeatsOfCurrentMeasure} - beats:{beats}) = {relativeBeat}");
    //                Debug.Log($"Tempo beats:{beats} measures:{measures} totalBeatsOfCurrentMeasure:{totalBeatsOfCurrentMeasure} relativeBeat:{relativeBeat}");
        var newTempo = new TempoInfo { TotalBeats = beats, TotalMeasures = totalMeasures, RelativeBeat = relativeBeat };
        tempo.OnNext(newTempo);
    }
}
