using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public LayerMask enemyMask;
    public Weapon weapon;

    [SerializeField] private float comboCooldown = 0.7f;
    private float lastClick = 0f;
    private int comboCount = 0;
    public bool IsAttacking => isAttacking;
    private bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public void ResetAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") || animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") || animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            isAttacking = true;
            EnableWeapon();
        }
        else
        {
            isAttacking = false;
            DisableWeapon();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            animator.SetBool("hit3", false);
            comboCount = 0;
        }

        if (Time.time - lastClick > comboCooldown)
        {
            comboCount = 0;
        }
    }
    public void AttackAnimation()
    {
        lastClick = Time.time;
        comboCount++;
        if (comboCount == 1)
        {
            animator.SetBool("hit1", true);
        }
        comboCount = Mathf.Clamp(comboCount, 0, 3);

        if (comboCount >= 2 && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", true);
        }

        if (comboCount >= 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
            animator.SetBool("hit3", true);
        }
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
