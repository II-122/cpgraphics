using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Potion, Key, Web, Heart, Spray};
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
        Vector3 item_location;
        if (type == Type.Heart)
            item_location = new Vector3(2f, 25f, -225f); //���� random, 3f, random���� ���� random���� -225 + (0~9)*50 Ȥ�� ���������� ���صΰ� ����
        else
            item_location = new Vector3(-8f, 25f, -225f); //���� random, 3f, random���� ���� random���� -225 + (0~9)*50 Ȥ�� ���������� ���صΰ� ����
        transform.position = item_location;
    }
}
