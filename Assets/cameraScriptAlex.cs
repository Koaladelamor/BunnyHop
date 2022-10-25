using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScriptAlex : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private float targetDistance;

    [SerializeField]
    private float cameraLerp; //12f

    private float rotationX;
    private float rotationY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void LateUpdate()
    {
        rotationX += Manager_Input._INPUT_MANAGER.GetRightAxis().y;
        rotationY += Manager_Input._INPUT_MANAGER.GetRightAxis().x;

        //Debug.Log(Manager_Input._INPUT_MANAGER.GetRightAxis().y + "  " + Manager_Input._INPUT_MANAGER.GetRightAxis().x);

        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        RaycastHit hit;

        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);

        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }

        transform.position = finalPosition;

    }
}