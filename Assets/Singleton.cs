using Assets;
using Newtonsoft.Json.Linq;
using UnityEngine;
using WebSocketSharp;

public class Singleton : MonoBehaviour
{
    private static Singleton instance;

    public WebSocket socket = new WebSocket("ws://192.168.1.8:8080/");

    public static Singleton Instance
    {
        get
        {
            return instance
                ?? (
                instance = new GameObject("Singleton").AddComponent<Singleton>()
                );
        }
    }

    public void Awake()
    {
        socket.Connect();

        updatePlayers();

        listenForMatchProposal();
        listenForMessages();
    }

    //each time a new player logs in or logs out, update the players list
    public void updatePlayers()
    {
        socket.OnMessage += (sender, e) =>
        {
            string response = e.Data;
            JObject jsonResponse = JObject.Parse(response);
            string control = jsonResponse["control"].ToString();
            if (control == "players")
            {
                SharedResources.players = (JArray) jsonResponse["data"];
                SharedResources.playersUpdated = true;
            }
        };
    }

    //listen on message of type and oarse the data
    /*
    {
    "control": "match_proposal",
    "data": {
        "game": "cirulla",
        "teams": [
            [
                {"name": "john", "type": "human"},
                {"name": "cpu1", "type": "cpu"},
            ],
            [
                {"name": "mike", "type": "human"},
                {"name": "cpu2", "type": "cpu"},
            ]
        ]
    }
}//*/
    public void listenForMatchProposal()
    {
        socket.OnMessage += (sender, e) =>
        {
            string response = e.Data;
            JObject jsonResponse = JObject.Parse(response);
            string control = jsonResponse["control"].ToString();
            if (control == "match_proposal")
            {
                SharedResources.matchProposal = jsonResponse;
                SharedResources.matchProposalUpdated = true;
            }
        };
    }

    //listen for messages that the server sends and print them
    public void listenForMessages()
    {
        socket.OnMessage += (sender, e) =>
        {
            string response = e.Data;
            JObject jsonResponse = JObject.Parse(response);
            string control = jsonResponse["control"].ToString();
            if (control == "message")
            {
                string message = jsonResponse["data"].ToString();
                Debug.Log (message);
            }
        };
    }

    public void KillConnection()
    {
        socket.Close();
    }

    public void sendMatchRequest(string message)
    {
        socket.Send (message);
    }
}
