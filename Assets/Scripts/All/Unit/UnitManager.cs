using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using System;
using Firebase.Extensions;

public class UnitManager : MonoBehaviour
{
    public Card[] cards;
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private Transform parent;
    public GameObject SelectionCanvas;

    public List<GameObject> cardGO = new List<GameObject>();
    public List<UnitCardManager> unitCardListManager = new List<UnitCardManager>();
    public List<Card> cardList = new List<Card>();
    GameObject cardUI;
    UnitCardManager unitCardManager;
    private string userID;

    void Awake()
    {
        cardGO.Clear();
        unitCardListManager.Clear();
        cardList.Clear();
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("units").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError("Get Units Faulted: " + task.Exception);

            }
            else if (task.IsCompleted)
            {
                Debug.Log("Get Units Success: " + task.Exception);
                DataSnapshot snapshot = task.Result;

                SelectionCanvas.SetActive(true);
                cards = Resources.LoadAll<Card>("Character Cards");
                for (int i = 0; i < cards.Length; i++)
                {
                    if (cards[i].unlocked)
                    {
                        foreach (DataSnapshot eachUnit in snapshot.Child(cards[i].charaName).Children)
                        {
                            var clone = Instantiate(cards[i]);
                            clone.lv = Int32.Parse(eachUnit.Child("lvl").Value.ToString());
                            clone._hp = Int32.Parse(eachUnit.Child("hp").Value.ToString());
                            clone._atk = Int32.Parse(eachUnit.Child("atk").Value.ToString());
                            clone._def = Int32.Parse(eachUnit.Child("def").Value.ToString());

                            cardUI = Instantiate(cardUIPrefab, parent.position, Quaternion.identity) as GameObject;
                            cardUI.transform.localScale = new Vector3(1, 1, 1);
                            cardUI.transform.SetParent(parent);
                            unitCardManager = cardUI.GetComponent<UnitCardManager>();
                            unitCardManager.card = clone;

                            cardGO.Add(cardUI);
                            unitCardListManager.Add(unitCardManager);
                            cardList.Add(clone);
                        }
                        
                    }
                }

                SelectionCanvas.SetActive(true);
                // Do something with snapshot...
                foreach (DataSnapshot unitName in snapshot.Children)
                {
                    foreach (DataSnapshot eachUnit in unitName.Children)
                    {
                        Debug.Log(unitName.Key + " " + "Level: " + eachUnit.Child("lvl").Value.ToString());
                    }
                }

            }
        });
        
            
    }
}
