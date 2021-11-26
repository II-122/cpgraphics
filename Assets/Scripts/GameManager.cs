using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public GameObject Camera;
    public PlayerMove player;
    public float PlayTime;
    public MonsterAI monster;

    public GameObject InGame;
    public Text countTimeTxt;
    public Image SprayImg;
    public Image WebImg;
    public Image PotionImg;

    private void Awake()
    {
        
    }
}
