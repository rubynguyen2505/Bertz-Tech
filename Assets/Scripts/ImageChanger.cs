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
    public void DisableGameObject()
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

    /*public void EnableGameObject()
    {
        if (temp == 1)
            targetGameObject.SetActive(true);
        else if (temp == 2)
            targetGameObject2.SetActive(true);
        else if (temp == 3)
            targetGameObject3.SetActive(true);
        else if (temp == 4)
            targetGameObject4.SetActive(true);
        else
            targetGameObject.SetActive(true);
    }

    public void ToggleGameObject()
    {
        if (temp == 1)
            if (targetGameObject.activeSelf)
                DisableGameObject();
            else
                EnableGameObject();
        else if (temp == 2)
            if (targetGameObject2.activeSelf)
            DisableGameObject();
        else
            EnableGameObject();
        else if (temp == 3)
            if (targetGameObject3.activeSelf)
                DisableGameObject();
            else
                EnableGameObject();
        else if (temp == 4)
            if (targetGameObject4.activeSelf)
                DisableGameObject();
            else
                EnableGameObject();
        else
             if (targetGameObject.activeSelf)
            DisableGameObject();
        else
            EnableGameObject();
    }*/
    
    void OnEnable()
    {

        DisableGameObject();
    }

}
