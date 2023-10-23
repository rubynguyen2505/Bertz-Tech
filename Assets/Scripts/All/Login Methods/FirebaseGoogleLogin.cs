using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UI;
using Google;
using System.Net.Http;
using Firebase.Database;

public class GoogleSigninManager : MonoBehaviour
{
    public string GoogleWebAPI = "72316320405-mnclj2u2rio6msr2tggumj8qkdtn9b8q.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public TMPro.TMP_Text UsernameTxt, UserEmailTxt;
    public GameObject LoginScreen, ProfileScreen;

    private string userID;
    private DatabaseReference dbReference;

    void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebAPI,
            RequestIdToken = true
        };
    }
    void Start()
    {
        InitFirebase();
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;

    }
    void InitFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }
    public void GoogleSignInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);

    }
    void OnGoogleAuthenticatedFinished(Task <GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Fault");
        }
        else if(task.IsCanceled)
        {
            Debug.LogError("Login Cancel");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCedentialAsync encountered an error" + task.Exception);
                    return;
                }
                user = auth.CurrentUser;

                UsernameTxt.text = user.DisplayName;
                UserEmailTxt.text = user.Email;

                //LoginScreen.SetActive(false);
                //ProfileScreen.SetActive(true);

            });
            //google user to database
            userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            if (dbReference.Child("user").Child(userID).GetValueAsync().Result.Exists == false)
            {
                User newUser = new User(userID);
                string json = JsonUtility.ToJson(newUser);
                dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);
            }
        }
    } 
}
   