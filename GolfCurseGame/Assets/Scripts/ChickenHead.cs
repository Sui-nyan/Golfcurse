using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenHead : MonoBehaviour
{
    public Collider collider;
    public float damage = 10f;


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
