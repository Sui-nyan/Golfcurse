using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private PlayerCombat combat;

    [SerializeField] private float speed = 5f;

    Vector3 mousePos;
    private Vector3 direction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(combat.TriggerAttack()) LookToMouse();
        }
    }

    void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = new Vector3(x, 0, z);
    }

    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    void HandlePlayerMovement()
    {
        if (combat.IsAttacking || direction == Vector3.zero)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            controller.Move(direction.normalized * (speed * Time.fixedDeltaTime));
            animator.SetBool("isWalking", true);
            transform.LookAt(transform.position + direction);
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