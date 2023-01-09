using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StatsTest
{
    [UnityTest]
    public IEnumerator ShouldReduceHealthOnDamage()
    {
        // Arrange
        GameObject go = new GameObject();
        Stats stats = go.AddComponent<Stats>();
        var healthBefore = stats.Health;

        // Act
        stats.TakeDamage(10);

        // Assert
        Assert.AreEqual(healthBefore - 10, stats.Health);
        yield return null;
    }
}