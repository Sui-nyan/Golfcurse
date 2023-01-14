using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] Camera playerCamera;

    private GameObject[] enemies;
    
    private Door door;
    private GameObject room;
    private GUIManager gui;

    private bool isRoomCleared;
    // Start is called before the first frame update


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        door = FindObjectOfType<Door>();    
        gui = GetComponent<GUIManager>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        room = GameObject.FindGameObjectWithTag("Room");
    }

    private void Update()
    {
        CheckEnemies();
        if (isRoomCleared)
        {
            OpenDoors();
        }

        if (door.enteredDoor)
        {
            door.enteredDoor = false;
            LoadNextDungeonRoom();
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
                isRoomCleared = false;
                return;
            }
        }
        isRoomCleared = true;
    }

    public void LoadNextDungeonRoom()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex == 1)
            Destroy(room);

        else SceneManager.UnloadScene(sceneIndex);
        isRoomCleared = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        gui.TransistionOut();
    }
}