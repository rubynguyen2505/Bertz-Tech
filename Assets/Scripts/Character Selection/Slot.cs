using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Card card;
    public GameObject nameBG, cardImage;
    public TextMeshProUGUI nameText;

    [HideInInspector]
    public int cardIdx;

    Image _cardImage;

    // Start is called before the first frame update
    void Start()
    {
        _cardImage = cardImage.GetComponent<Image>();
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
            _cardImage.sprite = card.image;
            nameText.text = card.charaName;
        }
        else
        {
            nameBG.SetActive(false);
            cardImage.SetActive(false);
            _cardImage.sprite = null;
        }
    }
}
