using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public GameObject Canva;

    public void OnMouseDown()
    {
        Canva.SetActive(true);
        TextAsset txt = (TextAsset)Resources.Load("playerinfo",typeof(TextAsset));
        string[] param = txt.text.Split('|');
        Canva.transform.GetChild(1).GetComponent<TMP_Text>().text = param[0];
        Canva.transform.GetChild(2).GetComponent<TMP_Text>().text = "ур. " + param[1];
        Canva.transform.GetChild(3).GetComponent<TMP_Text>().text = param[2];
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Canva.activeSelf)
        {
            Canva.SetActive(false);
        }
    }
}
