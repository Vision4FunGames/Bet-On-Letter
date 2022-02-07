using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateType
{
    policeCar, Banana, Apple, Bike, Car, Ambulance, Ring, Ball, Candy, Knife, Baby, Mouse, Snake, Money, Crown, Sock, Cake,Girl
}
public class HarfGate : MonoBehaviour
{
    public GateType myGateType;
    public void asasas()
    {
        string a = SetText();
    }
    public string SetText()
    {
        string gateTypeString;
        if (myGateType == GateType.policeCar)
        {
            gateTypeString = "P _ lice Car";
            return gateTypeString;
        }
        else if(myGateType == GateType.Banana)
        {
            gateTypeString = "Ba _ ana";
            return gateTypeString;
        }
        else if (myGateType == GateType.Apple)
        {
            gateTypeString = "Appl _ ";
            return gateTypeString;
        }
        else if (myGateType == GateType.Bike)
        {
            gateTypeString = "Bi _ e ";
            return gateTypeString;
        }
        else if (myGateType == GateType.Car)
        {
            gateTypeString = "Ca _ ";
            return gateTypeString;
        }
        else if (myGateType == GateType.Girl)
        {
            gateTypeString = "G _ rl ";
            return gateTypeString;
        }
        else if (myGateType == GateType.Ambulance)
        {
            gateTypeString = "Am _ ulance";
            return gateTypeString;
        }
        else if (myGateType == GateType.Ring)
        {
            gateTypeString = "Ri _ g";
            return gateTypeString;
        }
        else if (myGateType == GateType.Ball)
        {
            gateTypeString = "Ba _ l";
            return gateTypeString;
        }
        else if (myGateType == GateType.Candy)
        {
            gateTypeString = "Can _ y";
            return gateTypeString;
        }
        else if (myGateType == GateType.Knife)
        {
            gateTypeString = "Kni _ e";
            return gateTypeString;
        }
        else if (myGateType == GateType.Baby)
        {
            gateTypeString = "_ aby";
            return gateTypeString;
        }
        else if (myGateType == GateType.Mouse)
        {
            gateTypeString = "Mou _ e";
            return gateTypeString;
        }
        else if (myGateType == GateType.Snake)
        {
            gateTypeString = "Sna _ e";
            return gateTypeString;
        }
        else if (myGateType == GateType.Money)
        {
            gateTypeString = "M _ ney";
            return gateTypeString;
        }
        else if (myGateType == GateType.Crown)
        {
            gateTypeString = "Cro _ n";
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
            return gateTypeString;
        }
        return null;
    }
}

