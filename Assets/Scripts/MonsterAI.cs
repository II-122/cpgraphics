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

    public float detectDistance = 180.0f;
    public Vector3 walkPoint;
    public float walkPointRange;

    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;

    public bool playerInSight;

    public Transform[] wallposition;
    public int randomIdx;
    public bool pickNewWall;

    RaycastHit hit;
    LayerMask m_maskwall;


    private void Start()
    {
        m_maskwall = GameObject.FindWithTag("Wall").GetComponent<LayerMask>();

        wallposition = new Transform[]
        { GameObject.Find("wall (1)").transform, GameObject.Find("wall (7)").transform, GameObject.Find("wall (13)").transform,
        GameObject.Find("wall (16)").transform, GameObject.Find("wall (19)").transform, GameObject.Find("wall (21)").transform,
        GameObject.Find("wall (27)").transform, GameObject.Find("wall (30)").transform, GameObject.Find("wall (32)").transform,
        GameObject.Find("wall (34)").transform, GameObject.Find("wall (37)").transform, GameObject.Find("wall (39)").transform,
        GameObject.Find("wall (41)").transform, GameObject.Find("wall (44)").transform, GameObject.Find("wall (48)").transform };


        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        animator.SetTrigger("Walk");
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, agent.transform.position);
        // run
        if (60.0f < distance && distance <= detectDistance)
        {
            playerInSight = true;
            agent.SetDestination(player.transform.position);
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Run");
        }
        // walk
        else // if (distance <= 60.0f || distance > lookRadius)
        {
            playerInSight = false;
            //SearchWalkPoint();
            Patroling();
            animator.ResetTrigger("Run");
            animator.SetTrigger("Walk");
        }
    }

    private float waitTime;
    public float startWaitTime = 1f;
    private int randomSpot;

    void Patroling()
    {
        agent.SetDestination(wallposition[randomSpot].position);

        if (Vector3.Distance(transform.position, wallposition[randomSpot].position) < 2.0f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, wallposition.Length);
                agent.SetDestination(transform.position + Vector3.forward * agent.speed * Time.deltaTime);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }


    public void Blind() // 렉걸리게 장님
    {
        randomIdx = Random.Range(0, wallposition.Length);
        agent.SetDestination(wallposition[randomIdx].position);

    }



    private void SearchWalkPoint()
    {
        agent.SetDestination(wallposition[randomIdx].position);
        // 도착했으면 새로운 벽 목적지 지정
        if (Physics.Raycast(agent.transform.position, Vector3.forward, out hit, m_maskwall))
        {
            randomIdx = Random.Range(0, wallposition.Length);
        }


        //// Calculate random point in range
        //float randomZ = Random.Range(-walkPointRange, walkPointRange);
        //float randomX = Random.Range(-walkPointRange, walkPointRange);

        //walkPoint = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);
        //NavMeshHit hit;
        //if (NavMesh.Raycast(walkPoint, Vector3.up * -4, out hit, NavMesh.GetAreaFromName("Walkable")))
        //{
        //    agent.SetDestination(walkPoint);
        //}
    }
}