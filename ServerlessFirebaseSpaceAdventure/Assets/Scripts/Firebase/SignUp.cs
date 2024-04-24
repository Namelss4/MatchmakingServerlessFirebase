using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignUp : MonoBehaviour
{
    [SerializeField] private Button _registerButton;
    [SerializeField] private TMP_InputField _emailInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    private DatabaseReference dataBaseReference;

    public ErrorMessage _errorMessage;
    private void Reset()
    {
        _registerButton = GetComponent<Button>();
    }
    void Start()
    {
        _registerButton.onClick.AddListener(HandleRegisterButtonClicked);
        dataBaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        _emailInputField = GameObject.Find("InputFieldEmail").GetComponent<TMP_InputField>();
        _passwordInputField = GameObject.Find("InputFieldPassword").GetComponent<TMP_InputField>();
    }
    private void HandleRegisterButtonClicked()
    {
        if (GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text != ""&& GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text.Length>=5)
        {
            Debug.Log("Registering user...");
            StartCoroutine(Register(_emailInputField.text.ToString(), _passwordInputField.text.ToString()));
        }
        else if(GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text.Length<5)
        {
            _errorMessage.ShowErrorMessage("The username must have at least 5 letters");
        }
        else
        {
            _errorMessage.ShowErrorMessage("You must enter a username");
        }
    }

    IEnumerator Register(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
        yield return new WaitUntil(() => registerTask.IsCompleted);
        Debug.Log("Registering the user... " + auth);
        Debug.Log("Registering the user...... " + registerTask);
        if (registerTask.IsCanceled)
        {
            Debug.Log("Canceled");
        }
        else if (registerTask.IsFaulted)
        {
            FirebaseException firebaseEx = registerTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Encountered an error: " + registerTask.Exception;
            if (errorCode == AuthError.EmailAlreadyInUse)
            {
                _errorMessage.ShowErrorMessage("The email is already in use");
            }
            else if (errorCode == AuthError.WeakPassword)
            {
                // Este es el código de error cuando la contraseña es demasiado débil/shorta
                _errorMessage.ShowErrorMessage("The password is too weak");
            }
        }
        else
        {
            AuthResult result = registerTask.Result;

            Debug.LogFormat($"Firebase created successfully: {result.User.DisplayName}, {result.User.UserId}");
            string name = GameObject.Find("InputFieldUsername").GetComponent<TMP_InputField>().text;
            dataBaseReference.Child("users").Child(result.User.UserId).Child("username").SetValueAsync(name);
            dataBaseReference.Child("users").Child(result.User.UserId).Child("score").SetValueAsync(0);
            
            Debug.Log("Successful registration");
        }
    }

}
