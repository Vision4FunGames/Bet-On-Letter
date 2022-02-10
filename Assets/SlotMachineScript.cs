using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlotMachineScript : MonoBehaviour
{
    public GameObject slotpart1, slotpart2, slotpart3;
    float degree;
    void Start()
    {
        StartRotating();
    }

    void Update()
    {
        
    }
    public void StartRotating()
    {
        int rand = Random.Range(0, 8);
        switch (rand)
        {
            case 0:
                degree = 3600;
                print("x4");
                break;
            case 1:
                degree = 3645;
                print("x7");
                break;
            case 2:
                degree = 3690;
                print("x10");
                break;
            case 3:
                degree = 3735;
                print("x3");
                break;
            case 4:
                degree = 3780;
                print("x5");
                break;
            case 5:
                degree = 3825;
                print("x6");
                break;
            case 6:
                degree = 3870;
                print("x8");
                break;
            case 7:
                degree = 3915;
                print("x2");
                break;

            default:
                break;
        }
      
        slotpart1.transform.DORotate(new Vector3(degree,180,0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
        slotpart2.transform.DORotate(new Vector3(degree, 180, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc).SetDelay(0.5f);
        slotpart3.transform.DORotate(new Vector3(degree, 180, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc).SetDelay(1);
    }
}
