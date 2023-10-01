using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopTabSwitcher : MonoBehaviour
{
    public GameObject CommonGoods, Recharge;
    public Button CommonGoodsButton, RechargeButton;

    void Awake()
    {
        OnCommonGoodsTab();
    }
    public void OnCommonGoodsTab()
    {
        CommonGoods.SetActive(true);
        Recharge.SetActive(false);
        CommonGoodsButton.interactable = false;
        RechargeButton.interactable = true;
    }

    public void OnRechargeTab()
    {
        CommonGoods.SetActive(false);
        Recharge.SetActive(true);
        RechargeButton.interactable = false;
        CommonGoodsButton.interactable = true;
    }
    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

}
