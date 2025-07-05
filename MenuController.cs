using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{    public void GameScene()
    {
        //SceneManager.LoadScene("Menu");
        GameManager.Instance.RestartGame();       
    }
}
