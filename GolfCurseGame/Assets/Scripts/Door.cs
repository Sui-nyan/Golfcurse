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
    private void OnTriggerStay(Collider other)
    {
        if (isPassable)
        {
            Debug.Log("Standing at door");
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Entering Door...");
                StartCoroutine(Teleport(other.gameObject));
                enteredDoor = true;
            }
        }
    }


    /// <summary>
    /// Resets player position 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    IEnumerator Teleport(GameObject player)
    {
        Debug.Log("Teleporting..." + player.transform.position);

        player.TryGetComponent<Player>(out Player PlayerScript);

        PlayerScript.enabled = false;

        yield return new WaitForSeconds(0.2f);
        player.transform.position = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.2f);

        PlayerScript.enabled = true;
    }
}
