using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;

public class GameServices : MonoBehaviour {

    public PopUp popUp;

    private void Start()
    {
        bool _isAvailable = NPBinding.GameServices.IsAvailable();
        bool _isAuthenticated = NPBinding.GameServices.LocalUser.IsAuthenticated;

        if (_isAvailable)
        {
            if (!_isAuthenticated)
            {
                NPBinding.GameServices.LocalUser.Authenticate((bool _success, string _error) =>
                {
                    if (_success)
                    {
                        Debug.Log("Sign-In Successfully");
                        Debug.Log("Local User Details : " + NPBinding.GameServices.LocalUser.ToString());
                    }
                    else
                    {
                        Debug.Log("Sign-In Failed with error " + _error);
                    }
                });
            }

            NPBinding.GameServices.LoadAchievements((Achievement[] _achievements, string _error) => 
            {
                if (_achievements == null)
                {
                    Debug.Log("Couldn't load achievement list with error = " + _error);
                    return;
                }

                int _achievementCount = _achievements.Length;
                Debug.Log(string.Format("Successfully loaded achievement list. Count={0}.", _achievementCount));

                for (int _iter = 0; _iter < _achievementCount; _iter++)
                {
                    Debug.Log(string.Format("[Index {0}]: {1}", _iter, _achievements[_iter]));
                }
            });

            NPBinding.GameServices.LoadAchievementDescriptions((AchievementDescription[] _descriptions, string _error) => {

                if (_descriptions == null)
                {
                    Debug.Log("Couldn't load achievement description list with error = " + _error);
                    return;
                }

                int _descriptionCount = _descriptions.Length;
                Debug.Log(string.Format("Successfully loaded achievement description list. Count={0}.", _descriptionCount));

                for (int _iter = 0; _iter < _descriptionCount; _iter++)
                {
                    Debug.Log(string.Format("[Index {0}]: {1}", _iter, _descriptions[_iter]));
                }
            });
        }

        Utilities.Initialize();

        if (!PlayerPrefs.HasKey("AskedAboutCloud")) {
            if (Utilities.DifferenceExistsCloudLocal())
            {
                popUp.InitPopUp("Hint");
            }
        }  
    }

    public void LoadLocal()
    {
        Utilities.UseLocalSave();
        popUp.StopPopUp();
        PlayerPrefs.SetInt("AskedAboutCloud", 1);
    }

    public void LoadCloud()
    {
        Utilities.UseCloudSave();
        popUp.StopPopUp();
        PlayerPrefs.SetInt("AskedAboutCloud", 1);
    }
}