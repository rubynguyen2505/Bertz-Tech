using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecruitCardManager : MonoBehaviour
{
    public Card card;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image frame;
    [SerializeField] private Image stars;

    private void LateUpdate()
    {
        if (card != null)
        {
            image.sprite = card.image;
            nameText.text = card.charaName;
            frame.sprite = card.itemFrame;
            stars.sprite = card.stars;
        }
    }
}
