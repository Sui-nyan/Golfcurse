using System;
using UnityEngine;

public class VisualEffectsManager : MonoBehaviour
{
    public VFXEffect[] effects;

    /// <summary>
    /// handles vfx for defeated objects
    /// </summary>
    /// <param name="go"></param>
    public void OnDeath(string name, GameObject go)
    {
        Debug.Log("Defeated " + name);
        VFXEffect vfx = Array.Find(effects, effect => effect.name.Contains(name));
        if (vfx != null)
        {
            ParticleSystem p = Instantiate<ParticleSystem>(vfx.effect, gameObject.transform);
            p.transform.position = go.transform.position;
            p.name = go.name + "DeathParticle";
            Destroy(p.gameObject, p.main.duration);
        }
    }
    /// <summary>
    /// holds visual effects
    /// </summary>
    [Serializable]
    public class VFXEffect
    {
        public string name;

        public ParticleSystem effect;
    }
}
