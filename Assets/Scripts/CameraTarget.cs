using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        if (GameManager.GetInstance().gameSequence != GAMESEQUENCE.FINISH)
            transform.position = target.position;

    }
}
