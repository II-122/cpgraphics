using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject Camera;
    public PlayerMove player;
    public MonsterAI monster;
    public float playTime;
    public GameObject InGame;
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
        playTime += Time.deltaTime;
    }
    void LateUpdate()
    {

        int hour = (int)(playTime / 3600);
        int min = 4 - (int)((playTime - hour * 3600) / 60);
        int second = 59 - (int)playTime % 60;
        countTimeTxt.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);
        KeyNum.text = player.hasitem[0] + " / 5";
        HeartNum.text = "X " + player.hasitem[1];
        SprayNum.text = "X" + player.hasitem[2];
        WebNum.text = "X" + player.hasitem[3];

        staminaBar.localScale = new Vector3((float)((float)(player.hasitem[4])) / (float)2000, 1, 1);
    }
}