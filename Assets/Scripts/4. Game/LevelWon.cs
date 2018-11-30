﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour
{
    public GameObject allLevelsWon;

    public void NextLevel()
    {
        int lastWonLevel = PlayerPrefs.GetInt(Utilities.currentPack);

        if (Utilities.currentLevel == 150)
        {
            allLevelsWon.SetActive(true);

            if (Utilities.currentLevel == lastWonLevel + 1)
            {
                PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
                NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
            }
        }

        else
        {
            if (Utilities.currentLevel == lastWonLevel + 1)
            {
                PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
                NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
            }

            Debug.Log(Utilities.currentLevel);

            Utilities.currentLevel += 1;
            Utilities.LoadScene("4. Game");
        }
    }
}