using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Audio : MonoBehaviour
{

    public static Manager_Audio _AUDIO_MANAGER;

    private void Awake()
    {
        if (_AUDIO_MANAGER != null && _AUDIO_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _AUDIO_MANAGER = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
