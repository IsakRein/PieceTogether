using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

#if UNITY_IOS && !UNITY_EDITOR
    static int platform = 0;
#elif UNITY_ANDROID && !UNITY_EDITOR
    static int platform = 1;
#else
    static int platform = 2;
#endif


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


    public static void Vibrate() 
    {
        if (platform == 0)
        {
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactMedium);
        }
        else if (platform == 1)
        {
            AndroidManager.HapticFeedback();
        }
    }
}
