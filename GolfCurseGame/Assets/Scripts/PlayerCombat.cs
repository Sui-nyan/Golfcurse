using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public LayerMask enemyMask;
    private Stats stats;
    public Weapon weapon;
    
    private float comboTimer = 0;
    private int attackIndex = 0; // attackIndex

    [SerializeField] private Attack[] attacks = new Attack[]
    {
        new Attack("h1", 0.4f, 1.2f, 1),
        new Attack("h2", 0.4f, 1f, 1.5f),
        new Attack("h3", 1f, 1f, 2f)
    };

    [SerializeField] private bool isAnimationLocked = false;
    public bool IsAttacking => attackIndex != -1;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
        stats = GetComponent<Stats>();
    }

    private void Update()
    {
        // time passes
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }

        // Check animation lock
        if (0 <= attackIndex && attackIndex < attacks.Length)
        {
            var currentAttack = attacks[attackIndex];
            var timePassed = currentAttack.maxDelay - comboTimer;
            if (timePassed > currentAttack.minDelay)
            {
                isAnimationLocked = false;
            }
        }

        // Check combo timeout
        if (comboTimer <= 0)
        {
            attackIndex = -1;
            foreach (var atk in attacks)
            {
                animator.ResetTrigger(atk.triggerName);
            }
            if(weapon != null)
                DisableWeapon();
        }

        if (!stats.isAlive)
        {
            stats.onDying("Player");
        }
    }
    /// <summary>
    /// triggers attacks based on animation and current attackindex
    /// </summary>
    /// <returns>whether an attack is currently triggered</returns>
    public bool TriggerAttack()
    {
        if (isAnimationLocked) return false;
        
        if (attackIndex == -1 || attackIndex < attacks.Length - 1)
        {
            attackIndex = Mathf.Clamp(attackIndex + 1, 0, attacks.Length - 1);
            var attack = attacks[attackIndex];
            comboTimer = attack.maxDelay;
            isAnimationLocked = true;
            animator.SetTrigger(attack.triggerName);
            weapon.damage = attack.damageMultiplier * stats.Attack;

            if(weapon != null)
                EnableWeapon();

            Debug.Log("Attack" + attackIndex);
            return true;
        }

        return false;
    }
    /// <summary>
    /// enables weapon
    /// </summary>
    void EnableWeapon()
    {
        weapon.enabled = true;
    }
    /// <summary>
    /// disables weapon
    /// </summary>
    void DisableWeapon()
    {
        weapon.enabled = false;
    }
}

/// <summary>
/// class for handling attack
/// </summary>
[Serializable]
public class Attack
{
    // parameter name in animation controller
    public string triggerName;
    // min delay between attacks, isAnimationLocked
    public float minDelay;
    // max delay between attacks, determines IsAttacking (cannot run)
    public float maxDelay;
    // DAMAGE DEALT
    public float damageMultiplier;

    public Attack(string triggerName, float minDelay, float maxDelay, float damage)
    {
        this.triggerName = triggerName;
        this.minDelay = minDelay;
        this.maxDelay = maxDelay;
        this.damageMultiplier = damage;
    }
}