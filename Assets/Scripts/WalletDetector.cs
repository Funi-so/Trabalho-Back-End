using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class WalletDetector : MonoBehaviour
{
    public int wallet;
    public int price;
    public TMP_Text txtlabel;
    [SerializeField] private String url;

    void Start()
    {
        UpdateWallet();
    }

    // Update is called once per frame
    public void UpdateWallet()
    {
        StartCoroutine("LoadText");
    }


    IEnumerator LoadText()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            txtlabel.text = request.downloadHandler.text;
            
            price = int.Parse(txtlabel.text);
        }
        
    }
}
