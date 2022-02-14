using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GateType
{
    policeCar, Banana, Apple, Bike, Car, Ambulance, Ring, Ball, Candy, Knife, Baby, Mouse, Snake, Money, Crown, Sock, Cake,Girl
}
public class HarfGate : MonoBehaviour
{
    iconManager iconManager;
    public GateType myGateType;
    public void asasas()
    {
        string a = SetText();
    }
    public void Start()
    {
        iconManager = FindObjectOfType<iconManager>();
    }
    public string SetText()
    {
        string gateTypeString;
        if (myGateType == GateType.policeCar)
        {
            gateTypeString = "P _ lice Car";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.policeCar;
            return gateTypeString;
        }
        else if(myGateType == GateType.Banana)
        {
            gateTypeString = "Ba _ ana";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.banana;
            return gateTypeString;
        }
        else if (myGateType == GateType.Apple)
        {
            gateTypeString = "Appl _ ";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.apple;
            return gateTypeString;
        }
        else if (myGateType == GateType.Bike)
        {
            gateTypeString = "Bi _ e ";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.bike;
            return gateTypeString;
        }
        else if (myGateType == GateType.Car)
        {
            gateTypeString = "Ca _ ";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.car;
            return gateTypeString;
        }
        else if (myGateType == GateType.Girl)
        {
            gateTypeString = "G _ rl ";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.girl;
            return gateTypeString;
        }
        else if (myGateType == GateType.Ambulance)
        {
            gateTypeString = "Am _ ulance";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.ambulance;
            return gateTypeString;
        }
        else if (myGateType == GateType.Ring)
        {
            gateTypeString = "Ri _ g";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.ring;
            return gateTypeString;
        }
        else if (myGateType == GateType.Ball)
        {
            gateTypeString = "Ba _ l";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.ball;
            return gateTypeString;
        }
        else if (myGateType == GateType.Candy)
        {
            gateTypeString = "Can _ y";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.candy;
            return gateTypeString;
        }
        else if (myGateType == GateType.Knife)
        {
            gateTypeString = "Kni _ e";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.knife;
            return gateTypeString;
        }
        else if (myGateType == GateType.Baby)
        {
            gateTypeString = "_ aby";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.baby;
            return gateTypeString;
        }
        else if (myGateType == GateType.Mouse)
        {
            gateTypeString = "Mou _ e";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.mouse;
            return gateTypeString;
        }
        else if (myGateType == GateType.Snake)
        {
            gateTypeString = "Sna _ e";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.snake;
            return gateTypeString;
        }
        else if (myGateType == GateType.Money)
        {
            gateTypeString = "M _ ney";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.money;
            return gateTypeString;
        }
        else if (myGateType == GateType.Crown)
        {
            gateTypeString = "Cro _ n";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.crown;
            return gateTypeString;
        }
        else if (myGateType == GateType.Sock)
        {
            gateTypeString = "So _ k";
            return gateTypeString;
        }
        else if (myGateType == GateType.Cake)
        {
            gateTypeString = "C _ ke";
            iconManager.iconImage.GetComponent<Image>().sprite = iconManager.cake;
            return gateTypeString;
        }
        return null;
    }
}

