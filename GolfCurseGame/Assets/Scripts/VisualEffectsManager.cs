using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsManager : MonoBehaviour
{
    public string[] deathKeys, hitKeys;
    public ParticleSystem[] deathVFX, hitVFX;

    [SerializeField] private Dictionary<string, ParticleSystem> deathVFXs;
    [SerializeField] private Dictionary<string, ParticleSystem> hitVFXs;

    private void Start()
    {
        deathVFXs = new Dictionary<string, ParticleSystem>();

        for (int i = 0; i <= Mathf.Min(deathKeys.Length, deathVFX.Length); i++)
        {
            deathVFXs.Add(deathKeys[i], deathVFX[i]);
        }

        hitVFXs = new Dictionary<string, ParticleSystem>();

        for (int i = 0; i <= Mathf.Min(hitKeys.Length, hitVFX.Length); i++)
        {
            hitVFXs.Add(hitKeys[i], hitVFX[i]);
        }
    }

    public void onDeath(GameObject gameObject)
    {
        ParticleSystem p = GameObject.Instantiate<ParticleSystem>(deathVFXs[gameObject.tag], gameObject.transform.position, Quaternion.identity);
        Destroy(p, p.main.duration);
        Debug.Log(p.name);
    }

    public void onHit(GameObject gameObject)
    {
        ParticleSystem p = GameObject.Instantiate<ParticleSystem>(deathVFXs[gameObject.tag], gameObject.transform.position, Quaternion.identity);
        Destroy(p, p.main.duration);
    }
}
