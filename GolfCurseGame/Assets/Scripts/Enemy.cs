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

    public float health = 10;
    public float range = 5f;
    private bool isInRange, isRunning;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
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
        print(distance);

        if(distance >= 2f)
        {
            animator.SetBool("Run", true);
            agent.SetDestination(player.transform.position);
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
