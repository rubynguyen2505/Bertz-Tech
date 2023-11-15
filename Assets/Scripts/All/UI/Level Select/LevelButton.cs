using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;

public class LevelButton : MonoBehaviour
{
    [Header ("Active Stuffs")]
    public bool isActive;
    private Image buttonImage;
    private Button myButton;
    public GameObject Lock;
    private int starsActive;

    [Header ("Level UI")]
    public Image[] stars;
    public Image[] starsNone;
    public TextMeshProUGUI levelText;
    public int level;
    public GameObject confirmPanel;
    

    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
        LoadData();
        ActivateStar();
        ShowLevel();
        DecideSprite();
    }

    void LoadData()
    {
        //Is GameData present?
        if (gameData != null)
        {
            //Decide if the level is active
            if (gameData.saveData.isActive[level - 1])
            {
                isActive = true;
            }
            else
            {
                isActive = false;
            }
            //Decide how many stars to activate
            starsActive = gameData.saveData.stars[level - 1];
        }
    }

    void ActivateStar()
    {
        for (int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }
    void ActivateStarsNone()
    {
        for (int i = 0; i < starsActive; i++)
        {
            starsNone[i].enabled = true;
        }
    }

    void DeactivateStarsNone()
    {
        for (int i = 0; i < 3; i++)
        {
            starsNone[i].enabled = false;
        }
    }

    void DecideSprite()
    {
        if (isActive)
        {
            myButton.enabled = true;
            Lock.SetActive(false);
            levelText.enabled = true;
            ActivateStarsNone();
        }
        else
        {
            myButton.enabled = false;
            Lock.SetActive(true);
            levelText.enabled = false;
            DeactivateStarsNone();
        }
    }

    void ShowLevel()
    {
        levelText.text = "" + level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConfirmPanel(int level)
    {
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
