using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCoinText : MonoBehaviour
{
    public GameObject player;
    public Text text;
    void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        Debug.Log("Texto Moeda Atualizado");
        text.text = "Moedas: " + player.GetComponent<Player>().inventory.GetItemAmount(Item.ItemType.Moeda);
    }
}
