using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class TimerInter : MonoBehaviour
{
    public static int Energy = 1;
    public static float time;
    public static int CurTurn = 0;
    public static int Rounds = 0;
    public static int Turns = 0;

    private void OnMouseDown()
    {
        if (Camera.main.GetComponent<MapVisualiser>().ViewSide != CurTurn)
        {
            return; 
        }
        ResetTimer();
    }

    private void Update()
    {
        if (Turns/2 > Rounds)
        {
            Rounds++;
            Energy = Rounds + 1;
            for (int i = 0; i < Rounds / 5 + 1; i++)
            {
                Interface.Deck.Add(new Card((byte)Random.Range(1, 6), Camera.main.GetComponent<MapVisualiser>().ViewSide));
            }
            Camera.main.GetComponent<Interface>().UpdLive();
            Map.EndTurn();
        }
        if (Camera.main.GetComponent<MapVisualiser>().ViewSide == CurTurn)
        {
            gameObject.transform.GetChild(2).GetComponent<Light>().color = Color.green;
        }
        else
        {
            gameObject.transform.GetChild(2).GetComponent<Light>().color = Color.red;
        }
        if (GameObject.Find("NetworkStorage") != null)
        {
            float delta = GameObject.Find("NetworkStorage").GetComponent<SyncData>().Turntime.Value - time;
            if (delta < 0)
            {
                delta = 0;
            }
            int inttime = Mathf.FloorToInt(delta);
            gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<TMP_Text>()[0].text = (inttime / 60).ToString();
            gameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<TMP_Text>()[1].text = (inttime % 60 < 10 ? "0" : "") + (inttime % 60).ToString();
            gameObject.transform.GetChild(1).gameObject.GetComponentInChildren<TMP_Text>().text = Energy.ToString();
            time += Time.deltaTime;
            if (GameObject.Find("NetworkStorage").GetComponent<SyncData>().Turntime.Value > 0 && (Camera.main.GetComponent<MapVisualiser>().ViewSide == CurTurn))
            {
                if (time > GameObject.Find("NetworkStorage").GetComponent<SyncData>().Turntime.Value)
                {
                    ResetTimer();
                }
            }
        }
    }

    public void ResetTimer()
    {
        Map map;
        map = GameObject.FindGameObjectsWithTag("map")[0].GetComponent<Map>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("map").Length; i++)
        {
            map = GameObject.FindGameObjectsWithTag("map")[i].GetComponent<Map>();
            if (map.gameObject.GetComponent<NetworkObject>().IsOwner)
            {
                break;
            }
        }
        map.VarSyncRpc(); 
        Debug.Log(CurTurn + "'s turn");
    }    
}
