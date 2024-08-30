using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public GameObject Canva;
    public GameObject GameOver;

    public void OnMouseDown()
    {
        Canva.SetActive(true);  

    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject.transform.parent);
        DontDestroyOnLoad(GameOver);
        Map.gameOver = GameOver;
    }

    public void Forfeit()
    {
        Map.EndGame(Camera.main.GetComponent<MapVisualiser>().ViewSide * -1 + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Canva.activeSelf)
            {
                Canva.SetActive(false);
            }
            else
            {
                if (GameObject.Find("Folder") != null)
                {
                    if (!GameObject.Find("Folder").GetComponent<PlayerInfo>().Canva.activeSelf)
                    {
                        Canva.SetActive(true);
                    }
                }
                else
                {
                    Canva.SetActive(true);
                }
            }
        }
    }
}
