using UnityEngine;

public class Boss : MonoBehaviour
{
    private Player player;
    private Stats bossStats;
    private Animator animator;

    [SerializeField]
    private AttackMove[] attackMoves;
    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        bossStats = GetComponent<Stats>();
        animator = GetComponent<Animator>();

        StartSceneAnimation();
    }

    private void Update()
    {
        int random = Random.Range(0, attackMoves.Length - 1);
        TriggerAttack(random);
    }

    /// <summary>
    /// Sets animation that plays, when player encounters boss
    /// </summary>
    void StartSceneAnimation()
    {
        animator.SetTrigger("TurnHead");
    }

    void TriggerAttack(int attackIndex)
    {
        isAttacking = true;
    }


    /// <summary>
    /// struct for handling boss attack patterns
    /// </summary>
    public class AttackMove
    {
        //Corresponding attack animation
        string animationTrigger;
        //Attack damage dealt
        float damage;
        //cooldown after next attack occurs
        float cooldown;

        public AttackMove(string animationTrigger, float damage, float cooldown)
        {
            this.animationTrigger = animationTrigger;
            this.damage = damage;
            this.cooldown = cooldown;
        }
    }
}
