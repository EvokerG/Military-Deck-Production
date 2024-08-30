using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using TMPro;

public class SyncData : NetworkBehaviour
{
    public GameObject HealthLabels;
    public GameObject Timer;

    public NetworkVariable<int> Health1 = new NetworkVariable<int>(30,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    public NetworkVariable<int> Health2 = new NetworkVariable<int>(30, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> Turntime = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void Start()
    {
        HealthLabels.GetComponentsInChildren<TMP_Text>()[0].text = (Camera.main.GetComponent<MapVisualiser>().ViewSide == 0 )? Health2.Value.ToString() : Health1.Value.ToString();
        HealthLabels.GetComponentsInChildren<TMP_Text>()[1].text = (Camera.main.GetComponent<MapVisualiser>().ViewSide == 0 )? Health1.Value.ToString() : Health2.Value.ToString();
        Health1.OnValueChanged += (int prev, int cur) =>
        {
            HealthLabels.GetComponentsInChildren<TMP_Text>()[0].text = Camera.main.GetComponent<MapVisualiser>().ViewSide == 0 ? Health2.Value.ToString() : Health1.Value.ToString();
            HealthLabels.GetComponentsInChildren<TMP_Text>()[1].text = Camera.main.GetComponent<MapVisualiser>().ViewSide == 0 ? Health1.Value.ToString() : Health2.Value.ToString();
        };
        Health2.OnValueChanged += (int prev, int cur) =>
        {
            HealthLabels.GetComponentsInChildren<TMP_Text>()[0].text = Camera.main.GetComponent<MapVisualiser>().ViewSide == 0 ? Health2.Value.ToString() : Health1.Value.ToString();
            HealthLabels.GetComponentsInChildren<TMP_Text>()[1].text = Camera.main.GetComponent<MapVisualiser>().ViewSide == 0 ? Health1.Value.ToString() : Health2.Value.ToString();
        };        
    }
}
