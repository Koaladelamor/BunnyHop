using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputKeys : MonoBehaviour
{
    private Dictionary<char, Image> m_light_images;

    // Start is called before the first frame update
    private void Awake()
    {
        m_light_images = new Dictionary<char, Image>();

        Image[] images = FindObjectsOfType<Image>();

        foreach (Image img in images) {
            if (img.name == "img_light_w")
            {
                m_light_images.Add('w', img);
                img.enabled = false;
            }
            else if (img.name == "img_light_a") 
            {
                m_light_images.Add('a', img);
                img.enabled = false;
            }
            else if (img.name == "img_light_s")
            {
                m_light_images.Add('s', img);
                img.enabled = false;
            }
            else if (img.name == "img_light_d")
            {
                m_light_images.Add('d', img);
                img.enabled = false;
            }
            else if (img.name == "img_light_jump")
            {
                m_light_images.Add('j', img);
                img.enabled = false;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputMovement = new Vector3(InputManager.Instance.GetLeftAxis().x, 0, InputManager.Instance.GetLeftAxis().y);
        inputMovement.Normalize();

        if (inputMovement.z > 0)
        { // FORWARD
            m_light_images['w'].enabled = true;
        }
        else if (inputMovement.z <= 0 && m_light_images['w'].enabled)
        {
            m_light_images['w'].enabled = false;
        }

        if (inputMovement.z < 0)
        { // BACKWARD
            m_light_images['s'].enabled = true;
        }
        else if(inputMovement.z >= 0 && m_light_images['s'].enabled)
        {
            m_light_images['s'].enabled = false;
        }

        if (inputMovement.x > 0)
        { // RIGHT
            m_light_images['d'].enabled = true;
        }
        else if (inputMovement.x <= 0 && m_light_images['d'].enabled)
        {
            m_light_images['d'].enabled = false;
        }

        if (inputMovement.x < 0)
        { // LEFT
            m_light_images['a'].enabled = true;
        }
        else if (inputMovement.x >= 0 && m_light_images['a'].enabled)
        {
            m_light_images['a'].enabled = false;
        }

        if (InputManager.Instance.GetJumpButtonDown() || !InputManager.Instance.GetJumpButtonReleased())
        { // JUMP
            m_light_images['j'].enabled = true;
        }
        else 
        {
            m_light_images['j'].enabled = false;
        }
    }
}
