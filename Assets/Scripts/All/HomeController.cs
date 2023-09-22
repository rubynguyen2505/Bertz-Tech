using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class HomeController : MonoBehaviour
{

    //public GameObject battleButton, shopButton, unitsButton, teamsButton, enhanceButton, inventoryBUtton, recruitButton;

    public void onBattleButton()
    {
        SceneManager.LoadScene("Level Select");
    }

    
    public void onShopButton()
    {
        SceneManager.LoadScene("Shop Screen");
    }

    public void onUnitsButton()
    {
        SceneManager.LoadScene("Units Screen");
    }

    public void onTeamsButton()
    {
        SceneManager.LoadScene("Teams Screen");
    }

    public void onEnhanceButton()
    {
        SceneManager.LoadScene("Enhance Screen");
    }

    public void onInventoryButton()
    {
        SceneManager.LoadScene("Inventory Screen");
    }

    public void onRecruitButton()
    {
        SceneManager.LoadScene("Recruit Screen");
    }
    

}