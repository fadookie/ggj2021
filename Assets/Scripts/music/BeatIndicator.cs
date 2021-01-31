using System;
using System.Collections;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

namespace music
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class BeatIndicator : MonoBehaviour
    {
        public GameObject CenterBar;
        public Transform BeatPulseIndicator;
        
        public Vector3 centerBarInitialScale;
        private TempoManager.TempoInfo prevTempoInfo;
        public GameObject beatBarPrefab;
        private Coroutine pulseBeatRoutine;
        private BoxCollider2D bounds;
        private TempoManager tempoManager;
        private GameObject beatBarRight;
        private GameObject beatBarLeft;
        
        
        void Start() {
            tempoManager = Services.instance.Get<TempoManager>();
            tempoManager.TotalBeats.Subscribe(OnBeat).AddTo(this);
//            tempoManager.TotalMeasures.Subscribe(OnMeasure).AddTo(this);
            tempoManager.Tempo.Subscribe(OnTempo).AddTo(this);
            bounds = GetComponent<BoxCollider2D>();
            centerBarInitialScale = CenterBar.transform.localScale;
            beatBarRight = Instantiate(beatBarPrefab, transform);
            beatBarLeft = Instantiate(beatBarPrefab, transform);
        }

        private void Update() {
            var pctToNextBeat = tempoManager.percentElapsedToNextTargetBeat();
            {
                var startPosX = bounds.bounds.min.x;
                var newBarPos = beatBarRight.transform.position;
                newBarPos.x = Mathf.LerpUnclamped(startPosX, CenterBar.transform.position.x, pctToNextBeat);
                beatBarLeft.transform.position = newBarPos;
            }
            {
                var startPosX = bounds.bounds.max.x;
                var newBarPos = beatBarRight.transform.position;
                newBarPos.x = Mathf.LerpUnclamped(startPosX, CenterBar.transform.position.x, pctToNextBeat);
                beatBarRight.transform.position = newBarPos;
            }
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
//                Services.instance.Get<SpawnArea>().Spawn(info.TotalBeats + 2); // Try to spawn for 2 beats from now
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
            var startTransform = centerBarInitialScale * 1.25f;
            var duration = 0.25f;
            var startTime = Time.time;
            float elapsedTime;
//            Debug.LogWarning($"@@@ PULSEC START elapsedTime:{elapsedTime} pct:{elapsedTime/duration}");
            do {
                elapsedTime = Time.time - startTime;
//                Debug.LogWarning($"@@@ PULSEC RUN elapsedTime:{elapsedTime} pct:{elapsedTime / duration}");
                BeatPulseIndicator.localScale = Vector3.Lerp(startTransform, centerBarInitialScale, elapsedTime / duration);
                yield return null;
            } while (elapsedTime < duration);
        }
    }
}