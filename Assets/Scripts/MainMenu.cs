using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour           // 게임의 첫 화면 (메인메뉴)
{
    public Animator SceneTransition;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       // Run 버튼을 선택하면 다음 장면으로(난이도 선택)
    }

    public void QuitGame()
    {
        Application.Quit();             // 게임 종료
    }

    public void GoToMain()
    {
        StartCoroutine("LoadMain"); // 게임 오버와 게임 승리 화면에서 메인 메뉴로 이어줌
    }

    IEnumerator LoadMain()
    {
        SceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("MainMenu");
    }
}
