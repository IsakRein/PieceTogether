using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class LoadNativeUI : MonoBehaviour {

    public PopUp2 popUp;

    public bool errorTriggered = false;

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
                    if (!errorTriggered)
                    {
                        popUp.InitPopUp("FailedToLogIn");
                        errorTriggered = true;
                    }
                }
            });
        }

        else
        {
            LoadUI();
        }

        errorTriggered = false;
    }

    private void LoadUI()
    {
        if (!errorTriggered)
        {
            NPBinding.GameServices.ShowAchievementsUI((string _error2) =>
            {
                if (_error2 != null)
                {
                    if (!errorTriggered)
                    {
                        popUp.InitPopUp("FailedToLogIn");
                        errorTriggered = true;
                    }
                }
            });
        }
    }
}
