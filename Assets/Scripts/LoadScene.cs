using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour          // 게임 오버/성공 화면으로 부드럽게 전환
{
    public Animator SceneTransition;

    public void GoToMain()
    {
        StartCoroutine("LoadMain");
    }

    IEnumerator LoadMain()
    {
        SceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(0);

        SceneManager.LoadScene("MainMenu");
    }
};
