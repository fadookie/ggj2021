﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenMenu : MonoBehaviour
{

    public void startGame(){
        SceneManager.LoadScene("JonYarnTests");
    }
}