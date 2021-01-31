using UnityEngine;
using System.Collections;

using com.eliotlash.core.service;
using UniRx;

public class TestMusicListener : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public Color newScreenColor;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.MusicEventStream.Subscribe(onMusicEvent).AddTo(this);
	}

	void onMusicEvent(MusicController.MusicEvent mEvent) {
		if (mEvent.type.Equals(listenedEvent)) {
			Camera.main.backgroundColor = newScreenColor;
		}
	}
}
