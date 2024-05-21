using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Spawning positions");
        Card[,] map = Camera.main.GetComponent<MapVisualiser>().ExampleMap;
        byte side = Camera.main.GetComponent<MapVisualiser>().ViewSide;
        if (side == 1)
        {
            Card[,] mirror = map;
            for (int i = 0; i < Camera.main.GetComponent<MapVisualiser>().Columns; i++)
            {
                for (int j = 0; j < Camera.main.GetComponent<MapVisualiser>().Rows; j++)
                {
                    mirror[i, j] = map[Camera.main.GetComponent<MapVisualiser>().Columns - i - 1, Camera.main.GetComponent<MapVisualiser>().Rows - j - 1];
                }
            }
            map = mirror;            
        }
        for (int i = 0; i < Camera.main.GetComponent<MapVisualiser>().Columns; i++)
        {
            for (int j = 0; j < Camera.main.GetComponent<MapVisualiser>().Rows; j++)
            {
                bool FullColumn = true;
                for (int j1 = j; j1 < Camera.main.GetComponent<MapVisualiser>().Rows; j1++)
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
                    for (int j1 = j; j1 < Camera.main.GetComponent<MapVisualiser>().Rows; j1++)
                    {
                        if (map[i, j1].Empty)
                        {
                            CanBeMoved = true;
                        }
                    }
                    if (map[i, j].Side == Card.Side)
                    {                        
                        if (Card.Abilities.Contains(Card.Ability.Support) || (map[i, j].Abilities.Contains(Card.Ability.Cover) && CanBeMoved))
                        {
                            GameObject pick = Instantiate(PickPrefab, new Vector3(Camera.main.GetComponent<MapVisualiser>().ZeroX + i * Camera.main.GetComponent<MapVisualiser>().StepX, 2.5f, Camera.main.GetComponent<MapVisualiser>().ZeroZ + j * Camera.main.GetComponent<MapVisualiser>().StepZ), Quaternion.Euler(new Vector3(0, 0, 0)));
                            pick.tag = "destroyOnSpawning";
                            pick.AddComponent<PlacingScriptTemp>();
                            if (Card.Side == 1)
                            {
                                pick.GetComponent<PlacingScriptTemp>().RealI = 4 - i;
                                pick.GetComponent<PlacingScriptTemp>().RealJ = 5 - j;
                            }
                            else
                            {
                                pick.GetComponent<PlacingScriptTemp>().RealI = i;
                                pick.GetComponent<PlacingScriptTemp>().RealJ = j;
                            }
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
                            if (Card.Side == 1)
                            {
                                pick.GetComponent<PlacingScriptTemp>().RealI = 4 - i;
                                pick.GetComponent<PlacingScriptTemp>().RealJ = 5 - j;
                            }
                            else
                            {
                                pick.GetComponent<PlacingScriptTemp>().RealI = i;
                                pick.GetComponent<PlacingScriptTemp>().RealJ = j;
                            }
                            pick.GetComponent<PlacingScriptTemp>().Card = Card;
                        }
                    }
                }
                else
                {
                    GameObject pick = Instantiate(PickPrefab, new Vector3(Camera.main.GetComponent<MapVisualiser>().ZeroX + i * Camera.main.GetComponent<MapVisualiser>().StepX, 2.5f, Camera.main.GetComponent<MapVisualiser>().ZeroZ + j * Camera.main.GetComponent<MapVisualiser>().StepZ), Quaternion.Euler(new Vector3(0, 0, 0)));
                    pick.tag = "destroyOnSpawning";
                    pick.AddComponent<PlacingScriptTemp>();
                    if (Card.Side == 1)
                    {
                        pick.GetComponent<PlacingScriptTemp>().RealI = 4 - i;
                        pick.GetComponent<PlacingScriptTemp>().RealJ = 5 - j;
                    }
                    else
                    {
                        pick.GetComponent<PlacingScriptTemp>().RealI = i;
                        pick.GetComponent<PlacingScriptTemp>().RealJ = j;
                    }
                    pick.GetComponent<PlacingScriptTemp>().Card = Card;
                }
                if (!Card.Abilities.Contains(Card.Ability.Cover) || map[i, j].Empty || map[i,j].Side != Card.Side)
                {
                    break;
                }
            }
        }
    }
}
