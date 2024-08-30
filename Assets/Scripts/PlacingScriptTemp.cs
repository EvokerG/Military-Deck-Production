using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlacingScriptTemp : MonoBehaviour
{
    public Card Card;
    public int RealI;
    public int RealJ;

    private unsafe void OnMouseDown()
    {
        GameObject[] trashbin = GameObject.FindGameObjectsWithTag("destroyOnSpawning");
        foreach (var g in trashbin)
        {
            Destroy(g.gameObject);
        }
        if (TimerInter.CurTurn != (GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().IsOwner ? (byte)GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId : (byte)((byte)GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId * -1 + 1)))
        {
            return;
        }
        Card[,] temp = Camera.main.GetComponent<MapVisualiser>().ExampleMap;        
        if (temp[RealI, RealJ].Empty)
        {
            temp[RealI, RealJ] = Card;
        }
        else
        {
            var buf1 = Card;
            Card buf2 = temp[RealI, RealJ];
            int i = 0;
            do
            {
                temp[RealI, RealJ + i] = buf1;
                buf1 = buf2;
                if (Card.Side == 0)
                {
                    i++;
                }
                else
                {
                    i--;
                }
                buf2 = temp[RealI, RealJ + i];
                if (buf2.Empty)
                {
                    temp[RealI, RealJ + i] = buf1;
                    break;
                }
            } while (!temp[RealI, RealJ + i - 1].Empty || !buf1.Empty);
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                //Debug.Log("Reading" + i + "|" + j + "Prev:" + Map.Read()[i, j].ToString()); //+ Map.Read()[i,j].Empty + " New:" + temp[i,j].Id + temp[i,j].Empty);
            }
        }
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
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                map.WriteRpc(temp[i,j].ToString(),i,j);
            }
        }
        Camera.main.GetComponent<MapVisualiser>().ExampleMap = Map.Read();
        Interface.Deck.Remove(Card);
        TimerInter.Energy -= Card.Cost;
        Camera.main.GetComponent<Interface>().UpdLive();
        map.VisualiseRpc();
    }
}
