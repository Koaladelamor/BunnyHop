using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    private GameObject pnl_menu;
    private GameObject pnl_settings;

    private void Awake()
    {
        pnl_menu = GameObject.Find("pnl_MainMenu");
        pnl_settings = GameObject.Find("pnl_Settings");
    }

    // Start is called before the first frame update
    private void Start()
    {
        pnl_settings.SetActive(false);
    }

    // Update is called once per frame
    /*private void Update()
    {
        
    }*/

    public void DisplaySettings() {
        pnl_menu.SetActive(false);
        pnl_settings.SetActive(true);
    }

    public void DisplayMainMenu()
    {
        pnl_settings.SetActive(false);
        pnl_menu.SetActive(true);
    }

}
