using UnityEngine;
using System.Collections.Generic;

using com.eliotlash.core.service;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour {
	#region Event declarations
	public delegate void MusicEventHandler(MusicEvent mEvent);
	public event MusicEventHandler musicEvent;
	#endregion

	public enum MusicEventType {
		Start,
		Synth1,
		Synth2,
		Break1,
		Break1End,
		Outtro,
		End,

		/* For Son:
		LyricsStart,
		FirstVerseStart,
		BreakStart,
*/
	}

	public class MusicEvent {
		public int popTimeSmp;
		public MusicEventType type;
		public MusicEvent(int popTimeSmp, MusicEventType type) {
			this.popTimeSmp = popTimeSmp;
			this.type = type;
		}
	}

	public List<MusicEvent> events = new List<MusicEvent> {
		//For Raney2
		new MusicEvent(0, MusicEventType.Start),
		new MusicEvent(690327, MusicEventType.Synth1),
		new MusicEvent(1382652, MusicEventType.Synth2),
		new MusicEvent(2768088, MusicEventType.Break1),
		new MusicEvent(4142762, MusicEventType.Break1End),
		new MusicEvent(6222134, MusicEventType.Outtro),
//		new MusicEvent(6571292, MusicEventType.End), Doesn't seem to fire, hardcode this
		/* For Son:
		new MusicEvent(35955, MusicEventType.Start),
		new MusicEvent(505827, MusicEventType.LyricsStart),
		new MusicEvent(1554084, MusicEventType.FirstVerseStart),
		new MusicEvent(1984066, MusicEventType.BreakStart),
*/
	};

	List<MusicEvent> popped = new List<MusicEvent>(3);

	AudioSource source;
	bool endPopped = false;


	private void OnEnable() {
		if (Services.instance.Get<MusicController>() == null) {
			Services.instance.Set<MusicController>(this);
		}
	}

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		foreach(MusicEvent mEvent in events) {
			if (source.timeSamples >= mEvent.popTimeSmp) {
				Debug.Log(string.Format("Pop event at {0}, popTimeSmp={1}, type={2}", source.timeSamples, mEvent.popTimeSmp, mEvent.type.ToString()));
				if (musicEvent != null) {
					musicEvent(mEvent);
				}
				popped.Add(mEvent);
			}
		}

		foreach(MusicEvent mEvent in popped) {
			events.Remove(mEvent);
		}
		popped.Clear();

		if (!endPopped && !source.isPlaying) {
			musicEvent(new MusicEvent(source.timeSamples, MusicEventType.End));
			endPopped = true;
		}
	}
}
