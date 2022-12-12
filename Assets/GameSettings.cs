using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    private Slider fx_slider;
    private Slider music_slider;

    private void Awake()
    {
        Slider[] _sliders = GetComponentsInChildren<Slider>();

        foreach (Slider _slider in _sliders)
        {
            if (_slider.name == "sld_fx") { 
                fx_slider = _slider;
            }
            else if (_slider.name == "sld_music")
            {
                music_slider = _slider;
            }
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (AudioManager.Instance)
        {
            fx_slider.value = AudioManager.Instance.GetFXVolume();
            music_slider.value = AudioManager.Instance.GetMusicVolume();
        }

        fx_slider.onValueChanged.AddListener(delegate { OnFXValueChanged(); });
        music_slider.onValueChanged.AddListener(delegate { OnMusicValueChanged(); });
    }

    // Update is called once per frame
    /*private void Update()
    {
        
    }*/

    public void OnFXValueChanged() 
    {
        AudioManager.Instance.SetFXVolume(fx_slider.value);
    }

    public void OnMusicValueChanged()
    {
        AudioManager.Instance.SetMusicVolume(music_slider.value);
    }

}
