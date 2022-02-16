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
    Canvas canvasMain;

    private void Start()
    {
        txtGate = GameObject.FindGameObjectWithTag("textGate").GetComponent<TextMeshProUGUI>();
        canvasMain = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();
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
            txtGate.text = "Knife";
        }
        else if (myGateType == GateType.Baby)
        {
            txtGate.text = "Baby";
        }
        else if (myGateType == GateType.Mouse)
        {
            txtGate.text = "Mouse";
        }
        else if (myGateType == GateType.Snake)
        {
            txtGate.text = "Snake";
        }
        else if (myGateType == GateType.Money)
        {
            txtGate.text = "Money";
        }
        else if (myGateType == GateType.Crown)
        {
            txtGate.text = "Crown";
        }
        else if (myGateType == GateType.Sock)
        {
            txtGate.text = "Sock";
        }
        else if (myGateType == GateType.Cake)
        {
            txtGate.text = "Cake";
        }
        txtGate.transform.parent.GetComponent<Image>().rectTransform.DOAnchorPos(new Vector2(0, 400), 1f).SetDelay(2f);
    }

    public void MovementBar()
    {
        GameObject current = Instantiate(Resources.Load<GameObject>("Harf"), canvasMain.transform.GetChild(5));
        myGateType = GetComponentInParent<HarfGate>().myGateType;

        if (myGateType == GateType.policeCar)
        {
            txtGate.text = "Police Car";
            current.GetComponent<TextMeshProUGUI>().text = "o";
        }
        else if (myGateType == GateType.Banana)
        {
            txtGate.text = "Banana";
            current.GetComponent<TextMeshProUGUI>().text = "n";
        }
        else if (myGateType == GateType.Apple)
        {
            txtGate.text = "Apple";
            current.GetComponent<TextMeshProUGUI>().text = "e";
        }
        else if (myGateType == GateType.Bike)
        {
            txtGate.text = "Bike";
            current.GetComponent<TextMeshProUGUI>().text = "k";
        }
        else if (myGateType == GateType.Car)
        {
            txtGate.text = "Car";
            current.GetComponent<TextMeshProUGUI>().text = "r";
        }
        else if (myGateType == GateType.Girl)
        {
            txtGate.text = "Girl ";
            current.GetComponent<TextMeshProUGUI>().text = "i";
        }
        else if (myGateType == GateType.Ambulance)
        {
            txtGate.text = "Ambulance";
            current.GetComponent<TextMeshProUGUI>().text = "b";
        }
        else if (myGateType == GateType.Ring)
        {
            txtGate.text = "Ring";
            current.GetComponent<TextMeshProUGUI>().text = "n";
        }
        else if (myGateType == GateType.Ball)
        {
            txtGate.text = "Ball";
            current.GetComponent<TextMeshProUGUI>().text = "l";
        }
        else if (myGateType == GateType.Candy)
        {
            txtGate.text = "Candy";
            current.GetComponent<TextMeshProUGUI>().text = "d";
        }
        else if (myGateType == GateType.Knife)
        {
            txtGate.text = "Knife";
            current.GetComponent<TextMeshProUGUI>().text = "f";
        }
        else if (myGateType == GateType.Baby)
        {
            txtGate.text = "Baby";
            current.GetComponent<TextMeshProUGUI>().text = "b";
        }
        else if (myGateType == GateType.Mouse)
        {
            txtGate.text = "Mouse";
            current.GetComponent<TextMeshProUGUI>().text = "s";
        }
        else if (myGateType == GateType.Snake)
        {
            txtGate.text = "Snake";
            current.GetComponent<TextMeshProUGUI>().text = "k";
        }
        else if (myGateType == GateType.Money)
        {
            txtGate.text = "Money";
            current.GetComponent<TextMeshProUGUI>().text = "o";
        }
        else if (myGateType == GateType.Crown)
        {
            txtGate.text = "Crown";
            current.GetComponent<TextMeshProUGUI>().text = "w";
        }
        else if (myGateType == GateType.Sock)
        {
            txtGate.text = "Sock";
            current.GetComponent<TextMeshProUGUI>().text = "c";
        }
        else if (myGateType == GateType.Cake)
        {
            txtGate.text = "Cake";
            current.GetComponent<TextMeshProUGUI>().text = "a";
        }
        Vector3 goldpos = Camera.main.WorldToScreenPoint(this.transform.position);
        current.transform.position = goldpos;
        current.transform.DOLocalMove(canvasMain.transform.GetChild(5).GetChild(0).transform.localPosition, 1f).OnComplete(() =>
        {
            Destroy(current);
            ShowText();
        });
    }
}
