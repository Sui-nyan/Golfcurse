using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    private PlayerCombat combat;

    [SerializeField] private float speed = 5f;
    
    Vector3 mousePos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        combat.ResetAttack();
        if(Input.GetMouseButtonDown(0))
        {
            combat.AttackAnimation();
        }
    }
    
    void FixedUpdate()
    {
        if(!combat.IsAttacking)
            PlayerMovement();

        LookToMouse();
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
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        Physics.Raycast(ray, out hit, Mathf.Infinity, mask);

        Vector3 angle = new Vector3(hit.point.x, 0, hit.point.z);
        Debug.DrawLine(transform.position, angle, color: Color.yellow);

        transform.LookAt(angle);
    }
}
