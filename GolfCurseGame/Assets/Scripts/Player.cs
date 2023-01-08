using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float comboCooldown = 0.7f;
    private bool isWalking;
    private bool isAttacking;
    private float lastClick = 0f;
    private int comboCount = 0;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isWalking = animator.GetBool("isWalking");
        if(!isAttacking)
            PlayerMovement();

        LookToMouse();
        ResetAttack();
        if(Input.GetMouseButtonDown(0))
            Attack();

    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(x, 0, z);
        controller.Move(direction.normalized * speed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void LookToMouse()
    {
        mousePos = Input.mousePosition;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

        Vector3 angle = new Vector3(hit.point.x, 0, hit.point.z);
        Debug.DrawLine(transform.position, angle, color: Color.yellow);

        transform.LookAt(angle);
    }

    private void ResetAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") || animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") || animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            animator.SetBool("hit3", false);
            comboCount = 0;
        }

        if (Time.time - lastClick > comboCooldown)
        {
            comboCount = 0;
        }
    }

    void Attack()
    {
        lastClick = Time.time;
        comboCount++;
        if (comboCount == 1)
        {
           animator.SetBool("hit1", true); 
        }
        comboCount = Mathf.Clamp(comboCount, 0, 3);

        if (comboCount >= 2 && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", true);
        }

        if(comboCount >= 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
            animator.SetBool("hit3", true);
        }
    }
}
