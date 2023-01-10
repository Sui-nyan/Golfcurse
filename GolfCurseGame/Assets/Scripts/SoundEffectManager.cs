using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public string[] deathKeys, hitKeys;
    public GameObject[] deathValues, hitValues;
    private Dictionary<string, ParticleSystem> deathSounds;
    private Dictionary<string, GameObject> hitSounds;

    private void Start()
    {
        foreach(string s in deathKeys)
        {
            //Some mess to be impelemnted later
        }
    }

    public void onDeath(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Player":
                //TODO
                break;
            case "Enemy":
                //TODO
                break;
            case "Boss":
                //TODO
                break;
            default:
                Debug.Log("no death sound found");
                break;
        }
    }

    public void onHit(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "Player":
                //TODO
                break;
            case "Enemy":
                //TODO
                break;
            case "Boss":
                //TODO
                break;
            default:
                Debug.Log("no hit sound found");
                break;
        }
    }
}
