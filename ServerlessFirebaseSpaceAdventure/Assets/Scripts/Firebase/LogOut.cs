using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
public class LogOut : MonoBehaviour
{
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private GameObject UserMenu;
    
    public void LogOutClick()
    {
        StartMenu.SetActive(false);
        UserMenu.SetActive(true);
        FirebaseAuth.DefaultInstance.SignOut();
    }
}
