using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButtonPress : MonoBehaviour
{
    public int SceneID;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneID,LoadSceneMode.Single);
    }
}
