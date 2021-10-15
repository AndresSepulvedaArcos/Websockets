using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NetworkClient  
{
    public string networkID;
     public string name;
    
}

[System.Serializable]
public class Player
{
    public NetworkClient networkClient;
    public FVector2 position;
}
