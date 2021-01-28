using UnityEngine;
using System.Collections;

using com.eliotlash.core.service;

[RequireComponent(typeof(Renderer))]
public class MusicListenerVisibility : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public bool setVisibleTo;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.musicEvent += onMusicEvent;
	}

	void onMusicEvent(MusicController.MusicEvent mEvent) {
		if (mEvent.type.Equals(listenedEvent)) {
			GetComponent<Renderer>().enabled = setVisibleTo;
		}
	}
}
