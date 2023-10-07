using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;

public class ImageChanger : MonoBehaviour
{
    // Drag & Drop the gameobject in the inspector
    public GameObject[] targetGameObject;


    public int getLen()
    {
        int len = 0;
        for (int i = 0; i < targetGameObject.Length; i++)
        {
            len = i;
        }
        return len;

    }


    public int randomNum(int len)
    {
        int randomNumber;
        System.Random RNG = new System.Random();
        randomNumber = RNG.Next(0,len+1);
        return randomNumber;
    }
    public void DisplayGO()
    {
        int temp = randomNum(getLen());
        for (int i = 0; i < targetGameObject.Length; i++)
            if (i == temp){
                targetGameObject[i].SetActive(true);
            } else
            {
                targetGameObject[i].SetActive(false);
            }
            
    }

    void OnEnable()
    {

        DisplayGO();
    }

}
