using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using Assets;

public class NewMatch : MonoBehaviour
{

    //text label variables
    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_ItemPrefab;
    [SerializeField] private TMP_Text player;
    [SerializeField] private Button loginButton;
    
    Singleton singleton;



     
    void init() {
        foreach (JObject player in SharedResources.players)
        {
            var item_go = Instantiate(m_ItemPrefab, m_ContentContainer);
            // do something with the instantiated item -- for instance
            if (item_go.TryGetComponent<PlayerTemplateScript>( out PlayerTemplateScript item))
            {
                item.SetPlayer(player);
                
            }    

            

         
        
            //parent the item to the content container
            item_go.transform.SetParent(m_ContentContainer);
            //reset the item's scale -- this can get munged with UI prefabs
            item_go.transform.localScale = Vector2.one;
                 
            
        }
        
    }

           
    void Start()
    {
    
    
        singleton = Singleton.Instance;
       
        init();

        loginButton.onClick.AddListener(doAddPlayer);

        
    
    }

    void Update() {
        if (singleton.updatePlayers()) {
            //clear the list
            foreach (Transform child in m_ContentContainer) {
                Destroy(child.gameObject);
            }
            //generate the list
            init();
        }
        else {
            Debug.Log("Players not updated");
        }
    }

    public void doAddPlayer()
    {
        player.text = SharedResources.selectedPlayer;
    }
    
    

    
   

   


    
}
