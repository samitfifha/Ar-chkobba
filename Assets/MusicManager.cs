using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource _audiosource;
    public AudioClip[] song;
    public float volume;
    [SerializeField] private float _tracktimer;
    [SerializeField] private float _songsplayed;
    [SerializeField] private bool[] _beenplayed;

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
        _beenplayed = new bool[song.Length];
        if (!_audiosource.isPlaying)
            ChangeSong(Random.Range(0, song.Length));
    }
    void Update()
    {
        _audiosource.volume = volume;


    
        if (!_audiosource.isPlaying || Input.GetKeyDown(KeyCode.Space))

            ChangeSong(Random.Range(0, song.Length));
        
        if (_songsplayed ==song.Length)
        {
            _songsplayed = 0;
            for (int i = 0; i < song.Length; i++)
            {
                if (i == song.Length)
                    break;
                else
                    _beenplayed[i] = false;
            }
        }

    }
    public void ChangeSong(int songPicked)
    {
        if (!_beenplayed[songPicked])
        {
            _songsplayed++;
            _beenplayed[songPicked] = true;
            _audiosource.clip = song[songPicked];
            _audiosource.Play();
        }
        else
            _audiosource.Stop();
        
    }
}

