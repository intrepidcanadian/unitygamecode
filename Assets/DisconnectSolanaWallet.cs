using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using Solana.Unity.SDK;
using Solana.Unity.Rpc.Types;
using Solana.Unity.Wallet;


public class DisconnectSolanaWallet : MonoBehaviour
{
    [SerializeField] private Button btnConnect;
    [SerializeField] private Button btnDisconnect;
    [SerializeField] GameObject txtPublicKey;
    [SerializeField] GameObject txtBalance; 
    private void Start()
    {
        btnDisconnect.onClick.AddListener(call:() => Web3.Instance.Logout());
    }

    private void OnEnable()
    {
        Web3.OnLogin += OnLogin;
        Web3.OnLogout += OnLogout;
    }

    private void OnDisable()
    {
        Web3.OnLogin -= OnLogin;
        Web3.OnLogout -= OnLogout;
    }

    private void OnLogout()
    {
        btnConnect.gameObject.SetActive(true);
        btnDisconnect.gameObject.SetActive(false);
        txtPublicKey.SetActive(false);
        txtBalance.SetActive(false);
    }


    private void OnLogin(Account obj)
    {
        btnConnect.gameObject.SetActive(false);
        btnDisconnect.gameObject.SetActive(true);
        txtPublicKey.SetActive(true);
        txtBalance.SetActive(true);
    }

}
