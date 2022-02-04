using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GiyotinScript : MonoBehaviour
{
    public float PosYMinGiyotin;
    public float PosYGiyotin;

    void Start()
    {
        GiyotinMovement();
    }

    public void GiyotinMovement()
    {
        transform.DOLocalMoveY(PosYMinGiyotin, 0.5f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            transform.DOLocalMoveY(PosYGiyotin, 1f).OnComplete(() => { GiyotinMovement(); }).SetDelay(1.5f);
        }).SetDelay(1.5f);
    }
}
