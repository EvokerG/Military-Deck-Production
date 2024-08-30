using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class NetworkManagerScript : MonoBehaviour
{
    NetworkManager manager;

    private void Start()
    {
        manager = gameObject.GetComponent<NetworkManager>();
    }

    public void ConnectHost()
    {
        Debug.Log("Host Connect");
        manager.StartHost();        
    }
    public void ConnectClient()
    {
        Debug.Log("Client Connect");
        manager.StartClient();        
    }

    public void Disconnect()
    {
        Debug.Log("Disconnected");
        manager.Shutdown();        
    }
}
