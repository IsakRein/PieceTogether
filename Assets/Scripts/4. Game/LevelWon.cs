using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

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
        if (!Utilities.removeAdsBought && adManager.time <= 0)
        {
            int lastWonLevel = PlayerPrefs.GetInt(Utilities.currentPack);
            if (Utilities.currentLevel == lastWonLevel + 1)
            {
                PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
                NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
            }

            Utilities.UpdateAchivements();
            Utilities.currentLevel += 1;

            int ad = adManager.adTypes[0];
            adManager.ShowInBetweenAd();

            if (ad != 2) { LoadNextScene(); }
        }

        else
        {
            int lastWonLevel = PlayerPrefs.GetInt(Utilities.currentPack);
            if (Utilities.currentLevel == lastWonLevel + 1)
            {
                PlayerPrefs.SetInt(Utilities.currentPack, lastWonLevel + 1);
                NPBinding.CloudServices.SetLong(Utilities.currentPack, lastWonLevel + 1);
            }

            Utilities.UpdateAchivements();
            Utilities.currentLevel += 1;

            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        if (Utilities.currentLevel == 151)
        {
            allLevelsWon.SetActive(true);
        }

        else
        {
            Utilities.LoadScene("4. Game");
        }
    }
}