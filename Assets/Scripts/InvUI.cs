using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InvUI : MonoBehaviour

{
    private TextMeshProUGUI coins;
    // Start is called before the first frame update
    void Start()
    {
        coins = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCoins(PlayerInventory playerInventory){
        coins.text = playerInventory.NumberOfCoins.ToString();
    }
}
