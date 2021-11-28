using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MenuMusic");
        if (musicObj.Length > 100)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Easy")
        {
            Destroy(gameObject);
        }
        if (currentScene.name == "Medium")
        {
            Destroy(gameObject);
        }
        if (currentScene.name == "Hard")
        {
            Destroy(gameObject);
        }
        if (currentScene.name == "Hell")
        {
            Destroy(gameObject);
        }
    }
}
