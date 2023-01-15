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
    private Spawner[] spawnPoints;
    private ChickenHead head;

    private bool isAttacking, hasHitPlayer;
    public bool canAttack;
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
        spawnPoints = FindObjectsOfType<Spawner>();
        head = GetComponentInChildren<ChickenHead>();

        MaxHealth = bossStats.Health;
    }

    private void Update()
    {
        if (canAttack)
        {
            int random = Random.Range(0, attackMoves.Length - 1);
            //time passes
            CoolDownTimer();

            if (attackCooldown <= 0)
            {
                TriggerAttack(random);
            }

            if (moveCooldown <= 0)
            {
                //only moves, when it is not attacking
                if (!CheckAnimationState("Run") && !CheckAnimationState("Eat"))
                {
                    Move();
                }
            }
            //starts spawning chicks when health drops below half
            if (bossStats.Health <= MaxHealth / 2 && currentSpawnCooldown <= 0)
            {
                SpawnChicks();
            }
        }
    }
    /// <summary>
    /// hecks whether certain animations are playing, it is used to check whether the boss is attacking or not
    /// </summary>
    /// <param name="animationname">the name for the animation to be checked</param>
    /// <returns></returns>
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
    /// <summary>
    /// triggers an attack based of the attack move array that is defined at the top
    /// </summary>
    /// <param name="attackIndex">the attack that is executed</param>
    void TriggerAttack(int attackIndex)
    {
        EnableHead();
        Debug.Log("ATTACKE");
        AttackMove attack = attackMoves[attackIndex];
        animator.SetTrigger(attack.animationTrigger);
        attackCooldown = attack.cooldown;

        if (hasHitPlayer)
        {
            hasHitPlayer = false;
            head.damage = attack.damageMultiplier * bossStats.Attack;
        }

        if (attack.dashAttack)
        {
            rb.AddForce(transform.forward * attack.velocity, ForceMode.VelocityChange);

            Debug.Log("force");
        }
    }
    /// <summary>
    /// moves the boss to a random position, after moving the chicken pauses for a random time
    /// </summary>
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
    /// <summary>
    /// takes in spawners in the room and spawns one chicken from each spawnpoint
    /// </summary>
    void SpawnChicks()
    {
        Debug.Log("Spawning Chicks");
        foreach (Spawner s in spawnPoints)
        {
            s.SpawnEnemy();
        }

        currentSpawnCooldown = spawnCooldown;
    }
    /// <summary>
    /// enables the head
    /// </summary>
    void EnableHead()
    {
        head.enabled = true;
    }
    /// <summary>
    /// disables the head
    /// </summary>
    void DisableHead()
    {
        head.enabled = false;
    }
    /// <summary>
    /// triggers an animation for when the boss scene is loaded
    /// this method is public because it is used in the dungeonmanager
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    public IEnumerator BossSceneAnimation(float delay)
    {
        animator.SetTrigger("TurnHead");
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
    /// <summary>
    /// counts cooldown down
    /// </summary>
    void CoolDownTimer()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            DisableHead();
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

    /// <summary>
    /// class for handling boss attack patterns
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
