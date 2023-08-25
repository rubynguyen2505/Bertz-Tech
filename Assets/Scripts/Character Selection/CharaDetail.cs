using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AKCS
{
    public class CharaDetail : MonoBehaviour
    {
        public static Cards cardDetail;
        [SerializeField] private GameObject noInfoText;
        
        [Header("Character Details")]
        [SerializeField] private GameObject detailUI;
        [SerializeField] private TextMeshProUGUI charaName, atk, hp, def, role, type;

        // Update is called once per frame
        void Update()
        {
            if(cardDetail == null)
            {
                noInfoText.SetActive(true);
                detailUI.SetActive(false);
                return;
            }
            noInfoText.SetActive(false);
            detailUI.SetActive(true);
            
            charaName.text = "NAME : " + cardDetail.name;
            atk.text = "ATK : " + cardDetail.atk;
            hp.text = "HP : " + cardDetail.hp;
            def.text = "DEF : " + cardDetail.def;
            role.text = "ROLE : " + cardDetail.role.ToString();
            type.text = "TYPE : " + cardDetail.type.ToString();
        }
    }
}
