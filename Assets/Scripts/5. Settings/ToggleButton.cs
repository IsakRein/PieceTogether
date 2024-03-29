﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {
    public string type;
    public bool isOn;
    public Image background;
    public GameObject on;
    public GameObject off;

    public Color onColor;
    public Color offColor;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey(type))
        {
            if (PlayerPrefs.GetInt(type) == 1)
            {
                isOn = true;
                On();
            }
            else
            {
                isOn = false;
                Off();
            }
        }

        else
        {
            isOn = true;
            On();
        }
    }

    public void Toggle()
    {
        isOn = !isOn;

        if (isOn)
        {
            On();
        }

        else
        {
            Off();
        }
    }

    private void On()
    {
        if (type == "Sound")
        {
            Utilities.SoundOn = true;
        }
        else
        {
            Utilities.VibrationOn = true;
            Utilities.Vibrate();
        }

        PlayerPrefs.SetInt(type, 1);

        background.color = onColor;
        on.SetActive(true);
        off.SetActive(false);
    }

    private void Off()
    {
        if (type == "Sound")
        {
            Utilities.SoundOn = false;
        }
        else
        {
            Utilities.VibrationOn = false;
        }

        PlayerPrefs.SetInt(type, 0);

        background.color = offColor;
        on.SetActive(false);
        off.SetActive(true);
    }
}
