using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyPack : MonoBehaviour
{
    public void DestroyMoney()
    {
        transform.DOScale(Vector3.zero, .5f).OnComplete(() => gameObject.SetActive(false));
    }
}
