using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Collections.Generic;
using System.Collections;
using System;
using Firebase.Extensions; // for ContinueWithOnMainThread


public class DatabaseManager : MonoBehaviour
{
    private string userID;
    public TMPro.TMP_InputField Score;
    private DatabaseReference dbReference;
    public TMPro.TMP_Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database.
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateUser()
    {
       // userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //User newUser = new User(int.Parse(Score.text));
        //string json = JsonUtility.ToJson(newUser);
        //dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
    }
    
    public void GetScoreData()
    {
        /*
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("units").GetValueAsync().ContinueWithOnMainThread(task =>
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("units").GetValueAsync().ContinueWithOnMainThread(task =>

        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError("Get Score Faulted: " + task.Exception);

            }
            else if (task.IsCompleted)
            {
                //Debug.Log("Get Score Success: " + task.Exception);

               DataSnapshot snapshot = task.Result;

                // Do something with snapshot...
                //Debug.Log("Score" + snapshot.Value.ToString());

                foreach (DataSnapshot unitName in snapshot.Children)
                {
                    //Debug.Log(unitName.Key);
                    foreach (DataSnapshot eachUnit in unitName.Children)
                    {
                        Debug.Log(unitName.Key + "  " + "Level: " + eachUnit.Child("lvl").Value.ToString());
                    }

                }
            }
        });
        */
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        dbReference.Child("user").Child(userID).Child("currency").Child("coin").SetValueAsync(999);

    }

}