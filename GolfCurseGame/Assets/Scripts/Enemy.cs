using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask playerMask;
    public NavMeshAgent agent;
    private GameObject player;
    private Animator animator;
    private Rigidbody rb;
    private Stats stats;
    public float range, attackrange = 1;

    private bool isInRange, isRunning, isInAttackRange;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<Stats>();
    }

    private void Update()
    {
        isInRange = Physics.CheckSphere(transform.position, range, playerMask);
        animator.SetBool("Run", false);

        if (isInRange)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if(distance >= attackrange)
        {
            animator.SetBool("Run", true);
            agent.SetDestination(player.transform.position);
        } 
        else
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
                rb.velocity = Vector3.zero;
            }
            
        }   
    }

    void Attack()
    {
        isInAttackRange = Physics.CheckSphere(transform.position, attackrange, playerMask);
        if (isInAttackRange)
        {
            animator.SetTrigger("Attack");
            Stats playerStats = player.GetComponent<Stats>();
            playerStats.TakeDamage(stats.attack);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
