using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxelBusters.NativePlugins;

public static class Utilities
{
    public static List<string> previousScenes = new List<string>();

    public static string currentPack;
    public static int currentLevel;

    public static int beginnerLastDone;
    public static int easyLastDone;
    public static int normalLastDone;
    public static int hardLastDone;
    public static int advancedLastDone;
    public static int expert1LastDone;
    public static int expert2LastDone;
    public static int expert3LastDone;

    public static bool SoundOn;
    public static bool VibrationOn;

    public static bool removeAdsBought;
    public static bool expertBundleBought;
    public static int hintCount;
    public static int skipCount;

#if UNITY_IOS && !UNITY_EDITOR
    public static int platform = 0;
#elif UNITY_ANDROID && !UNITY_EDITOR
    public static int platform = 1;
#else
    public static int platform = 2;
#endif

    public static void Initialize()
    {
        //removeads
        if (PlayerPrefs.HasKey("removeAdsBought"))
        {
            if (PlayerPrefs.GetInt("removeAdsBought") == 1)
            {
                removeAdsBought = true;
            }
            else
            {
                removeAdsBought = false;
            }
        }
        else
        {
            removeAdsBought = false;
        }

        //expertbundle
        if (PlayerPrefs.HasKey("expertBundleBought"))
        {
            if (PlayerPrefs.GetInt("expertBundleBought") == 1)
            {
                expertBundleBought = true;
            }
            else
            {
                expertBundleBought = false;
            }
        }
        else
        {
            expertBundleBought = false;
        }

        //hintCount
        if (PlayerPrefs.HasKey("hintCount"))
        {
            hintCount = PlayerPrefs.GetInt("hintCount");
        }
        else
        {
            PlayerPrefs.SetInt("hintCount", 0);
            hintCount = 0;
        }

        //hintCount
        if (PlayerPrefs.HasKey("skipCount"))
        {
            skipCount = PlayerPrefs.GetInt("skipCount");
        }
        else
        {
            PlayerPrefs.SetInt("skipCount", 0);
            skipCount = 0;
        }

        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                SoundOn = true;
            }
            else
            {
                SoundOn = false;
            }
        }
        else
        {
            SoundOn = true;
        }

        if (PlayerPrefs.HasKey("Vibration"))
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                VibrationOn = true;
            }
            else
            {
                VibrationOn = false;
            }
        }
        else
        {
            VibrationOn = true;
        }
    }

    public static void CompareCloudAndLocal()
    {
        bool differenceExists = false;
        
        if ((NPBinding.CloudServices.GetBool("expertBundleBought") == true && PlayerPrefs.GetInt("expertBundleBought") == 0) || (NPBinding.CloudServices.GetBool("expertBundleBought") == false && PlayerPrefs.GetInt("expertBundleBought") == 1))
        {

        }
        PlayerPrefs.SetInt("removeAdsBought", 1);
        NPBinding.CloudServices.SetBool("removeAdsBought", true);

        PlayerPrefs.SetInt("expertBundleBought", 1);
        NPBinding.CloudServices.SetBool("expertBundleBought", true);

        PlayerPrefs.SetInt("hintCount", hintCount);
        NPBinding.CloudServices.SetLong("hintCount", hintCount);


        PlayerPrefs.SetInt("skipCount", skipCount);
        NPBinding.CloudServices.SetLong("skipCount", skipCount);
    }

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);

        if (scene != SceneManager.GetActiveScene().name)
        {
            previousScenes.Add(SceneManager.GetActiveScene().name);
        }
    }

    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScenes[previousScenes.Count - 1], LoadSceneMode.Single);
        previousScenes.Remove(previousScenes[previousScenes.Count - 1]);
    }

    public static void SaveLastLevel(string level) {
       //PlayerPrefs.SetInt("")
    }

    public static void LoadLastLevel() {

    }

    public static void BuyRemoveAds()
    {
        removeAdsBought = true;
        PlayerPrefs.SetInt("removeAdsBought", 1);
        NPBinding.CloudServices.SetBool("removeAdsBought", true);
    }

    public static void BuyExpertBundle()
    {
        expertBundleBought = true;
        PlayerPrefs.SetInt("expertBundleBought", 1);
        NPBinding.CloudServices.SetBool("expertBundleBought", true);
    }

    public static void AddHints(int count)
    {
        hintCount = hintCount + count;
        PlayerPrefs.SetInt("hintCount", hintCount);
        NPBinding.CloudServices.SetLong("hintCount", hintCount);
    }

    public static void AddSkips(int count)
    {
        skipCount = skipCount + count;
        PlayerPrefs.SetInt("skipCount", skipCount);
        NPBinding.CloudServices.SetLong("skipCount", skipCount);
    }

    public static void Vibrate() 
    {
        if (platform == 0)
        {
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactLight);
        }
        else if (platform == 1)
        {
            AndroidManager.HapticFeedback();
        }
    }
}
