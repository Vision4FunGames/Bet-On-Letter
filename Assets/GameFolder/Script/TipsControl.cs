using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TipsControl : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform hand;

    private void Start()
    {
        hand.DOMove(left.position, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
}
