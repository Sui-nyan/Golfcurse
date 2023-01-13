using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private Healthbar healthbar;
    public float Health => health;
    public float Attack => attack;
    [SerializeField] private float health;
    [SerializeField] private float attack;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(healthbar) healthbar.SetMaxSliderValue(health);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(healthbar) healthbar.SetHeath(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            VisualEffectsManager.onDeath(gameObject);
        }
    }

    public void Knockback(float thrust)
    {
        rb.AddForce(Vector3.back * thrust, ForceMode.Impulse);
    }

}
