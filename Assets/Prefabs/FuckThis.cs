using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class FuckThis : NetworkBehaviour
{
    public NetworkManager manager;

    public override void OnNetworkSpawn()
    {
        manager = GameObject.Find("Settings Interface").GetComponent<LANInterface>().managerObj.GetComponent<NetworkManager>();
    }

    public void ChangeScene(int Scene)
    {
        Debug.Log(manager); 
        if (manager.IsHost)
        {
            ClientRpcParams clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new List<ulong>() { 0, 1 }
                }
            };
            ChangeSceneClientRpc(Scene, clientRpcParams);
        }
    }

    [ClientRpc]

    public void ChangeSceneClientRpc(int Scene, ClientRpcParams rpcParams)
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Single);
        Debug.Log("Changed scene");
    }
}
