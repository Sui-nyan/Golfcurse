using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectManager : MonoBehaviour
{
    public SoundEffect[] sounds;

    private void Start()
    {
        foreach(SoundEffect s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.audio;
            s.source.volume = s.volume;
            s.source.loop = s.isLooping;
        }

        PlaySound("MainTheme");

        if (SceneManager.GetActiveScene().name == "EndScene") 
            PlaySound("End");

    }

    public void PlaySound(string name)
    {
        SoundEffect s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log(name + "not found");
            return;
        }

        s.source.Play();
    }

    [Serializable]
    public class SoundEffect
    {
        public string name;
        public AudioSource source;
        public AudioClip audio;

        [Range(0f, 1f)]
        public float volume;

        public bool isLooping;
    }
}
