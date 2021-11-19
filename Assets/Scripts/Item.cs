using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon};
    public Type type;
    public int value;

    void Update()
    {
        transform.Rotate(Vector3.up * 40 * Time.deltaTime);
    }

    private void Start()
    {
        item_spawn();
    }

    public void item_spawn()
    {
        Vector3 item_location = new Vector3(2f, 50f, -225f); //추후 random, 3f, random으로 변경 random값은 -225 + (0~9)*50 혹은 스폰지역을 정해두고 하자
        transform.position = item_location;
    }
}
