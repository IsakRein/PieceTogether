using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;
using TMPro;

public class CheaperHint : MonoBehaviour {

    public float maxTime;
    public float time;
    public PopUp popUp;
    private bool popUpCalled = false;
    private bool timeSet = false;

    public SortOrder sortOrder;
    public AdManager adManager;
    public int adType;

    private void Start()
    {
        adManager = GameObject.Find("/AdManager").GetComponent<AdManager>();
    }

    void CustomStart(BillingProduct[] _regProductsList)
    {
        maxTime += Random.Range(-20f, 20f);

        if (Random.Range(0f, 1f) > 0.33f)
        {
            if (Random.Range(0f, 1f) > 0.5f)
            {
                adType = 0;
                transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("" + _regProductsList[8].LocalizedPrice);
            }
            else 
            {
                adType = 1;
            }

            time = maxTime;

            timeSet = true;
        }
    }


	// Update is called once per frame
	void Update () {

        if (timeSet)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }

            else
            {
                if (!popUpCalled)
                {
                    if (adType == 0)
                    {
                        popUp.InitPopUp("CheaperHint");
                    }
                    else
                    {
                        popUp.InitPopUp("WatchVideo");
                    }
                    popUpCalled = true;
                }
            }
        }
	}

    public void WatchVideoForHint()
    {
    }

    public void RequestBillingProducts()
    {
        NPBinding.Billing.RequestForBillingProducts(NPSettings.Billing.Products);

        // At this point you can display an activity indicator to inform user that task is in progress
    }

    private void OnEnable()
    {
        // Register for callbacks
        Billing.DidFinishRequestForBillingProductsEvent += OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent += OnDidFinishTransaction;

        RequestBillingProducts();
    }

    private void OnDisable()
    {
        // Deregister for callbacks
        Billing.DidFinishRequestForBillingProductsEvent -= OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent -= OnDidFinishTransaction;
    }

    private void OnDidFinishProductsRequest(BillingProduct[] _regProductsList, string _error)
    {
        // Hide activity indicator

        // Handle response
        if (_error != null)
        {
            // Something went wrong

        }
        else
        {
            // Inject code to display received products
            if (_regProductsList != null && _regProductsList.Length != 0)
            {
                CustomStart(_regProductsList);
            }

        }
    }

    public void PurchaseProduct()
    {
        BuyItem(NPSettings.Billing.Products[8]);
    }

    public void BuyItem(BillingProduct _product)
    {
        if (NPBinding.Billing.IsProductPurchased(_product.ProductIdentifier))
        {
            // Show alert message that item is already purchased

            return;
        }

        // Call method to make purchase
        NPBinding.Billing.BuyProduct(_product);

        // At this point you can display an activity indicator to inform user that task is in progress

    }

    private void OnDidFinishTransaction(BillingTransaction _transaction)
    {
        popUp.StopPopUp();

        if (_transaction != null)
        {
            if (_transaction.VerificationState == eBillingTransactionVerificationState.SUCCESS)
            {
                if (_transaction.TransactionState == eBillingTransactionState.PURCHASED)
                {
                    Utilities.AddHints(10);
                    sortOrder.UpdateTexts();
                }
            }
        }
    }
}
