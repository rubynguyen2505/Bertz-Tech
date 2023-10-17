using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntEleChar : MonoBehaviour
{
    public Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    
    public void onMouseClick()
    {
        myAnimator.SetTrigger("Click");
    }
}
