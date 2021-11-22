using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * 3 STATES MONSTER
 *  patrolling when the player is far away
 *  chasing when the player gets inside range
 *  attacking as soon as the player enters attack range
 */

public class MonsterAI : MonoBehaviour
{
    public float lookRadius = 10f;

    public NavMeshAgent agent;
    public Transform player;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        //if(distance <= lookRadius)
        //{
            agent.SetDestination(player.transform.position);
        //}
    }

}