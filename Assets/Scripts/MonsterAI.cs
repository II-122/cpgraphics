using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 3 STATES MONSTER
 *  patrolling when the player is far away
 *  chasing when the player gets inside range
 */

public class MonsterAI : MonoBehaviour
{

    public float detectDistance;
    public Vector3 walkPoint;
    public float walkPointRange;

    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;

    public bool playerInSight;

    public Transform[] navPoints;
    public int idx;

    public int item1_cnt;
    public int item2_cnt;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.SetTrigger("Walk");
        item1_cnt = 0;
        item2_cnt = 0;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, agent.transform.position);
        // run
        if (30.0f < distance && distance <= detectDistance && item1_cnt == 0 && item2_cnt == 0) //아이템 사용하지 않았고, 몬스터가 쫓아올 때
        {
            playerInSight = true; //사용자 발견
            agent.SetDestination(player.transform.position); //사용자 쪽으로 다가감
            agent.speed = 75; //달리기 속도
            animator.SetTrigger("Run"); //달리는 애니메이션
        }
        else if(30.0f < distance && distance <= detectDistance && item1_cnt > 0 && item2_cnt == 0) //몬스터가 쫓아오는 중에 아이템 1을 사용했을 때
        {
            playerInSight = false; //사용자 미발견
            GotoNextPoint(); //사용자쪽이 아닌 목표물로 가도록
            agent.speed = 75; //달리기 속도
            animator.SetTrigger("Run"); //달리기 애니메이션
            item1_cnt--; //아이템1 지속시간 줄어듬
        }
        else if (30.0f < distance && distance <= detectDistance && item1_cnt == 0 && item2_cnt > 0) //몬스터가 쫓아오는 중에 아이템 2를 사용했을 때
        {
            playerInSight = true; //사용자 발견
            agent.SetDestination(player.transform.position); //사용자 쪽으로 다가감
            agent.speed = 20; //걷기 속도
            animator.SetTrigger("Run"); //달리는 애니메이션
            item2_cnt--; //아이템1 지속시간 줄어듬

        }
        else if (30.0f < distance && distance <= detectDistance && item1_cnt > 0 && item2_cnt > 0) //몬스터가 쫓아오는 중에 아이템1,2를 모두 사용했을 때
        {
            playerInSight = false;//사용자 미발견
            GotoNextPoint(); //사용자쪽이 아닌 목표물로 가도록
            agent.speed = 20; //걷기 속도
            animator.SetTrigger("Walk"); //걷는 애니메이션
            item1_cnt--; //아이템1 지속시간 줄어듬
            item2_cnt--; //아이템1 지속시간 줄어듬
        }
        // walk
        else // if (distance <= 30.0f || distance > lookRadius) 몬스터가 쫒아오지 않을 때
        {
            playerInSight = false; //사용자 미발견
            GotoNextPoint(); //사용자쪽이 아닌 목표물로 가도록
            agent.speed = 20; //걷기 속도
            animator.SetTrigger("Walk"); //걷는 애니메이션

            //아이템 사용한 뒤 멀어졌을때도 아이템 지속시간 줄어들도록
            if (item1_cnt>0) item1_cnt--; 
            if (item2_cnt > 0) item2_cnt--;
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (navPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = navPoints[idx].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            idx = (idx + 1) % navPoints.Length;
    }
}