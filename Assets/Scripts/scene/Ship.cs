using com.eliotlash.core.service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private NavPointManager _navPointManager;
    private int nextNavPointIdx = -1;
    private GameObject nextNavPoint;
    private Vector3 navStartPos;
    private float navStartTime;
    private bool flying;
    private PlayerController player;
    public GameObject hitParticle;

    public float Speed = 20;
    
    // Start is called before the first frame update
    void Start() {
        _navPointManager = Services.instance.Get<NavPointManager>();
        player = Services.instance.Get<PlayerController>();
    }

    // Update is called once per frame
    void Update() {
        if (_navPointManager.NavPoints.Count <= 0) return;

        if (!flying) {
            TryAdvanceNavPoint();
        }
    }

    IEnumerator FlyToNavPoint() {
        flying = true;
//        nextNavPoint.GetComponent<SpriteRenderer>().color = Color.red;
        navStartPos = transform.position;
        navStartTime = Time.time;
        var navDuration = Vector3.Distance(navStartPos, nextNavPoint.transform.position) / Speed;
        float elapsedTime;
        do {
            elapsedTime = Time.time - navStartTime;
            transform.position = Vector3.Lerp(navStartPos, nextNavPoint.transform.position, elapsedTime / navDuration);
//            Debug.LogWarning($"FlyToNavPoint navDuration:{navDuration} navStartPos:{navStartPos} navStartTime:{navStartTime} elapsedTime:{elapsedTime}");
            yield return null;
        } while (elapsedTime < navDuration);
        flying = false;
    }

    void TryAdvanceNavPoint() {
        if (nextNavPointIdx + 1 < _navPointManager.NavPoints.Count) {
            ++nextNavPointIdx;
            nextNavPoint = _navPointManager.NavPoints[nextNavPointIdx];
//            Debug.LogWarning($"TryAdvanceNavPoint nextNavPointIdx:{nextNavPointIdx} nextNavPoint:{nextNavPoint}");
            StartCoroutine(FlyToNavPoint());
        } else {
            nextNavPoint = null;
            flying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.LogWarning($"Ship OnTriggerEnter2D other:{other}");
        player.Damage();
        Instantiate(hitParticle, transform.position, Quaternion.identity);
    }
}
