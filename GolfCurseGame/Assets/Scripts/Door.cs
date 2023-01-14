using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isPassable;
    public bool enteredDoor;

    
    /// <summary>
    /// teleports the player if in range
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (isPassable && !enteredDoor)
        {
            Debug.Log("Standing at door");
            if (other.CompareTag("Player"))
            {
                Debug.Log("Entering Door...");
                Teleport(other.gameObject);
                enteredDoor = true;
            }
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
