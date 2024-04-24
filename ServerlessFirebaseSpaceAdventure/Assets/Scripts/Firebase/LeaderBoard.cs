using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using UnityEngine.Serialization;

public class LeaderBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scores;
    private int count=0;

    public void GetUsersHighScore()
    {
        FirebaseDatabase.DefaultInstance.GetReference("users").OrderByChild("score").GetValueAsync()
            .ContinueWithOnMainThread(
                task =>
                {
                    if (task.IsFaulted)
                    {
                        Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        return;
                    }
                    if (task.IsCompleted)
                    {
                        DataSnapshot snapshot = task.Result;
                        
                        var sortedUsers = new SortedDictionary<int, string>();
                        
                        foreach (DataSnapshot childSnapshot in snapshot.Children)
                        {
                            string username = childSnapshot.Child("username").Value.ToString();
                            int score = int.Parse(childSnapshot.Child("score").Value.ToString());
                            sortedUsers[score] = username;
                        }
                        //panel.gameObject.SetActive(true);
                        foreach (var keyValuePair in sortedUsers.Reverse())
                        {
                            Debug.Log($"{keyValuePair.Value} with {keyValuePair.Key}");
                            if (count < 5)
                            {
                                scores[count].text = $"{keyValuePair.Value}: {keyValuePair.Key}\"";
                                count++;
                            }
                        }
                    }
                });
    }
}
