using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public LayerMask enemyMask;
    public Weapon weapon;
    
    private float comboTimer = 0;
    private int attackIndex = 0; // attackIndex

    [SerializeField] private Attack[] attacks = new Attack[]
    {
        new Attack("h1", 0.4f, 1.2f, 1),
        new Attack("h2", 0.4f, 1f, 1.5f),
        new Attack("h3", 1f, 1f, 5f)
    };

    [SerializeField] private bool isAnimationLocked = false;
    public bool IsAttacking => attackIndex != -1;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
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
            DisableWeapon();
        }
    }


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
            weapon.damage = attack.damage;
            EnableWeapon();
            Debug.Log("Attack" + attackIndex);
            return true;
        }

        return false;
    }

    void EnableWeapon()
    {
        weapon.enabled = true;
    }

    void DisableWeapon()
    {
        weapon.enabled = false;
    }
}

    
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
    public float damage;

    public Attack(string triggerName, float minDelay, float maxDelay, float damage)
    {
        this.triggerName = triggerName;
        this.minDelay = minDelay;
        this.maxDelay = maxDelay;
        this.damage = damage;
    }
}