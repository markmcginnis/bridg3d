using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public string currentSongName = null;

    [System.Serializable]
    public class Sound
    {
        public AudioClip clip;
        [Range(0f,1f)]
        public float volume;
        public bool loop;
        [HideInInspector]
        public AudioSource source;
    }

    [System.Serializable]
    public class SoundType
    {
        public string name;

        public Sound[] sounds;
    }

    public SoundType[] soundTypes;

    void Start()
    {
        foreach(SoundType st in soundTypes)
        {
            foreach(Sound s in st.sounds){
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
            }
            
        }
        ChangeVolume(SettingsManager.settings.audioVolume);
    }

    public void Play(string name)
    {
        if(name == null || name == ""){
            // Debug.Log((name == null) ? "name was null to play" : "name was empty to play");
            return;
        }
        SoundType s = Array.Find(soundTypes, sound => sound.name == name);
        if(s == null)
        {
            UnityEngine.Debug.LogError(name + " sound not found in Play");
            return;
        }
        // Debug.Log("name to play: " + name + " - currentsongname: ~" + currentSongName + "~");
        if(name.EndsWith("Song")){
            currentSongName = name;
        }
        s.sounds[UnityEngine.Random.Range(0,s.sounds.Length)].source.Play();
    }

    public void Pause(string name)
    {
        if(name == null || name == ""){
            // Debug.Log((name == null) ? "name was null to pause" : "name was empty to pause");
            return;
        }
        SoundType s = Array.Find(soundTypes, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.LogError(name + " sound not found in Pause");
            return;
        }
        // Debug.Log("name to pause: " + name + " - currentsongname: ~" + currentSongName + "~");
        if(name.EndsWith("Song")){
            currentSongName = name;
        }
        s.sounds[UnityEngine.Random.Range(0,s.sounds.Length)].source.Pause();
    }

    public void Stop(string name)
    {
        SoundType s = Array.Find(soundTypes, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.LogError(name + " sound not found in Stop");
            return;
        }
        if(name.EndsWith("Song"))
            currentSongName = null;
        s.sounds[UnityEngine.Random.Range(0,s.sounds.Length)].source.Stop();
    }

    public void ChangeVolume(float newVolume){
        foreach(SoundType st in soundTypes){
            foreach(Sound s in st.sounds){
                s.source.volume = newVolume;
            }            
        }
    }
}
