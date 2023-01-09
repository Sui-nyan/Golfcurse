using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public BoxCollider collider;
    public float damage = 10f;
    public float thrust = 5f;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
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
        if (other.CompareTag("Enemy"))
        {
            DamageEnemy(other.gameObject);
        }
    }

    void DamageEnemy(GameObject enemy)
    {
        Stats stats = enemy.GetComponent<Stats>();
        stats.TakeDamage(damage);
        stats.Knockback(thrust);
    }
}
