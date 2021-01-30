using UnityEngine;
using System.Collections;
using com.eliotlash.core.service;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnArea : MonoBehaviour {
	public GameObject spawnedPrefab;
	BoxCollider2D spawnArea;
	Vector2 maxSpawnPos;

	float lastSpawnTimeS = -1;
	public float spawnDelayS = 5;

	void Awake() {
		Services.instance.Set(this);
	}

	// Use this for initialization
	void Start () {
		spawnArea = GetComponent<BoxCollider2D>();
		spawnArea.enabled = false; //We don't need this to test for any collisions, just to show visual bounds info in the editor.
		maxSpawnPos = new Vector2(spawnArea.size.x / 2, spawnArea.size.y / 2);
	}

	public void Spawn(int targetBeat) {
		var spawned = Instantiate(spawnedPrefab, Vector3.zero, Quaternion.identity);
		spawned.transform.parent = transform;
		var pos = new Vector3(Random.Range(-maxSpawnPos.x, maxSpawnPos.x), Random.Range(-maxSpawnPos.y, maxSpawnPos.y), 0);
		spawned.transform.localPosition = pos;

		spawned.GetComponent<TapTarget>().targetBeat = targetBeat;
	}
}
