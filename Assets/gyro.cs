using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyro : MonoBehaviour
{

    [SerializeField]
    private float shiftModofier = 1f;
    private Gyroscope giro;
    private void Start()
    {
        giro = Input.gyro;
        giro.enabled = true;

    }
    void Update()
    {
        transform.Translate((float)System.Math.Round(giro.rotationRateUnbiased.y, 1) * shiftModofier, 0f, 0f);    
    }
}
