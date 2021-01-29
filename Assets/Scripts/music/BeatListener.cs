using System;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

namespace music
{
    public class BeatListener : MonoBehaviour
    {
        void Start() {
            var tempoManager = Services.instance.Get<TempoManager>();
            tempoManager.TotalBeats.Subscribe(OnBeat).AddTo(this);
            tempoManager.TotalMeasures.Subscribe(OnMeasure).AddTo(this);
            tempoManager.Tempo.Subscribe(OnTempo).AddTo(this);
        }

        void OnBeat(uint totalBeats) {
//            Debug.Log($"BeatListener OnBeat total:{totalBeats}");
//			Camera.main.backgroundColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
        
        void OnMeasure(uint totalMeasures) {
//            Debug.Log($"BeatListener OnMeasure total:{totalMeasures}");
//			Camera.main.backgroundColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }

        void OnTempo(TempoManager.TempoInfo info) {
            Debug.Log($"BeatListener OnTempo info:{info}");
        }
    }
}