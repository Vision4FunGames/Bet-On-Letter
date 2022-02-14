using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using MoneyTransfer;

public class SlotMachineScript : MonoBehaviour
{
    public GameObject slotpart1, slotpart2, slotpart3;
    float degree;
    int valueCount;
    bool isFinish;
    FinishManager fs;
    int coinCount;
    public GameObject confetti;

    void Start()
    {
        fs = FindObjectOfType<FinishManager>();
    }
    public void StartRotating(int coinValue)
    {
        int rand = Random.Range(0, 8);
        switch (rand)
        {
            case 0:
                degree = 3600;
                print("x4");
                valueCount = 4;
                break;
            case 1:
                degree = 3645;
                valueCount = 7;
                print("x7");
                break;
            case 2:
                degree = 3690;
                valueCount = 10;
                print("x10");
                break;
            case 3:
                degree = 3735;
                valueCount = 3;
                print("x3");
                break;
            case 4:
                degree = 3780;
                valueCount = 5;
                print("x5");
                break;
            case 5:
                degree = 3825;
                valueCount = 6;
                print("x6");
                break;
            case 6:
                degree = 3870;
                valueCount = 8;
                print("x8");
                break;
            case 7:
                degree = 3915;
                valueCount = 2;
                print("x2");
                break;

            default:
                break;
        }
        slotpart1.transform.DORotate(new Vector3(degree, 180, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
        slotpart2.transform.DORotate(new Vector3(degree, 180, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc).SetDelay(0.5f);
        slotpart3.transform.DORotate(new Vector3(degree, 180, 0), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc).SetDelay(1).OnComplete(() =>
        {
            coinCount = coinValue + 1;
            DOTween.To(() => coinCount, x => coinCount = x, coinCount * valueCount, 2).OnComplete(() => {
                confetti.SetActive(true);
                UIManager uimanager = FindObjectOfType<UIManager>();
                confetti.SetActive(true);
                uimanager.winPanel.SetActive(true);
            });
            isFinish = true;
        });
    }
    void Update()
    {
        if (isFinish)
        {
            fs.slotMachineText.text = coinCount.ToString();
        }
    }
}
