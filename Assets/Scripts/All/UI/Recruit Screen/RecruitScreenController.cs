using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RecruitScreenController : MonoBehaviour
{
    public GameObject[] bannerSelection;
    public GameObject bannerPanel, resultPanel;
    public Transform content;

    public void OnBackButton()
    {
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        resultPanel.SetActive(false);
        bannerPanel.SetActive(true);
    }

    public void OnBanner(GameObject bannerSelect)
    {
        foreach (GameObject b in bannerSelection)
        {
            b.SetActive(false);
        }
        bannerSelect.SetActive(true);
    }
}
