using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpertBundle : MonoBehaviour
{
    public Transform expert1;
    public Transform expert2;
    public Transform expert3;

    public GameObject price;
    public GameObject buyButton;

    public StoreManager2 storeManager2;

    void Start()
    {
        if (PlayerPrefs.HasKey("ExpertBundleBought"))
        {
            if (PlayerPrefs.GetInt("ExpertBundleBought") == 1)
            {
                Bought();
            }

            else
            {
                storeManager2.CustomStart();
            }
        }

        else
        {
            storeManager2.CustomStart();
        }
    }

    private void Bought()
    {
        price.SetActive(false);
        buyButton.SetActive(false);

        expert1.GetComponent<Button>().interactable = true;
        expert2.GetComponent<Button>().interactable = true;
        expert3.GetComponent<Button>().interactable = true;

        expert1.GetComponent<LoadPack>().enabled = true;
        expert2.GetComponent<LoadPack>().enabled = true;
        expert3.GetComponent<LoadPack>().enabled = true;
    }


    public void Buy()
    {
        storeManager2.SelectProduct("Expert Bundle");
    }


    public void PurchaseSuccessful()
    {
        PlayerPrefs.SetInt("ExpertBundleBought", 1);
        NPBinding.CloudServices.SetBool("ExpertBundleBought", true);
        Bought();
    }
}

