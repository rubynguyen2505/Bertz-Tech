using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject profilePanel;

    [Header("Profile Page Player's Stats")]
    [SerializeField] private Slider profileEXPSlider;

    [Header("Home Screen Player's Stats")]
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    private int playerLevel;
    private int currentEXP;
    private float currentvelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 1;
        levelText.text = playerLevel.ToString();
        currentEXP = 0;
        experienceSlider.value = currentEXP;
        profileEXPSlider.value = currentEXP;
    }

    // Update is called once per frame
    void Update()
    {
        experienceSlider.value = Mathf.SmoothDamp(experienceSlider.value, currentEXP, ref currentvelocity, 10 * Time.deltaTime);
        if (currentEXP >= experienceSlider.maxValue)
        {
            playerLevel++;
            levelText.text = playerLevel.ToString();
            currentEXP = currentEXP - (int)experienceSlider.maxValue;
            experienceSlider.maxValue = (int)(experienceSlider.maxValue * (1.0f + playerLevel * 0.1f));
            experienceSlider.value = currentEXP;
        }
    }

    public void GainExperience()
    {
        currentEXP += 50;
    }

    public void OnProfilePage()
    {
        if (profilePanel.activeInHierarchy)
        {
            profilePanel.SetActive(false);
        }
        else
        {
            profilePanel.SetActive(true);
        }
    }
}
