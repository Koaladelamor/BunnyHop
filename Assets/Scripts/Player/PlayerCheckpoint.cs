using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint
{
    //[SerializeField] private GameObject player;
    private Vector3 playerPosition;
    private Quaternion playerRotation;

    private Vector3 camRotation;


    public void SaveCheckpoint(Vector3 currentPos, Quaternion currentRot, Vector3 currentCamRotation)
    {
        camRotation = currentCamRotation;
        playerPosition = currentPos;
        playerRotation = currentRot;
        Debug.Log("Saved Checkpoint " + playerPosition);
    }

    public void LoadCheckpoint()
    {
        if (playerPosition != null) 
        {
            Debug.Log("Load Position " + playerPosition);
            //player.GetComponent<PlayerMovementCC>().SetTransform(lastCheckpointPosition, lastCheckpointRotation);
        }
    }

    public Vector3 GetLastCheckpointPosition() { return playerPosition; }
    public Quaternion GetLastCheckpointRotation() { return playerRotation; }

    public Vector3 GetLastCheckpointCamRotation() { return camRotation; }
}
