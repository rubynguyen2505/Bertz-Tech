using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Extensions; // for ContinueWithOnMainThread

public class HomeCurrencyController : MonoBehaviour
{
    public int coins;
    public int gems;
    public TMP_Text coinsUI;
    public TMP_Text gemsUI;
    private string userID;
    private DatabaseReference dbReference;
    // Start is called before the first frame update
    void OnEnable()
    {
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
        GetCurrency();
    }

    public void GetCurrency()
    {
        userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        FirebaseDatabase.DefaultInstance.GetReference("user").Child(userID).Child("currency").GetValueAsync().ContinueWithOnMainThread(task =>

        {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.LogError("Get currency Faulted: " + task.Exception);

            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // Do something with snapshot...
                coins = int.Parse(snapshot.Child("coins").Value.ToString());
                Debug.Log("Get Coins:  " + coins);
                coinsUI.text = coins.ToString("D9");
                gems = int.Parse(snapshot.Child("gems").Value.ToString());
                Debug.Log("Get Gems:  " + gems);
                gemsUI.text = gems.ToString("D9");
            }
        });
    }
}
