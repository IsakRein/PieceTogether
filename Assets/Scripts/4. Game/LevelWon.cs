using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelWon : MonoBehaviour
{
    private AdManager adManager;
    private GameServices gameServices;

    private void Start()
    {
        GameObject parent = GameObject.Find("AdManager");

        adManager = parent.GetComponent<AdManager>();
        gameServices = parent.GetComponent<GameServices>();

        adManager.RequestInBetweenAds();
    }

    public GameObject allLevelsWon;

    public void NextLevel()
    {
        if (!Utilities.removeAdsBought)
        {
            int ad = adManager.adTypes[0];

            adManager.ShowInBetweenAd();

            int lastWonLevel = PlayerPrefs.GetInt(Utilities.currentPack);
            if (Utilities.currentLevel == lastWonLevel + 1)
            {
                PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
                NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
            }

            TotalLevelsWon();

            if (Utilities.currentLevel != 150)
            {
                Utilities.currentLevel += 1;
            }

            if (ad != 2)
            {
                Debug.Log(adManager.adTypes[0]);
                LoadNextScene();
            }
        }

        else
        {
            int lastWonLevel = PlayerPrefs.GetInt(Utilities.currentPack);
            if (Utilities.currentLevel == lastWonLevel + 1)
            {
                PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
                NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
            }

            TotalLevelsWon();

            if (Utilities.currentLevel != 150)
            {
                Utilities.currentLevel += 1;
            }

            LoadNextScene();
        }
    }

    public void LoadNextScene()
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

    public void TotalLevelsWon()
    {
        int totalLevelsWon = PlayerPrefs.GetInt("Beginner") + PlayerPrefs.GetInt("Easy") + PlayerPrefs.GetInt("Normal") + PlayerPrefs.GetInt("Hard") + PlayerPrefs.GetInt("Advanced") + PlayerPrefs.GetInt("Expert 1") + PlayerPrefs.GetInt("Expert 2") + PlayerPrefs.GetInt("Expert 3");

        if (totalLevelsWon >= 1200)
        {

        }
        else if (totalLevelsWon >= 1000)
        {

        }
        else if (totalLevelsWon >= 500)
        {

        }
        else if (totalLevelsWon >= 250)
        {

        }
        else if (totalLevelsWon >= 100)
        {

        }
        else if (totalLevelsWon >= 50)
        {

        }
        else if (totalLevelsWon >= 25)
        {

        }
    }
}