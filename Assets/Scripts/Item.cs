using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Potion, Key, Web, Heart, Spray};
    public Type type;
    public int value;
    public int key_value;

    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime);
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

        if (type == Type.Key)
        {
            switch (key_value) 
            {
                case 0:
                    loc_x = -450f;
                    loc_z = 250f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
                    transform.position = item_location;
                    break;
                case 1:
                    loc_x = 250f;
                    loc_z = 450f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
                    transform.position = item_location;
                    break;
                case 2:
                    loc_x = 450f;
                    loc_z = 150f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
                    transform.position = item_location;
                    break;
                case 3:
                    loc_x = -350f;
                    loc_z = -50f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
                    transform.position = item_location;
                    break;
                case 4:
                    loc_x = 250f;
                    loc_z = -150f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
                    transform.position = item_location;
                    break;
                case 5:
                    loc_x = -47f;
                    loc_z = 470f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
                    transform.position = item_location;
                    break;
            }

        }
        else
        {
            loc_x = (float)(Random.Range(0, 10));
            loc_z = (float)(Random.Range(0, 10));
            loc_x = loc_x * 100 - 450;
            loc_z = loc_z * 100 - 450;
            item_location = new Vector3(loc_x, 20f, loc_z); //추후 random, 3f, random으로 변경 random값은 (0~9)*100 - 450 혹은 스폰지역을 정해두고 하자
            transform.position = item_location;

        }
    }
}
