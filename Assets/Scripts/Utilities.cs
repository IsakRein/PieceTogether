using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxelBusters.NativePlugins;

public static class Utilities
{
    public static List<string> previousScenes = new List<string>();

    public static string currentPack;
    public static string currentPackInt;
    
    public static int currentLevel;

    public static List<int> lastDone = new List<int>();

    public static bool SoundOn;
    public static bool VibrationOn;

    public static bool removeAdsBought;
    public static bool expertBundleBought;
    public static int hintCount;
    public static int skipCount;

    public static int totalLevelsWon;

#if UNITY_IOS && !UNITY_EDITOR
    public static int platform = 0;
#elif UNITY_ANDROID && !UNITY_EDITOR
    public static int platform = 1;
#else
    public static int platform = 2;
#endif

    public static void Initialize()
    {
        NPBinding.CloudServices.Initialise();

        Debug.Log("HE: " + NPBinding.CloudServices.GetLong("asf"));

        if (!PlayerPrefs.HasKey("Beginner")) {PlayerPrefs.SetInt("Beginner", 0);}
        if (!PlayerPrefs.HasKey("Easy")){PlayerPrefs.SetInt("Easy", 0);}
        if (!PlayerPrefs.HasKey("Normal")){PlayerPrefs.SetInt("Normal", 0);}
        if (!PlayerPrefs.HasKey("Hard")){PlayerPrefs.SetInt("Hard", 0);}
        if (!PlayerPrefs.HasKey("Advanced")){PlayerPrefs.SetInt("Advanced", 0);}
        if (!PlayerPrefs.HasKey("Expert 1")){PlayerPrefs.SetInt("Expert 1", 0);}
        if (!PlayerPrefs.HasKey("Expert 2")){PlayerPrefs.SetInt("Expert 2", 0);}
        if (!PlayerPrefs.HasKey("Expert 3")){PlayerPrefs.SetInt("Expert 3", 0);}
        
        UpdateAchivements();

        if (PlayerPrefs.HasKey("removeAdsBought")) { if (PlayerPrefs.GetInt("removeAdsBought") == 1){removeAdsBought = true;}else{removeAdsBought = false;} }else {removeAdsBought = false;}
        if (PlayerPrefs.HasKey("expertBundleBought")) { if (PlayerPrefs.GetInt("expertBundleBought") == 1) { expertBundleBought = true; } else { expertBundleBought = false; } } else { expertBundleBought = false; }
        if (PlayerPrefs.HasKey("hintCount")) { hintCount = PlayerPrefs.GetInt("hintCount");}else{PlayerPrefs.SetInt("hintCount", 0);hintCount = 0;}
        if (PlayerPrefs.HasKey("skipCount")) { skipCount = PlayerPrefs.GetInt("skipCount");}else{PlayerPrefs.SetInt("skipCount", 0);skipCount = 0;}
        if (PlayerPrefs.HasKey("Sound")) { if (PlayerPrefs.GetInt("Sound") == 1){SoundOn = true;}else{SoundOn = false;}}else{SoundOn = true;}
        if (PlayerPrefs.HasKey("Vibration")) { if (PlayerPrefs.GetInt("Vibration") == 1){VibrationOn = true;}else{VibrationOn = false;}}else{VibrationOn = true;}
    }

    public static void UpdateAchivements()
    {
        bool _isAvailable = NPBinding.GameServices.IsAvailable();

        if (_isAvailable)
        {
            int totalLevelsWon = PlayerPrefs.GetInt("Beginner") + PlayerPrefs.GetInt("Easy") + PlayerPrefs.GetInt("Normal") + PlayerPrefs.GetInt("Hard") + PlayerPrefs.GetInt("Advanced") + PlayerPrefs.GetInt("Expert 1") + PlayerPrefs.GetInt("Expert 2") + PlayerPrefs.GetInt("Expert 3");

            double _progressPercentage25 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("25 Levels")) * 100d;
            double _progressPercentage50 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("50 Levels")) * 100d;
            double _progressPercentage100 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("100 Levels")) * 100d;
            double _progressPercentage250 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("250 Levels")) * 100d;
            double _progressPercentage500 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("500 Levels")) * 100d;
            double _progressPercentage1000 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("1000 Levels")) * 100d;
            double _progressPercentage1200 = ((double)totalLevelsWon / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Every Level")) * 100d;

            Debug.Log(NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Every Level"));

            if (_progressPercentage25 > 100.0) { _progressPercentage25 = 100.0; };
            if (_progressPercentage50 > 100.0) { _progressPercentage50 = 100.0; };
            if (_progressPercentage100 > 100.0) { _progressPercentage100 = 100.0; };
            if (_progressPercentage250 > 100.0) { _progressPercentage250 = 100.0; };
            if (_progressPercentage500 > 100.0) { _progressPercentage500 = 100.0; };
            if (_progressPercentage1000 > 100.0) { _progressPercentage1000 = 100.0; };
            if (_progressPercentage1200 > 100.0) { _progressPercentage1200 = 100.0; };

            NPBinding.GameServices.ReportProgressWithGlobalID("25 Levels", _progressPercentage25, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("50 Levels", _progressPercentage50, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("100 Levels", _progressPercentage100, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("250 Levels", _progressPercentage250, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("500 Levels", _progressPercentage500, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("1000 Levels", _progressPercentage1000, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Every Level", _progressPercentage1200, (bool _status, string _error) => { });

            NPBinding.GameServices.ReportProgressWithGlobalID("Beginner", ((double)PlayerPrefs.GetInt("Beginner") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Beginner")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Easy", ((double)PlayerPrefs.GetInt("Easy") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Easy")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Normal", ((double)PlayerPrefs.GetInt("Normal") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Normal")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Hard", ((double)PlayerPrefs.GetInt("Hard") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Hard")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Advanced", ((double)PlayerPrefs.GetInt("Advanced") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Advanced")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Expert 1", ((double)PlayerPrefs.GetInt("Expert 1") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Expert 1")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Expert 2", ((double)PlayerPrefs.GetInt("Expert 2") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Expert 2")) * 100d, (bool _status, string _error) => { });
            NPBinding.GameServices.ReportProgressWithGlobalID("Expert 3", ((double)PlayerPrefs.GetInt("Expert 3") / NPBinding.GameServices.GetNoOfStepsForCompletingAchievement("Expert 3")) * 100d, (bool _status, string _error) => { });

            NPBinding.GameServices.ReportScoreWithGlobalID("Completed Levels", totalLevelsWon, (bool _success, string _error) => {});

            int totalPacksWon = 0;
           
            if (PlayerPrefs.GetInt("Beginner") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Easy") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Normal") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Hard") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Advanced") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Expert 1") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Expert 2") == 150) { totalPacksWon += 1; };
            if (PlayerPrefs.GetInt("Expert 3") == 150) { totalPacksWon += 1; };

            NPBinding.GameServices.ReportScoreWithGlobalID("Completed Packs", totalPacksWon, (bool _success, string _error) => {});
        }
    }

    public static bool DifferenceExistsCloudLocal()
    {
        if (NPBinding.CloudServices.GetLong("Beginner") != 0 || NPBinding.CloudServices.GetLong("Easy") != 0 || NPBinding.CloudServices.GetLong("Normal") != 0 || NPBinding.CloudServices.GetLong("Hard") != 0 || NPBinding.CloudServices.GetLong("Advanced") != 0 || NPBinding.CloudServices.GetLong("Expert 1") != 0 || NPBinding.CloudServices.GetLong("Expert 2") != 0 || NPBinding.CloudServices.GetLong("Expert 3") != 0)
        {
            bool differenceExists = false;

            if (PlayerPrefs.GetInt("Beginner") != NPBinding.CloudServices.GetLong("Beginner")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Easy") != NPBinding.CloudServices.GetLong("Easy")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Normal") != NPBinding.CloudServices.GetLong("Normal")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Hard") != NPBinding.CloudServices.GetLong("Hard")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Advanced") != NPBinding.CloudServices.GetLong("Advanced")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Expert 1") != NPBinding.CloudServices.GetLong("Expert 1")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Expert 2") != NPBinding.CloudServices.GetLong("Expert 2")) { differenceExists = true; }
            else if (PlayerPrefs.GetInt("Expert 3") != NPBinding.CloudServices.GetLong("Expert 3")) { differenceExists = true; }

            return differenceExists;
        }

        else
        {
            return false;
        }
    }

    public static void UseCloudSave()
    {
        PlayerPrefs.SetInt("Beginner", (int)NPBinding.CloudServices.GetLong("Beginner"));
        PlayerPrefs.SetInt("Easy", (int)NPBinding.CloudServices.GetLong("Easy"));
        PlayerPrefs.SetInt("Normal", (int)NPBinding.CloudServices.GetLong("Normal"));
        PlayerPrefs.SetInt("Hard", (int)NPBinding.CloudServices.GetLong("Hard"));
        PlayerPrefs.SetInt("Advanced", (int)NPBinding.CloudServices.GetLong("Advanced"));
        PlayerPrefs.SetInt("Expert 1", (int)NPBinding.CloudServices.GetLong("Expert 1"));
        PlayerPrefs.SetInt("Expert 2", (int)NPBinding.CloudServices.GetLong("Expert 2"));
        PlayerPrefs.SetInt("Expert 3", (int)NPBinding.CloudServices.GetLong("Expert 3"));
    }

    public static void UseLocalSave()
    {
        NPBinding.CloudServices.SetLong("Beginner", PlayerPrefs.GetInt("Beginner"));
        NPBinding.CloudServices.SetLong("Easy", PlayerPrefs.GetInt("Easy"));
        NPBinding.CloudServices.SetLong("Normal", PlayerPrefs.GetInt("Normal"));
        NPBinding.CloudServices.SetLong("Hard", PlayerPrefs.GetInt("Hard"));
        NPBinding.CloudServices.SetLong("Advanced", PlayerPrefs.GetInt("Advanced"));
        NPBinding.CloudServices.SetLong("Expert 1", PlayerPrefs.GetInt("Expert 1"));
        NPBinding.CloudServices.SetLong("Expert 2", PlayerPrefs.GetInt("Expert 2"));
        NPBinding.CloudServices.SetLong("Expert 3", PlayerPrefs.GetInt("Expert 3"));
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
        if (VibrationOn)
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
}
