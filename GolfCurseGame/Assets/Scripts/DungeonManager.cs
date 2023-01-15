using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] RewardChest chest;

    private GameObject[] enemies;
    private NavMeshSurface navMesh;
    private GameObject room;
    private GUIManager gui;
    private int currentSceneIndex;
    private bool isLoading;
    private bool isBossRoom;
    private bool roomIsCleared;
    

    public event Action OnRoomCleared;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        navMesh = GetComponent<NavMeshSurface>();
    }


    private void Start()
    {
        gui = FindObjectOfType<GUIManager>();
        mainCamera = FindObjectOfType<PlayerCamera>().GetComponent<Camera>();
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Additive);
    }

    /// <summary>
    /// event added to SceneManager
    /// gets the room and enemies and resets parameters
    /// if the boss scene is active the camera moves to the boss and the boss plays an animation
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene {scene.name}");
        isLoading = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        room = GameObject.FindGameObjectWithTag("Room");
        navMesh.BuildNavMesh();
        roomIsCleared = false;
        currentSceneIndex = scene.buildIndex;
        Physics.SyncTransforms();
        
        if (scene.name.Equals("BossScene"))
        {
            isBossRoom = true;
            Debug.Log("BOSS ROOM");
            StartCoroutine(ChangeCameraTarget(FindObjectOfType<Boss>(), 2.5f));

            IEnumerator ChangeCameraTarget(Boss go, float delay)
            {
                var playerCam = mainCamera.GetComponent<PlayerCamera>();

                Debug.Log("new Camera Target");
                var prevTarget = playerCam.target;
                var prevSpeed = playerCam.cameraFollowSpeed;
                playerCam.target = go.gameObject;
                playerCam.cameraFollowSpeed = prevSpeed;
                playerCam.targetZoom = 6;
                playerCam.minTargetBound.z = -14;
                playerCam.maxTargetBound.z = 18;

                StartCoroutine(go.BossSceneAnimation(delay));
                yield return new WaitForSeconds(delay);
                Debug.Log("Revert camera target");

                playerCam.target = prevTarget;
                playerCam.cameraFollowSpeed = prevSpeed;
                playerCam.targetZoom = 4;
            }
        }

        if (gui)
        {
            gui.TransitionIn();
        }
    }
    /// <summary>
    /// Debug
    /// </summary>
    /// <param name="scene"></param>
    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log($"Unloaded scene {scene.name}");
    }

    private void Update()
    {
        if (!roomIsCleared && AllEnemiesDead())
        {
            Debug.Log("Room cleared");
            roomIsCleared = true;
            OnRoomCleared?.Invoke();
        }

        if(isBossRoom && AllEnemiesDead())
        {
            Debug.Log("Boss defeated");
            if(!chest)
                Instantiate(chest);
        }
    }
    /// <summary>
    /// checks if enemies exist in scene
    /// </summary>
    /// <returns>true when all enemy objects are destroyed</returns>
    bool AllEnemiesDead()
    {
        enemies =  GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go in enemies)
        {
            if (go)
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// loads and unloads the next scene
    /// </summary>
    public void LoadNextDungeonRoom()
    {
        IEnumerator ChangeScene(int targetSceneIndex)
        {
            gui.TransitionOut();
            yield return new WaitForSeconds(1f);
            Teleport(FindObjectOfType<Player>().gameObject);
            
            if (currentSceneIndex == 1)
            {
                Destroy(room);
            }
            else
            {
                yield return SceneManager.UnloadSceneAsync(currentSceneIndex);
            }

            yield return SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Additive);
        }

        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(ChangeScene(currentSceneIndex + 1));
        }
    }
    
    /// <summary>
    /// Resets player position 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    void Teleport(GameObject player)
    {
        Debug.Log("Teleporting..." + player.transform.position);
        player.transform.position = Vector3.zero;
    }
}