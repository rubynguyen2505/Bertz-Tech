using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using Firebase.Auth;
using System;
using UnityEngine.UI;



public class FacebookSignInManager : MonoBehaviour
{
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallBack, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    void Start()
    {
        InitFirebase();

    }
    void InitFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    private void InitCallBack()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize FB SDK");
        }
    }
    private void OnHideUnity(bool isgameshown)
    {
        if (!isgameshown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void Facebook_Login()
    {
        var permission = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(permission, AuthCallBack);
    }

    private void AuthCallBack(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            Credential credential = FacebookAuthProvider.GetCredential(accessToken.TokenString);


            authwithfirebase(credential);
        }
        else
        {
            Debug.LogError("User Cancelled login");
        }
    }
    public void authwithfirebase(Credential FBtoFirebase)
    {
        auth = FirebaseAuth.DefaultInstance;
        //Firebase.Auth.Credential credential = Firebase.Auth.FacebookAuthProvider.GetCredential(accessToken);
        auth.SignInWithCredentialAsync(FBtoFirebase).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("singin encountered error" + task.Exception);
            }
            Firebase.Auth.FirebaseUser newuser = task.Result;
            Debug.Log(newuser.DisplayName);
        });
    }
}
