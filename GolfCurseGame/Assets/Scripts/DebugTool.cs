using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTool : MonoBehaviour
{
    [Range(0, 5)]
    public float timeScale = 1;

    [Min(0)] public float damageOverride = -1;
    

    void Update()
    {
        Time.timeScale = timeScale;

        if (damageOverride >= 0)
        {
            foreach (var weapon in FindObjectsOfType<Weapon>())
            {
                weapon.damage = damageOverride;
            }
        }
        
    }
}
