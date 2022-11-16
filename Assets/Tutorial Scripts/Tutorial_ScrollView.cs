using System.Collections;
using System.Collections.Generic;
using Assets;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_ScrollView : MonoBehaviour
{
    public GameObject Button_Template;

    Singleton singleton;

    public GameObject panel;

    public TMP_Text panelText;

    //array of strings
    public TMP_Text player1;

    public TMP_Text player2;

    public TMP_Text player3;

    public TMP_Text player4;

    public TMP_Text player5;

    public TMP_Text player6;

    public TMP_Text player7;

    public TMP_Text player8;

    //button add to team 1
    public Button addTeam1;

    public Button addTeam2;

    public Button sendRequest;

    string matchString;

    TMP_Text[] teamA;

    TMP_Text[] teamB;

    JObject selectedPlayer;

    void Start()
    {
        init();

        singleton = Singleton.Instance;

        StartCoroutine(checkupdate());
        StartCoroutine(checkMatchProposal());

        //add the tmp text objects to the array
        teamA = new TMP_Text[] { player1, player2, player3, player4 };
        teamB = new TMP_Text[] { player5, player6, player7, player8 };

        //on team1 click add player to team1
        addTeam1.onClick.AddListener(() => addPlayerToTeam1(selectedPlayer));
        addTeam2.onClick.AddListener(() => addPlayerToTeam2(selectedPlayer));
        sendRequest.onClick.AddListener(() => sendRequestToServer());
    }

    void sendRequestToServer()
    {
        JObject json = new JObject();
        json["control"] = "new_match";
        JObject data = new JObject();
        data["game"] = "classic";
        JArray teams = new JArray();

        //add team1 players
        JArray team1 = new JArray();
        JArray team2 = new JArray();
        foreach (TMP_Text player in teamA)
        {
            if (player.text.StartsWith("Player") == false)
            {
                JObject playerObject = new JObject();
                playerObject["name"] = player.text;
                playerObject["type"] = "human";
                team1.Add (playerObject);
            }
        }
        foreach (TMP_Text player in teamB)
        {
            if (player.text.StartsWith("Player") == false)
            {
                JObject playerObject = new JObject();
                playerObject["name"] = player.text;
                playerObject["type"] = "human";
                team2.Add (playerObject);
            }
        }
        teams.Add (team1);
        teams.Add (team2);
        data["teams"] = teams;
        json["data"] = data;
        singleton.sendMatchRequest(json.ToString());
    }

    void addPlayerToTeam1(JObject player)
    {
        for (int i = 0; i < teamA.Length; i++)
        {
            //if player name starts with "player"
            if (teamA[i].text.StartsWith("Player"))
            {
                //set the text to the player that was clicked
                teamA[i].text = player["username"].ToString();

                break;
            }
        }
    }

    void addPlayerToTeam2(JObject player)
    {
        for (int i = 0; i < teamB.Length; i++)
        {
            //if player name starts with "player"
            if (teamB[i].text.StartsWith("Player"))
            {
                //set the text to the player that was clicked
                teamB[i].text = player["username"].ToString();

                break;
            }
        }
    }

    public void ButtonClicked(JObject player)
    {
        selectedPlayer = player;
    }

    void init()
    {
        foreach (JObject player in SharedResources.players)
        {
            GameObject go = Instantiate(Button_Template) as GameObject;
            go.SetActive(true);
            Tutorial_Button TB = go.GetComponent<Tutorial_Button>();
            TB.SetName (player);
            go.transform.SetParent(Button_Template.transform.parent);
        }
    }

    void Update()
    {
        // call the Ienumerator function
    }

    //listens for updates to the players list in another thread
    IEnumerator checkupdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            //check if the list in shared resources has been updated
            if (SharedResources.playersUpdated)
            {
                //if it has, update the list
                SharedResources.playersUpdated = false;
                foreach (Transform child in Button_Template.transform.parent)
                {
                    if (child.gameObject != Button_Template)
                    {
                        Destroy(child.gameObject);
                    }
                }

                //generate the list
                init();
            }
        }
    }

    //listen for march proposal in another thread
    IEnumerator checkMatchProposal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            //check if the list in shared resources has been updated
            if (SharedResources.matchProposalUpdated)
            {
                //parse the jobject match proposal and show the match in a string
                JObject matchProposal = SharedResources.matchProposal;
                matchString =
                    "Match Proposal: " +
                    matchProposal["data"]["game"].ToString() +
                    " " +
                    matchProposal["data"]["teams"][0][0]["name"].ToString() +
                    " vs " +
                    matchProposal["data"]["teams"][1][0]["name"].ToString();
                Debug.Log (matchString);
                SharedResources.matchProposalUpdated = false;

                panelText.text = matchString;

                //show hidden panel setactive
                Vibration.Vibrate(1000);

                //wait for 2 seconds
                yield return new WaitForSeconds(2);
                Vibration.Vibrate(1000);
                yield return new WaitForSeconds(2);
                Vibration.Vibrate(1000);

                //vibrate in pattern
                panel.SetActive(true);
            }
        }
    }
}
