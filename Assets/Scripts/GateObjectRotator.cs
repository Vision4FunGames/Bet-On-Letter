using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateObjectRotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 10;


    void Update()
    {
        transform.eulerAngles += new Vector3(0, Time.deltaTime * rotateSpeed, 0);
    }
}
