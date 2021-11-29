using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class PlayerMove : MonoBehaviour
{
    //몬스터에게 아이템 사용했음을 알려주기 위해 몬스터를 가져옴
    public MonsterAI monster1;
    public MonsterAI monster2;
    //몬스터의 위치를 알고 플레이어와의 거리를 계산하기 위해 각 몬스터의 agent.transform.position을 이용하기 위해 가져옴
    private NavMeshAgent m1_agent;
    private NavMeshAgent m2_agent;


    public GameObject[] items; // 갖고있는 아이템 리스트
    public int[] hasitem; //각 아이템 보유 개수
    public float playTime; //플레이한 시간

    [SerializeField]
    private float walkSpeed; //플레이어의 이동 속도
    public float sprintingMultiplier; // 플레이어 달리기 할 때 얼마나 빨라질 지

    [SerializeField]
    private float lookSensitivity; // 마우스 화면 전환 감도


    [SerializeField]
    private float cameraRotationLimit; // 마우스 화면 전환 범위
    private float currentCameraRotationX = 0; 


    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;
    private Transform myTransform;
    private float backstapLength = 3.0f;

    bool iDown; //아이템 획득 키를 눌렀는지 (z)
    //아이템 사용키를 눌렀는지(1,2)
    bool item1;
    bool item2;
    bool rDown;
    bool isBorder_front, isBorder_back, isBorder_left, isBorder_right;
    GameObject nearObject; //아이템과 문이 근처에 있는지 파악하기 위한 nearObject

    public Animator SceneTransition;        // 화면 전환 애니메이션


    private void Start()
    {
        m1_agent = monster1.agent;
        m2_agent = monster2.agent;

        //아이템 보유 개수 초기화
        hasitem[0] = 0; //열쇠
        hasitem[1] = 0; //생명
        hasitem[2] = 0; //스프레이
        hasitem[3] = 0; //거미줄
        hasitem[4] = 1000; //체력은 초기 1000/1000
        myRigid = GetComponent<Rigidbody>();
        myTransform = GetComponent<Transform>();

    }


    // 프레임마다 Update 실행
    void Update()
    {
        Move();     // player 오브젝트 이동
        CameraRotation();   // player 카메라 시야 이동
        CharacterRotation();    // player 오브젝트 방향 이동
        Interation();   // 맵(미로)에 있는 아이템과 상호 작용
        use_item();     // 보유한 아이템 사용
        playTime += Time.deltaTime; //플레이 시간 기록(제한시간 위해 사용)

    }

    // 일정 주기마다 FixedUpdate 실행
    void FixedUpdate()
    {
        CollisionWall();
    }

    private void CollisionWall()
    {
        // 캐릭터 시야 정방향 앞, 뒤, 왼쪽, 오른쪽 Border에
        // "wall" mask를 가진 오브젝트와 player 간 충돌이 감지된 경우 반대 방향으로 backstapLength 만큼 player 좌표 이동
        // 충돌이 감지되면 Bool 변수 값 = true
        isBorder_front = Physics.Raycast(transform.position, transform.forward, backstapLength, LayerMask.GetMask("wall"));
        isBorder_back = Physics.Raycast(transform.position, -transform.forward, backstapLength, LayerMask.GetMask("wall"));
        isBorder_left = Physics.Raycast(transform.position, -transform.right, backstapLength, LayerMask.GetMask("wall"));
        isBorder_right = Physics.Raycast(transform.position, transform.right, backstapLength, LayerMask.GetMask("wall"));
    }

    // 플레이어 이동 함수
    private void Move()
    {
        // 방향키 or wasd 입력으로 player 이동
        float _moveDirX = Input.GetAxisRaw("Horizontal");   // 입력된 수평 방향 만큼 이동할 거리
        float _moveDirZ = Input.GetAxisRaw("Vertical");     // 입력된 수직 방향 만큼 이동할 거리

        Vector3 _moveHorizontal = transform.right * _moveDirX;  // ←, →, a, d
        Vector3 _moveVertical = transform.forward * _moveDirZ;  // ↑, ↓, w, s
        rDown = Input.GetButton("Sprint"); //왼쪽 shift키
        bool rUp = Input.GetButtonUp("Sprint");

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;   // 일정 속도의 이동 위해 이동 거리 방향 벡터 normalize
        if (_velocity != Vector3.zero)
        {
            GetComponent<AudioSource>().UnPause();      // 움직이기 시작하면 발자국 소리
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
            GetComponent<AudioSource>().pitch = 1.5f;  // 달릴 때 발자국 소리 빠르게
        }

        if (rUp)
        {
            GetComponent<AudioSource>().pitch = 1.0f;   // 쉬프트 떼면 다시 원래 발자국 소리
        }

        if ((!isBorder_front) && (!isBorder_back) && (!isBorder_left) && (!isBorder_right))
        {
            // 플레이어 앞, 뒤, 양 옆 Border에 모두 충돌처리가 없는 경우
            // 입력 받은 이동 거리 만큼 player 이동
            myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
        }
        if (hasitem[0] == 5 && nearObject != null) // 열쇠 5개 먹고 근처에 object가 있을 때
        {
            if (nearObject.tag == "door") //근처 object가 문일 때
            {
                StartCoroutine("LoadSuccess"); //성공 화면
            }
        }
        float m1_distance = Vector3.Distance(transform.position, m1_agent.transform.position);
        float m2_distance = Vector3.Distance(transform.position, m2_agent.transform.position);

        if (m1_distance < 25f || m2_distance < 25f) // 몬스터에게 잡혔을 때
        {
            if (hasitem[1] > 0) //보유한 생명이 있으면
            {
                hasitem[1]--; //생명하나 감소
                transform.position = new Vector3(0f, 25f, -20f); //새로운 위치에서 리스폰
                hasitem[4] = 1000; //리스폰시 체력 다시 채워줌
            }
            else if(SceneManager.GetActiveScene().name != "Medium") // 보유한 생명도 없고 medium난이도가 아니면
            {
                StartCoroutine("LoadGameOver"); //게임 오버 화면
            }
        }
        if (playTime > 300) // 플레이 시간이 5분 지났을 때
        {
            StartCoroutine("LoadGameOver"); //게임 오버 화면
        }
    }

    private void CharacterRotation() // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");     // 마우스 수평 이동 만큼 시야(카메라) y rotate
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation() // 상하 카메라 회전
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");     // 마우스 수직 이동 만큼 시야(카메라) x rotate
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void Interation()
    {
        iDown = Input.GetButtonDown("Interation");
        if (iDown && nearObject != null) // 근처에 물체가 있고, interation키('z')를 누르면 아이템 획득
        {
            //Debug.Log("interation");

            if (nearObject.tag == "Item") // nearObject가 item일때 
            {
                Item item = nearObject.GetComponent<Item>();
                int itemIndex = item.value;
                if (itemIndex == 4) //물약을 먹었을 때 체력 800을 회복시켜줌
                {
                    hasitem[itemIndex] += 800;
                    if (hasitem[itemIndex] > 1000) //만약 800 채웠을 때 1000을 넘기면 체력은 1000으로 고정
                        hasitem[itemIndex] = 1000;
                }

                else //물약이 아닌 아이템을 먹었을 때
                    hasitem[itemIndex] += 1; //보유 개수 1개 늘려줌

                Destroy(nearObject); //획득한 아이템은 필드에서 삭제
            }
        }
    }
    void use_item()
    {
        item1 = Input.GetButtonDown("equip1");
        item2 = Input.GetButtonDown("equip2");

        if (item1 && hasitem[2] > 0)
        {
            hasitem[2] -= 1; // 몬스터 제자리에 돌도록
            //몬스터에게 아이템1을 사용했다고 알려줌
            monster1.item1_cnt = 1000; //아이템1의 지속 시간
            monster2.item1_cnt = 1000; //아이템2의 지속 시간

        }
        if (item2 && hasitem[3] > 0)
        {
            hasitem[3] -= 1; // 몬스터 느려지게 하도록
            
            //몬스터에게 아이템2를 사용했다고 알려줌
            monster1.item2_cnt = 1000; //아이템1의 지속 시간
            monster2.item2_cnt = 1000; //아이템2의 지속 시간
        }

    }
    private void OnTriggerStay(Collider other) // 충돌처리
    {
        if (other.tag == "Item")
            nearObject = other.gameObject; // 아이템과 충돌하면 nearObject 세팅
        else if(other.tag == "door")
            nearObject = other.gameObject; //문과 충돌하면 nearObject를 세팅

    }

    public void OnTriggerExit(Collider other) //object과 충돌이 되지 않은 상태이면
    {
        if (other.tag == "Item")
            nearObject = null; //nearobject를 없애줌
    }

    IEnumerator LoadGameOver()                      // 애니메이션을 추가해서 게임 종료 장면 불러옴
    {
        SceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("GameOver");
    }

    IEnumerator LoadSuccess()
    {
        SceneTransition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Success");
    }
}
