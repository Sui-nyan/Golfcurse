using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Rooms = new List<GameObject>();
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject player;
    private Spawner[] spawnPoints;
    private DoorBlocker[] blockers;

    public float numberOfSpawns;
    private bool isRoomCleared;
    // Start is called before the first frame update
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        Debug.Log("Loaded " + scene.name);
        spawnPoints = GameObject.FindObjectsOfType<Spawner>();
        if (spawnPoints.Length <= 0)
        {
            SpawnEnemies();
            Debug.Log("Enemies spawned");
        } 
    }

    private void Start()
    {
        blockers = GameObject.FindObjectsOfType<DoorBlocker>();
    }

    private void Update()
    {
        CheckEnemies();
        if (isRoomCleared)
        {
            OpenDoors();
        }
    }

    void SpawnEnemies()
    {
        GameObject spawnedEnemy;
        for (int i = 0; i < numberOfSpawns; i++)
        {
            int random = Random.Range(0, spawnPoints.Length - 1);
            spawnedEnemy = spawnPoints[random].SpawnEnemy();
            enemies.Add(spawnedEnemy);
        }
    }

    void OpenDoors()
    {
        foreach (DoorBlocker d in blockers)
        {
            if(d) Destroy(d);
        }
    }

    void CheckEnemies()
    {
        foreach(GameObject go in enemies)
        {
            if (go)
            {
                isRoomCleared = false;
                Debug.Log("Number of enemies left: " + enemies.Count);
            }
            else
            {
                enemies.Remove(go);
            }
        }

        if(enemies.Count <= 0)
        {

            isRoomCleared = true;
        }
    }

    void ReloadRoom()
    {

    }
}