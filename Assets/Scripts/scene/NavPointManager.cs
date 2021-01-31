using com.eliotlash.core.service;
using System.Collections;
using System.Collections.Generic;
using Antlr4.Runtime;
using UniRx;
using UnityEngine;

public class NavPointManager : MonoBehaviour
{
    public GameObject Prefab;
    private LineRenderer lineRenderer;

    private List<GameObject> navPoints = new List<GameObject>();
    public IReadOnlyList<GameObject> NavPoints => navPoints;
    
    void Awake() {
        Services.instance.Set(this);
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    // Start is called before the first frame update
    void Start() {
        Services.instance.Get<InputController>().MouseClicks.Subscribe(AddNavPoint).AddTo(this);
    }

    private GameObject spawnPrefabAt(Vector3 position)
    {
        var obj = Instantiate(Prefab);
		obj.transform.parent = transform;
        obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
        obj.transform.rotation = transform.rotation;
        return obj;
    }


    public void AddNavPoint(Vector3 position) {
        var go = spawnPrefabAt(position);
        navPoints.Add(go);
        lineRenderer.positionCount = navPoints.Count;
        lineRenderer.SetPosition(navPoints.Count - 1, go.transform.position);
    }
}
