using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButton : MonoBehaviour
{
    public TMP_InputField inputField;
     
    public void Login()
    {
        NetworkManager.Instance.ClientLogin(inputField.text);

        inputField.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
