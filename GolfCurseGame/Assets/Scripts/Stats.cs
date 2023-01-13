using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField]
    private Healthbar healthbar;
    public Vector3 healthbarOffset = new Vector3(0, 30, 0);
    public float Health => health;
    public float Attack => attack;
    [SerializeField] private float health;
    [SerializeField] private float attack;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(!healthbar) 
            healthbar = Instantiate<Healthbar>(healthbar);
        healthbar.SetMaxSliderValue(health);
    }

    private void Update()
    {
        if(healthbar)
        healthbar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + healthbarOffset;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(healthbar) healthbar.SetHeath(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(healthbar.gameObject);
            VisualEffectsManager.OnDeath(gameObject);
        }
    }

    public void Knockback(float thrust)
    {
        rb.AddForce(Vector3.back * thrust, ForceMode.Impulse);
    }

}
