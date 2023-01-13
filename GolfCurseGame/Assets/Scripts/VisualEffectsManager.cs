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
    public static void OnDeath(GameObject gameObject)
    {
        Debug.Log("Defeated " + gameObject.name);
        ParticleSystem p = GameObject.Instantiate<ParticleSystem>(deathVFXs[gameObject.tag], gameObject.transform.position, Quaternion.identity);
        Destroy(p, p.main.duration);
    }
}
