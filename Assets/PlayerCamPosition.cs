using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamPosition : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        if (playerTransform == null) {
            GameObject tempGO = GameObject.FindGameObjectWithTag("Player");
            playerTransform = tempGO.GetComponent<Transform>();
        }
    }


    // Update is called once per frame
    private void Update()
    {
        transform.position = playerTransform.position;
    }
}
