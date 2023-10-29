
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using TMPro;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private GachaRate[] gacha;
    public Card[] _reward;
    [SerializeField] private Transform parent, pos;
    [SerializeField] private GameObject characterCardGO;
    public GameObject bannerPanel, resultPanel;
    GameObject characterCard;
    RecruitCardManager card;

    //Rate Up Gacha
    [SerializeField] private Card[] rateUpReward;
    [Range(1, 10)][SerializeField] private int rateUpRate;

    //Soft Pity
    [SerializeField] private int LegendarySoftPity;
    private int[] NormalRate;

    //Guaranteed
    private int guaranteedPull = 60;
    [SerializeField] private TextMeshProUGUI pullLeft;

    private void Start()
    {
        //soft pity
        NormalRate = new int[gacha.Length];
        for (int i = 0; i < gacha.Length; i++)
        {
            NormalRate[i] = gacha[i].rate;
        }

        //guaranteed
        pullLeft.text = "Guaranteed in " + guaranteedPull + " Pulls";
    }

    private void Update()
    {
        pullLeft.text = "Guaranteed in " + guaranteedPull + " Pulls";
    }

    public void Gacha()
    {
        GuaranteedGachaPull();
        //SoftPityGacha();
    }

    #region Simple Gacha
    public void SimpleGacha()
    {
        if (characterCard == null)
        {
            characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<RecruitCardManager>();
            
        }
        int rnd = UnityEngine.Random.Range(1, 101);
        Debug.Log(rnd);
        for (int i = 0; i < gacha.Length; i++)
        {
            if (rnd <= gacha[i].rate)
            {
                card.card = Reward(gacha[i]._rarity);
                return;
            }
        }
    }
    #endregion

    #region Rate Up Gacha
    void RateUpGacha()
    {
        if (characterCard == null)
        {
            characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<RecruitCardManager>();
        }

        int rnd = UnityEngine.Random.Range(1, 101);
        for (int i = 0; i < gacha.Length; i++)
        {
            if (rnd <= gacha[i].rate)
            {
                card.card = RatesUpReward(gacha[i]._rarity);
                return;
            }
        }
    }
    #endregion

    #region Gacha w/ Soft Pity
    void SoftPityGacha()
    {
        bannerPanel.SetActive(false);
        resultPanel.SetActive(true);
        if (characterCard == null)
        {
            characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<RecruitCardManager>();
        }

        int rnd = UnityEngine.Random.Range(1, 101);
        for (int i = 0; i < gacha.Length; i++)
        {
            if (rnd <= gacha[i].rate)
            {
                Debug.Log(gacha[i].rarity);
                if (gacha[i]._rarity != Rarity.Legendary)
                {
                    AddRate();
                }
                else
                {
                    refreshGachaRate();
                }
                card.card = Reward(gacha[i]._rarity);
                return;
            }
        }
    }
    #endregion

    #region Guaranteed Pull
    void GuaranteedGachaPull()
    {
        bannerPanel.SetActive(false);
        resultPanel.SetActive(true);
        if (characterCard == null)
        {
            characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<RecruitCardManager>();
        }

        int rnd = UnityEngine.Random.Range(1, 101);
        for (int i = 0; i < gacha.Length; i++)
        {
            if (rnd <= gacha[i].rate)
            {
                if (gacha[i]._rarity != Rarity.Legendary)
                    guaranteedPull--;
                else
                    guaranteedPull = 60;

                if (guaranteedPull == 0)
                {
                    Debug.Log("Legendary");
                    guaranteedPull = 60;
                    card.card = Reward(Rarity.Legendary);
                    return;
                }
                Debug.Log(gacha[i].rarity);
                card.card = Reward(gacha[i]._rarity);
                return;
            }
        }
    }
    #endregion

    #region Multiple Pulls
    public void MultiplePullsGacha(int pulls)
    {
        bannerPanel.SetActive(false);
        resultPanel.SetActive(true);
        for (int i = 0; i < pulls; i++)
        {
            characterCard = Instantiate(characterCardGO, pos.position, Quaternion.identity) as GameObject;
            characterCard.transform.SetParent(parent);
            characterCard.transform.localScale = new Vector3(1, 1, 1);
            card = characterCard.GetComponent<RecruitCardManager>();

            int rnd = UnityEngine.Random.Range(1, 101);
            for (int j = 0; j < gacha.Length; j++)
            {
                if (rnd <= gacha[j].rate)
                {
                    if (gacha[j]._rarity != Rarity.Legendary)
                        guaranteedPull--;
                    else
                        guaranteedPull = 60;

                    if (guaranteedPull == 0)
                    {
                        Debug.Log("Legendary");
                        guaranteedPull = 60;
                        card.card = Reward(Rarity.Legendary);
                        break;
                    }
                    Debug.Log(gacha[j].rarity);
                    card.card = Reward(gacha[j]._rarity);
                    break;
                }
            }
        }
    }
    #endregion

    void AddRate()
    {
        for (int i = 0; i < gacha.Length; i++)
        {
            if (gacha[i].rate != 100)
                gacha[i].rate += LegendarySoftPity;
        }
    }

    void refreshGachaRate()
    {
        for (int i = 0; i < gacha.Length; i++)
        {
            gacha[i].rate = NormalRate[i];
        }
    }

    public int Rates(Rarity rarity)
    {
        GachaRate gr = Array.Find(gacha, rt => rt._rarity == rarity);
        if (gr != null)
        {
            return gr.rate;
        }
        else
        {
            return 0;
        }
    }

    Card Reward(Rarity rarity)
    {
        Card[] reward = Array.FindAll(_reward, r => r.rarity == rarity);

        int rnd = UnityEngine.Random.Range(0, reward.Length);

        return reward[rnd];
    }

    Card RatesUpReward(Rarity rarity)
    {
        int rnd;
        Card[] reward = Array.FindAll(_reward, r => r.rarity == rarity);

        Card[] RateUpReward = Array.FindAll(rateUpReward, r => r.rarity == rarity);

        if (RateUpReward.Length > 0)
        {
            rnd = UnityEngine.Random.Range(1, 11);
            if (rnd <= rateUpRate)
            {
                Debug.Log("Rate up reward");
                rnd = UnityEngine.Random.Range(0, RateUpReward.Length);
                return RateUpReward[rnd];
            }
        }
        rnd = UnityEngine.Random.Range(0, reward.Length);
        return reward[rnd];
    }
}

[CustomEditor(typeof(GachaManager))]
public class GachaEditor : Editor
{
    public int Rare, Epic, Legendary;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        GachaManager gm = (GachaManager)target;

        Rare = EditorGUILayout.IntField("Rare", (gm.Rates(Rarity.Rare) - gm.Rates(Rarity.Epic)));
        Epic = EditorGUILayout.IntField("Epic", (gm.Rates(Rarity.Epic) - gm.Rates(Rarity.Legendary)));
        Legendary = EditorGUILayout.IntField("Legendary", (gm.Rates(Rarity.Legendary)));
    }
}

