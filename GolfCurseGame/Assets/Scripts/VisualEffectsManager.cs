using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualEffectsManager : MonoBehaviour
{
    public SFXEffect[] effects;

    /// <summary>
    /// handles vfx for defeated objects
    /// </summary>
    /// <param name="gameObject"></param>
    public void OnDeath(GameObject gameObject)
    {
        Debug.Log("Defeated " + gameObject.name);
        SFXEffect vfx = Array.Find(effects, effect => effect.name == gameObject.name);
        if (vfx != null)
        {
            ParticleSystem p = Instantiate<ParticleSystem>(vfx.effect, gameObject.transform.position, Quaternion.identity);
            Destroy(p, p.main.duration);
        }
    }

    [Serializable]
    public class SFXEffect
    {
        public string name;

        public ParticleSystem effect;
    }
}
