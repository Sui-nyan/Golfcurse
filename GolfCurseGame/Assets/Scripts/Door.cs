using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private int[] DungeonScenes;
    [SerializeField]
    private int BossScene;

    public bool isNextBoss;
    public bool isPassable;
    bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isPassable)
        {
            if (other.CompareTag("Player") && !isNextBoss)
            {
                int i = Random.Range(0, DungeonScenes.Length - 1);
                SceneManager.LoadScene(DungeonScenes[i], LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(BossScene, LoadSceneMode.Additive);
            }

            Teleport(other.gameObject);
            Debug.Log(other.transform.position);
            StartCoroutine(Teleport(other.gameObject));
        }
    }

    IEnumerator Teleport(GameObject player)
    {
        if(canTeleport)
        {
            Debug.Log("Teleporting..." + player.transform.position);
            player.TryGetComponent<Player>(out Player PlayerScript);
            PlayerScript.enabled = false;
            yield return new WaitForSeconds(0.2f);
            player.transform.position = new Vector3(0, 0, 10);
            yield return new WaitForSeconds(0.2f);
            PlayerScript.enabled = true;
        }
        canTeleport = false;
    }
}
