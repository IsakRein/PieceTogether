using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class LoadNativeUI : MonoBehaviour {

    public PopUp popUp;

    public void LoadAchivementsUI()
    {
        bool _isAuthenticated = NPBinding.GameServices.LocalUser.IsAuthenticated;

        if (!_isAuthenticated)
        {
            NPBinding.GameServices.LocalUser.Authenticate((bool _success, string _error) =>
            {
                if (_success)
                {
                    Debug.Log("Sign-In Successfully");
                    Debug.Log("Local User Details : " + NPBinding.GameServices.LocalUser.ToString());
                    LoadUI();
                }
                else
                {
                    popUp.InitPopUp("Hint");
                }
            });
        }

        else
        {
            LoadUI();
        }

    }

    private void LoadUI()
    {
        NPBinding.GameServices.ShowAchievementsUI((string _error2) =>
        {
            if (_error2 != null)
            {
                popUp.InitPopUp("Hint");
            }
        });
    }
}
