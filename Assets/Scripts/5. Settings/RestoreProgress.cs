using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreProgress : MonoBehaviour {

    public GameObject confirm;
    public GameObject success;
    public GameObject failure;

    public void Restore()
    {
        if (Utilities.DifferenceExistsCloudLocal())
        {
            Utilities.UseCloudSave();
            confirm.SetActive(false);
            success.SetActive(true);
        }

        else
        {
            confirm.SetActive(false);
            failure.SetActive(true);
        }

        confirm.transform.localScale = new Vector2(0, 0);
    }
}
