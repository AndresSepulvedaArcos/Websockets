using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkController : MonoBehaviour
{
    public NetworkClient networkClient;
    public bool isLocalControlled;


    public void InitializePlayer(Player player)
    {
        networkClient=player.networkClient;

        isLocalControlled=networkClient.networkID==NetworkManager.Instance.networkClient.networkID;

    }

    private void OnEnable()
    {
        NetworkManager.OnServerMessageArrive += NetworkManager_OnServerMessageArrive;
    }

  
    private void OnDisable()
    {
        NetworkManager.OnServerMessageArrive -= NetworkManager_OnServerMessageArrive;

    }
    private void NetworkManager_OnServerMessageArrive(FNetworkPackage networkPackage)
    {
        if(networkPackage.RPC!= "OnPlayerMove")return;
        if(networkPackage.client.networkID!= NetworkManager.Instance.networkClient.networkID)return;


        FVector2 targetPosition=JsonConvert.DeserializeObject<FVector2>(networkPackage.data);

        transform.position=new Vector3(targetPosition.x,targetPosition.y,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MovePlayer()
    {
        if(!isLocalControlled)return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(h, v, 0);
        Vector3 targetPosition=transform.position + direction * Time.fixedDeltaTime;

        FVector2 playerPosition=new FVector2(targetPosition.x,targetPosition.y);

        
        NetworkManager.Instance.SendDataToServer("SendClientPosition", JsonConvert.SerializeObject(playerPosition));

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}
