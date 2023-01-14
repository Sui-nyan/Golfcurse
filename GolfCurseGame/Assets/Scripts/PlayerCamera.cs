using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Vector3 maxCameraBound;
    public Vector3 minCameraBound;
    public GameObject player;
    private float speed = 0.1f;
    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 targetPos = playerposition - offset;
        float distance = Vector3.Distance(transform.position, targetPos);

        bool isWithinBounds = targetPos.x <= maxCameraBound.x && targetPos.x >= minCameraBound.x && targetPos.z <= maxCameraBound.z && targetPos.z >= minCameraBound.z;
        
        if (isWithinBounds)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, speed/distance);
        }
    }
}
