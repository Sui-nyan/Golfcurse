using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
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
        gui = GetComponent<GUIManager>();
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
            if (currentSceneIndex == 1)
            {
                Destroy(room);
            }
            else
            {
                yield return SceneManager.UnloadSceneAsync(currentSceneIndex);
            }

            gui.TransistionOut();
            yield return SceneManager.LoadSceneAsync(targetSceneIndex, LoadSceneMode.Additive);
        }

        if (!isLoading)
        {
            isLoading = true;
            StartCoroutine(ChangeScene(currentSceneIndex + 1));
        }
    }
}