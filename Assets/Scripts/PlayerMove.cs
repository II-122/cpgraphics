using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerMove : MonoBehaviour
{
    public MonsterAI monster1;
    public MonsterAI monster2;
    private NavMeshAgent m1_agent;
    private NavMeshAgent m2_agent;


    public GameObject[] items; //갖고있는 아이템 리스트
    public int[] hasitem;
    public float playTime;

    [SerializeField]
    private float walkSpeed;
    public float sprintingMultiplier; //플레이어 이동 속도

    [SerializeField]
    private float lookSensitivity; //마우스 화면 전환 감도


    [SerializeField]
    private float cameraRotationLimit; //마우스 화면 전환 범위
    private float currentCameraRotationX = 0;


    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;
    private Transform myTransform;
    private float backstapLength = 3.0f;

    bool iDown;
    bool item1;
    bool item2;
    bool item3;
    bool rDown;
    bool isBorder_front, isBorder_back, isBorder_left, isBorder_right;
    GameObject nearObject;


    private void Start()
    {
        m1_agent = monster1.agent;
        m2_agent = monster2.agent;

        hasitem[0] = 0;
        hasitem[1] = 0;
        hasitem[2] = 0;
        hasitem[3] = 0;
        hasitem[4] = 1000;
        myRigid = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();

    }


    // Update is called once per frame
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
        Interation();
        use_item();
        playTime += Time.deltaTime;

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
        rDown = Input.GetButton("Sprint");
        bool rUp = Input.GetButtonUp("Sprint");

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;
        if (_velocity != Vector3.zero)
        {
            GetComponent<AudioSource>().UnPause();
        }
        else
        {
            GetComponent<AudioSource>().Pause();
            GetComponent<AudioSource>().pitch = 1.0f;
        }

        if (rDown && hasitem[4] > 0)
        {
            hasitem[4] -= 1;
            _velocity *= sprintingMultiplier;       // 왼쪽 Shift 누르면 달리기
            GetComponent<AudioSource>().pitch = 1.5f;
        }

        if(rUp)
        {
            GetComponent<AudioSource>().pitch = 1.0f;
        }

        if ((!isBorder_front)&&(!isBorder_back)&&(!isBorder_left)&&(!isBorder_right)) 
        {
            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        
        }
        if (hasitem[0] == 5 && nearObject != null) //열쇠 5개 먹고 문에 다가갔을 때
        {
            if(nearObject.tag == "door")
            {
                //Debug.Log("finish");
                //이 부분에 성공 화면 넣어주시면 될 것 같아요
            }
        }
        float m1_distance = Vector3.Distance(transform.position, m1_agent.transform.position);
        float m2_distance = Vector3.Distance(transform.position, m2_agent.transform.position);
        if (m1_distance < 30f || m2_distance <30f || playTime>300) //몬스터에게 잡히거나 플레이 시간이 5분 지났을 때
        {
            //Debug.Log("Lose");
            //이 부분에 실패 화면 넣어주시면 될 것 같아요
        }
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
                if (itemIndex == 4)
                {
                    hasitem[itemIndex] += 800;
                    if (hasitem[itemIndex] > 1000)
                        hasitem[itemIndex] = 1000;
                }

                else
                    hasitem[itemIndex] += 1;

                Destroy(nearObject);
            }
        }
    }
    void use_item()
    {
        item1 = Input.GetButtonDown("equip1");
        item2 = Input.GetButtonDown("equip2");
        item3 = Input.GetButtonDown("equip3");

        if (item1 && hasitem[2] > 0)
        {
            hasitem[2] -= 1; //몬스터 제자리
            monster1.item1_cnt = 1000;
            monster2.item1_cnt = 1000;

        }
        if (item2 && hasitem[3] > 0)
        {
            hasitem[3] -= 1; //몬스터 느려짐
            monster1.item2_cnt = 1000;
            monster2.item2_cnt = 1000;
        }
        if (item3 && hasitem[4] > 0)
        {
            hasitem[4] -= 1;
        }

    }
    private void OnTriggerStay(Collider other) //충돌처리
    {
        if (other.tag == "Item")
            nearObject = other.gameObject; //아이템과 충돌하면 nearObject 세팅
        else if(other.tag == "door")
            nearObject = other.gameObject;

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
            nearObject = null;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("충돌 감지");
    }
}
