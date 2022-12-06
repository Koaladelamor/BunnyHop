using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float songBpm;
    [SerializeField] private float beatInverval;
    [SerializeField] private float lastBeat;

    [SerializeField] private float songPosition;
    [SerializeField] private float dspSongTime;
    [SerializeField] private float songFirstBeatOffset;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        beatInverval = 60 / songBpm;
        lastBeat = 0;
        _audioSource.Play();
        dspSongTime = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    private void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - songFirstBeatOffset);
        //Debug.Log(songPosition);

        if (songPosition > lastBeat + beatInverval) 
        {
            // on beat
            lastBeat += beatInverval;
            Debug.Log("BEAT " + lastBeat);

        }
    }
}
