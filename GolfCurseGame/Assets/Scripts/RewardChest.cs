using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChest : MonoBehaviour
{
    private bool closed;


    /// <summary>
    /// Chest opens (animation)
    /// </summary>
    IEnumerator OpenChest()
    {
        yield return StartCoroutine(AnimateChestOpening());
    }

    IEnumerator AnimateChestOpening()
    {
        yield return null;
    }
}