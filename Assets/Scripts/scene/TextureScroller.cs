using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public Vector2[] ScrollSpeeds;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        for (var i = 0; i < ScrollSpeeds.Length; ++i) {
            meshRenderer.materials[i].mainTextureOffset += ScrollSpeeds[i] * Time.deltaTime;
        }
    }
}
