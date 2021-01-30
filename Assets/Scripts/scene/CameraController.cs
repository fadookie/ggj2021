using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        var newPos = transform.position;
        newPos.x += Speed * Time.deltaTime;
        transform.position = newPos;
    }
}
