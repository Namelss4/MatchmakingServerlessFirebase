using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
public class Auth : MonoBehaviour
{
    private DatabaseReference dataBaseReference;
    private FirebaseAuth auth;
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += AuthStateChange;
        dataBaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
    }
    public void ChangeAuthState()
    {
        Debug.Log("Auth State  Changed void");
        
    }

    public void AuthStateChange(object sender, EventArgs e)
    {
        Debug.Log("Auth State Changed");
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            Debug.Log("Authenticated");
            SceneManager.LoadScene(1);
            Debug.Log("Changing Scene...");
            /*if(auth.CurrentUser != null)
            {
                Debug.Log("User ID: " + auth.CurrentUser.UserId);
                Debug.Log("User Email: " + auth.CurrentUser.Email);
                Debug.Log("User Display Name: " + auth.CurrentUser.DisplayName);
                Debug.Log("User Photo URL: " + auth.CurrentUser.PhotoUrl);
                
                if(dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").GetValueAsync().Result.Value != null)
                {
                    Debug.Log("score: " + dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").GetValueAsync().Result.Value.ToString());
                    if (int.Parse(dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").GetValueAsync().Result.Value.ToString()) < amount)
                    {
                        dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").SetValueAsync(amount);
                    }
                }
                else
                {
                    dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").SetValueAsync(amount);
                }
            }
            else
            {
                Debug.LogError("User is null");
            }*/
            
        }
        else
        {
            Debug.Log("Not Authenticated");
        }
    }
    void OnDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= AuthStateChange;
    }
}
