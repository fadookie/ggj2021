using UnityEngine;
using System.Collections;
using System.Linq.Expressions;
using UniRx;
using com.eliotlash.core.service;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnArea : MonoBehaviour {
	public GameObject spawnedPrefab;
	public Transform spawnParent;
	
	BoxCollider2D spawnArea;
	Vector2 maxSpawnPos;
	private bool keepSpawning = true;
	

	void Awake() {
		Services.instance.Set(this);
	}

	// Use this for initialization
	void Start () {
		spawnArea = GetComponent<BoxCollider2D>();
		maxSpawnPos = new Vector2(spawnArea.size.x / 2, spawnArea.size.y / 2);
		
		Services.instance.Get<TempoManager>().Tempo.Subscribe(OnTempo).AddTo(this);

		Services.instance.Get<MusicController>()
			.MusicEventStream
			.Where(evt => evt.type == MusicController.MusicEventType.Outtro)
			.Subscribe(_ => keepSpawning = false)
			.AddTo(this);
	}
	
	void OnTempo(TempoManager.TempoInfo info) {
		if (info.RelativeBeat % 2 == 0) {
			// Beats 2 & 4
			if (keepSpawning) Spawn();
		}
	}

	private void Spawn() {
		var spawned = Instantiate(spawnedPrefab, Vector3.zero, Quaternion.identity);
		spawned.transform.parent = spawnParent;
		var pos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y), 0);
		spawned.transform.position = pos;
	}
}
