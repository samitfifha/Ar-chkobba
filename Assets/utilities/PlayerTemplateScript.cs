using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
using Assets;
using UnityEngine.EventSystems;

public class PlayerTemplateScript : MonoBehaviour

   
{

    [SerializeField] private Image background;
    [SerializeField] private TMP_Text playerName;


    public void SetPlayer(JObject player)
    {
        playerName.text = player["username"].ToString();
        if (player["status"].ToString() == "available") {
            background.color = Color.green;
        }
        else {
            background.color = Color.red;
        }
    }


    public string getPlayerName() {
        // return playerName.text;
        return playerName.text;
    }
    
    public string getPlayerStatus() {
        // return playerName.text;
        return background.color.ToString();
    }

    //when the item is clicked change the color of the background and the text
    public void test()
    {
        if (background.color == Color.green) {
            //navigate to Login Scene
            SceneManager.LoadScene("Login Scene");
           
        }
        else {
           
        }
    }
}
