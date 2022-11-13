using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json.Linq;
using Assets;

public class Singleton : MonoBehaviour
{
    private static Singleton instance;
    public WebSocket socket  = new WebSocket ("ws://localhost:8080/");

    public static Singleton Instance
    {

        get { 
            return instance ?? (instance = new GameObject("Singleton").AddComponent<Singleton>()); 
        }
    }

    public void Awake() {
        
        socket.Connect();

        updatePlayers();

    }


    //each time a new player logs in or logs out, update the players list
    public bool updatePlayers() {
        socket.OnMessage += (sender, e) => {
            string response = e.Data;
            JObject jsonResponse = JObject.Parse(response);
            string control = jsonResponse["control"].ToString();
            if (control == "players") {
                SharedResources.players = (JArray) jsonResponse["data"];
            }
        };
        return true;
    }




    public void KillConnection() {
        socket.Close();
    }




    
    




    

  
}