using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isPassable;

    void Start()
    {
        FindObjectOfType<DungeonManager>().OnRoomCleared += OpenTheGates;
    }

    private void OnDestroy()
    {
        FindObjectOfType<DungeonManager>().OnRoomCleared -= OpenTheGates;
    }

    /// <summary>
    /// teleports the player if in range
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (isPassable)
        {
            Debug.Log("Standing at door");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Entering Door...");
                FindObjectOfType<DungeonManager>().LoadNextDungeonRoom();
            }
        }
    }

    /// <summary>
    /// destroys the gate so the door is passable
    /// </summary>
    void OpenTheGates()
    {
        Debug.Log("Door opened");
        Debug.Log(this);
        isPassable = true;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}