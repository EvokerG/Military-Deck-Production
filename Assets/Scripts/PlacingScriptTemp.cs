using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingScriptTemp : MonoBehaviour
{
    public Card Card;
    public int RealI;
    public int RealJ;

    private void OnMouseDown()
    {
        GameObject[] trashbin = GameObject.FindGameObjectsWithTag("destroyOnSpawning");
        foreach (var g in trashbin)
        {
            Destroy(g.gameObject);
        }
        if (Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ].Empty)
        {
            Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ] = Card;
        }
        else
        {
            var buf1 = Card;
            Card buf2 = Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ];
            int i = 0;
            do
            {
                Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ + i] = buf1;
                buf1 = buf2;
                i++;
                buf2 = Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ + i];
                if (buf2.Empty)
                {
                    Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ + i] = buf1;
                    break;
                }
            } while (!Camera.main.GetComponent<MapVisualiser>().ExampleMap[RealI, RealJ + i - 1].Empty || !buf1.Empty);
        }
        Camera.main.GetComponent<Interface>().Deck.Remove(Card);
        Camera.main.GetComponent<Interface>().UpdLive();
        Camera.main.GetComponent<MapVisualiser>().Visualise(Camera.main.GetComponent<MapVisualiser>().ExampleMap, Camera.main.GetComponent<MapVisualiser>().ViewSide);
    }
}
