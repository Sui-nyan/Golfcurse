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
                Teleport(other.gameObject);
                FindObjectOfType<DungeonManager>().LoadNextDungeonRoom();
            }
        }
    }

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

    /// <summary>
    /// Resets player position 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    void Teleport(GameObject player)
    {
        Debug.Log("Teleporting..." + player.transform.position);

        player.TryGetComponent<Player>(out Player PlayerScript);

        player.transform.position = new Vector3(0, 0, 0);
        Physics.SyncTransforms();
    }
}