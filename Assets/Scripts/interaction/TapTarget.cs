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
    static readonly Color tappedColor = new Color(1, 0, 0, 0.5f);

    private void Start() {
        sprite = GetComponent<SpriteShapeRenderer>();
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
        StartCoroutine(Death());
    }

    private void Update() {
        if (!wasTapped) {
//            var percentElapsedToNextBeat = Services.instance.Get<TempoManager>().percentElapsedToNextBeat();
            var percentElapsedToNextMesaure = Services.instance.Get<TempoManager>().percentElapsedToNextMeasure();
            sprite.color = Color.Lerp(lerpStartColor, lerpEndColor, percentElapsedToNextMesaure);
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(1f);
        sprite.enabled = false;
        Destroy(gameObject);
    }
}
