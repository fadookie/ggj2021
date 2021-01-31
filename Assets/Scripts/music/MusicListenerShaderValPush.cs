using UnityEngine;
using System.Collections;
using UniRx;

using com.eliotlash.core.service;

[RequireComponent(typeof(Renderer))]
public class MusicListenerShaderValPush : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public string paramName;
	public float value;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.MusicEventStream.Subscribe(onMusicEvent).AddTo(this);
	}

	void onMusicEvent(MusicController.MusicEvent mEvent) {
		if (mEvent.type.Equals(listenedEvent)) {
			GetComponent<Renderer>().material.SetFloat(paramName, value);
		}
	}
}
