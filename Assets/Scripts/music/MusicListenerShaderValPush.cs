using UnityEngine;
using System.Collections;

using com.eliotlash.core.service;

[RequireComponent(typeof(Renderer))]
public class MusicListenerShaderValPush : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public string paramName;
	public float value;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.musicEvent += onMusicEvent;
	}

	void onMusicEvent(MusicController.MusicEvent mEvent) {
		if (mEvent.type.Equals(listenedEvent)) {
			GetComponent<Renderer>().material.SetFloat(paramName, value);
		}
	}
}
