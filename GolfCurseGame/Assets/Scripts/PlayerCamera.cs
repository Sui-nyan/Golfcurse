using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 minTargetBound = new Vector3(-10, 0, -10);
    public Vector3 maxTargetBound = new Vector3(10, 0, 10);
    [Range(0f, 1f)] public float cameraFollowSpeed = 0.5f;
    public Vector3 cameraOffset = new Vector3(0, 0, -20);
    [Range(1f, 30f)] public float targetZoom;

    private Camera _camera;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        _camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if(target)
            FollowPlayer();
    }
    /// <summary>
    /// faces and follows the player based of an offset
    /// </summary>
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

        if (targetZoom >= 1)
        {
            var currentZoom = _camera.orthographicSize;
            var zoomDiff = Mathf.Max(Mathf.Abs(targetZoom - currentZoom), 1);
            _camera.orthographicSize = Mathf.Lerp(currentZoom, targetZoom, cameraFollowSpeed / zoomDiff / 5);
        }
    }
    /// <summary>
    /// tool for debuging draws the boundary of the camera position
    /// </summary>
    private void OnDrawGizmos()
    {
        var center = (minTargetBound + maxTargetBound);
        var size = (minTargetBound - maxTargetBound);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z)));
    }
}