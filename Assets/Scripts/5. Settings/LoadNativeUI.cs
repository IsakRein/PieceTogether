using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class LoadNativeUI : MonoBehaviour {

    public void LoadAchivementsUI()
    {
        NPBinding.GameServices.ShowAchievementsUI((string _error) => {});
    }

    public void LoadLeaderboardsUI()
    {
        NPBinding.GameServices.ShowLeaderboardUIWithGlobalID("Completed Levels", eLeaderboardTimeScope.WEEK, (string _error) => {});
    }
}
