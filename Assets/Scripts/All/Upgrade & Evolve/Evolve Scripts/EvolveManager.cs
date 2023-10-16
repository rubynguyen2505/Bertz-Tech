using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvolveManager : MonoBehaviour
{
    public static EvolveManager instance;

    [HideInInspector]
    public static Card card;

    public GameObject character;
    public int coins;

    [Header("UI")]
    public TextMeshProUGUI charaName;
    public TextMeshProUGUI lv, maxHp, atk, def, btnText, coinsUI;
    public GameObject notEnoughCoin;
    public GameObject detail;

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
        }
        notEnoughCoin.SetActive(false);
        coinsUI.text = "Coins : " + coins.ToString();
    }

    public void SetUI()
    {
        //check if detail is already active or not
        if (!detail.activeSelf)
        {
            detail.SetActive(true);
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
        if (card.lv == card.maxLv)
            btnText.text = "Evolve  (Cost : <color=yellow>" + cost + "</color>)";
        else
            btnText.text = "<color=red>Not Max LVL</color>";
    }

    public void Evolve()
    {
        if (card.lv == card.maxLv)
        {
            if (coins >= cost)
            {
                coins -= cost;
                card.lv = 1;
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
