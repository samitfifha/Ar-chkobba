using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using Assets;

public class Login : MonoBehaviour
{

    public TMP_InputField userNameField;
    public TMP_InputField passwordField;
    public Button loginButton;
    Singleton singleton;

    void Start()
    {
        //Subscribe to onClick event
        loginButton.onClick.AddListener(doLogin);
        singleton = Singleton.Instance;
    
    }

    


    public void doLogin()
    {
        //Get Username from Input
        string userName = userNameField.text;
        //Get Password from Input 
        string password = passwordField.text;

        

        //create a json object like this { control: 'login', data: { username: 'nizar', password: '123' } }
        JObject json = new JObject();
        json["control"] = "login";
        JObject data = new JObject();
        data["username"] = userName;
        data["password"] = password;
        json["data"] = data;

        //send the json object to the server
        singleton.socket.Send(json.ToString());

        //clear the input fields
        userNameField.text = "";
        passwordField.text = "";

        //listen to the server response
        singleton.socket.OnMessage += (sender, e) =>
        {
            //get the response from the server
            string response = e.Data;
            //parse the response to json
            JObject jsonResponse = JObject.Parse(response);
            //get the control value from the json
            string control = jsonResponse["control"].ToString();
            //check if the control value is login
            if (control == "logged_in")
            {
               //print the response to the console
                Debug.Log("logged in");
                // navigate to game scene
                SceneManager.LoadScene("New Match Scene");
            } else
            {
                Debug.Log("login failed");
            }
        };

         

        
        
    }
}
