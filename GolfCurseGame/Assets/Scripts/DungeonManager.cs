using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] Camera mainCamera;

    private GameObject[] enemies;

    private GameObject room;
    private GUIManager gui;
    private int currentSceneIndex;
    private bool isLoading;

    private bool roomIsCleared;
    // Start is called before the first frame update

    public event Action OnRoomCleared;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }


    private void Start()
    {
        gui = FindObjectOfType<GUIManager>();
        mainCamera = FindObjectOfType<PlayerCamera>().GetComponent<Camera>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Loaded scene {scene.name}");
        isLoading = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        room = GameObject.FindGameObjectWithTag("Room");
        roomIsCleared = false;
        currentSceneIndex = scene.buildIndex;
        Physics.SyncTransforms();
        
        if (scene.name.Equals("BossScene"))
        {
            Debug.Log("BOSS ROOM");
            StartCoroutine(ChangeCameraTarget(FindObjectOfType<Boss>().gameObject, 2.5f));

            IEnumerator ChangeCameraTarget(GameObject go, float delay)
            {
                var playerCam = mainCamera.GetComponent<PlayerCamera>();

                Debug.Log("new Camera Target");
                var prevTarget = playerCam.target;
                var prevSpeed = playerCam.cameraFollowSpeed;
                playerCam.target = go;
                playerCam.cameraFollowSpeed = prevSpeed;
                playerCam.targetZoom = 6;

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
    }

    bool AllEnemiesDead()
    {
        foreach (GameObject go in enemies)
        {
            if (go)
            {
                return false;
            }
        }

        return true;
    }

    public void LoadNextDungeonRoom()
    {
        IEnumerator ChangeScene(int targetSceneIndex)
        {
            gui.TransitionOut();
            yield return new WaitForSeconds(1);
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