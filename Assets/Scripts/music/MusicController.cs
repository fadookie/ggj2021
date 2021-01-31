using System;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UnityEngine;
using UniRx;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour {
	public enum MusicEventType {
		Start,
		Outtro,
		Blastoff,
		End
	}

	public class MusicEvent {
		public float PopTime;
		public MusicEventType type;
		public MusicEvent(float popTime, MusicEventType type) {
			PopTime = popTime;
			this.type = type;
		}
	}
	
	Subject<MusicEvent> musicEventStream = new Subject<MusicEvent>();
	public IObservable<MusicEvent> MusicEventStream => musicEventStream;

	public List<MusicEvent> events = new List<MusicEvent> {
		new MusicEvent(0, MusicEventType.Start),
		
		// Real song
		new MusicEvent(98.182f, MusicEventType.Outtro),
		new MusicEvent(106.364f, MusicEventType.Blastoff),
		
		/*
		// Click Track
		new MusicEvent(1058400, MusicEventType.Outtro),
		new MusicEvent(1234800, MusicEventType.Blastoff),
		*/
	};

	List<MusicEvent> popped = new List<MusicEvent>(3);

	public AudioSource source;
	bool endPopped;


	private void OnEnable() {
		if (Services.instance.Get<MusicController>() == null) {
			Services.instance.Set(this);
		}
	}
	
	private void Start() {
		musicEventStream.Subscribe(evt =>
			Debug.LogWarning($"Pop event at s:{source.time} samples:{source.timeSamples}, popTimeSmp={evt.PopTime}, type={evt.type}")
		)
		.AddTo(this);
	}

	// Update is called once per frame
	void Update () {
		foreach(MusicEvent mEvent in events) {
			if (source.time >= mEvent.PopTime) {
				musicEventStream.OnNext(mEvent);
				popped.Add(mEvent);
			}
		}

		foreach(MusicEvent mEvent in popped) {
			events.Remove(mEvent);
		}
		popped.Clear();

		if (!endPopped && !source.isPlaying) {
			musicEventStream.OnNext(new MusicEvent(source.time, MusicEventType.End));
			endPopped = true;
		}
	}
}
