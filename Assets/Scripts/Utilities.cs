using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities {

    public static List<string> previousScenes = new List<string>();

    public static void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        previousScenes.Add(SceneManager.GetActiveScene().name);
        Debug.Log(previousScenes[previousScenes.Count - 1]);
    }

    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScenes[previousScenes.Count - 1], LoadSceneMode.Single);
        previousScenes.Remove(previousScenes[previousScenes.Count - 1]);
    }
}
