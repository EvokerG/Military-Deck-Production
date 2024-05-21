using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MapVisualiser : MonoBehaviour
{
    [SerializeField] GameObject CardPrefab;
    [SerializeField] public GameObject CardRenderPrefab;
    [SerializeField] public byte Columns;
    [SerializeField] public byte Rows;
    [SerializeField] public float ZeroX;
    [SerializeField] public float ZeroZ;
    [SerializeField] public float StepX;
    [SerializeField] public float StepZ;
    [SerializeField] float CardY;
    [Range(0,1)]
    [SerializeField] public byte ViewSide;
    public Card[,] ExampleMap;
    List<GameObject> Cards = new();
    public int CardRenders = 0;

    private void Start()
    {
        ExampleMap = new Card[Columns, Rows];
        for (int i = 0; i < 30; i++)
        {
            ExampleMap[i%5,i/5] = new Card(0);
        }
        ExampleMap[0, 0] = new Card(1, 0);
        ExampleMap[1, 0] = new Card(3, 0);
        ExampleMap[0, 1] = new Card(5, 1);
        ExampleMap[1, 2] = new Card(1, 1);
        ExampleMap[3, 3] = new Card(4, 0);
        ExampleMap[4, 4] = new Card(3, 0);
        ExampleMap[1, 5] = new Card(2, 1);
        Visualise(ExampleMap, ViewSide);
    }

    public void Visualise(Card[,] Map, byte Side)
    {
        foreach (GameObject card in Cards)
        {
            Destroy(card.gameObject);
        }
        foreach (GameObject render in GameObject.FindGameObjectsWithTag("RenderS"))
        {
            Destroy(render.gameObject);
        }
        Debug.Log("Visualizing array");
        for (int i = 0; i < Columns; i++)
        {
            for(int j = 0; j < Rows; j++)
            {
                if (!Map[i, j].Empty)
                {
                    int RealI = (Columns-1-i)*Side + i*(1-Side);
                    int RealJ = (Rows-1-j)*Side + j*(1-Side);
                    GameObject Card =  Instantiate(CardPrefab,new Vector3(ZeroX + RealI * StepX, CardY ,ZeroZ + RealJ * StepZ), Quaternion.Euler(new Vector3(0,0,0)));
                    Card.transform.GetChild(0).gameObject.AddComponent<CardDisplay>();
                    RenderTexture texture = new(512,512,0);
                    Card.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.mainTexture = texture;
                    GameObject Render = Instantiate(CardRenderPrefab, new Vector3(-50 + (CardRenders++ * -1f), 0, 0), Quaternion.Euler(Vector3.zero));
                    texture.Create();
                    Render.GetComponentInChildren<Camera>().targetTexture = texture;
                    Render.GetComponent<CardPainter>().Paint(Map[i,j]);
                    Render.GetComponentInChildren<Camera>().Render();
                    Render.SetActive(false);
                    if (Map[i,j].Side != Side)
                    {
                        Card.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                    else
                    {
                        Card.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }                    
                    Cards.Add(Card);
                }
            }            
        }        
    }
}
