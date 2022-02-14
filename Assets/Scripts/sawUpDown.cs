using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class sawUpDown : MonoBehaviour
{

    void Start()
    {
        UpAndDown();
    }
    public void UpAndDown()
    {
        transform.DOLocalMoveY(122, 3).OnComplete(() =>
        {
            transform.DOLocalMoveY(332, 3).OnComplete(() => UpAndDown());
        });
    }
    void Update()
    {

    }
}
