using TouchScript;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using com.eliotlash.core.service;

public class InputController : MonoBehaviour
{
//	private Dictionary<int, ITouch> dummies = new Dictionary<int, ITouch>(10);
//	private Dictionary<int, GameObject> objects = new Dictionary<int, GameObject>(10);
//	public GameObject prefab;
//	public Light touchLight;
//	public GameObject lightGoAwayPos;
//	public float lightGoAwayDuration = 3;
//
//	public bool particlesOn = true;
//	public bool lightOn = true;
//
//	private void OnEnable()
//	{
//		if (TouchManager.Instance != null)
//		{
//			TouchManager.Instance.TouchesBegan += touchesBeganHandler;
//			TouchManager.Instance.TouchesEnded += touchesEndedHandler;
//			TouchManager.Instance.TouchesMoved += touchesMovedHandler;
//			TouchManager.Instance.TouchesCancelled += touchesCancelledHandler;
//
//			MusicController mc = Services.instance.Get<MusicController>();	
//			mc.musicEvent += onMusicEvent;
//		}
//	}
//
//	private void OnDisable()
//	{
//		if (TouchManager.Instance != null)
//		{
//			TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
//			TouchManager.Instance.TouchesEnded -= touchesEndedHandler;
//			TouchManager.Instance.TouchesMoved -= touchesMovedHandler;
//			TouchManager.Instance.TouchesCancelled -= touchesCancelledHandler;
//
//			MusicController mc = Services.instance.Get<MusicController>();	
//			mc.musicEvent -= onMusicEvent;
//		}
//	}
//	
//	private GameObject spawnPrefabAt(Vector2 position)
//	{
//		var obj = Instantiate(prefab) as GameObject;
//		obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
//		return obj;
//	}
//	
//	private void updateDummy(ITouch dummy)
//	{
//		dummies[dummy.Id] = dummy;
//		objects[dummy.Id].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(dummy.Position.x, dummy.Position.y, 10));
//	}
//
//	private void updateLight(IList<ITouch> touches)
//	{
//		if (lightOn && touches.Count > 0) {
//			ITouch dummy = touches[0];
//			Vector3 newLightPos = Camera.main.ScreenToWorldPoint(new Vector3(dummy.Position.x, dummy.Position.y, 0));
//			//ScreenToWorldPoint does weird things with the z coord and we want this to stay to what it was set in the editor: 
//			newLightPos.z = touchLight.transform.position.z;
//			touchLight.transform.position = newLightPos;
//		}
//	}
//
//	#region Event handlers
//	
//	private void touchesBeganHandler(object sender, TouchEventArgs e)
//	{
//		updateLight(e.Touches);
//
//		if (particlesOn) {
//			foreach (var touch in e.Touches)
//			{
//				dummies.Add(touch.Id, touch);
//				objects.Add(touch.Id, spawnPrefabAt(touch.Position));
//			}
//		}
//	}
//	
//	private void touchesMovedHandler(object sender, TouchEventArgs e)
//	{
//		updateLight(e.Touches);
//
//		if (particlesOn) {
//			foreach (var touch in e.Touches)
//			{
//				ITouch dummy;
//				if (!dummies.TryGetValue(touch.Id, out dummy)) return;
//				updateDummy(touch);
//			}
//		}
//	}
//	
//	private void touchesEndedHandler(object sender, TouchEventArgs e)
//	{
//		updateLight(e.Touches);
//
//		if (particlesOn) {
//			foreach (var touch in e.Touches)
//			{
//				ITouch dummy;
//				if (!dummies.TryGetValue(touch.Id, out dummy)) return;
//				dummies.Remove(touch.Id);
//
//				objects[touch.Id].particleEmitter.emit = false;
//				objects.Remove(touch.Id);
//			}
//		}
//	}
//	
//	private void touchesCancelledHandler(object sender, TouchEventArgs e)
//	{
//		touchesEndedHandler(sender, e);
//	}
//
//	void onMusicEvent(MusicController.MusicEvent mEvent) {
//		switch(mEvent.type) {
//		case MusicController.MusicEventType.Synth2:
//			particlesOn = false;
//			List<int> removeKeys = new List<int>(objects.Count);
//			foreach(var kvp in objects) {
//				kvp.Value.particleEmitter.emit = false;
//				removeKeys.Add(kvp.Key);
//			}
//			foreach(int key in removeKeys) {
//				objects.Remove(key);
//			}
//			break;
//		case MusicController.MusicEventType.Outtro:
//			lightOn = false;
//			StartCoroutine(makeLightGoAway());
//			break;
//		}
//	}
//
//	IEnumerator makeLightGoAway() {
//		float elapsed = 0;
//		Vector3 lightPosStart = touchLight.transform.position;
//		while(elapsed < lightGoAwayDuration) {
//			elapsed += Time.deltaTime;
//			touchLight.transform.position = Vector3.Lerp(lightPosStart, lightGoAwayPos.transform.position, elapsed / lightGoAwayDuration);
//			yield return new WaitForEndOfFrame();
//		}
//	}
//	
//	#endregion
}
