using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MoneyTransfer
{
    public class CountDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI moneyDisplay;

        public void SetToText(int amount)
        {
            moneyDisplay.text = amount.ToString();
        }
    }
}

