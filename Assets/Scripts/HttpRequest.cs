using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class JsonResponse
{
    public string nombre;
    public int numero;
}

public class HttpRequest : MonoBehaviour
{
    public string url;
    public int miNumero;
    public string miNombre;
    public JsonResponse response;
    // Start is called before the first frame update
    void Start()
    {
         SendToServer();
    }

  [ContextMenu("Call Send Number")]
    async void SendToServer()
    { 
        JsonResponse obj=new JsonResponse();
        obj.nombre=miNombre;
        obj.numero=miNumero;
        string jsonPost=JsonConvert.SerializeObject(obj);
             
       
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonPost);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        UnityWebRequestAsyncOperation operation = request.SendWebRequest();
        while (!operation.isDone)
            await Task.Yield();


      //  response = JsonUtility.FromJson<JsonResponse>(request.downloadHandler.text);
        Debug.Log(request.downloadHandler.text);



    }
    async void ConnectToServer()
    {
         UnityWebRequest request=  UnityWebRequest.Get(url);
         UnityWebRequestAsyncOperation result=   request.SendWebRequest();
         while(!result.isDone)
            await Task.Yield();
         if(!request.isNetworkError)
        {
            response = JsonUtility.FromJson<JsonResponse>(request.downloadHandler.text);

            Debug.Log(request.downloadHandler.text);
        }


      



    }
    
}
