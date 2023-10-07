using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvolveSlot : MonoBehaviour
{
    public Card card;
    public GameObject cardImage;

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
            cardImage.SetActive(true);
            _cardImage.sprite = card.image;
        }
        else
        {
            cardImage.SetActive(false);
            _cardImage.sprite = null;
        }
    }
}
