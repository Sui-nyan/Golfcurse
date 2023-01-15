using System.Collections.Generic;
using UnityEngine;
using System;

public class VisualEffectsManager : MonoBehaviour
{
    public SFXEffect[] effects;

    /// <summary>
    /// handles vfx for defeated objects
    /// </summary>
    /// <param name="go"></param>
    public void OnDeath(string name, GameObject go)
    {
        Debug.Log("Defeated " + name);
        SFXEffect vfx = Array.Find(effects, effect => effect.name.Contains(name));
        if (vfx != null)
        {
            ParticleSystem p = Instantiate<ParticleSystem>(vfx.effect, gameObject.transform);
            p.transform.position = go.transform.position;
            p.name = go.name + "DeathParticle";
            Destroy(p.gameObject, p.main.duration);
        }
    }

    [Serializable]
    public class SFXEffect
    {
        public string name;

        public ParticleSystem effect;
    }
}
