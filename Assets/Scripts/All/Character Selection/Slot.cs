using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Card card;
    public GameObject nameBG, cardImage, borderImage, starsImage, roleImage;
    public TextMeshProUGUI nameText;

    [HideInInspector]
    public int cardIdx;

    Image _cardImage;
    Image _borderImage;
    Image _starsImage;
    Image _roleImage;

    // Start is called before the first frame update
    void Start()
    {
        _cardImage = cardImage.GetComponent<Image>();
        _borderImage = borderImage.GetComponent<Image>();
        _starsImage = starsImage.GetComponent<Image>();
        _roleImage = roleImage.GetComponent<Image>();
    }

    private void OnEnable()
    {
        //set cardIdx to -1 if there's no card
        if (card == null)
            cardIdx = -1;

        StartCoroutine(Show());
    }

    public IEnumerator Show()
    {
        yield return new WaitForSeconds(0.2f);
        if (card != null)
        {
            nameBG.SetActive(true);
            cardImage.SetActive(true);
            borderImage.SetActive(true);
            starsImage.SetActive(true);
            roleImage.SetActive(true);
            _cardImage.sprite = card.image;
            _borderImage.sprite = card.charaFrame;
            _starsImage.sprite = card.starsAcross;
            _roleImage.sprite = card.charaRole;
            nameText.text = card.charaName;
        }
        else
        {
            nameBG.SetActive(false);
            cardImage.SetActive(false);
            borderImage.SetActive(false);
            starsImage.SetActive(false);
            roleImage.SetActive(false);
            _cardImage.sprite = null;
            _borderImage.sprite = null;
            _starsImage.sprite = null;
            _roleImage.sprite = null;
        }
    }
}
