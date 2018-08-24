using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities {

    public static List<string> previousScenes = new List<string>();

    public static string currentPack;
    public static int currentLevel;

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        previousScenes.Add(SceneManager.GetActiveScene().name);
    }

    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScenes[previousScenes.Count - 1], LoadSceneMode.Single);
        previousScenes.Remove(previousScenes[previousScenes.Count - 1]);
    }
}
