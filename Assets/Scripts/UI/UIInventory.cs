using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class UIInventory : MonoBehaviour
{
    [SerializeField] Pickable.PickableType resource;
    [SerializeField] int stock;

    [SerializeField] Text counter;
    CharacterController player;

    private void Start()
    {
        player = CharacterController.Character;
        UpdateUI();
    }

    private void LateUpdate()
    {
        int currentStock = player.GetStock(resource);
        if (currentStock != stock)
        {
            stock = currentStock;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        counter.text = "x" + stock;
    }
}
