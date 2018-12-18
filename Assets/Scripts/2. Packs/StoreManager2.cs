using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;
using TMPro;
using UnityEngine.UI;

public class StoreManager2 : MonoBehaviour
{
    public string lastSelectedProduct;

    public ExpertBundle expertBundle;
    public Button expertBundleButton;

    public TextMeshProUGUI price;
    public bool storeAccessable;

    [Space]

    public GameObject loading;

    public Animator blurAnimator;
    public Animator alertAnimator;
    public Animator restoreAnimator;

    [Space]

    public GameObject alreadyPurchased;
    public GameObject itemBought;
    public GameObject nothingToRestore;
    public GameObject itemsRestored;

    [Space]

    public TextMeshProUGUI productTextPopUp;

    //start
    public void CustomStart()
    {
        // Register for callbacks
        Billing.DidFinishRequestForBillingProductsEvent += OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent += OnDidFinishTransaction;

        // For receiving restored transactions.
        Billing.DidFinishRestoringPurchasesEvent += OnDidFinishRestoringPurchases;

        RequestBillingProducts();
    }

    public void RequestBillingProducts()
    {
        NPBinding.Billing.RequestForBillingProducts(NPSettings.Billing.Products);

        // At this point you can display an activity indicator to inform user that task is in progress
    }

    private void OnDisable()
    {
        // Deregister for callbacks
        Billing.DidFinishRequestForBillingProductsEvent -= OnDidFinishProductsRequest;
        Billing.DidFinishProductPurchaseEvent -= OnDidFinishTransaction;
        Billing.DidFinishRestoringPurchasesEvent -= OnDidFinishRestoringPurchases;
    }

    private void OnDidFinishProductsRequest(BillingProduct[] _regProductsList, string _error)
    {
        // Handle response
        if (_error != null)
        {
            // Something went wrong
            storeAccessable = false;
        }
        else
        {
            // Inject code to display received products
            if (_regProductsList != null && _regProductsList.Length != 0)
            {
                storeAccessable = true;
                price.SetText("" + _regProductsList[6].LocalizedPrice);
            }
            else
            {
                storeAccessable = false;
            }
        }

        if (!storeAccessable)
        {
            price.transform.parent.gameObject.SetActive(false);
            expertBundleButton.interactable = false;
        }
    }


    //purchase
    public void SelectProduct(string product)
    {
        lastSelectedProduct = product;

        productTextPopUp.SetText(product);
        blurAnimator.SetTrigger("Start");
        alertAnimator.gameObject.SetActive(true);
        alertAnimator.SetTrigger("Start");
    }

    public void PurchaseProduct()
    {
        BuyItem(NPSettings.Billing.Products[6]);
    }

    public void BuyItem(BillingProduct _product)
    {
        if (NPBinding.Billing.IsProductPurchased(_product))
        {
            // Show alert message that item is already purchased
            alertAnimator.transform.localScale = new Vector2(0, 0);
            alertAnimator.gameObject.SetActive(false);

            alreadyPurchased.SetActive(true);
            alreadyPurchased.transform.localScale = new Vector2(45f, 45f);

            expertBundle.PurchaseSuccessful();

            return;
        }

        // Call method to make purchase
        NPBinding.Billing.BuyProduct(_product);

        // At this point you can display an activity indicator to inform user that task is in progress

        loading.SetActive(true);
    }

    private void OnDidFinishTransaction(BillingTransaction _transaction)
    {
        loading.SetActive(false);

        if (_transaction != null)
        {
            if (_transaction.VerificationState == eBillingTransactionVerificationState.SUCCESS)
            {
                if (_transaction.TransactionState == eBillingTransactionState.PURCHASED)
                {


                    // Your code to handle purchased products
                    alertAnimator.transform.localScale = new Vector2(0, 0);
                    alertAnimator.gameObject.SetActive(false);

                    itemBought.SetActive(true);
                    itemBought.transform.localScale = new Vector2(45f, 45f);

                    expertBundle.PurchaseSuccessful();
                }
            }
        }
    }


    //restore
    public void StartRestorePurchases()
    {
        blurAnimator.SetTrigger("Start");
        restoreAnimator.gameObject.SetActive(true);
        restoreAnimator.SetTrigger("Start");
    }

    public void RestorePurchases()
    {
        NPBinding.Billing.RestorePurchases();
    }

    private void OnDidFinishRestoringPurchases(BillingTransaction[] _transactions, string _error)
    {
        Debug.Log(string.Format("Received restore purchases response. Error = {0}.", _error));

        if (_transactions != null)
        {
            Debug.Log(string.Format("Count of transaction information received = {0}.", _transactions.Length));

            foreach (BillingTransaction _currentTransaction in _transactions)
            {
                Debug.Log("Product Identifier = " + _currentTransaction.ProductIdentifier);
                Debug.Log("Transaction State = " + _currentTransaction.TransactionState);
                Debug.Log("Verification State = " + _currentTransaction.VerificationState);
                Debug.Log("Transaction Date[UTC] = " + _currentTransaction.TransactionDateUTC);
                Debug.Log("Transaction Date[Local] = " + _currentTransaction.TransactionDateLocal);
                Debug.Log("Transaction Identifier = " + _currentTransaction.TransactionIdentifier);
                Debug.Log("Transaction Receipt = " + _currentTransaction.TransactionReceipt);
                Debug.Log("Error = " + _currentTransaction.Error);
            }
            restoreAnimator.transform.localScale = new Vector2(0, 0);
            restoreAnimator.gameObject.SetActive(false);
            itemsRestored.SetActive(true);
            itemsRestored.transform.localScale = new Vector2(45f, 45f);
        }

        else
        {
            restoreAnimator.transform.localScale = new Vector2(0, 0);
            restoreAnimator.gameObject.SetActive(false);
            nothingToRestore.SetActive(true);
            nothingToRestore.transform.localScale = new Vector2(45f, 45f);
        }
    }

    //animation
    public void DeSelectProduct()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Animator>().SetTrigger("Stop");
        }
    }
}
