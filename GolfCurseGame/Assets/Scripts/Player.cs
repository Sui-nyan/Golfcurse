using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private PlayerCombat combat;
    private Camera playerCamera;
    [Tooltip("Movement is relative to camera angle")][SerializeField] private bool relativeMovement;

    [SerializeField] private float speed = 5f;

    Vector3 mousePos;
    private Vector3 direction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
        playerCamera = FindObjectOfType<PlayerCamera>()?.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(combat.TriggerAttack()) LookToMouse();
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        direction = new Vector3(x, 0, z);
        
        Vector3 TransformInputToCamera(Vector3 dir, Camera cam)
        {
            Quaternion rot = cam.transform.rotation;
            rot.x = 0;
            return  rot * dir;
        }
        if (relativeMovement) direction = TransformInputToCamera(direction, playerCamera);
    }
    
    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }

    /// <summary>
    /// handles player movement and walking animation
    /// </summary>
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

    /// <summary>
    /// player faces towards mouse cursor
    /// the mouse cursor position is raycast to the ground
    /// </summary>
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

    private void OnDestroy()
    {
        FindObjectOfType<GUIManager>().GameOver();
        playerCamera.enabled = false;
        Debug.Log("Player died");
    }
}