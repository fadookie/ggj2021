using System.Collections;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UniRx;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 2;
    public float ShakeDuration = 1;
    public float ShakeMagnitude = 1;
    private Vector3 canoncialPos;
    private Vector3 screenShakeOffset = Vector3.zero;
    private Coroutine shakeRoutine;

    void Start() {
        Services.instance.Get<PlayerController>().OnDamage.Subscribe(OnDamage).AddTo(this);
        canoncialPos = transform.position;
    }

    // Update is called once per frame
    void Update() {
        canoncialPos.x += Speed * Time.deltaTime;
        transform.position = canoncialPos + screenShakeOffset;
    }

    void OnDamage(Unit _) {
        if (shakeRoutine != null) {
            StopCoroutine(shakeRoutine);
        }
        shakeRoutine = StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        var startTime = Time.time;
        float elapsed;
        do {
            elapsed = Time.time - startTime;
            screenShakeOffset.x = Random.Range(-1f, 1f) * ShakeMagnitude;
            screenShakeOffset.y = Random.Range(-1f, 1f) * ShakeMagnitude;
            yield return null;
        } while (elapsed < ShakeDuration);
        screenShakeOffset = Vector3.zero;
    }
}
