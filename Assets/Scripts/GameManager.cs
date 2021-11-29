using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�ʿ��� ������ ����
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
        playTime += Time.deltaTime; //�÷��� �ð� ����
    }
    void LateUpdate()
    {
        //�÷��� �ð��� �̿��Ͽ� ���ѽð�(5��)�� �پ�鵵�� ��
        int hour = (int)(playTime / 3600);
        int min = 4 - (int)((playTime - hour * 3600) / 60);
        int second = 59 - (int)playTime % 60;
        countTimeTxt.text = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

        KeyNum.text = player.hasitem[0] + " / 5"; //������ ����
        HeartNum.text = "X " + player.hasitem[1]; //������ ����
        SprayNum.text = "X" + player.hasitem[2]; //������1(��������)�� ����
        WebNum.text = "X" + player.hasitem[3]; //������2(�Ź���)�� ����

        staminaBar.localScale = new Vector3((float)((float)(player.hasitem[4])) / (float)1000, 1, 1); //ü�¹� ����
    }
}