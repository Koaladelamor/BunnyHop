using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour
{

    [SerializeField] private RectTransform spawnTransform;
    [SerializeField] private RectTransform hitPoint;
    //[SerializeField] private RectTransform destroyTransform;

    [SerializeField] private GameObject beatPrefab;


    [SerializeField] private Conductor _conductor;

    [SerializeField] private Vector3 beatVelocity = Vector3.zero;
    //[SerializeField] private Vector3 beatVelocitytoStartSong = Vector3.zero;

    [SerializeField] private float songBpm;
    [SerializeField] private float beatPerSecond;
    [SerializeField] private float beatSpawnTimer;
    [SerializeField] private int beatsToStartSong;
    [SerializeField] private float songOffset;
    [SerializeField] private bool startSong;
    [SerializeField] private int beatCount;

    

    // Start is called before the first frame update
    void Start()
    {
        startSong = false;
        songOffset = _conductor.GetBeatOffset();
        songBpm = _conductor.GetSongBPM();
        beatPerSecond = 60 / songBpm * _conductor.GetPitch();
        beatCount = 0;
        beatVelocity = (hitPoint.position - spawnTransform.position) / beatPerSecond;
        //beatVelocitytoStartSong = (hitPoint.position - spawnTransform.position) / beatPerSecond * beatsToStartSong;
        //Debug.Log(beatVelocitytoStartSong);
        //Debug.Log("vel " + beatVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        /*beatVelocitytoStartSong -= beatVelocity * Time.deltaTime;
        Debug.Log(beatVelocitytoStartSong);
        if (beatVelocitytoStartSong.x <= 0) {
            _conductor.StartSong();
        }*/
        beatSpawnTimer += Time.deltaTime;
        if (beatSpawnTimer >= beatPerSecond) {
            beatSpawnTimer = 0;
            beatCount++;
            if (beatCount == beatsToStartSong) 
            {
                startSong = true;
            }
            //spawn beat
            GameObject beatTemp = Instantiate(beatPrefab, spawnTransform.position, transform.rotation, this.transform);
            beatTemp.GetComponent<BeatDisplayMovement>().SetBeatSpeed(songBpm);
            beatTemp.GetComponent<BeatDisplayMovement>().SetBeatVelocity(beatVelocity);
        }
        if (startSong) {
            songOffset -= Time.deltaTime;
            if (songOffset <= 0)
            {
                songOffset = _conductor.GetBeatOffset();
                startSong = false;
                _conductor.StartSong();
            }
        }
    }

    public void SetBeatPerSecond(float bps) { beatPerSecond = bps; }
}
