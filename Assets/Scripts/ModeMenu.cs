using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeMenu : MonoBehaviour           // 모드 선택 창의 버튼들의 동작
{
    public void PlayEasy()
    {
        SceneManager.LoadScene("Easy");         // 쉬운모드
    }

    public void PlayMedium()
    {
        SceneManager.LoadScene("Medium");       // 중간모드
    }

    public void PlayHard()
    {
        SceneManager.LoadScene("Hard");         // 어려움모드
    }

    public void PlayHell()
    {
        SceneManager.LoadScene("Hell");         // 지옥모드
    }
}
