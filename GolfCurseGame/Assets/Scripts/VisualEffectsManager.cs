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
        switch (gameObject.tag)
        {
            case "Player": GameObject.Instantiate<ParticleSystem>(deathVFXs["player"], gameObject.transform.position, Quaternion.identity);
                break;
            case "Enemy":
                GameObject.Instantiate<ParticleSystem>(deathVFXs["enemy"], gameObject.transform.position, Quaternion.identity);
                break;
            case "Boss":
                GameObject.Instantiate<ParticleSystem>(deathVFXs["boss"], gameObject.transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("no death VFX found");
                break;
        }
    }

    public void onHit(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Player":
                GameObject.Instantiate<ParticleSystem>(deathVFXs["player"], gameObject.transform.position, Quaternion.identity);
                break;
            case "Enemy":
                GameObject.Instantiate<ParticleSystem>(deathVFXs["enemy"], gameObject.transform.position, Quaternion.identity);
                break;
            case "Boss":
                GameObject.Instantiate<ParticleSystem>(deathVFXs["boss"], gameObject.transform.position, Quaternion.identity);
                break;
            default:
                Debug.Log("no hit VFX found");
                break;
        }
    }
}
