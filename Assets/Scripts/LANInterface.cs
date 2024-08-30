using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LANInterface : MonoBehaviour
{
    public GameObject managerObj;

    public void ChangeCounter(GameObject Scrollbar)
    {
        int index = (int)Mathf.Round(Scrollbar.GetComponent<Scrollbar>().value / (1f / (Scrollbar.GetComponent<Scrollbar>().numberOfSteps - 1)));
        Scrollbar.GetComponentsInChildren<TMP_Text>()[0].text = Scrollbar.GetComponent<ScrollbarParams>().Namings[index];
        if (Scrollbar == GameObject.Find("PlayerHealth Scrollbar"))
        {
            GameObject.Find("SceneManager").GetComponent<SceneTransitionHandler>().HealthValue = Scrollbar.GetComponent<ScrollbarParams>().Values[index];
        }
        if (Scrollbar == GameObject.Find("TurnTime Scrollbar"))
        {
            GameObject.Find("SceneManager").GetComponent<SceneTransitionHandler>().TurnMaxTimeValue = Scrollbar.GetComponent<ScrollbarParams>().Values[index];
        }
    }

    
}
