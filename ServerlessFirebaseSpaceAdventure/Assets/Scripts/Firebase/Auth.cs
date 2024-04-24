using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
public class Auth : MonoBehaviour
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject UserMenu;
    private DatabaseReference dataBaseReference;
    private FirebaseAuth auth;
    void Start()
    {
        Debug.Log("Auth Start");
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
        dataBaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
    }

    private void HandleAuthStateChange(object sender, EventArgs e)
    {
        Debug.Log("Auth State Changed");
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            Debug.Log("Authenticated");
            StartMenu.SetActive(true);
            UserMenu.SetActive(false);
            
            if(auth.CurrentUser != null)
            {
                Debug.Log("User ID: " + auth.CurrentUser.UserId);
                Debug.Log("User Email: " + auth.CurrentUser.Email);
                Debug.Log("User Display Name: " + auth.CurrentUser.DisplayName);
                Debug.Log("User Photo URL: " + auth.CurrentUser.PhotoUrl);
                
                if(dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").GetValueAsync().Result.Value != null)
                {
                    if (int.Parse(dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").GetValueAsync().Result.Value.ToString()) < Highscore.GetAmount())
                    {
                        dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").SetValueAsync(Highscore.GetAmount());
                    }
                }
                else
                {
                    dataBaseReference.Child("users").Child(auth.CurrentUser.UserId).Child("score").SetValueAsync(Highscore.GetAmount());
                }
            }
            else
            {
                Debug.LogError("User is null");
            }
        }
    }
    void OnDestroy()
    {
        FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChange;
    }
}
