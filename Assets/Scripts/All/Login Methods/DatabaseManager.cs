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
    public int score;
    public string level;

    
    // Start is called before the first frame update
    void Start()
    {
        // Get the root reference location of the database.
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    

    public void getLevelscore()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        level = "1";
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("levelscores").Child("level" + level).GetValueAsync().ContinueWithOnMainThread(task =>

        {
        if (task.IsFaulted)
        {
            // Handle the error...
            Debug.LogError("Get level scores Faulted: " + task.Exception);

        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            // Do something with snapshot...
            if (snapshot.Exists == false)
            {
                    Debug.Log("It is NULL");
                    score = 0;
                    Debug.Log("Get level" + level + " score: " + score);
                }
            else
            {
                    score = int.Parse(snapshot.Value.ToString());
                    Debug.Log("Get level" + level + " score: " + score);
            }
        }
        });
    }

    public void setLevelscore()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        level = "1";
        score = 999;
        dbReference.Child("user").Child(userID).Child("levelscores").Child("level" + level).SetValueAsync(score);
    }
    /*
    public void CreateUser()
    {
       // userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        //User newUser = new User(int.Parse(Score.text));
        //string json = JsonUtility.ToJson(newUser);
        //dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
    }
    
    public void GetScoreData()
    {
        
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
       
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        dbReference.Child("user").Child(userID).Child("currency").Child("coin").SetValueAsync(999);
         
    }
       */
}