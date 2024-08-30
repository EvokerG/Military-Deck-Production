using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameSettingsInterface : MonoBehaviour
{
    public GameObject DurationLabel;
    public GameObject DurationSlider;
    string[] Duration = {"10 сек.","30 сек.","1 мин.","Неогр."};
    public GameObject HealthLabel;
    public GameObject HealthSlider;
    string[] Health = { "10","20","30","40","50","60","70","80","90","100" };

    void Update()
    {
        DurationLabel.GetComponent<TMP_Text>().text = Duration[(int)Mathf.Round(DurationSlider.GetComponent<Scrollbar>().value / 0.33f)];
        HealthLabel.GetComponent<TMP_Text>().text = Health[(int)Mathf.Round(HealthSlider.GetComponent<Scrollbar>().value / 0.11f)];
    }
}
