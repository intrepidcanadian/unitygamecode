using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


using Solana.Unity.SDK;
using Solana.Unity.Rpc.Types;
using Solana.Unity.Wallet;

public class DisplayAccountBalance : MonoBehaviour {

    private TextMeshProUGUI _txtBalance;

    void Start() 

    {
        _txtBalance = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Web3.OnBalanceChange += OnBalanceChange;
    }
    private void OnDisable()
    {
        Web3.OnBalanceChange -= OnBalanceChange;
    }

    private void OnBalanceChange(double amount)
    {
        _txtBalance.text = amount.ToString();
    }

}

