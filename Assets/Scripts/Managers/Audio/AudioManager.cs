using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [SerializeField] private float musicVolume;
    [SerializeField] private float fxVolume;

    [SerializeField] private bool mute;

    [SerializeField] private AudioClip stepLanding;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    /*private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {

    }*/

    public void SetVolume(float musicVol, float fxVol) {
        musicVolume = musicVol;
        fxVolume = fxVol;
    }

    public void SetFXVolume(float fxVol) { fxVolume = fxVol; }

    public void SetMusicVolume(float musicVol) { musicVolume = musicVol; }

    public float GetFXVolume() { return fxVolume; }

    public float GetMusicVolume() { return musicVolume; }

    public AudioClip GetStepLanding() { return stepLanding; }
}
