using System;
using com.eliotlash.core.service;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class InputController : MonoBehaviour
{
    private Subject<Vector3> mouseClicks = new Subject<Vector3>();
    public IObservable<Vector3> MouseClicks => mouseClicks;
    
    void Awake() {
        Services.instance.Set(this);
    }

    void Start() {
        // WHY doesn't this work?????
//        this.OnMouseDownAsObservable()
//            .Select(_ => Input.mousePosition)
//            .Subscribe(mouseClicks)
//            .AddTo(this);
//        mouseClicks.Subscribe(pos => Debug.LogWarning($"mouseClicked pos:{pos}")).AddTo(this);
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            mouseClicks.OnNext(Input.mousePosition);
        }
    }
}
