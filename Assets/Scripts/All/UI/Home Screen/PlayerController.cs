using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Slider experienceSlider;
    [SerializeField] private TextMeshProUGUI levelText;

    private int playerLevel;
    private int currentEXP;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = 1;
        levelText.text = playerLevel.ToString();
        currentEXP = 0;
        experienceSlider.value = currentEXP;
    }

    // Update is called once per frame
    void Update()
    {
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
        experienceSlider.value = currentEXP;
    }
}
