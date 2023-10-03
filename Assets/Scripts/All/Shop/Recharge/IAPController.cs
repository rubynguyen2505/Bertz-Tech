using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

/* TO ADD NEW PRODUCTS: Go into the IAP Catalogue and add your product under the convention "com.bertztech.elementalists.productname"
 * Adjust the price as necessary. For now, please add the item's ID as a string down below and add it to the OnPurchaseComplete method
 */
public class IAPController : MonoBehaviour
{
    private string upgradeItem = "com.BERTZTech.Elementalists.UpgradeItem";
    private string evolveItem = "com.BERTZTech.Elementalists.EvolveItem";
    [SerializeField]
    TMP_Text title;
    [SerializeField]
    TMP_Text price;
    //[SerializeField]
    //TMP_Text description;

    public void OnPurchaseComplete(UnityEngine.Purchasing.Product product)
    {
        if (product.definition.id == upgradeItem)
        {
            //This would add the item to the player's inventory
            Debug.Log("Upgrade Item granted");
        }
        else if (product.definition.id == evolveItem)
        {
            // This would add the item to the player's inventory
            Debug.Log("Evolve Item granted");
        }
    }

    //Tell user purchase failed
    public void OnPurchaseFailure(UnityEngine.Purchasing.Product product, PurchaseFailureDescription reason)
    {
        Debug.Log("Purchase failed: " + reason);
    }

    //May be used to localize title and price
    public void OnProductFetched(UnityEngine.Purchasing.Product product)
    {
        if (title.text != null)
        {
            title.text = product.metadata.localizedTitle;
        }

        if (price != null)
        {
            price.text = product.metadata.localizedPriceString;
        }
    }
}