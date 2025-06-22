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
    public GameObject Arvore;
    public GameObject banana;
    public GameObject Chapeu;
    public TMP_Text txtlabel;
    [SerializeField] private String url;



    public void ComprarArvore()
    {
        StartCoroutine("BuyArvore");
    }

    public void Comprarbanana()
    {
        StartCoroutine("Buybanana");
    }

    public void ComprarChapeu()
    {
        {
            StartCoroutine("BuyHat");
        }

       
    }
    IEnumerator Buybanana()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            txtlabel.text = request.downloadHandler.text;

            price = int.Parse(txtlabel.text);
            if (wallet >= price)
            {
                wallet = -price;
                banana.SetActive(true);
                txtlabel.text = "você comprou a banana";
            }
            txtlabel.text = "você n tem dinheiro para comprar isso";
        }

    }
    IEnumerator BuyHat()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            txtlabel.text = request.downloadHandler.text;

            price = int.Parse(txtlabel.text);
            if (wallet >= price)
            {
                wallet = -price;
                Chapeu.SetActive(true);
                txtlabel.text = "você comprou o chapeu";
            }
            txtlabel.text = "você n tem dinheiro para comprar isso";
        }



    }
    IEnumerator BuyArvore()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            txtlabel.text = request.downloadHandler.text;

            price = int.Parse(txtlabel.text);
            if (wallet >= price)
            {
                wallet = -price;
                Arvore.SetActive(true);
                txtlabel.text = "você comprou a arvore";
            }
            txtlabel.text = "você n tem dinheiro para comprar isso";
        }

    }
}

