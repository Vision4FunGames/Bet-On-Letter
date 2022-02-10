using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MoneyTransfer
{
    public enum GateMath
    {
        Minus,
        Divide,
        Addition,
        Multiplication
    }

    public enum GateBrand
    {
        Apple,
        Android,
        Dollar,
        Bitcoin,
        Tesla,
        Ford,
        Ebay,
        Amazon,
        Vine,
        TikTok,
        Spotify,
        Walkman,
        McDonalds,
        BurgerKing,
        CocaCola,
        Pepsi,
        Nike,
        Puma,
        Netflix,
        Uydu
    }

    public class Gate : MonoBehaviour
    {
        //public GateBrand gateBrand;
        public GateMath gateMath;
        [SerializeField] private float amount;
        private float lastAmount;
        [HideInInspector]
        public Sprite sprite;
        TextMeshProUGUI textGate;
        public bool isHarfGate;

        private void Start()
        {
            Canvas canvas = transform.GetComponentInChildren<Canvas>();
            //sprite = canvas.transform.GetChild(0).GetComponent<Image>().sprite;
            textGate = canvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

            if(isHarfGate)
            {
                textGate.gameObject.SetActive(false);
            }

            if (gateMath == GateMath.Addition) // toplama
            {
                textGate.text = "+" + amount;
            }
            else if (gateMath == GateMath.Divide) // bölme
            {
                textGate.text = "/" + amount;
            }
            else if (gateMath == GateMath.Minus) // çýkarma
            {
                textGate.text = "-" + amount;
            }
            else if (gateMath == GateMath.Multiplication) //çarpma
            {
                textGate.text = "x" + amount;
            }
        }

        public float GetAmount(float currentAmount)
        {
            float tempAmount = 0;

            switch (gateMath)
            {
                case GateMath.Minus:
                    tempAmount = amount;
                    break;
                case GateMath.Divide:
                    tempAmount = currentAmount / amount;
                    break;
                case GateMath.Addition:
                    tempAmount = amount;
                    break;
                case GateMath.Multiplication:
                    tempAmount = currentAmount * amount;
                    break;
            }

            lastAmount = Mathf.FloorToInt(tempAmount);
            return lastAmount;
        }

        //public string GetPercentage()
        //{
        //    string amount = "";

        //    switch (gateBrand)
        //    {
        //        case GateBrand.Apple:
        //            amount = "+%125";
        //            break;
        //        case GateBrand.Android:
        //            amount = "-%40";
        //            break;
        //        case GateBrand.Dollar:
        //            amount = "-%60";
        //            break;
        //        case GateBrand.Bitcoin:
        //            amount = "-%70";
        //            break;
        //        case GateBrand.Tesla:
        //            amount = "-%73";
        //            break;
        //        case GateBrand.Ford:
        //            amount = "-%44";
        //            break;
        //        case GateBrand.Ebay:
        //            amount = "+%20";
        //            break;
        //        case GateBrand.Amazon:
        //            amount = "+%97";
        //            break;
        //        case GateBrand.Vine:
        //            amount = "-%73";
        //            break;
        //        case GateBrand.TikTok:
        //            amount = "-%42";
        //            break;
        //        case GateBrand.Spotify:
        //            amount ="+%55";
        //            break;
        //        case GateBrand.Walkman:
        //            amount = "-%92";
        //            break;
        //        case GateBrand.McDonalds:
        //            amount = "-%62";
        //            break;
        //        case GateBrand.BurgerKing:
        //            amount = "+%23";
        //            break;
        //        case GateBrand.CocaCola:
        //            amount = "+%48";
        //            break;
        //        case GateBrand.Pepsi:
        //            amount = "+%17";
        //            break;
        //        case GateBrand.Nike:
        //            amount = "+%66";
        //            break;
        //        case GateBrand.Puma:
        //            amount = "-%48";
        //            break;
        //        case GateBrand.Netflix:
        //            amount = "+%82";
        //            break;
        //        case GateBrand.Uydu:
        //            amount = "-%75";
        //            break;
        //        default:
        //            break;
        //    }
        //    return amount;
        //}
    }
}

