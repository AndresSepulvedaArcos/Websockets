using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
     
    public void Login()
    {
        NetworkManager.Instance.ClientLogin();
        gameObject.SetActive(false);
    }
}
