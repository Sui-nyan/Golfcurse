using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Health => health;
    public float Attack => attack;
    [SerializeField] private float health;
    [SerializeField] private float attack;
    [SerializeField] private ParticleSystem destroyVFX;
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
            try
            {
                Instantiate<ParticleSystem>(destroyVFX, transform.position, Quaternion.identity);
            } catch (NullReferenceException e)
            {
                Debug.LogError("You forgot to add destroyVFX..." + e);
            }
                
        }
    }

    public void Knockback(float thrust)
    {

        rb.AddForce(Vector3.back * thrust, ForceMode.Impulse);
    }

}
