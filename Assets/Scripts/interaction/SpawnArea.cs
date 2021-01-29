using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class SpawnArea : MonoBehaviour {
	public GameObject spawnedPrefab;
	BoxCollider2D spawnArea;
	Vector2 maxSpawnPos;

	float lastSpawnTimeS = -1;
	public float spawnDelayS = 5;

	// Use this for initialization
	void Start () {
		spawnArea = GetComponent<BoxCollider2D>();
		spawnArea.enabled = false; //We don't need this to test for any collisions, just to show visual bounds info in the editor.
		maxSpawnPos = new Vector2(spawnArea.size.x / 2, spawnArea.size.y / 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (lastSpawnTimeS < 0) {
			lastSpawnTimeS = Time.time;
			print ("spawn timer fire");
			GameObject spawned = Instantiate(spawnedPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			spawned.transform.parent = transform;
			Vector3 pos = new Vector3(Random.Range(-maxSpawnPos.x, maxSpawnPos.x), Random.Range(-maxSpawnPos.y, maxSpawnPos.y), 0);
			spawned.transform.localPosition = pos;
		} else if (lastSpawnTimeS >= 0 && Time.time - lastSpawnTimeS > spawnDelayS) {
			lastSpawnTimeS = -1;
		}
	}
}
