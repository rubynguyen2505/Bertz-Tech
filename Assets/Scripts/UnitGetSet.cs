using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Collections.Generic;
using System.Collections;
using System;
using Firebase.Extensions; // for ContinueWithOnMainThread
using System.Threading.Tasks;

public class UnitGetSet : MonoBehaviour
{
    public string unitName, jsonUnitData;
    private DatabaseReference dbReference;
    private string userID;
    

    // Start is called before the first frame update
    void Start()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gets Unit from MAIN Unit List
    public async Task getUnitByKey()
    {
        unitName = "Zeus";
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        await FirebaseDatabase.DefaultInstance.GetReference("units").Child(unitName).GetValueAsync().ContinueWithOnMainThread(task =>

        {
            if (task.IsFaulted)
            {
                Debug.LogError("Get Unit Faulted: " + task.Exception);


            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    UnitData unitData = JsonUtility.FromJson<UnitData>(snapshot.GetRawJsonValue());
                    //List<UnitData> unitDataList = new List<UnitData> ();
                    //unitDataList.Add(unitData);
                    //UnitData unitData1 = new UnitData(unitData.hp, unitData.atk, unitData.def, unitData.maxlvl, unitData.lvl, unitData.role, unitData.type, unitData.stars);



                    jsonUnitData = JsonUtility.ToJson(unitData);
                    Debug.Log("JSON OUTPUT" + jsonUnitData);

                    /*
                    Debug.Log("Key: " + unitName);
                    Debug.Log("Unit Data: ");
                    Debug.Log("HP: " + unitData.hp);
                    Debug.Log("ATK: " + unitData.atk);
                    Debug.Log("DEF: " + unitData.def);
                    Debug.Log("Max Level: " + unitData.maxlvl);
                    Debug.Log("Level: " + unitData.lvl);
                    Debug.Log("Role: " + unitData.role);
                    Debug.Log("Type: " + unitData.type);
                    Debug.Log("Stars: " + unitData.stars);
                    */
                }
            }
        });
        return;
    }

    public void addUnitToList()
    {
        Debug.Log("unit name"+ unitName);
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //Dictionary<string, List<UnitData>> unitsData = new Dictionary<string, List<UnitData>>();
        dbReference.Child("user").Child(userID).Child("units").Child(unitName).Push().SetRawJsonValueAsync(jsonUnitData);
    }

    public async void getAndSet()
    {
        await getUnitByKey();
        addUnitToList();
    }

}

   