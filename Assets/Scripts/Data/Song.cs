using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Songs/Add Song")]
public class Song : ScriptableObject
{
    public enum SongType { LOOP, TRACK }

    public SongType type;
    public AudioClip clip;
    public float bpm;
    public float offset;
    public float beatsToStart;

}
