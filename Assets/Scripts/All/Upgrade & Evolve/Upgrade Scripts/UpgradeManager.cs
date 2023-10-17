using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [HideInInspector]
    public static Card card;

    public GameObject character;
    public int coins;

    [Header("UI")]
    public TextMeshProUGUI charaName;
    public TextMeshProUGUI lv, maxHp, atk, def, btnText, coinsUI;
    public TextMeshProUGUI _lv, _maxHp, _atk, _def;
    public GameObject notEnoughCoin;
    public GameObject detail, newDetail;

    #region change sprite and animator
    // SpriteRenderer sprite;
    // Animator animator;
    #endregion
    int cost;

    #region Instantiate GameObject
    GameObject _character;
    GameObject _oldCharacter;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        #region change sprite and animator
        // sprite = character.GetComponent<SpriteRenderer>();
        // animator = character.GetComponent<Animator>();
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        if (card == null)
        {
            #region change sprite and animator
            // sprite.sprite = null;
            // animator.runtimeAnimatorController = null;
            #endregion

            detail.SetActive(false);
            newDetail.SetActive(false);
        }
        notEnoughCoin.SetActive(false);
        coinsUI.text = "Coins : " + coins.ToString();
    }

    public void SetUI()
    {
        //check if detail is already active or not
        if (!detail.activeSelf && !newDetail.activeSelf)
        {
            detail.SetActive(true);
            newDetail.SetActive(true);
        }

        #region change sprite and animator
        // sprite.sprite = info.image;
        // animator.runtimeAnimatorController = info.animation;
        #endregion
        /*
        #region Instantiate GameObject
        if (_character == null)
        {
            _character = Instantiate(card.character, character.transform.position, Quaternion.identity) as GameObject;
            _oldCharacter = _character;
        }
        else
        {
            _character = card.character;
            if (_character != _oldCharacter)
            {
                Destroy(_oldCharacter);
                _character = Instantiate(card.character, character.transform.position, Quaternion.identity) as GameObject;
                _oldCharacter = _character;
            }
        }
        #endregion
        */
        charaName.text = card.charaName.ToString();
        lv.text = "Lvl: " + card.lv.ToString();
        maxHp.text = "Hp: " + card._hp.ToString();
        atk.text = "Atk: " + card._atk.ToString();
        def.text = "Def: " + card._def.ToString();
        coinsUI.text = "Coins: " + coins.ToString();

        cost = card.lv * 20;
        if (card.lv < card.maxLv)
        {
            btnText.text = "Upgrade  (Cost : <color=yellow>" + cost + "</color>)";
            newDetail.SetActive(true); 
            _lv.text = "Lvl: " + (card.lv + 1).ToString();
            _maxHp.text = "Hp: " + (card.hp * (card.lv + 1)).ToString();
            _atk.text = "Atk: " + (card.atk * (card.lv + 1)).ToString();
            _def.text = "Def: " + (card.def * (card.lv + 1)).ToString();
        } 
        else
        {
            btnText.text = "<color=red>MAX LV</color>";
            newDetail.SetActive(false);
        }
            
    }

    public void Upgrade()
    {
        if (card.lv < card.maxLv)
        {
            if (coins >= cost)
            {
                coins -= cost;
                card.lv++;
                card._hp = card.hp * card.lv;
                card._atk = card.atk * card.lv;
                card._def = card.def * card.lv;
                SetUI();
            }
            else
            {
                if (!notEnoughCoin.activeSelf)
                    StartCoroutine(Show());
            }
        }
    }

    IEnumerator Show()
    {
        notEnoughCoin.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        notEnoughCoin.SetActive(false);
    }
}
