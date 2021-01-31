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
    public GameObject explosionParticle;
    private bool isDying = false;

    public float Speed = 20;
    public float RotationSpeed = 2;
    public float DeathSpinSpeed = 1;
    
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
        
        var vectorToTarget = nextNavPoint.transform.position - transform.position;
        var targetAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90; // Sprite is pointing up so offset angle by 90 degrees
        var targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        
        do {
            elapsedTime = Time.time - navStartTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
            transform.position = Vector3.Lerp(navStartPos, nextNavPoint.transform.position, Easing.Sinusoidal.InOut(elapsedTime / navDuration));
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
        if (isDying) return;
        Services.instance.Get<SfxPlayer>().PlaySound(SfxPlayer.Sound.Damage);
        player.Damage();
        if (player.health.Value <= 0) {
            StartCoroutine(Death());
        } else {
            // Hit obstacle
            Instantiate(hitParticle, transform.position, Quaternion.identity);
        }
    }

    IEnumerator Death() {
        isDying = true;
        var explosionGo = Instantiate(explosionParticle, transform);
        explosionGo.transform.localPosition = Vector3.zero;
        var deathStartTime = Time.time;
        do {
//            var newRotation = transform.rotation.eulerAngles;
//            newRotation.z += DeathSpinSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, DeathSpinSpeed * Time.deltaTime));
            yield return null;
        } while (Time.time - deathStartTime < 2);
        GetComponent<SpriteRenderer>().enabled = false;
        player.OnShipDeathFinished();
    }
}
