using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Serialization;

public class RewardChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;
    public event Action OnChestOpened;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //Placeholder
        if (Input.GetKeyDown(KeyCode.F1))
        {
            StartCoroutine(Open());
        }
    }
    
    /// <summary>
    /// Chest opens (animation)
    /// </summary>
    IEnumerator Open()
    {
        if (isOpen)
        {
            yield break;
        }
        
        // Change state
        isOpen = true;
        
        // Animation
        yield return StartCoroutine(AnimateOpen());

        // Animation Done
        OnChestOpened?.Invoke();
        yield return null;
    }
    
    private IEnumerator AnimateOpen()
    {
        animator.SetTrigger("Open");
        var state = animator.GetCurrentAnimatorStateInfo(0);
        while (!state.IsName("Open") && state.normalizedTime < 1)
        {
            yield return null;
        }
    }
    
    private IEnumerator AnimateClose()
    {
        animator.SetTrigger("Close");
        var state = animator.GetCurrentAnimatorStateInfo(0);
        while (!state.IsName("Idle"))
        {
            yield return null;
        }
    }


}