using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking;
using Unity.Netcode;

public class PlayerDataHolder : NetworkBehaviour
{
    private NetworkVariable<long> CypherCode;

    public byte PlayerHealth { get; private set; }
    public byte OpponentHealth { get; private set; }
    public byte MaxEnergy { get; private set; }

    public void ServerCall(int type,int target)
    {
        long Message = 0;

        Debug.Log("Call" + type + "|" + target + "sent with encryption of" + CypherCode + " : " + Message);
    }

    public void OnCanvasGroupChanged()
    {
        Debug.Log("Data received");
        PlayerHealth = 23;
        OpponentHealth = 27;
    }
}
