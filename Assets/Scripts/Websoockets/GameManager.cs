using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerPrefab;

    private void Awake()
    {
        Instance=this;
    }


    public void SpawnPlayer(Player player)
    {
        Vector3 position=new Vector3(player.position.x,player.position.y,0);

         GameObject obj= Instantiate(playerPrefab, position,Quaternion.identity);
            obj.GetComponent<PlayerNetworkController>().InitializePlayer(player);

    }

}
