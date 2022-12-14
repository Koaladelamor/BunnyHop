using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    [SerializeField] private PlayerMovementCC _player;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float songBpm;
    [SerializeField] private float beatInverval;
    [SerializeField] private float lastBeat;

    [SerializeField] private bool beatOnHit;
    [SerializeField] private float beatDuration;

    [SerializeField] private float songPosition;
    [SerializeField] private float lastBeatSongPosition;
    [SerializeField] private float dspSongTime;
    [SerializeField] private float songFirstBeatOffset;

    [SerializeField] private List<Song> _songList;
    [SerializeField] private int currentSongIndex;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        _audioSource.volume = AudioManager.Instance.GetMusicVolume();

        if (currentSongIndex < _songList.Count) {
            LoadSong(_songList[currentSongIndex]);
        }

        beatInverval = 60 / songBpm * _audioSource.pitch;
        lastBeat = 0;
        //_audioSource.Play();
        //dspSongTime = (float)AudioSettings.dspTime;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_audioSource.isPlaying) {
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - songFirstBeatOffset * _audioSource.pitch);
            //Debug.Log(songPosition);

            if (songPosition > lastBeat + beatInverval)
            {
                // on beat
                lastBeatSongPosition = songPosition;
                lastBeat += beatInverval;
                //Debug.Log("BEAT " + lastBeat);
                beatOnHit = true;
            }
        }

        if (beatOnHit) {
            if (songPosition - lastBeatSongPosition > beatDuration) {
                beatOnHit = false;
            }
            /*beatDuration -= Time.deltaTime * 2;
            if (beatDuration <= 0) 
            {
                beatOnHit = false;
                beatDuration = 0.5f;
            }*/
        }
    }

    public void LoadSong(Song _song) {
        songBpm = _song.bpm;
        _audioSource.clip = _song.clip;
        songFirstBeatOffset = _song.offset;
    }

    public void StartSong()
    {
        _audioSource.Play();
        dspSongTime = (float)AudioSettings.dspTime;
    }

    public void PauseSong() {
        _audioSource.Pause();
    }

    public void UnpauseSong()
    {
        _audioSource.UnPause();
    }

    public bool IsSongPlaying() { return _audioSource.isPlaying; }

    public float GetBeatOffset() { return songFirstBeatOffset; }

    public float GetSongBPM() { return songBpm; }

    public float GetPitch() { return _audioSource.pitch; }

    public void SetBeatOnHit(bool onHit) { beatOnHit = onHit; }

    public bool GetBeatOnHit() { return beatOnHit; }
}
