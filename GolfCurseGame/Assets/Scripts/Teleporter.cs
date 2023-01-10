using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private Scene[] DungeonScenes;

    private Scene BossScene;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int i = Random.Range(0, DungeonScenes.Length - 1);
            SceneManager.LoadScene(i);
        }
    }
}
