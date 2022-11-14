using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Newtonsoft.Json.Linq;

public class Tutorial_Button : MonoBehaviour {

	private JObject Player;
	public TMP_Text ButtonText;
	public Tutorial_ScrollView ScrollView;

	

	public void SetName(JObject player)
	{
		Player = player;
		ButtonText.text = Player["username"].ToString();
		
		if (player["status"].ToString() == "available") {
            //set button color to green
			gameObject.GetComponent<Button>().interactable = true;
			gameObject.GetComponent<Image>().color = Color.green;
			
            
        } else {
			gameObject.GetComponent<Image>().color = Color.red;
			//make button unclickable
			gameObject.GetComponent<Button>().interactable = false;
		}

		
	}


	public void Button_Click()
	{
		ScrollView.ButtonClicked(Player["username"].ToString());

	}
}
