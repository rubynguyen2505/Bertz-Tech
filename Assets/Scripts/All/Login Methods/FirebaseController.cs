using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Database;


public class FirebaseController : MonoBehaviour
{
    public GameObject titleScreen, loginPanel, signupPanel, profilePanel, forgetPasswordPanel, notificationPanel, dbTestPanel;
    public TMPro.TMP_InputField loginEmail, loginPassword, signupEmail, signupPassword, signupCPassword, signupUserName, forgetPassEmail;
    public Toggle rememberMe;
    public TMPro.TMP_Text profileUserName_Text, profileUserEmail_Text, notifTitle_Text, notifMessage_Text;

    private string userID;
    private bool isSignedIn;
    private DatabaseReference dbReference;




    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    bool isSignIn = false;

    void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                InitializeFirebase();

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }


    public void OpenLoginPanel()
    {
        if (isSignIn == true)
        {
            titleScreen.SetActive(false);
            profilePanel.SetActive(true);
            loginPanel.SetActive(false);
        }
        else
        {
            titleScreen.SetActive(false);
            loginPanel.SetActive(true);
            signupPanel.SetActive(false);
            profilePanel.SetActive(false);
            forgetPasswordPanel.SetActive(false);
        }
    }
    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(true);
        profilePanel.SetActive(false);
        //forgetPasswordPanel.SetActive(false);
    }
    public void OpenProfilePanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(true);
        //forgetPasswordPanel.SetActive(false);
    }
    
    public void OpenForgetPassPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }
    public void CloseLoginPanel()
    {
        loginPanel.SetActive(false);
        signupPanel.SetActive(false);
        profilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
        titleScreen.SetActive(true);
    }
    public void OpendbTestPanel()
    {
        dbTestPanel.SetActive(true);
    }

    public void ExitdbTestPanel()
    {
        dbTestPanel.SetActive(false);
    }
    
    public void LoginUser()
    {
        if(string.IsNullOrEmpty(loginEmail.text)&&string.IsNullOrEmpty(loginPassword.text))
        {
            showNotifcationMessage("Error", "Field Empty!");
            return;
        }
        // Do Login
        SignInUser(loginEmail.text, loginPassword.text);
    }
    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(signupEmail.text) && string.IsNullOrEmpty(signupPassword.text) && string.IsNullOrEmpty(signupCPassword.text) && string.IsNullOrEmpty(signupUserName.text))
        {
            showNotifcationMessage("Error", "Field Empty!");
            return;
        }
        // Do Signup
        CreateUser(signupEmail.text, signupPassword.text, signupUserName.text);
    }

    
    public void forgetPass()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotifcationMessage("Error", "Field Empty!");
            return;
        }
        ForgetPasswordSubmit(forgetPassEmail.text);
    }

    public void Logout()
    {
        isSignIn = false;
        auth.SignOut();
        profileUserEmail_Text.text = "";
        profileUserName_Text.text = "";
        OpenLoginPanel();
    }
    

    void CreateUser(string email, string password, string Username)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        //return GetErrorMessage(errorCode);
                        showNotifcationMessage("Error", GetErrorMessage(errorCode));
                    }
                }

              
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            //User to Database
            userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            User newUser = new User(userID);
            string json = JsonUtility.ToJson(newUser);
            dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);

            //add default currency
            dbReference.Child("user").Child(userID).Child("currency").Child("coins").SetRawJsonValueAsync("0");
            dbReference.Child("user").Child(userID).Child("currency").Child("gems").SetRawJsonValueAsync("0");

            //add level score
            dbReference.Child("user").Child(userID).Child("levelscores").Child("level0").SetRawJsonValueAsync("0");


            UpdateUserProfile(Username);
            updateUsername(Username);
        });
    }
    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        //return GetErrorMessage(errorCode);
                        showNotifcationMessage("Error", GetErrorMessage(errorCode));
                    }
                }

                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            profileUserName_Text.text = "" + result.User.DisplayName;
            profileUserEmail_Text.text = "" + result.User.Email;

            OpenProfilePanel();
        });
    }



    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);

                isSignIn = true;
                
            }
        }
    }


    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }

    void UpdateUserProfile(string UserName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = UserName,
                PhotoUrl = new System.Uri("https://dummyimage.com/300"),
            };
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");

                // shownotificatyionMessage("Alert", " Account Succesfully Created")
            });
        }
    }

    bool isSigned = false;

    void Update()
    {
       
        if(isSignIn)
        {
            if(!isSigned)
            {
                isSigned = true;
                profileUserName_Text.text = "" + user.DisplayName;
                profileUserEmail_Text.text = "" + user.Email;
                //OpenLoginPanel();
            }
        }
       
    }

    private static string GetErrorMessage(AuthError errorCode)
    {
        var message = "";
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                message = "Account Does NOT Exist";
                break;
            case AuthError.MissingPassword:
                message = "Missing Password";
                break;
            case AuthError.WeakPassword:
                message = "Weak Password";
                break;
            case AuthError.WrongPassword:
                message = "Wrong Password";
                break;
            case AuthError.EmailAlreadyInUse:
                message = "Email Already In Use";
                break;
            case AuthError.InvalidEmail:
                message = "Invalid Email";
                break;
            case AuthError.MissingEmail:
                message = "Missing Email";
                break;
            default:
                message = "Invalid Email Or Password";
                break;
        }
        return message;
    }

    void ForgetPasswordSubmit(string forgetPasswordEmail)
    {
        auth.SendPasswordResetEmailAsync(forgetPasswordEmail).ContinueWithOnMainThread(task=>{
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordEmailAsync was canceled");
            }
            if (task.IsFaulted)
            {
                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        //return GetErrorMessage(errorCode);
                        showNotifcationMessage("Error", GetErrorMessage(errorCode));
                    }
                }
            }
            showNotifcationMessage("Alert","Success Send Email for Reset Password");
        });
    }

    public void SignInAnon()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);

                foreach (Exception exception in task.Exception.Flatten().InnerExceptions)
                {
                    Firebase.FirebaseException firebaseEx = exception as Firebase.FirebaseException;
                    if (firebaseEx != null)
                    {
                        var errorCode = (AuthError)firebaseEx.ErrorCode;
                        //return GetErrorMessage(errorCode);
                        showNotifcationMessage("Error", GetErrorMessage(errorCode));
                    }
                }
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            //User to Database
            userID = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;

            //Debug.Log(dbReference.Child("user").Child(userID).GetValueAsync().Result.Exists);
            //anon user to database
            if (dbReference.Child("user").Child(userID).GetValueAsync().Result.Exists == false)
            {
                User newUser = new User(userID);
                string json = JsonUtility.ToJson(newUser);
                dbReference.Child("user").Child(userID).SetRawJsonValueAsync(json);

                //add default currency
                dbReference.Child("user").Child(userID).Child("currency").Child("coins").SetRawJsonValueAsync("0");
                dbReference.Child("user").Child(userID).Child("currency").Child("gems").SetRawJsonValueAsync("0");

                //add level score
                dbReference.Child("user").Child(userID).Child("levelscores").Child("level0").SetRawJsonValueAsync("0");
            }

            CloseLoginPanel();
            OpendbTestPanel();
        });
    }

    private void showNotifcationMessage(string title, string messege)
    {
        notifTitle_Text.text = "" + title;
        notifMessage_Text.text = "" + messege;

        notificationPanel.SetActive(true);
    }
    public void closeNotifPanel()
    {
        notifTitle_Text.text = "";
        notifMessage_Text.text = "";
        notificationPanel.SetActive(false);

    }
    public void deleteAccount()
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        
        if (user != null)
        {
            dbReference.Child("user").Child(userID).RemoveValueAsync().ContinueWith(task => {
                if (task.IsCompleted)
                {
                    Debug.Log("User data for ID " + userID + " has been deleted.");
                }
                else if (task.IsFaulted)
                {
                    Debug.LogError("Failed to delete user data: " + task.Exception);
                }
            });
            user.DeleteAsync().ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("DeleteAsync was canceled.");
                    Debug.LogError("The user must have signed in recently in order to delete account.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("DeleteAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User deleted successfully.");
            });
        }
       
    }
    public void updateUsername(string username)
    {
        dbReference.Child("user").Child(userID).Child("username").SetValueAsync(username);

    }
}
