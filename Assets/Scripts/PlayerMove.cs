using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    Vector3 moveVec;

    Animator anim;


    // Update is called once per frame
    void Update()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        //wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        //transform.position += moveVec * speed * (wDown ? 0.3f : 1.5f) * Time.deltaTime;
        transform.position += moveVec * speed * Time.deltaTime;



        transform.LookAt(transform.position + moveVec); //이후 마우스에 따라 시점 변경
    }
}