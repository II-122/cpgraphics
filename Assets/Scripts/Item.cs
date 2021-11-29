using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Potion, Key, Web, Heart, Spray};
    public Type type;
    public int value; //아이템 획득 시 구분을 위한 value설정
    public int key_value; //열쇠 스폰을 위한 열쇠 번호 지정

    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime); //아이템들을 잘 보이도록 회전시켜줌
    }

    private void Start()
    {
        item_spawn();
    }

    public void item_spawn()
    {   
        Vector3 item_location;
        float loc_x;
        float loc_z;

        if (type == Type.Key) //스폰하려는 아이템이 열쇠일 때
        {
            switch (key_value) //열쇠 번호에 따라 스폰 위치 설정
            {
                case 0:
                    loc_x = -450f;
                    loc_z = 250f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //0번 열쇠의 위치
                    transform.position = item_location;
                    break;
                case 1:
                    loc_x = 250f;
                    loc_z = 450f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //1번 열쇠의 위치
                    transform.position = item_location;
                    break;
                case 2:
                    loc_x = 450f;
                    loc_z = 150f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //2번 열쇠의 위치
                    transform.position = item_location;
                    break;
                case 3:
                    loc_x = -350f;
                    loc_z = -50f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //3번 열쇠의 위치
                    transform.position = item_location;
                    break;
                case 4:
                    loc_x = 250f;
                    loc_z = -150f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //4번 열쇠의 위치
                    transform.position = item_location;
                    break;
                case 5: //door object를 설정하기 위해 열쇠를 문 위치에 두고 보이지 않게 설정
                    loc_x = -47f;
                    loc_z = 470f;
                    item_location = new Vector3(loc_x, 20f, loc_z); 
                    transform.position = item_location;
                    break;
            }

        }
        else //열쇠가 아닌 다른 아이템일 때
        {
            //랜덤으로 스폰 되도록
            loc_x = (float)(Random.Range(0, 10));
            loc_z = (float)(Random.Range(0, 10));

            //길의 가운데에 있도록 설정
            loc_x = loc_x * 100 - 450; 
            loc_z = loc_z * 100 - 450;
            item_location = new Vector3(loc_x, 20f, loc_z);
            transform.position = item_location;

        }
    }
}
