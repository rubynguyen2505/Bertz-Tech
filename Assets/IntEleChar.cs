using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntEleChar : MonoBehaviour
{
    public Animator myAnimator;
    public GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;
    private float charPerSec = 20;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        dialogueBox.SetActive(false);
    }
    
    public void onMouseClick()
    {
        myAnimator.SetTrigger("Click");
        dialogueBox.SetActive(true);
        StartCoroutine(TypeText("Hey there, Doc! See any gems that you like? I can give you a big discount if you'd allow me to go on a date with you, tee~hee~"));
        /*
        dialogueText.text = null;
        dialogueBox.SetActive(false);
        */
    }

    IEnumerator TypeText(string line)
    {
        string textBuffer = null;
        foreach (char c in line)
        {
            textBuffer += c;
            dialogueText.text = textBuffer;
            yield return new WaitForSeconds(1 /  charPerSec);
        }
        yield return new WaitForSeconds(2);
        dialogueBox.SetActive(false);
        dialogueText.text = null;
    }
}
