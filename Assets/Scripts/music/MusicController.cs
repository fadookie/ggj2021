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
		public int popTimeSmp;
		public MusicEventType type;
		public MusicEvent(int popTimeSmp, MusicEventType type) {
			this.popTimeSmp = popTimeSmp;
			this.type = type;
		}
	}
	
	Subject<MusicEvent> musicEventStream = new Subject<MusicEvent>();
	public IObservable<MusicEvent> MusicEventStream => musicEventStream;

	public List<MusicEvent> events = new List<MusicEvent> {
		new MusicEvent(0, MusicEventType.Start),
		
		/*
		// Real song
		new MusicEvent(4498026, MusicEventType.Outtro),
		new MusicEvent(4695018, MusicEventType.Blastoff),
		*/
		
		// Click Track
		new MusicEvent(1058400, MusicEventType.Outtro),
		new MusicEvent(1234800, MusicEventType.Blastoff),
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
			Debug.LogWarning($"Pop event at {source.timeSamples}, popTimeSmp={evt.popTimeSmp}, type={evt.type}")
		)
		.AddTo(this);
	}

	// Update is called once per frame
	void Update () {
		foreach(MusicEvent mEvent in events) {
			if (source.timeSamples >= mEvent.popTimeSmp) {
				musicEventStream.OnNext(mEvent);
				popped.Add(mEvent);
			}
		}

		foreach(MusicEvent mEvent in popped) {
			events.Remove(mEvent);
		}
		popped.Clear();

		if (!endPopped && !source.isPlaying) {
			musicEventStream.OnNext(new MusicEvent(source.timeSamples, MusicEventType.End));
			endPopped = true;
		}
	}
}
