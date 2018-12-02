using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour
{
    private AdManager adManager;

    private void Start()
    {
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        adManager.RequestInBetweenAds();
    }

    public GameObject allLevelsWon;

    public void NextLevel()
    {
        int ad = adManager.adTypes[0];

        adManager.ShowInBetweenAd();

        int lastWonLevel = PlayerPrefs.GetInt(Utilities.currentPack);
        if (Utilities.currentLevel == lastWonLevel + 1)
        {
            PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
            NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
        }

        if (Utilities.currentLevel != 150)
        {
            Utilities.currentLevel += 1;
        }

        if (ad != 2)
        {
            Debug.Log(adManager.adTypes[0]);
            NextLevel2();
        }
    }

    public void NextLevel2()
    {
        if (Utilities.currentLevel == 150)
        {
            allLevelsWon.SetActive(true);
        }

        else
        {
            Utilities.LoadScene("4. Game");
        }
    }
}