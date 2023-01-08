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
    public float range;

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

        if(distance >= 2f)
        {
            animator.SetBool("Run", true);
            agent.SetDestination(player.transform.position);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
