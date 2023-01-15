using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    private Healthbar healthbar;

    [SerializeField] private Vector3 healthbarOffset = new Vector3(0, 30, 0);
    public float Health => health;
    public float Attack => attack;
    [SerializeField] private float health;
    [SerializeField] private float attack;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // If there's no healthbar
        if (!healthbar)
        {
            var prefab = Resources.Load<Healthbar>("EnemyHealthbar");
            var canvas = FindObjectOfType<Canvas>();
            healthbar = Instantiate<Healthbar>(prefab, canvas.transform);
        }

        healthbar.SetMaxSliderValue(health);
    }

    private void Update()
    {
        //healthbar follows the game object
        if (healthbar && Camera.main)
        {
            healthbar.transform.position = Camera.main.WorldToScreenPoint(transform.position) + healthbarOffset;
        }
    }

    /// <summary>
    /// handles health and what happens when health drops below 0
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (healthbar) healthbar.SetHeath(health);

        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(healthbar.gameObject);
            VisualEffectsManager.OnDeath(gameObject);
        }
    }
}