using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BoxCollider collider;
    public float damage = 10f;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }
    /// <summary>
    /// collider deactivates so trigger is disabled
    /// </summary>
    private void OnDisable()
    {
        collider.enabled = false;
    }
    /// <summary>
    /// collider activates so trigger is enabled
    /// </summary>
    private void OnEnable()
    {
        collider.enabled = true;
    }

    /// <summary>
    /// damages if other collider is an enemy
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DamageEnemy(other.gameObject);
        }
    }

    /// <summary>
    /// damages object
    /// </summary>
    /// <param name="enemy">object to be damages</param>
    void DamageEnemy(GameObject enemy)
    {
        Stats stats = enemy.GetComponent<Stats>();
        if (stats)
        {
            stats.TakeDamage(damage);
        }
    }
}
