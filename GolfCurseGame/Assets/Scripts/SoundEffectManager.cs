using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public SoundEffect[] sounds;
    public void onDeath(GameObject gameObject)
    {
        SoundEffect s = Array.Find(sounds, sound => sound.name == gameObject.name);
        GameObject soundObject = new GameObject();
        soundObject.transform.parent = gameObject.transform;
        AudioSource source = soundObject.AddComponent<AudioSource>();
        source.clip = s.audio;
        source.volume = s.volume;
        source.Play();
        Destroy(soundObject, s.audio.length);
        Debug.Log("Playing soud " + source.name);
    }

    [Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioClip audio;

        [Range(0f, 1f)]
        public float volume;
    }
}
