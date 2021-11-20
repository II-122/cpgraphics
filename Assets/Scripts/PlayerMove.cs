using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject[] items; //갖고있는 아이템 리스트
    public bool[] hasitem;

    [SerializeField]
    private float walkSpeed; //플레이어 이동 속도

    [SerializeField]
    private float lookSensitivity; //마우스 화면 전환 감도


    [SerializeField]
    private float cameraRotationLimit; //마우스 화면 전환 범위
    private float currentCameraRotationX = 0;


    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;

    private float backstapLength = 3.0f;

    bool iDown;
    bool isBorder_front, isBorder_back, isBorder_left, isBorder_right;
    GameObject nearObject;

    private void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
        Interation();
    }

    void FixedUpdate()
    {
        CollisionWall();
    }

    private void CollisionWall()
    {
        
        isBorder_front = Physics.Raycast(transform.position, transform.forward, backstapLength, LayerMask.GetMask("wall"));
        isBorder_back = Physics.Raycast(transform.position, -transform.forward, backstapLength, LayerMask.GetMask("wall"));
        isBorder_left = Physics.Raycast(transform.position, -transform.right, backstapLength, LayerMask.GetMask("wall"));
        isBorder_right = Physics.Raycast(transform.position, transform.right, backstapLength, LayerMask.GetMask("wall"));
    }
    //플레이어 이동 함수
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
  
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed; //대각선 이동속도도 동일하게 맞춰주기 위해 normalized를 통해 정규화

        if ((!isBorder_front)&&(!isBorder_back)&&(!isBorder_left)&&(!isBorder_right)) { myRigid.MovePosition(transform.position + _velocity * Time.deltaTime); }

    }

    private void CharacterRotation() //좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation() //상하 카메라 회전
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void Interation()
    {
        iDown = Input.GetButtonDown("Interation");
        if (iDown && nearObject != null) //근처에 물체가 있고, interation키('z')를 누르면 아이템 획득
        {
            //Debug.Log("interation");

            if (nearObject.tag == "Item") //nearObject가 item일때 
            {
                Item item = nearObject.GetComponent<Item>();
                int itemIndex = item.value;
                hasitem[itemIndex] = true;

                Destroy(nearObject);
            }
        }
    }
    private void OnTriggerStay(Collider other) //충돌처리
    {
        if (other.tag == "Item")
            nearObject = other.gameObject; //아이템과 충돌하면 nearObject 세팅

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
            nearObject = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("충돌 감지");
    }
}
