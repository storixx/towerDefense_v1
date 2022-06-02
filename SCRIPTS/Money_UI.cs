using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_UI : MonoBehaviour
{
    public Text moneyBalance;
    
    private void Update()
    {
        moneyBalance.text = "$" + PlayerStats.playerMoney.ToString();
    }

}
