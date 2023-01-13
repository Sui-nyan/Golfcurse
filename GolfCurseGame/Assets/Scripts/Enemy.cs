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

    private float attackrange = 1;
    private float attackCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        stats = GetComponent<Stats>();
    }

    private void FixedUpdate()
    {
        animator.SetBool("Run", false);

        ChasePlayer();
    }

    void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance >= attackrange)
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
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attackCooldown -= Time.deltaTime;
            if(attackCooldown <= 0)
            {
                Stats playerStats = other.GetComponent<Stats>();
                Attack(playerStats);
            }
        }
    }

    void Attack(Stats player)
    {   
        animator.SetTrigger("Attack");
        
        player.TakeDamage(stats.Attack);
        attackCooldown = 1f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
