using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Scene[] DungeonScenes;

    bool isNextBoss;
    public bool isPassable;

    private void OnTriggerEnter(Collider other)
    {
        if (isPassable)
        {
            if (other.CompareTag("Player") && !isNextBoss)
            {
                int i = Random.Range(0, DungeonScenes.Length - 1);
                //SceneManager.LoadScene(i, LoadSceneMode.Additive);
            }
            else
            {
                //SceneManager.LoadScene(DungeonScenes.Length, LoadSceneMode.Additive);
            }

            RepositionPlayer(other.gameObject);
            Debug.Log(other.transform.position);
            StartCoroutine(RepositionPlayer(other.gameObject));
        }
    }

    IEnumerator RepositionPlayer(GameObject player)
    {
        Debug.Log("Teleporting..." + player.transform.position);
        player.TryGetComponent<Player>(out Player PlayerScript);
        PlayerScript.enabled = false;
        yield return new WaitForSeconds(0.2f);
        player.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.2f);
        PlayerScript.enabled = true;
    }
}
