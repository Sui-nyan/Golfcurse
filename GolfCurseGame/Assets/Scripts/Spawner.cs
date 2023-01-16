using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    Enemy enemyPrefab;

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}
