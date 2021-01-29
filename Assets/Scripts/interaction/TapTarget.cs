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
        sprite.color = Color.red;
        wasTapped = true;
        StartCoroutine(Death());
    }

    private void Update() {
        if (!wasTapped) {
            var percentElapsedToNextBeat = Services.instance.Get<TempoManager>().percentElapsedToNextBeat();
            sprite.color = Color.Lerp(Color.black, Color.white, percentElapsedToNextBeat);
        }
    }

    IEnumerator Death() {
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
