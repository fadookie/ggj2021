using System;
using System.Collections;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

namespace music
{
    public class BeatListener : MonoBehaviour
    {
        private TempoManager.TempoInfo prevTempoInfo;
        public Transform beatIndicator;
        private Coroutine pulseBeatRoutine;
        
        void Start() {
            var tempoManager = Services.instance.Get<TempoManager>();
            tempoManager.TotalBeats.Subscribe(OnBeat).AddTo(this);
//            tempoManager.TotalMeasures.Subscribe(OnMeasure).AddTo(this);
            tempoManager.Tempo.Subscribe(OnTempo).AddTo(this);
        }

        void OnBeat(int totalBeats) {
//            Debug.Log($"BeatListener OnBeat total:{totalBeats}");
//			Camera.main.backgroundColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
        
        void OnMeasure(int totalMeasures) {
//            Debug.LogWarning($"======== BeatListener OnMeasure total:{totalMeasures}");
//			Camera.main.backgroundColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }

        void OnTempo(TempoManager.TempoInfo info) {
            if (info.TotalMeasures > prevTempoInfo.TotalMeasures) {
                OnMeasure(info.TotalMeasures);
            }
            if (info.RelativeBeat % 2 != 0) {
                // Beats 1 & 3
                PulseBeatIndicator();
            }
            Debug.Log($"BeatListener OnTempo info:{info}");
            prevTempoInfo = info;
        }

        void PulseBeatIndicator() {
            if (pulseBeatRoutine != null) {
                StopCoroutine(pulseBeatRoutine);
            }
            pulseBeatRoutine = StartCoroutine(PulseBeatIndicatorRoutine());
        }

        IEnumerator PulseBeatIndicatorRoutine() {
            var startTransform = new Vector3(2, 2, 2);
            var duration = 0.25f;
            var startTime = Time.time;
            float elapsedTime = 0;
//            Debug.LogWarning($"@@@ PULSEC START elapsedTime:{elapsedTime} pct:{elapsedTime/duration}");
            do {
                elapsedTime = Time.time - startTime;
//                Debug.LogWarning($"@@@ PULSEC RUN elapsedTime:{elapsedTime} pct:{elapsedTime / duration}");
                beatIndicator.localScale = Vector3.Lerp(startTransform, Vector3.one, elapsedTime / duration);
                yield return null;
            } while (elapsedTime < duration);
        }
    }
}