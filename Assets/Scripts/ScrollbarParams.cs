using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarParams : MonoBehaviour
{
    public List<string> Namings = new List<string>();
    public List<int> Values = new List<int>();

    private void Start()
    {
        GameObject.Find("Settings Interface").gameObject.GetComponent<LANInterface>().ChangeCounter(this.gameObject);
    }
}
