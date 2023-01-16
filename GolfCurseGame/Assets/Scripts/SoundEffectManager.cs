using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public string[] deathKeys;
    public GameObject[] deathValues;
    private Dictionary<string, AudioSource> deathSounds;

    private void Start()
    {
        foreach(string s in deathKeys)
        {
            //Some mess to be impelemnted later
        }
    }

    public static void onDeath(GameObject gameObject)
    {
        //TODO
    }
}
