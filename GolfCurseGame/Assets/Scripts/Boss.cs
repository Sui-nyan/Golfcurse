using UnityEngine;

public class Boss : MonoBehaviour
{
    private Stats player;
    private Stats bossStats;
    private Animator animator;
    private Rigidbody rb;

    [SerializeField]
    private AttackMove[] attackMoves = new AttackMove[]
    {
        //new AttackMove("TurnHead", 1f, 5f),
        //new AttackMove("Eat", 0.5f, 3f),
        new AttackMove("Run", 1.5f, 8f, true, 5f)
    };

    bool isAttacking;
    bool hasHitPlayer;
    float attackCooldown = 0;
    public bool triggerdash;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        bossStats = GetComponent<Stats>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, 5);
    }

    private void Update()
    {
        int random = Random.Range(0, attackMoves.Length - 1);

        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0)
        {
            TriggerAttack(random);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && isAttacking)
            hasHitPlayer = true;
    }

    void TriggerAttack(int attackIndex)
    {
        isAttacking = true;
        AttackMove attack = attackMoves[attackIndex];
        animator.SetTrigger(attack.animationTrigger);
        attackCooldown = attack.cooldown;

        if (hasHitPlayer)
        {
            hasHitPlayer = false;
            if(!player)
            player.TakeDamage(attack.damageMultiplier * bossStats.Attack);
        }

        if (attack.dashAttack || triggerdash)
        {
            triggerdash = false;
            rb.velocity = transform.forward * attack.velocity;

            Debug.Log("force");
        }
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
