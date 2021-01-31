using UnityEngine;
using System.Collections;

using com.eliotlash.core.service;
using UniRx;

public class MusicListenerQuit : MonoBehaviour {
	public MusicController.MusicEventType listenedEvent;
	public float quitDelay = 2;

	// Use this for initialization
	void Start () {
		MusicController mc = Services.instance.Get<MusicController>();	
		mc.MusicEventStream.Subscribe(onMusicEvent).AddTo(this);
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
