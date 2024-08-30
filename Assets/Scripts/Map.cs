using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : NetworkBehaviour
{
    public unsafe static Card*[,] MapPointers = new Card*[5,6];
    static Card Card00 = new Card(0);
    static Card Card01 = new Card(0);
    static Card Card02 = new Card(0);
    static Card Card03 = new Card(0);
    static Card Card04 = new Card(0);
    static Card Card05 = new Card(0);
    static Card Card10 = new Card(0);
    static Card Card11 = new Card(0);
    static Card Card12 = new Card(0);
    static Card Card13 = new Card(0); 
    static Card Card14 = new Card(0);
    static Card Card15 = new Card(0);
    static Card Card20 = new Card(0);
    static Card Card21 = new Card(0);
    static Card Card22 = new Card(0);
    static Card Card23 = new Card(0);
    static Card Card24 = new Card(0);
    static Card Card25 = new Card(0);
    static Card Card30 = new Card(0);
    static Card Card31 = new Card(0);
    static Card Card32 = new Card(0);
    static Card Card33 = new Card(0);
    static Card Card34 = new Card(0);
    static Card Card35 = new Card(0);
    static Card Card40 = new Card(0);
    static Card Card41 = new Card(0);
    static Card Card42 = new Card(0);
    static Card Card43 = new Card(0);
    static Card Card44 = new Card(0);
    static Card Card45 = new Card(0);
    bool cycle1 = true;
    static bool cycle2 = false;
    static int winner;
    static int myid;
    public static GameObject gameOver;

    public unsafe void Awake()
    {
        fixed (Card* Card00M = &Card00, Card01M = &Card01, Card02M = &Card02, Card03M = &Card03, Card04M = &Card04, Card05M = &Card05, Card10M = &Card10, Card11M = &Card11, Card12M = &Card12, Card13M = &Card13, Card14M = &Card14, Card15M = &Card15, Card20M = &Card20, Card21M = &Card21, Card22M = &Card22, Card23M = &Card23, Card24M = &Card24, Card25M = &Card25, Card30M = &Card30, Card31M = &Card31, Card32M = &Card32, Card33M = &Card33, Card34M = &Card34, Card35M = &Card35, Card40M = &Card40, Card41M = &Card41, Card42M = &Card42, Card43M = &Card43, Card44M = &Card44, Card45M = &Card45)
        {
            MapPointers[0, 0] = Card00M;
            MapPointers[0, 1] = Card01M;
            MapPointers[0, 2] = Card02M;
            MapPointers[0, 3] = Card03M;
            MapPointers[0, 4] = Card04M;
            MapPointers[0, 5] = Card05M;
            MapPointers[1, 0] = Card10M;
            MapPointers[1, 1] = Card11M;
            MapPointers[1, 2] = Card12M;
            MapPointers[1, 3] = Card13M;
            MapPointers[1, 4] = Card14M;
            MapPointers[1, 5] = Card15M;
            MapPointers[2, 0] = Card20M;
            MapPointers[2, 1] = Card21M;
            MapPointers[2, 2] = Card22M;
            MapPointers[2, 3] = Card23M;
            MapPointers[2, 4] = Card24M;
            MapPointers[2, 5] = Card25M;
            MapPointers[3, 0] = Card30M;
            MapPointers[3, 1] = Card31M;
            MapPointers[3, 2] = Card32M;
            MapPointers[3, 3] = Card33M;
            MapPointers[3, 4] = Card34M;
            MapPointers[3, 5] = Card35M;
            MapPointers[4, 0] = Card40M;
            MapPointers[4, 1] = Card41M;
            MapPointers[4, 2] = Card42M;
            MapPointers[4, 3] = Card43M;
            MapPointers[4, 4] = Card44M;
            MapPointers[4, 5] = Card45M;
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Map.Write(new Card(0).ToString(), i, j);
            }
        }
        gameObject.tag = "map";
    }

    public FixedString4096Bytes MapToString(Card[,] map)
    {
        string encryption = new string("");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0;j < 6; j++)
            {
                encryption += map[i, j].ToString();
                if (i*j != 20)
                {
                    encryption += "|";
                }
            }
        }
        FixedString4096Bytes str = new FixedString4096Bytes();      
        str.CopyFrom<FixedString4096Bytes>(encryption);
        return str;
    }

    public Card[,] StringToMap(FixedString4096Bytes str)
    {
        Card[,] map = new Card[5, 6];
        string convstr = str.ToString();
        string[] cards = convstr.Split("|");
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j< 6; j++)
            {
                map[i, j].FromString(cards[i*6+j]);
            }
        }
        return map;
    }

    public void Update()
    {
        if (cycle1 && Camera.main.gameObject.GetComponent<MapVisualiser>() != null)
        {
            Empty();
            Camera.main.gameObject.GetComponent<MapVisualiser>().ExampleMap = Read();
            cycle1 = false;
        }
        if (cycle2)
        {
            cycle2 = false;
            gameOver.SetActive(true);
            if (winner == myid)
            {
                gameOver.transform.GetChild(3).gameObject.SetActive(true);
                gameOver.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                gameOver.transform.GetChild(3).gameObject.SetActive(false);
                gameOver.transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }

    public static void Empty()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Map.Write(new Card(0).ToString(), i, j);
            }
        }
    }

    public unsafe static Card Read(int i,int j)
    {
        return *MapPointers[i, j];
    }

    public unsafe static Card[,] Read()
    {
        Card[,] mas = new Card[5, 6];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                mas[i, j] = Read(i,j);
            }
        }
        return mas;
    }

    public unsafe static void Write(string cardstr,int row,int collumn)
    {
        Card card = new();
        card.FromString(cardstr);
        //Debug.Log("Writing to [" + row + "," + collumn + "] card with id:" + card.Id + " and empty:" + card.Empty);
        *MapPointers[row,collumn] = card;
        if (Camera.main.gameObject.GetComponent<MapVisualiser>() != null)
        { 
            Camera.main.gameObject.GetComponent<MapVisualiser>().ExampleMap = Map.Read();
        }
    }

    [Rpc(SendTo.Everyone)]
    public void WriteRpc(string card, int row, int collumn)
    {
        Write(card, row, collumn);
    }

    [Rpc(SendTo.Everyone)]
    public void VisualiseRpc()
    {
        Camera.main.gameObject.GetComponent<MapVisualiser>().Visualise(Camera.main.GetComponent<MapVisualiser>().ExampleMap, Camera.main.GetComponent<MapVisualiser>().ViewSide);
    }

    public static void DebugMap(Card[,] map)
    {
        string debug = new("");
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 5; i++)
            {
                debug += map[i, j].Empty ? "{ }" : "[@]";
                if (j < 5)
                {
                    debug += "|";
                }
            }
            debug += "\n";
        }
        //Debug.Log(debug);
    }

    public static void EndGame(int winner)
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
        map.DisconectRpc(winner);
    }

    [Rpc(SendTo.Everyone)]
    public void DisconectRpc(int winner)
    {
        SceneManager.LoadScene("GameMenu", LoadSceneMode.Single);
        Map.winner = winner;
        gameOver = GameObject.Find("10299_Monkey-Wrench_v1_L3").transform.GetChild(0).GetComponent<Options>().GameOver;
        myid = (int)NetworkManager.Singleton.LocalClientId;        
        NetworkManager.Singleton.Shutdown();
        cycle2 = true;
    }

    public static void EndTurn()
    {
        if ((GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().IsOwner && GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId == 1) || (!GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().IsOwner && GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId == 0))
        {
            return;
        }
        Card[,] map = Camera.main.GetComponent<MapVisualiser>().ExampleMap;
        Map mapy;
        mapy = GameObject.FindGameObjectsWithTag("map")[0].GetComponent<Map>();
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("map").Length; i++)
        {
            mapy = GameObject.FindGameObjectsWithTag("map")[i].GetComponent<Map>();
            if (mapy.gameObject.GetComponent<NetworkObject>().IsOwner)
            {
                break;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0;j < 6; j++)
            {
                if (!map[i, j].Empty)
                {
                    if (map[i, j].Side == 0) 
                    {
                        Card enemy = new Card(0);
                        int j1 = j;
                        for (; j1 < 6; j1++)
                        {
                            if (map[i, j1].Side == 1)
                            {
                                enemy = map[i, j1];
                                break;
                            }
                        }
                        if (enemy.Empty)
                        {
                            Debug.Log("Damaging player 2 from" + i + " " + j + " for " + (map[i, j].RangedDamage < map[i, j].MeleeDamage && j == 5 ? map[i, j].MeleeDamage : map[i, j].RangedDamage));
                            mapy.HealthSetRpc(GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health1.Value, GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health2.Value - (map[i, j].RangedDamage < map[i, j].MeleeDamage && j == 5 ? map[i, j].MeleeDamage : map[i, j].RangedDamage));
                        }
                        else
                        {
                            enemy.Health -= (map[i, j].RangedDamage < map[i, j].MeleeDamage && j1 - j == 1 ? map[i, j].MeleeDamage : map[i, j].RangedDamage);
                            map[i, j1] = enemy;
                        }
                    }
                    else
                    {
                        Card enemy = new Card(0);
                        int j1 = j;
                        for (; j1 >= 0; j1--)
                        {
                            if (map[i, j1].Side == 0)
                            {
                                enemy = map[i, j1];
                                break;
                            }
                        }
                        if (enemy.Empty)
                        {
                            Debug.Log("Damaging player 1 from" + i + " " + j + " for " + (map[i, j].RangedDamage < map[i, j].MeleeDamage && j == 0 ? map[i, j].MeleeDamage : map[i, j].RangedDamage));
                            mapy.HealthSetRpc(GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health1.Value - (map[i, j].RangedDamage < map[i, j].MeleeDamage && j == 0 ? map[i, j].MeleeDamage : map[i, j].RangedDamage), GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health2.Value);
                        }
                        else
                        {
                            enemy.Health = (map[i, j].RangedDamage < map[i, j].MeleeDamage && j1 - j == -1 ? map[i, j].MeleeDamage : map[i, j].RangedDamage);
                            map[i, j1] = enemy;
                        }
                    }
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (map[i,j].Health <= 0 && !map[i,j].Empty)
                {
                    map[i, j] = new Card(0);
                    Debug.Log("Card dead at " + i + " " + j);
                }
            }
        }
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                mapy.WriteRpc(map[i, j].ToString(), i, j);
            }
        }
        mapy.VisualiseRpc();
    }

    [Rpc(SendTo.Everyone)]
    public void VarSyncRpc()
    {
        TimerInter.CurTurn = TimerInter.CurTurn * -1 + 1;
        TimerInter.Turns++;
        TimerInter.time = 0;
    }

    [Rpc(SendTo.Server)]
    public void HealthSetRpc(int h1, int h2)
    {
        GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health1.Value = h1;
        GameObject.Find("NetworkStorage").GetComponent<SyncData>().Health2.Value = h2;
    }
}
