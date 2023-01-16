using UnityEngine;

/// <summary>
/// functions similarly to the weapon script
/// </summary>
public class ChickenHead : MonoBehaviour
{
    public Collider collider;
    public float damage;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void OnDisable()
    {
        collider.enabled = false;
    }

    private void OnEnable()
    {
        collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DamageEnemy(other.gameObject);
        }
    }

    void DamageEnemy(GameObject enemy)
    {
        Stats stats = enemy.GetComponent<Stats>();
        if (stats)
        {
            stats.TakeDamage(damage);
        }
    }
}
