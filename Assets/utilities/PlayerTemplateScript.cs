using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Linq;
using Assets;
using UnityEngine.EventSystems;

public class PlayerTemplateScript : MonoBehaviour

   
{

    [SerializeField] private Button playerButton;


    public void SetPlayer(JObject player)
    {
        //set the player's name in the text label
        playerButton.GetComponentInChildren<TMP_Text>().text = player["username"].ToString();
        if (player["status"].ToString() == "available") {
            //set button color to green
            playerButton.interactable = true;
            playerButton.image.color = Color.green;
            
        }
        else {
            playerButton.image.color = Color.red;
            //make button unclickable
            playerButton.interactable = false;
        }
    }


    public string getPlayerName() {
        // return 
        return playerButton.GetComponentInChildren<TMP_Text>().text.ToString();
    }
    
   

    //when the item is clicked change the color of the background and the text
    public void test()
    {
        //change the button color to blue
        playerButton.image.color = Color.blue;
    }
    
    void Start()
    {
        //on click, change the color of the button to blue
        playerButton.onClick.AddListener(test);
        
    }
    void Update()
    {
        //on click, change the color of the button to blue
        playerButton.onClick.AddListener(test);
        
    }
}
