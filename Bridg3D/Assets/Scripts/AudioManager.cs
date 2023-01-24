using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public string currentSongName = null;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f,1f)]
        public float volume;
        public bool loop;
        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        ChangeVolume(SettingsManager.settings.audioVolume);
    }

    public void Play(string name)
    {
        if(name == null || name == ""){
            // Debug.Log((name == null) ? "name was null to play" : "name was empty to play");
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            UnityEngine.Debug.LogError(name + " sound not found in Play");
            return;
        }
        // Debug.Log("name to play: " + name + " - currentsongname: ~" + currentSongName + "~");
        if(name.EndsWith("Song")){
            currentSongName = name;
            if(!s.source.isPlaying){
                s.source.Play();
            }
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        if(name == null || name == ""){
            // Debug.Log((name == null) ? "name was null to pause" : "name was empty to pause");
            return;
        }
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.LogError(name + " sound not found in Pause");
            return;
        }
        // Debug.Log("name to pause: " + name + " - currentsongname: ~" + currentSongName + "~");
        if(name.EndsWith("Song")){
            currentSongName = name;
            if(s.source.isPlaying){
                s.source.Pause();
            }
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.LogError(name + " sound not found in Stop");
            return;
        }
        s.source.Stop();
        if(name.EndsWith("Song"))
            currentSongName = null;
    }

    public void ChangeVolume(float newVolume){
        foreach(Sound s in sounds){
            s.source.volume = newVolume;
        }
    }
}
