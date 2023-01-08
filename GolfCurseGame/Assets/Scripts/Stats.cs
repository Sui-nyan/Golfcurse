using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Health => Health;
    private float health;
    

    public float attack;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Knockback(float thrust)
    {

        rb.AddForce(Vector3.back * thrust, ForceMode.Impulse);
    }
}
