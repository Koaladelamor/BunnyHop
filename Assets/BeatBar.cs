using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour
{

    [SerializeField] private RectTransform spawnTransform;
    [SerializeField] private RectTransform destroyTransform;

    [SerializeField] private float beatSpeed;
    [SerializeField] private RectTransform beat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 newPosition = new Vector3(beat.position +, transform.position.y, transform.position.z);
        //beat.localPosition += Vector3.right * Time.deltaTime * beatSpeed;
    }
}
