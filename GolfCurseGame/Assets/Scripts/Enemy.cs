using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public LayerMask playerMask;
    public NavMeshAgent agent;
    private GameObject player;

    private Animator animator;
    private Rigidbody rb;
    private Stats enemyStats;

    private float attackrange = 1;
    private float attackCooldown = 1f;
   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<Stats>();
    }

    private void Update()
    {
        animator.SetBool("Run", false);

        if(player)
            ChasePlayer();

        if(!enemyStats.isAlive)
        {
            enemyStats.onDying("Chick");
        }
    }

    /// <summary>
    /// chases the player, stops within attack range
    /// </summary>
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

    /// <summary>
    /// attacks if player is within range
    /// </summary>
    /// <param name="other">colliding collider</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                Stats playerStats = other.GetComponent<Stats>();
                Attack(playerStats);
            }
        }
    }

    /// <summary>
    /// attacks the player and resets the attack cooldown before initiating new attack
    /// </summary>
    /// <param name="player">player to be attacked</param>
    void Attack(Stats player)
    {
        animator.SetTrigger("Attack");

        player.TakeDamage(enemyStats.Attack);
        attackCooldown = 1f;
    }

    /// <summary>
    /// For debugging purposes draws the range
    /// within the chicken can attack
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackrange);
    }
}
