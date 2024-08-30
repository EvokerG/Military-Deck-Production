using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionHandler : MonoBehaviour
{
    static public SceneTransitionHandler transitionHandler {  get; internal set; }
    public GameObject Dropdown;
    public GameObject Label;
    public GameObject button1;
    public GameObject obj1;
    public GameObject obj2;
    public GameObject Counter;
    public GameObject button2;
    int prevConnectedClients = 0;
    public int HealthValue;
    public int TurnMaxTimeValue;

    private void Awake()
    {
        if (transitionHandler != this && transitionHandler != null)
        {
            Destroy(transitionHandler.gameObject);
        }
        transitionHandler = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        button2.GetComponent<Button>().onClick.AddListener(() => { NetworkManager.Singleton.DisconnectClient(1); });
    }

    public void ChangeScene(string Scene)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(Scene, LoadSceneMode.Single);
    }

    private bool Cycle = true;

    private void Update()
    {
        if (GameObject.Find("NetworkStorage") != null && Cycle && NetworkManager.Singleton.IsHost)
        {
            GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health1.Value = HealthValue;
            GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health2.Value = HealthValue;
            GameObject.Find("NetworkStorage").GetComponent<SyncData>().Turntime.Value = TurnMaxTimeValue;
            Cycle = false;
        }
        if(Dropdown != null && Label != null && NetworkManager.Singleton.IsServer) 
        {
            Counter.GetComponent<TMP_Text>().text = (NetworkManager.Singleton.ConnectedClients.Count - 1).ToString();
            Dropdown.GetComponent<TMP_Dropdown>().ClearOptions();
            Label.GetComponent<TMP_Text>().text = "";
            button1.GetComponent<Button>().interactable = false;
            if (NetworkManager.Singleton.ConnectedClients.Count > 1) 
            {
                button1.GetComponent<Button>().interactable = true;
                Dropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData("Admin " + NetworkManager.Singleton.ConnectedClientsIds[1]));
                Dropdown.GetComponentInChildren<TMP_Text>().text = "Admin" + NetworkManager.Singleton.ConnectedClientsIds[1];
                Label.GetComponent<TMP_Text>().text = "Admin" + NetworkManager.Singleton.ConnectedClientsIds[1];
            }
            if(NetworkManager.Singleton.ConnectedClients.Count < prevConnectedClients)
            {
                obj1.SetActive(true);
                obj2.SetActive(false);
            }
            prevConnectedClients = NetworkManager.Singleton.ConnectedClients.Count;
        }
    }
}
