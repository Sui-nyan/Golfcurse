using System;
using System.Collections;
using UnityEngine;

public class RewardChest : MonoBehaviour
{
    private Animator animator;
    private bool isOpen;
    public event Action OnChestOpened;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            StartCoroutine(Open());
    }

    /// <summary>
    /// Chest opens (animation)
    /// </summary>
    public IEnumerator Open()
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