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
    private GUIManager gui;

    private bool isRoomCleared;
    // Start is called before the first frame update


    private void Start()
    {
        door = FindObjectOfType<Door>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gui = GetComponent<GUIManager>();
    }

    private void FixedUpdate()
    {
        CheckEnemies();
        if (isRoomCleared) OpenDoors();


        if (door.enteredDoor)
            LoadNextDungeonRoom();
            
        door.enteredDoor = false;
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
                isRoomCleared = false;
                return;
            }
        }
        isRoomCleared = true;
    }

    public void LoadNextDungeonRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        //gui.TransistionOut();
    }
}