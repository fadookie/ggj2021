using System.Collections;
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
        public uint TotalBeats;
        public uint TotalMeasures;
        public uint RelativeBeat;

        public override string ToString() {
            return $"{nameof(TotalBeats)}: {TotalBeats}, {nameof(TotalMeasures)}: {TotalMeasures}, {nameof(RelativeBeat)}: {RelativeBeat}";
        }
    }
    
	AudioSource source;
    public float bpm;
    public uint beatsPerMeasure = 4;
    private BehaviorSubject<uint> totalBeats;
    private BehaviorSubject<uint> totalMeasures;
    public IObservable<uint> TotalBeats => totalBeats;
    public IObservable<uint> TotalMeasures => totalMeasures;
    public IObservable<TempoInfo> Tempo;

    void Awake() {
        totalBeats = new BehaviorSubject<uint>(0);
        totalMeasures = new BehaviorSubject<uint>(0);
        var measureStream = totalBeats.Select(beats => {
            if ((beats - 1) % beatsPerMeasure == 0) {
                return totalMeasures.Value + 1;
            }
            return totalMeasures.Value;
        });
        measureStream.Subscribe(totalMeasures).AddTo(this);
        Tempo = totalBeats.CombineLatest(totalMeasures, Tuple.Create)
            .Select(tuple => {
                var beats = tuple.Item1;
                var measures = tuple.Item2;
                var totalBeatsOfCurrentMeasure = measures * beatsPerMeasure;
                var relativeBeat = checked(beatsPerMeasure - (totalBeatsOfCurrentMeasure - beats));
//                Debug.Log($"Tempo beats:{beats} measures:{measures} totalBeatsOfCurrentMeasure:{totalBeatsOfCurrentMeasure} relativeBeat:{relativeBeat}");
                return new TempoInfo { TotalBeats = beats, TotalMeasures = measures, RelativeBeat = relativeBeat };
            });
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

    public float percentElapsedToNextBeat() {
        var beatDelay = beatsPerMinuteToDelay(bpm);
        var numerator = (beatDelay * (totalBeats.Value + 1) - source.time) - beatDelay;
        var res =  1 - (numerator / beatDelay);
        Debug.Log($"percentElapsedToNextBeat delay:{beatDelay} time:{source.time} nextBeat:{beatDelay * (totalBeats.Value + 1)} numerator:{numerator} pct:{res}");
        return res;
    }
    
    public float percentElapsedToNextMeasure() {
        var measureDelay = measuresPerMinuteToDelay(bpm, beatsPerMeasure);
        var lastMeasureTime = measureDelay * totalMeasures.Value;
        var nextMeasureTime = measureDelay * (totalMeasures.Value + 1);
//        var numerator = (measureDelay * (_totalMeasures + 1) - source.time) - measureDelay;
        var res = source.time - lastMeasureTime / nextMeasureTime - lastMeasureTime;
//        Debug.Log($"percentElapsedToNextMeasure delay:{measureDelay} time:{source.time} lastMeasure:{lastMeasureTime} nextMeasure:{nextMeasureTime} equation:{source.time - lastMeasureTime} / {nextMeasureTime - lastMeasureTime} = {res}");
        return res;
    }
    
    public static float beatsPerMinuteToDelay(float beatsPerMinute) {
        //beats per second = beatsPerMinute / 60
        return 1.0f / (beatsPerMinute / 60.0f);
    }
    
    public static float measuresPerMinuteToDelay(float beatsPerMinute, uint beatsPerMeasure) {
        var beatDelay = beatsPerMinuteToDelay(beatsPerMinute);
        return beatDelay * beatsPerMeasure;
    }


    void beat() {
        totalBeats.OnNext(totalBeats.Value + 1);
    }
}
