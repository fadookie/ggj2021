using UnityEngine;
using System.Collections;

using com.eliotlash.core.service;
using UniRx;

[RequireComponent(typeof(Renderer))]
public class MusicListenerVisibility : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public bool setVisibleTo;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.MusicEventStream.Subscribe(onMusicEvent).AddTo(this);
	}

	void onMusicEvent(MusicController.MusicEvent mEvent) {
		if (mEvent.type.Equals(listenedEvent)) {
			GetComponent<Renderer>().enabled = setVisibleTo;
		}
	}
}
