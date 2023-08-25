using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AKCS
{
    public class CharacterManager : MonoBehaviour
    {
        public Cards[] cards;//
        [SerializeField] private GameObject cardsUI;
        [SerializeField] private Transform parent;
        public GameObject SelectionCanvas;

        public List<GameObject> cardGO = new List<GameObject>(); //
        public List<CardsManager> listCardManager = new List<CardsManager>(); //
        public List<Cards> listCards = new List<Cards>(); //
        GameObject _cardsUI; 
        CardsManager cardManager;

        void Awake()
        {
            cardGO.Clear();
            listCardManager.Clear();
            listCards.Clear();
            
            SelectionCanvas.SetActive(true);
            cards = Resources.LoadAll<Cards>("Cards"); 
            for(int i=0;i<cards.Length;i++)
            {
                if(cards[i].unlocked)
                {
                    _cardsUI = Instantiate(cardsUI, parent.position, Quaternion.identity) as GameObject;
                    _cardsUI.transform.localScale = new Vector3(1,1,1);
                    _cardsUI.transform.SetParent(parent);
                    cardManager = _cardsUI.GetComponent<CardsManager>();
                    cardManager.card = cards[i];

                    cardGO.Add(_cardsUI);
                    listCardManager.Add(cardManager);
                    listCards.Add(cards[i]);
                }
            }
            SelectionCanvas.SetActive(false);
        }
    }   
}

