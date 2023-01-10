using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Scene[] DungeonScenes;

    bool isNextBoss;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isNextBoss)
        {
            int i = Random.Range(0, DungeonScenes.Length - 1);
            SceneManager.LoadScene(i, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(DungeonScenes.Length, LoadSceneMode.Additive);
        }

        RepositionPlayer(other.gameObject);
    }

    private void RepositionPlayer(GameObject player)
    {
        player.transform.position = Vector3.zero; //TODO
    }
}
