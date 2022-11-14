using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Assets;



public class Tutorial_ScrollView : MonoBehaviour {

	public GameObject Button_Template;

	    Singleton singleton;



	// Use this for initialization
	void Start () {
	
		
		init();

		        singleton = Singleton.Instance;

		//start coroutine to update the list
		StartCoroutine(checkupdate());
	}
	
	public void ButtonClicked(string str)
	{
		Debug.Log(str + " button clicked.");

	}


	  void init() {
        foreach (JObject player in SharedResources.players)
        {
            GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			Tutorial_Button TB = go.GetComponent<Tutorial_Button>();
			TB.SetName(player);
			go.transform.SetParent(Button_Template.transform.parent);
        }
        
    }


	 void Update() {
        
		// call the Ienumerator function


    }


	//listens for updates to the players list in another thread
	IEnumerator checkupdate() {
		while (true) {
			yield return new WaitForSeconds(1);
			//check if the list in shared resources has been updated
			if (SharedResources.playersUpdated) {
				//if it has, update the list
				SharedResources.playersUpdated = false;
				foreach (Transform child in Button_Template.transform.parent) {
					if (child.gameObject != Button_Template) {
						Destroy(child.gameObject);
					}
				}
				//generate the list
				init();
			}
		}
	}
	
	
		

}




