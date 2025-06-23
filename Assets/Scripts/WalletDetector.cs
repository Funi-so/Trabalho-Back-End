using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;


public class WalletDetector : MonoBehaviour
{
    public int wallet;
    private int price;
    public GameObject player;
    private Inventory inventory;
    private Item arvore = new Item(Item.ItemType.Arvore, 1);
    private Item banana = new Item(Item.ItemType.Banana, 1);
    private Item chapeu = new Item(Item.ItemType.Chapeu, 1);
    public Text txtlabel;
    [SerializeField] private String url;
    private String arquivo;


    public void Comprar(string item)
    {
        inventory = player.GetComponent<Player>().inventory;
        wallet = inventory.GetItemAmount(Item.ItemType.Moeda);
        arquivo = item;
        StartCoroutine("Buy" + item);

        Debug.Log("Tentativa de Compra");
    }

    IEnumerator BuyBanana()
    {
        UnityWebRequest request = UnityWebRequest.Get(url+arquivo+".txt");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Requisitado com sucesso");
            txtlabel.text = request.downloadHandler.text;

            price = int.Parse(txtlabel.text);
            if (wallet >= price)
            {

                Item moeda = new Item(Item.ItemType.Moeda, wallet);
                inventory.RemoveItem(moeda);
                moeda = new Item(Item.ItemType.Moeda, wallet - price);
                inventory.AddItem(moeda);

                inventory.AddItem(banana);
                txtlabel.text = "voc� comprou a banana";
                Debug.Log("Banana comprada");
            }
            else
            {
                txtlabel.text = "voc� n tem dinheiro para comprar isso";
                Debug.Log("Sem dinheiro pra banana");
            }
        }
        else
            Debug.Log("Requisitado sem sucesso");

    }
    IEnumerator BuyChapeu()
    {
        UnityWebRequest request = UnityWebRequest.Get(url+arquivo+".txt");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            txtlabel.text = request.downloadHandler.text;

            price = int.Parse(txtlabel.text);
            if (wallet >= price)
            {
                Item moeda = new Item(Item.ItemType.Moeda, wallet);
                inventory.RemoveItem(moeda);
                moeda = new Item(Item.ItemType.Moeda, wallet - price);
                inventory.AddItem(moeda);
                inventory.AddItem(chapeu);
                txtlabel.text = "voc� comprou o chapeu";
            }else
            txtlabel.text = "voc� n tem dinheiro para comprar isso";
        }



    }
    IEnumerator BuyArvore()
    {
        UnityWebRequest request = UnityWebRequest.Get(url + arquivo + ".txt");
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Requisitado com sucesso");
            txtlabel.text = request.downloadHandler.text;

            price = int.Parse(txtlabel.text);
            if (wallet >= price)
            {
                Item moeda = new Item(Item.ItemType.Moeda, wallet);
                inventory.RemoveItem(moeda);
                moeda = new Item(Item.ItemType.Moeda, wallet - price);
                inventory.AddItem(moeda);
                
                inventory.AddItem(arvore);
                txtlabel.text = "voc� comprou a arvore";
                Debug.Log("Arvore comprada");
            }
            else
            {
                txtlabel.text = "voc� n tem dinheiro para comprar isso";
                Debug.Log("Arvore nao comprada (sem dimdim)");
            }
        }
        else
            Debug.Log("Requisitado sem sucesso");

    }
}

