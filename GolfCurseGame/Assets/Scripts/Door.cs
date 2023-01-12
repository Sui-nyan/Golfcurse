using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool isPassable;

    private void OnTriggerEnter(Collider other)
    {
        if (isPassable)
        {
            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
            }
          
            Debug.Log(other.transform.position);
            StartCoroutine(Teleport(other.gameObject));
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
