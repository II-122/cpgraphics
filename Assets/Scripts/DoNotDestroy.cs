using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MenuMusic");     // 메인 메뉴 음악을 찾고 리스트에 넣기
        if (musicObj.Length > 1)            
        {
            Destroy(this.gameObject);           // 2개 이상 오디오 소스가 발견되면 음악 종료
        }
        DontDestroyOnLoad(this.gameObject);         // 하나면 다음 장면으로 가도 오디오가 자연스럽게 이어짐(난이도 선택 화면)
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "Easy")                    // 만약 장면이 또 넘어갔는데 게임 화면이면
        {
            Destroy(gameObject);                            // 메인메뉴 오디오 종료
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
