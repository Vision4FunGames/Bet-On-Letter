using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Player : MonoBehaviour
{
    public GameObject instantiateMoney;
    public Hand rightHand, leftHand;
    public float verticalSpeed, vertical = 1;
    private float firstPosX, lastPosX;
    public static Player Instance;
    public void Awake()
    {
        Instance = this;
    }
    public void SagMove()
    {
        float c = 0;
        int sayi = 4;
        if (leftHand.objects.Count <= sayi)
        {
            sayi = leftHand.objects.Count;
        }
        for (int i = 0; i < sayi; i++)
        {
            leftHand.objects[leftHand.objects.Count - 1].transform.parent = rightHand.transform;
            leftHand.objects[leftHand.objects.Count - 1].transform.DORotate(new Vector3(0, 0, -180), 0.5f).SetEase(Ease.Linear).SetRelative();
            leftHand.objects[leftHand.objects.Count - 1].transform.DOLocalJump(rightHand.point.localPosition, 2, 1, 0.5f).SetEase(Ease.Linear).SetDelay(c * 0.1f);
            rightHand.point.localPosition = new Vector3(rightHand.point.localPosition.x, rightHand.point.localPosition.y + 0.05f, rightHand.point.localPosition.z);
            c += 0.2f;
            leftHand.objects.Remove(leftHand.objects[leftHand.objects.Count - 1]);
        }
        if (leftHand.count > 0) leftHand.count -= 4;
    }
    public void SolMove()
    {
        /*  oynatilacakObje.transform.DORotate(new Vector3(0, 0, 180), 2f).SetEase(Ease.Linear).SetRelative();
          oynatilacakObje.transform.DOJump(cubOne.position, 2, 1, 2f).SetEase(Ease.Linear).OnComplete(() =>
             {
                 oynatilacakObje.transform.parent = cubOne.transform;
             });*/

    }
    private void Update()
    {
        transform.Translate(new Vector3(0, 0, vertical) * Time.deltaTime * verticalSpeed);
        if (Input.GetMouseButtonDown(0))
        {
            firstPosX = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastPosX = Input.mousePosition.x;
            if (lastPosX - firstPosX > 0)
            {
                SagMove();
            }
            else
            {
                SolMove();
            }
        }
    }
}
  
