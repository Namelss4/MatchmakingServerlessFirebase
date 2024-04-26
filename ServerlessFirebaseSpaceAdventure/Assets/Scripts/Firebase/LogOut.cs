using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.SceneManagement;
public class LogOut : MonoBehaviour
{
    public void LogOutClick()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        Highscore.SetAmount(0);
        SceneManager.LoadScene(0);
    }
}
