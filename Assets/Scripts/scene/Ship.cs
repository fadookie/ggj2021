using com.eliotlash.core.service;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private NavPointManager _navPointManager;
    private int nextNavPointIdx = -1;
    private Vector3? nextNavPoint;
    private Vector3 navStartPos;
    private float navStartTime;
    private bool flying;
    private PlayerController player;
    public GameObject hitParticle;
    public GameObject healParticle;
    public GameObject explosionParticle;
    private bool isDying = false;
    private bool isWinning = false;
    private Coroutine flyingRoutine;
    private float lastFlyingAbortTime;

    public float Speed = 20;
    public float RotationSpeed = 2;
    public float DeathSpinSpeed = 1;
    
    // Start is called before the first frame update
    void Start() {
        _navPointManager = Services.instance.Get<NavPointManager>();
        player = Services.instance.Get<PlayerController>();
        player.OnHeal.Subscribe(_ => SpawnParticleChild(healParticle)).AddTo(this);
		Services.instance.Get<MusicController>()
			.MusicEventStream
			.Where(evt => evt.type == MusicController.MusicEventType.Blastoff)
			.Subscribe(MaybeStartWin)
			.AddTo(this);
    }

    // Update is called once per frame
    void Update() {
        // Check if we fell off the left side
        var leftSidePos = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Debug.LogWarning($"Ship posX:{transform.position.x} < leftSidePosX:{leftSidePos.x} = {transform.position.x < leftSidePos.x}");
        if (transform.position.x < leftSidePos.x) {
            var newPos = transform.position;
            newPos.x = leftSidePos.x;
            transform.position = newPos;
            if (flying && Time.time - lastFlyingAbortTime < 0.25f) {
                lastFlyingAbortTime = Time.time;
                StopCoroutine(flyingRoutine);
                flyingRoutine = StartCoroutine(FlyToNavPoint());
            }
        }
        
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
        var navDuration = Vector3.Distance(navStartPos, nextNavPoint.Value) / Speed;
        float elapsedTime;
        
        var vectorToTarget = nextNavPoint.Value - transform.position;
        var targetAngle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90; // Sprite is pointing up so offset angle by 90 degrees
        var targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        
        do {
            elapsedTime = Time.time - navStartTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * RotationSpeed);
            transform.position = Vector3.Lerp(navStartPos, nextNavPoint.Value, Easing.Sinusoidal.InOut(elapsedTime / navDuration));
//            Debug.LogWarning($"FlyToNavPoint navDuration:{navDuration} navStartPos:{navStartPos} navStartTime:{navStartTime} elapsedTime:{elapsedTime}");
            yield return null;
        } while (elapsedTime < navDuration);
        flying = false;
    }

    void TryAdvanceNavPoint() {
        if (isWinning) return;
        if (nextNavPointIdx + 1 < _navPointManager.NavPoints.Count) {
            ++nextNavPointIdx;
            nextNavPoint = _navPointManager.NavPoints[nextNavPointIdx].transform.position;
//            Debug.LogWarning($"TryAdvanceNavPoint nextNavPointIdx:{nextNavPointIdx} nextNavPoint:{nextNavPoint}");
            flyingRoutine = StartCoroutine(FlyToNavPoint());
        } else {
            nextNavPoint = null;
            flying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.LogWarning($"Ship OnTriggerEnter2D other:{other}");
        if (isDying || isWinning) return;
        Services.instance.Get<SfxPlayer>().PlaySound(SfxPlayer.Sound.Damage);
        player.Damage();
        if (player.health.Value <= 0) {
            StartCoroutine(Death());
        } else {
            // Hit obstacle
            Instantiate(hitParticle, transform.position, Quaternion.identity);
        }
    }

    void MaybeStartWin(MusicController.MusicEvent _) {
        if (isDying) return;
        isWinning = true;
        Services.instance.Get<GameController>().IsWinning = true;
        StopCoroutine(flyingRoutine);
        nextNavPointIdx = -1;
        var flyAwayTarget = transform.position;
        flyAwayTarget.x += 50;
        nextNavPoint = flyAwayTarget;
        flyingRoutine = StartCoroutine(FlyToNavPoint());
    }

    IEnumerator Death() {
        isDying = true;
        SpawnParticleChild(explosionParticle);
        var deathStartTime = Time.time;
        do {
//            var newRotation = transform.rotation.eulerAngles;
//            newRotation.z += DeathSpinSpeed * Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, DeathSpinSpeed * Time.deltaTime));
            yield return null;
        } while (Time.time - deathStartTime < 2);
        GetComponent<SpriteRenderer>().enabled = false;
        Services.instance.Get<GameController>().OnShipDeathFinished();
    }

    void SpawnParticleChild(GameObject prefab) {
        var childGo = Instantiate(prefab, transform);
        childGo.transform.localPosition = Vector3.zero;
    }
}
