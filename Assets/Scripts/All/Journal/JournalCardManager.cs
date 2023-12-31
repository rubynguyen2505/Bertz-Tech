using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class JournalCardManager : MonoBehaviour
{
    public Card card;
    [SerializeField] private Image clippedSprite;
    [SerializeField] private Image typeBG;
    [SerializeField] private TextMeshProUGUI nameText;
    Button button;
    JournalManager journalManager; 
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        journalManager = FindObjectOfType<JournalManager>();
        clippedSprite.sprite = card.charFull;
        typeBG.sprite = card.journalBG;
        nameText.text = card.charaName;

        button.onClick.AddListener(Selected);
    }

    // Update is called once per frame
    void Selected()
    {
        JournalManager.card = card;
        journalManager.SetUI();
    }
}
