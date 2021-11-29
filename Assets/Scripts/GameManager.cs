using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //필요한 변수들 세팅
    public PlayerMove player;
    public float playTime;
    public Text countTimeTxt;
    public Text KeyNum;
    public Text HeartNum;
    public Text SprayNum;
    public Text WebNum;

    public Image SprayImg;
    public Image WebImg;
    public Image PotionImg;

    public RectTransform staminaGroup;
    public RectTransform staminaBar;
    void Update()
    {
        playTime += Time.deltaTime; //플레이 시간 측정
    }
    void LateUpdate()
    {
        //플레이 시간을 이용하여 제한시간(5분)이 줄어들도록 함
        int hour = (int)(playTime / 3600);
        int min = 4 - (int)((playTime - hour * 3600) / 60);
        int second = 59 - (int)playTime % 60;
        countTimeTxt.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

        KeyNum.text = player.hasitem[0] + " / 5"; //열쇠의 개수
        HeartNum.text = "X " + player.hasitem[1]; //생명의 개수
        SprayNum.text = "X" + player.hasitem[2]; //아이템1(스프레이)의 개수
        WebNum.text = "X" + player.hasitem[3]; //아이템2(거미줄)의 개수

        staminaBar.localScale = new Vector3((float)((float)(player.hasitem[4])) / (float)1000, 1, 1); //체력바 설정
    }
}