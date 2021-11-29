using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour          // 정지화면 스크립트
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))            // Esc 키에 따른 동작
        {
            if(GameIsPaused)
            {
                Resume();                           // 정지화면 상태에서 Esc 키 누르면 돌아가기
            }
            else
            {
                Pause();                            // 게임 중 Esc 키 누르면 정지화면
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);                       // 정지화면 비활성화
        Time.timeScale = 1f;
        GameIsPaused = false;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();            // 현재 멈춰있는 오디오 소스 찾기(발자국, 몬스터, 배경)

        foreach (AudioSource a in audios)
        {
            a.Play();                                                       // 게임이 재개되면 오디오 이어 재생
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);                        // 정지화면 활성화
        Time.timeScale = 0f;                                // 시간 멈춤 (모든 게임 오브젝트의 동작 멈춤)
        GameIsPaused = true;

        AudioSource[] audios = FindObjectsOfType<AudioSource>();            // 재생중인 오디오 소스 찾고

        foreach (AudioSource a in audios)
        {
            a.Pause();                                                      // 정지화면시 오디오 멈춤
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");                           // Quit 버튼 누르면 메인메뉴로 돌아감
    }
}
