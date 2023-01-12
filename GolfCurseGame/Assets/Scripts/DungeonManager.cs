using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    private GameObject[] enemies;
    [SerializeField] private GameObject player;
    [SerializeField] Camera playerCamera;
    private Door door;

    public float numberOfSpawns;
    private bool isRoomCleared;
    // Start is called before the first frame update


    private void Start()
    {
        door = GameObject.FindObjectOfType<Door>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    private void Update()
    {
        CheckEnemies();
        if (isRoomCleared)
        {
            OpenDoors();
        }
    }


    void OpenDoors()
    {
        if (door)
        {
            door.isPassable = true;
            foreach(Transform child in door.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void CheckEnemies()
    {
        foreach(GameObject go in enemies)
        {
            if (go)
            {
                isRoomCleared = true;
                break;
            }
        }
    }
}