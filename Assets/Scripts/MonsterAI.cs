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
        if (30.0f < distance && distance <= detectDistance && item1_cnt == 0 && item2_cnt == 0) //������ ������� �ʾҰ�, ���Ͱ� �Ѿƿ� ��
        {
            playerInSight = true; //����� �߰�
            agent.SetDestination(player.transform.position); //����� ������ �ٰ���
            agent.speed = 75; //�޸��� �ӵ�
            animator.SetTrigger("Run"); //�޸��� �ִϸ��̼�
        }
        else if(30.0f < distance && distance <= detectDistance && item1_cnt > 0 && item2_cnt == 0) //���Ͱ� �Ѿƿ��� �߿� ������ 1�� ������� ��
        {
            playerInSight = false; //����� �̹߰�
            GotoNextPoint(); //��������� �ƴ� ��ǥ���� ������
            agent.speed = 75; //�޸��� �ӵ�
            animator.SetTrigger("Run"); //�޸��� �ִϸ��̼�
            item1_cnt--; //������1 ���ӽð� �پ��
        }
        else if (30.0f < distance && distance <= detectDistance && item1_cnt == 0 && item2_cnt > 0) //���Ͱ� �Ѿƿ��� �߿� ������ 2�� ������� ��
        {
            playerInSight = true; //����� �߰�
            agent.SetDestination(player.transform.position); //����� ������ �ٰ���
            agent.speed = 20; //�ȱ� �ӵ�
            animator.SetTrigger("Run"); //�޸��� �ִϸ��̼�
            item2_cnt--; //������1 ���ӽð� �پ��

        }
        else if (30.0f < distance && distance <= detectDistance && item1_cnt > 0 && item2_cnt > 0) //���Ͱ� �Ѿƿ��� �߿� ������1,2�� ��� ������� ��
        {
            playerInSight = false;//����� �̹߰�
            GotoNextPoint(); //��������� �ƴ� ��ǥ���� ������
            agent.speed = 20; //�ȱ� �ӵ�
            animator.SetTrigger("Walk"); //�ȴ� �ִϸ��̼�
            item1_cnt--; //������1 ���ӽð� �پ��
            item2_cnt--; //������1 ���ӽð� �پ��
        }
        // walk
        else // if (distance <= 30.0f || distance > lookRadius) ���Ͱ� �i�ƿ��� ���� ��
        {
            playerInSight = false; //����� �̹߰�
            GotoNextPoint(); //��������� �ƴ� ��ǥ���� ������
            agent.speed = 20; //�ȱ� �ӵ�
            animator.SetTrigger("Walk"); //�ȴ� �ִϸ��̼�

            //������ ����� �� �־��������� ������ ���ӽð� �پ�鵵��
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