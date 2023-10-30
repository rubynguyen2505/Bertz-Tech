using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System.Collections.Generic;
using System.Collections;
using System;
using Firebase.Extensions; // for ContinueWithOnMainThread


public class LevelScores : MonoBehaviour
{
    private string userID;
    private DatabaseReference dbReference;
    public int score;
    public string level;

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

    public int getLevelscore(string level)
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
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
        return score;
    }

    public void setLevelscore(string level, int score)
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        dbReference.Child("user").Child(userID).Child("levelscores").Child("level" + level).SetValueAsync(score);
        Debug.Log("Set level Score");
    }
}
