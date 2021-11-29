using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Potion, Key, Web, Heart, Spray};
    public Type type;
    public int value; //������ ȹ�� �� ������ ���� value����
    public int key_value; //���� ������ ���� ���� ��ȣ ����

    void Update()
    {
        transform.Rotate(Vector3.up * 50 * Time.deltaTime); //�����۵��� �� ���̵��� ȸ��������
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

        if (type == Type.Key) //�����Ϸ��� �������� ������ ��
        {
            switch (key_value) //���� ��ȣ�� ���� ���� ��ġ ����
            {
                case 0:
                    loc_x = -450f;
                    loc_z = 250f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //0�� ������ ��ġ
                    transform.position = item_location;
                    break;
                case 1:
                    loc_x = 250f;
                    loc_z = 450f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //1�� ������ ��ġ
                    transform.position = item_location;
                    break;
                case 2:
                    loc_x = 450f;
                    loc_z = 150f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //2�� ������ ��ġ
                    transform.position = item_location;
                    break;
                case 3:
                    loc_x = -350f;
                    loc_z = -50f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //3�� ������ ��ġ
                    transform.position = item_location;
                    break;
                case 4:
                    loc_x = 250f;
                    loc_z = -150f;
                    item_location = new Vector3(loc_x, 20f, loc_z); //4�� ������ ��ġ
                    transform.position = item_location;
                    break;
                case 5: //door object�� �����ϱ� ���� ���踦 �� ��ġ�� �ΰ� ������ �ʰ� ����
                    loc_x = -47f;
                    loc_z = 470f;
                    item_location = new Vector3(loc_x, 20f, loc_z); 
                    transform.position = item_location;
                    break;
            }

        }
        else //���谡 �ƴ� �ٸ� �������� ��
        {
            //�������� ���� �ǵ���
            loc_x = (float)(Random.Range(0, 10));
            loc_z = (float)(Random.Range(0, 10));

            //���� ����� �ֵ��� ����
            loc_x = loc_x * 100 - 450; 
            loc_z = loc_z * 100 - 450;
            item_location = new Vector3(loc_x, 20f, loc_z);
            transform.position = item_location;

        }
    }
}
