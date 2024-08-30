using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
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
    [SerializeField] public byte ViewSide;
    [SerializeField]public Card[,] ExampleMap;
    List<GameObject> Cards = new();
    public int CardRenders = 0;

    private void Awake()
    {
        ViewSide = (GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().IsOwner ? (byte)GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId : (byte)((byte)GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId * -1 + 1));
        ExampleMap = Map.Read();
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
        Debug.Log("Visualizing array for client id" + (GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().IsOwner ? GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId : (int)GameObject.Find("NetworkStorage").GetComponent<NetworkObject>().OwnerClientId * -1 + 1));        
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
