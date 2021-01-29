using System;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

namespace music
{
    public class BeatListener : MonoBehaviour
    {
        void Start() {
            Services.instance.Get<TempoManager>().beats.Subscribe(OnBeat).AddTo(this);
        }

        void OnBeat(Unit unit) {
            Debug.Log("BeatListener OnBeat");
			Camera.main.backgroundColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        }
    }
}