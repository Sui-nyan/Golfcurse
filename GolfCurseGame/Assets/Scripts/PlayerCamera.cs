using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 minTargetBound = new Vector3(-10, 0, -10);
    public Vector3 maxTargetBound = new Vector3(10, 0, 10);
    public float cameraFollowSpeed = 0.5f;
    public Vector3 cameraOffset = new Vector3(0, 0, -20);

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 playerPos = target.transform.position;
        Vector3 boundedPosition = new Vector3(Mathf.Clamp(playerPos.x, minTargetBound.x, maxTargetBound.x),
            playerPos.y, Mathf.Clamp(playerPos.z, minTargetBound.z, maxTargetBound.z));

        Transform cameraTransform = transform;
        Vector3 cameraPos = cameraTransform.position;
        var offset = cameraTransform.rotation * cameraOffset;
        float t = cameraFollowSpeed / Vector3.Distance(cameraPos, boundedPosition);
        transform.position = Vector3.Lerp(cameraPos, boundedPosition + offset, t);
    }

    private void OnDrawGizmos()
    {
        var center = (minTargetBound + maxTargetBound);
        var size = (minTargetBound - maxTargetBound);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z)));
    }
}