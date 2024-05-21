using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    private bool Show = false;
    private bool ShowPrev = false;
    private GameObject Card;
    [SerializeField] static GameObject Pref;

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Show = true;
        }
        else
        {
            Show = false;
        }
    }

    private void OnMouseExit()
    {
        Show = false;
    }

    private void Awake()
    {
        if (Pref == null)
        { 
            Pref = GameObject.FindGameObjectWithTag("imagepref"); 
        }
    }

    private void Update()
    {
        if (Show != ShowPrev)
        {
            if (Show)
            {               
                Texture2D texture = new Texture2D(512, 768);         
                RenderTexture orig = (RenderTexture)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
                RenderTexture.active = orig;
                texture.ReadPixels(new Rect(256, 0, 256, 384), 0, 0);
                texture.Apply();
                Sprite sprite = Sprite.Create(texture, new Rect(0,0,256,384), new Vector2(0,0));
                Card = Instantiate(Pref, new Vector3(Screen.width/2f,Screen.height/2f,0), Quaternion.Euler(0,0,0),GameObject.FindGameObjectWithTag("Canvas").transform);
                Card.GetComponent<Image>().sprite = sprite;
                Card.tag = "destroy";
            }
            if (!Show)
            {
                {
                    Destroy(GameObject.FindGameObjectWithTag("destroy"));
                }
            }
        }
        ShowPrev = Show;
    }
}
