using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsManager : MonoBehaviour
{
    private string[] deathKeys;
    private ParticleSystem[] deathVFX;

    [SerializeField] public static Dictionary<string, ParticleSystem> deathVFXs;

    private void Start()
    {
        deathVFXs = new Dictionary<string, ParticleSystem>();

        for (int i = 0; i <= Mathf.Min(deathKeys.Length, deathVFX.Length); i++)
        {
            if(deathVFX[0]) deathVFXs.Add(deathKeys[i], deathVFX[i]);
        }
    }
    public static void onDeath(GameObject gameObject)
    {
        ParticleSystem p = GameObject.Instantiate<ParticleSystem>(deathVFXs[gameObject.tag], gameObject.transform.position, Quaternion.identity);
        Destroy(p, p.main.duration);
        Debug.Log(p.name);
    }
}
