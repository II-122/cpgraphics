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

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    // Patroling 
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Start is called before the first frame update
    public void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for sight and attach range ( Collider 있어야만 작동 : Player 에 Collide 지우면 안돼요)
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();

    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // check with raycast if random point is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        // Make sure monster doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        
        if(!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}