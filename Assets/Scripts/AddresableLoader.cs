using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddresableLoader : MonoBehaviour
{
    public string SoftReference;
    public AssetReference assetReference;
    // Start is called before the first frame update
    void Start()
    {
        Addressables.LoadAssetAsync<Sprite>(assetReference).Completed += AddresableLoader_Completed;
    }

    private void AddresableLoader_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> obj)
    {
         if(obj.IsValid() && obj.IsDone )
        {
            GetComponent<SpriteRenderer>().sprite=obj.Result; 
        }

    }
}
