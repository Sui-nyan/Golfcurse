using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsManager : MonoBehaviour
{
    [SerializeField] private string[] deathKeys;
    [SerializeField] private ParticleSystem[] deathVFX;

     public static Dictionary<string, ParticleSystem> deathVFXs;

    private void Start()
    {
        deathVFXs = new Dictionary<string, ParticleSystem>();

        for (int i = 0; i <= Mathf.Min(deathKeys.Length - 1 , deathVFX.Length - 1); i++)
        {
            if(deathVFX[i]) deathVFXs.Add(deathKeys[i], deathVFX[i]);
        }
    }

    /// <summary>
    /// handles vfx for defeated objects
    /// </summary>
    /// <param name="gameObject"></param>
    public static void OnDeath(GameObject gameObject)
    {
        Debug.Log("Defeated " + gameObject.name);
        var vfx = deathVFXs[gameObject.tag];
        ParticleSystem p = Instantiate<ParticleSystem>(vfx, gameObject.transform.position, Quaternion.identity);
        Destroy(p, p.main.duration);
    }
}
