using NativeWebSocket;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    public NetworkClient networkClient;
    WebSocket webSocket;
    public string url;

    public  delegate void FNotifyServerEvent(FNetworkPackage networkPackage);
    public static event FNotifyServerEvent OnServerMessageArrive;

    private void Awake()
    {
        Instance=this;
    }
    // Start is called before the first frame update
    async void Start()
    {
        webSocket = new WebSocket(url);

        webSocket.OnMessage += WebSocket_OnMessage;
        webSocket.OnOpen += WebSocket_OnOpen;
        await webSocket.Connect();
        


    }

    private void WebSocket_OnOpen()
    {

    }

    private void WebSocket_OnMessage(byte[] data)
    {
        string serverMessage = System.Text.Encoding.UTF8.GetString(data);

        FNetworkPackage serverResponse = JsonConvert.DeserializeObject<FNetworkPackage>(serverMessage);

        if(serverResponse.RPC== "ClientLoginCallback")
        {
            networkClient=JsonConvert.DeserializeObject<NetworkClient>(serverResponse.data);
            return;
        }

        if (serverResponse.RPC == "OnPlayerSpawn")
        {
            List<Player> players = JsonConvert.DeserializeObject<List<Player>>(serverResponse.data);
             
            GameManager.Instance.SpawnPlayer(players);
            return;
        }

        if(serverResponse.RPC== "OnPlayerDisconnect")
        {
            NetworkClient networkClient= JsonConvert.DeserializeObject<NetworkClient>(serverResponse.data);
           GameManager.Instance.OnPlayerDisconnect(networkClient);
            return;

        }

        OnServerMessageArrive?.Invoke(serverResponse);


    }

   
    private async void OnDestroy()
    {
        await webSocket.Close();
    }
    void Update()
    {

        webSocket.DispatchMessageQueue();

    }

    public async void ClientLogin(string PlayerName )
    {
        FNetworkPackage networkPackage;
        networkPackage.RPC = "ClientLogin";
        networkPackage.client=  networkClient ;
        Player player=new Player();
        player.name=PlayerName;
        player.networkClient=networkClient;

        networkPackage.data = JsonConvert.SerializeObject(player);
        string jsonPost = JsonConvert.SerializeObject(networkPackage);
        await webSocket.SendText(jsonPost);
    }

    public async void SendDataToServer(string rpcName, string data)
    {
        FNetworkPackage networkPackage;
        networkPackage.RPC = rpcName;
        networkPackage.client=networkClient;
        networkPackage.data=data;
        string jsonPost=JsonConvert.SerializeObject(networkPackage);
        await webSocket.SendText(jsonPost);

    }
}
