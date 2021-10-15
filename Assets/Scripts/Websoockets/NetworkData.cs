using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public struct FNetworkPackage
{
    public string RPC;
    public NetworkClient client;
    public string data;
}
[System.Serializable]
public struct FVector2
{
    public float x;
    public float y;

    public FVector2(float X,float Y)
    {
        x=X;
        y=Y;
    }
}