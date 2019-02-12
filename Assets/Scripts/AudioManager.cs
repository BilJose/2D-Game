using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f,0.5f)]
    public float randomVol = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPit = 1f;

    public bool loop = false;

    private AudioSource source;


    public void SetSource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;

    }
    public void Play()
    {
        source.volume = volume*(1+Random.Range(-randomVol/2f, randomVol/2f));
        source.pitch = pitch * (1 + Random.Range(-randomPit / 2f, randomPit / 2f)); ;
        source.Play();
    }
    public void Stop()
    {
        
        source.Stop();
    }
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if(instance != null)
        {

            if(instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }


    void Start()
    {
        for(int i = 0; i<sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + 1 + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource (_go.AddComponent<AudioSource>());

        }
        PlaySound("Menu");
    }
    public void PlaySound(string _name)
    {
        for(int i = 0; i <sounds.Length;i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        Debug.LogWarning("audioMaanager sound not found in list: " + _name);
    }
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        Debug.LogWarning("audioMaanager sound not found in list: " + _name);
    }
}
