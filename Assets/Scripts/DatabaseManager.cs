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
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        User newUser = new User(int.Parse(Score.text));
        string json = JsonUtility.ToJson(newUser);
        dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
    }
    
    public void GetScoreData()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("score").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError("Get Score Faulted: " + task.Exception);

            }
            else if (task.IsCompleted)
            {
                Debug.LogError("Get Score Success: " + task.Exception);

                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...
                Debug.LogError("Score" + snapshot.Value.ToString());
                
            }
        });
    }

}