using System.Collections;
using System.Collections.Generic;
using com.eliotlash.core.service;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart() {
        var currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        Services.instance.Clear();
        SceneManager.LoadScene(currentBuildIndex, LoadSceneMode.Single);
    }
}
