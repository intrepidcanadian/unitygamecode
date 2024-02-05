using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Solana.Unity.SDK;
using Solana.Unity.Rpc.Types;
using Solana.Unity.Wallet;

public class DisplayPublicKey : MonoBehaviour
{
    private TextMeshProUGUI _txtPublicKey;

    void Start()

    {
        _txtPublicKey = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Web3.OnLogin += OnLogin;
    }
    private void OnDisable()
    {
        Web3.OnLogin -= OnLogin;
    }

    private void OnLogin(Account account)
    {
    if (_txtPublicKey != null) 
    {
        _txtPublicKey.text = account.PublicKey;
    }
    }

}

