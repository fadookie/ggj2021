using System;
using System.Collections;
using com.eliotlash.core.service;
using TouchScript.Gestures;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeRenderer))]
[RequireComponent(typeof(TapGesture))]
public class TapTarget : MonoBehaviour
{
    private bool wasTapped;
    private SpriteShapeRenderer sprite;
    static readonly Color lerpStartColor = new Color(0, 0, 0, 0.5f);
    static readonly Color lerpEndColor = new Color(1, 1, 1, 0.5f);
    static readonly Color tappedColor = new Color(0, 1, 0, 0.5f);
    static readonly Color missedColor = new Color(1, 0, 0, 0.5f);
    public int targetBeat;
    private float startTime;
    private TempoManager tempoManager;

    private void Start() {
        sprite = GetComponent<SpriteShapeRenderer>();
        tempoManager = Services.instance.Get<TempoManager>();
        startTime = tempoManager.SourceTime;
    }

    private void OnEnable()
    {
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }

    private void tappedHandler(object sender, EventArgs e) {
        sprite.color = tappedColor;
        wasTapped = true;
        var targetTime = tempoManager.beatTime(targetBeat);
        var accuracy = Mathf.Abs(tempoManager.SourceTime - targetTime);
        Debug.LogWarning($"Target tapped, time:{tempoManager.SourceTime} targetBeat:{targetBeat} targetTime:{targetTime} accuracy:{accuracy}");
        StartCoroutine(Death());
    }

    private void Update() {
        if (!wasTapped) {
//            var percentElapsedToNextBeat = Services.instance.Get<TempoManager>().percentElapsedToNextBeat();
//            var percentElapsedToNextMesaure = Services.instance.Get<TempoManager>().percentElapsedToNextMeasure();
            var percentElapsedToTarget = tempoManager.percentElapsedToBeat(startTime, targetBeat);
            if (percentElapsedToTarget <= 1.0f) {
                sprite.color = Color.Lerp(lerpStartColor, lerpEndColor, percentElapsedToTarget);
            } else if (percentElapsedToTarget > 1.0f && percentElapsedToTarget < 1.5f) {
                sprite.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            } else {
                //consider this missed
                sprite.color = missedColor;
                StartCoroutine(Death());
            }
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(1f);
        sprite.enabled = false;
        Destroy(gameObject);
    }
}
