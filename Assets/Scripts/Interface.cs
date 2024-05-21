using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField] private Vector3 CameraPosition1;
    [SerializeField] private Vector3 CameraRotation1;
    [SerializeField] private Vector3 CameraPosition2;
    [SerializeField] private Vector3 CameraRotation2;
    private Vector3 Pos;
    private Vector3 Rot;
    [SerializeField] Button CameraButton;
    [SerializeField] Button DeckButton;
    bool DeckVisibility;
    bool PrevDeckVisibility;
    public List<Card> Deck = new List<Card>();
    public List<GameObject> PhysicDeck;
    [SerializeField] GameObject CardPrefab;
    [SerializeField] Vector3 InitialCardPos;
    [SerializeField] Vector3 InitialCardRot;
    bool upd = false;

    private void Awake()
    {
        DeckButton.GetComponent<Button>().onClick.AddListener(() =>{
            DeckVisibility = !DeckVisibility;
        });
        DeckVisibility = false;
        PrevDeckVisibility = true;
        Deck.Add(new Card(5, 0));
        Deck.Add(new Card(2, 0));
        Deck.Add(new Card(4, 0));
        Deck.Add(new Card(1, 0));
        Deck.Add(new Card(3, 0));
    }

    public void UpdLive()
    {
        upd = true;
    }

    private void Update()
    {
        if (CameraButton.GetComponent<MouseOverToggle>().Toggled) //Угол камеры
        {
            Pos = CameraPosition2;
            Rot = CameraRotation2;
        }
        else
        {
            Pos = CameraPosition1;
            Rot = CameraRotation1;
        }
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Pos, 0.1f);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(Rot), 0.1f);
        if (DeckVisibility != PrevDeckVisibility || upd) //Показывание колоды
        {            
            foreach (GameObject obj in PhysicDeck)
            {
                Destroy(obj.gameObject);
            }
            PhysicDeck.Clear();
            if (DeckVisibility)
            {
            }
            else
            {
                int i = 0;
                if (Deck.Count > 0)
                {
                    foreach (Card c in Deck)
                    {
                        int delta1 = (i + 1) / 2;
                        int delta2 = 1 + (-2 * (i % 2));                        
                        GameObject card = Instantiate(CardPrefab, InitialCardPos + new Vector3(-0.5f * delta1 * delta2, -0.05f * delta1, 0), Quaternion.Euler(InitialCardRot + new Vector3(0, 0, -4 * delta1 * delta2)));
                        RenderTexture texture = new(512, 512,0);
                        GameObject Render = Instantiate(Camera.main.GetComponent<MapVisualiser>().CardRenderPrefab, new Vector3(-50 + (Camera.main.GetComponent<MapVisualiser>().CardRenders++ * -1f), 0, 0), Quaternion.Euler(Vector3.zero));
                        texture.Create();
                        Render.GetComponentInChildren<Camera>().targetTexture = texture;
                        Render.GetComponent<CardPainter>().Paint(c);
                        Render.GetComponentInChildren<Camera>().Render();
                        Render.SetActive(false);
                        card.GetComponentInChildren<MeshRenderer>().material.mainTexture = texture;
                        card.transform.GetChild(0).gameObject.AddComponent<CardDisplay>();
                        card.transform.GetChild(0).gameObject.AddComponent<CardPlacer>();
                        card.transform.GetChild(0).gameObject.AddComponent<MeshCollider>();
                        card.transform.GetChild(0).gameObject.GetComponent<CardPlacer>().Card = c;
                        PhysicDeck.Add(card);
                        i++;
                    }
                }
            }
            upd = false;
        }
        PrevDeckVisibility = DeckVisibility;
    }
}
