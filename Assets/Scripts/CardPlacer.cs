using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardPlacer : MonoBehaviour
{
    [SerializeField] GameObject PickPrefab;
    public Card Card;

    void Start()
    {
        PickPrefab = (GameObject)Resources.Load("CardPositionChoice", typeof(GameObject));
    }

    private void OnMouseDown()
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
        if (TimerInter.Energy < Card.Cost)
        {
            return;
        }
        Debug.Log("Spawning positions");
        Card[,] map = Map.Read();
        byte side = Camera.main.GetComponent<MapVisualiser>().ViewSide;
        if (side == 1)
        {
            Debug.Log("Mirroring");
            Card[,] mirror = new Card[5,6];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    mirror[i, j] = map[4 - i, 5 - j];
                }
            }
            Map.DebugMap(map);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    map[i, j] = mirror[i,j];
                }
            }
        }
        Map.DebugMap(map);
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                bool FullColumn = true;
                for (int j1 = 0; j1 < 6; j1++)
                {
                    if (map[i, j1].Empty)
                    {
                        FullColumn = false;
                    }
                }
                if (FullColumn)
                {
                    break;
                }
                if (!map[i, j].Empty)
                {
                    bool CanBeMoved = false;
                    for (int j1 = j; j1 < 6; j1++)
                    {
                        if (map[i, j1].Empty)
                        {
                            CanBeMoved = true;
                        }
                    }
                    if (map[i, j].Side == Card.Side)
                    {                        
                        if (Card.Abilities == 2 || (map[i, j].Abilities==1 && CanBeMoved))
                        {
                            GameObject pick = Instantiate(PickPrefab, new Vector3(Camera.main.GetComponent<MapVisualiser>().ZeroX + i * Camera.main.GetComponent<MapVisualiser>().StepX, 2.47f, Camera.main.GetComponent<MapVisualiser>().ZeroZ + j * Camera.main.GetComponent<MapVisualiser>().StepZ), Quaternion.Euler(new Vector3(0, 0, 0)));
                            pick.tag = "destroyOnSpawning";
                            pick.AddComponent<PlacingScriptTemp>();
                            pick.GetComponent<PlacingScriptTemp>().RealI = (side == 0 ? i : 4 - i);
                            pick.GetComponent<PlacingScriptTemp>().RealJ = (side == 0 ? j : 5 - j);
                            pick.GetComponent<PlacingScriptTemp>().Card = Card;
                        }
                    }
                    else
                    {
                        if (CanBeMoved)
                        {
                            GameObject pick = Instantiate(PickPrefab, new Vector3(Camera.main.GetComponent<MapVisualiser>().ZeroX + i * Camera.main.GetComponent<MapVisualiser>().StepX, 2.5f, Camera.main.GetComponent<MapVisualiser>().ZeroZ + j * Camera.main.GetComponent<MapVisualiser>().StepZ), Quaternion.Euler(new Vector3(0, 0, 0)));
                            pick.tag = "destroyOnSpawning";
                            pick.AddComponent<PlacingScriptTemp>();
                            pick.GetComponent<PlacingScriptTemp>().RealI = (side == 0 ? i : 4 - i);
                            pick.GetComponent<PlacingScriptTemp>().RealJ = (side == 0 ? j : 5 - j);
                            pick.GetComponent<PlacingScriptTemp>().Card = Card;
                        }
                    }
                }
                else
                {
                    GameObject pick = Instantiate(PickPrefab, new Vector3(Camera.main.GetComponent<MapVisualiser>().ZeroX + i * Camera.main.GetComponent<MapVisualiser>().StepX, 2.5f, Camera.main.GetComponent<MapVisualiser>().ZeroZ + j * Camera.main.GetComponent<MapVisualiser>().StepZ), Quaternion.Euler(new Vector3(0, 0, 0)));
                    pick.tag = "destroyOnSpawning";
                    pick.AddComponent<PlacingScriptTemp>();                   
                    pick.GetComponent<PlacingScriptTemp>().RealI = (side == 0 ? i : 4 - i);
                    pick.GetComponent<PlacingScriptTemp>().RealJ = (side == 0 ? j : 5 - j);                    
                    pick.GetComponent<PlacingScriptTemp>().Card = Card;
                }
                if (!(Card.Abilities == 1) || map[i, j].Empty || map[i,j].Side != Card.Side)
                {
                    break;
                }
            }
        }
    }
}
