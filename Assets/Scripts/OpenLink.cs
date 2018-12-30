using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class OpenLink : MonoBehaviour
{
    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void AskForReview()
    {
        NPBinding.Utility.RateMyApp.AskForReviewNow();
    }
}