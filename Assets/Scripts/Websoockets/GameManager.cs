using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerPrefab;
    public Dictionary<string,GameObject> playerList=new Dictionary<string, GameObject>();

    private void Awake()
    {
        Instance=this;
    }


    public void SpawnPlayer(List<Player> players)
    {

     
        for (int i = 0; i < players.Count; i++)
        {
            Player player=players[i];

           
            if (playerList.ContainsKey(player.networkClient.networkID))continue;

            Vector3 position = new Vector3(player.position.x, player.position.y, 0);

            GameObject obj = Instantiate(playerPrefab, position, Quaternion.identity);
            obj.GetComponent<PlayerNetworkController>().InitializePlayer(player);

            playerList.Add(player.networkClient.networkID,obj);
        }
         

    }

    public void OnPlayerDisconnect(NetworkClient networkClient)
    {
        if(playerList.ContainsKey(networkClient.networkID))
        {
            Destroy(playerList[networkClient.networkID]);

        }
    }

}
