using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public LayerMask playerMask;
    public NavMeshAgent agent;
    private GameObject player;

    [SerializeField] private ParticleSystem hitVFX;
    private Animator animator;
    private Rigidbody rb;
    private Stats stats;
    public float range, attackrange = 1;

    private bool isInRange;
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
    IEnumerator Attack(Stats player)
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetTrigger("Attack");
        player.TakeDamage(stats.Attack);
        yield return new WaitForSeconds(0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");
        Stats playerStats = other.GetComponent<Stats>();
        StartCoroutine(Attack(playerStats));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
