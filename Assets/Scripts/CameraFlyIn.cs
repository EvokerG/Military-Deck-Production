using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlyIn : MonoBehaviour
{
    [SerializeField] Vector3 StartFlyInPos;
    [SerializeField] Vector3 EndFlyInPos;
    [SerializeField] float FlyInSpeed;

    private void Start()
    {
        gameObject.transform.position = StartFlyInPos;
    }

    void Update()
    {
        float Mult = FlyInSpeed * Time.deltaTime;
        if (Vector3.Distance(gameObject.transform.position,EndFlyInPos) >= 0.01)
        {
            gameObject.transform.position += Vector3.Scale((EndFlyInPos - gameObject.transform.position),new Vector3(Mult,Mult,Mult));
        }
    }
}
