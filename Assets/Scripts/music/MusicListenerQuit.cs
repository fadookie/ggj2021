using UnityEngine;
using System.Collections;

using com.eliotlash.core.service;

public class MusicListenerQuit : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public float quitDelay = 2;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.musicEvent += onMusicEvent;
	}

	void onMusicEvent(MusicController.MusicEvent mEvent) {
		if (mEvent.type.Equals(listenedEvent)) {
		}
	}

	IEnumerator quitAfterDelay() {
		yield return new WaitForSeconds(quitDelay);
		Application.Quit();
	}
}
