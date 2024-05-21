using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardPainter : MonoBehaviour
{
    public void Paint(Card card)
    {
        gameObject.GetComponent<MeshRenderer>().material.SetFloat(Shader.PropertyToID("_CardID"),card.Id-1);
        GameObject canvas = gameObject.GetComponentInChildren<Canvas>().gameObject;
        if (card.MaxHealth <= 0)
        {
            canvas.transform.Find("Health Icon").gameObject.SetActive(false);
        }else
        {
            canvas.transform.Find("Health Icon").Find("Health").gameObject.GetComponent<TMP_Text>().text = card.Health.ToString() + " / " + card.MaxHealth.ToString();
        }
        if (card.RangedDamage <= 0 && card.MeleeDamage <= 0)
        {
            canvas.transform.Find("Damage Icon").gameObject.SetActive(false);
        }
        else
        {
            canvas.transform.Find("Damage Icon").Find("Melee Attack").gameObject.GetComponent<TMP_Text>().text = card.MeleeDamage > 0 ? card.MeleeDamage.ToString() : "-";
            canvas.transform.Find("Damage Icon").Find("Ranged Attack").gameObject.GetComponent<TMP_Text>().text = card.RangedDamage > 0 ? card.RangedDamage.ToString() : "-";
        }
        if (card.Speed <= 0)
        {
            canvas.transform.Find("Speed Icon").gameObject.SetActive(false);
        }
        else
        {
            canvas.transform.Find("Speed Icon").Find("Speed").gameObject.GetComponent<TMP_Text>().text = card.Speed.ToString();
        }
        if (card.Armor <= 0)
        {
            canvas.transform.Find("Armor Icon").gameObject.SetActive(false);
        }
        else
        {
            canvas.transform.Find("Armor Icon").Find("Armor").gameObject.GetComponent<TMP_Text>().text = card.Armor.ToString();
        }
        canvas.transform.Find("Energy").gameObject.GetComponent<TMP_Text>().text = card.Cost.ToString();
    }
}
