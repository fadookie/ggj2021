using com.eliotlash.core.service;
using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject spawnPrefabAt(Vector2 position)
    {
        var obj = Instantiate(Prefab);
		obj.transform.parent = transform;
        obj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10));
        obj.transform.rotation = transform.rotation;
        return obj;
    }


    public void AddNavPoint(Vector2 position) {
        var go = spawnPrefabAt(position);
        navPoints.Add(go);
        lineRenderer.positionCount = navPoints.Count;
        lineRenderer.SetPosition(navPoints.Count - 1, go.transform.position);
    }
}
