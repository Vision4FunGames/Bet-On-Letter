using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class HarfGateChil : MonoBehaviour
{
    public GateType myGateType;
    TextMeshProUGUI txtGate;
    private void Start()
    {
        txtGate = GameObject.FindGameObjectWithTag("textGate").GetComponent<TextMeshProUGUI>();
    }
    public void ShowText()
    {
        myGateType = GetComponentInParent<HarfGate>().myGateType;

        if (myGateType == GateType.policeCar)
        {
            txtGate.text = "Police Car";
        }
        else if (myGateType == GateType.Banana)
        {
            txtGate.text = "Banana";
        }
        else if (myGateType == GateType.Apple)
        {
            txtGate.text = "Apple";
        }
        else if (myGateType == GateType.Bike)
        {
            txtGate.text = "Bike";
        }
        else if (myGateType == GateType.Car)
        {
            txtGate.text = "Car";
        }
        else if (myGateType == GateType.Girl)
        {
            txtGate.text = "Girl ";
        }
        else if (myGateType == GateType.Ambulance)
        {
            txtGate.text = "Ambulance";
        }
        else if (myGateType == GateType.Ring)
        {
            txtGate.text = "Ring";
        }
        else if (myGateType == GateType.Ball)
        {
            txtGate.text = "Ball";
        }
        else if (myGateType == GateType.Candy)
        {
            txtGate.text = "Candy";
        }
        else if (myGateType == GateType.Knife)
        {
            txtGate.text = "Kni _ e";
        }
        else if (myGateType == GateType.Baby)
        {
            txtGate.text = "_ aby";
        }
        else if (myGateType == GateType.Mouse)
        {
            txtGate.text = "Mou _ e";
        }
        else if (myGateType == GateType.Snake)
        {
            txtGate.text = "Sna _ e";
        }
        else if (myGateType == GateType.Money)
        {
            txtGate.text = "M _ ney";
        }
        else if (myGateType == GateType.Crown)
        {
            txtGate.text = "Cro _ n";
        }
        else if (myGateType == GateType.Sock)
        {
            txtGate.text = "So _ k";
        }
        else if (myGateType == GateType.Cake)
        {
            txtGate.text = "C _ ke";
        }
        txtGate.transform.parent.GetComponent<Image>().rectTransform.DOAnchorPos(new Vector2(0,400),1f).SetDelay(2f);
    }
}
