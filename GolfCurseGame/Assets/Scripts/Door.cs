using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isPassable;
    public bool enteredDoor;

    private void OnTriggerStay(Collider other)
    {
        if (isPassable)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Entering Door...");
                StartCoroutine(Teleport(other.gameObject));
                enteredDoor = true;
            }
        }
    }

    IEnumerator Teleport(GameObject player)
    {
        Debug.Log("Teleporting..." + player.transform.position);

        player.TryGetComponent<Player>(out Player PlayerScript);

        PlayerScript.enabled = false;

        yield return new WaitForSeconds(0.2f);
        player.transform.position = new Vector3(0, 0, 10);
        yield return new WaitForSeconds(0.2f);

        PlayerScript.enabled = true;
    }
}
