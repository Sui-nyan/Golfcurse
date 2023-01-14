using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField]
    Vector3 minBoundary;

    [SerializeField]
    Vector3 maxBoundary;

    [SerializeField]
    private AttackMove[] attackMoves = new AttackMove[]
    {
        new AttackMove("Eat", 0.5f, 6f),
        new AttackMove("Run", 1.5f, 8f, true, 5f)
    };

    private Stats player, bossStats;
    private Animator animator;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Spawner[] chickenNest;

    private bool isAttacking, hasHitPlayer;
    private float attackCooldown = 0, moveCooldown = 0.1f, maxMovementCooldown = 2f, spawnCooldown = 10f, currentSpawnCooldown;
    float MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        bossStats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        chickenNest = FindObjectsOfType<Spawner>();

        MaxHealth = bossStats.Health;
    }

    private void Update()
    {
        int random = Random.Range(0, attackMoves.Length - 1);

        Timer();

        if (attackCooldown <= 0)
        {
            TriggerAttack(random);
        }

        if(moveCooldown <= 0)
        {
            if (!CheckAnimationState("Run") && !CheckAnimationState("Eat"))
            {
                Move();
            }
        }

        if(bossStats.Health <= MaxHealth / 2 && currentSpawnCooldown <= 0)
        {
            SpawnChicks();
        }
    }

    bool CheckAnimationState(string animationname) 
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName(animationname)) {
            Debug.Log("Playing: " + animationname);
            return true;
        } 
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isAttacking)
            hasHitPlayer = true;
    }

    void TriggerAttack(int attackIndex)
    {
        Debug.Log("ATTACKE");
        AttackMove attack = attackMoves[attackIndex];
        animator.SetTrigger(attack.animationTrigger);
        attackCooldown = attack.cooldown;

        if (hasHitPlayer)
        {
            hasHitPlayer = false;
            if(!player)
            player.TakeDamage(attack.damageMultiplier * bossStats.Attack);
        }

        if (attack.dashAttack)
        {
            rb.AddForce(transform.forward * attack.velocity, ForceMode.VelocityChange);

            Debug.Log("force");
        }
    }

    void Move()
    {
        Debug.Log("Moving");
        float x = Random.Range(minBoundary.x, maxBoundary.x);
        float z = Random.Range(minBoundary.z, maxBoundary.z);
        Vector3 direction = new Vector3(x,0,z);

        Vector3 newPos = transform.position + direction;

        if(newPos.x >= minBoundary.x && newPos.z >= minBoundary.z && newPos.x <= maxBoundary.x && newPos.z <= maxBoundary.z)
        {
            animator.SetBool("isWalking", true);
            agent.SetDestination(newPos);
            
        }

        moveCooldown = Random.Range(1f,maxMovementCooldown);
        StartCoroutine(endMovement());

        IEnumerator endMovement()
        {
            yield return new WaitForSeconds(moveCooldown);
            agent.ResetPath();
            animator.SetBool("isWalking", false);
        }
    }

    void Timer()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (moveCooldown > 0)
        {
            moveCooldown -= Time.deltaTime;
        }

        if(currentSpawnCooldown > 0 && bossStats.Health <= MaxHealth/2)
        {
            currentSpawnCooldown -= Time.deltaTime;
        }
    }

    void SpawnChicks()
    {
        Debug.Log("Spawning Chicks");
        foreach(Spawner s in chickenNest)
        {
            s.SpawnEnemy();
        }

        currentSpawnCooldown = spawnCooldown;
    }
    /// <summary>
    /// struct for handling boss attack patterns
    /// </summary>
    public class AttackMove
    {
        //Corresponding attack animation
        public string animationTrigger;
        //Attack damage dealt
        public float damageMultiplier;
        //cooldown after next attack occurs
        public float cooldown;
        //to dash or not to dash
        public bool dashAttack;
        public float velocity;

        public AttackMove(string animationTrigger, float damage, float cooldown)
        {
            this.animationTrigger = animationTrigger;
            this.damageMultiplier = damage;
            this.cooldown = cooldown;
        }

        public AttackMove(string animationTrigger, float damage, float cooldown, bool dashAttack, float velocity)
        {
            this.animationTrigger = animationTrigger;
            this.damageMultiplier = damage;
            this.cooldown = cooldown;
            this.dashAttack = dashAttack;
            this.velocity = velocity;
        }
    }
}
