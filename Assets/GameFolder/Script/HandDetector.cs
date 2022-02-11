using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyTransfer
{
    public class HandDetector : MonoBehaviour
    {
        [SerializeField] private bool isLeft;
        private HandStackControl _handBaseControl;
        private MoneySeperate _moneySeparate;
        private HandControl _handControl;
        private InvestManager _investManager;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _handBaseControl = GetComponent<HandStackControl>();
            _moneySeparate = FindObjectOfType<MoneySeperate>();
            _handControl = FindObjectOfType<HandControl>();
            _investManager = FindObjectOfType<InvestManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            Gate gate = other.GetComponent<Gate>();

            if (gate != null)
            {
                print("d");
                VibrationManager.Instance.VibratePop();
                _investManager.OpenInvestPanel(true);
                if (gate.gateMath == GateMath.Addition || gate.gateMath == GateMath.Multiplication)
                {
                    if (isLeft)
                    {
                        _investManager.LeftHandRenderer(true, gate.sprite, _handBaseControl, gate.gateMath, gate.GetAmount(_handBaseControl.moneyList.Count));
                    }
                    else
                    {
                        _investManager.RightHandRenderer(true, gate.sprite, _handBaseControl, gate.gateMath, gate.GetAmount(_handBaseControl.moneyList.Count));
                    }
                }
                else
                {
                    if (isLeft)
                    {
                        _investManager.LeftHandRenderer(false, gate.sprite, _handBaseControl, gate.gateMath, gate.GetAmount(_handBaseControl.moneyList.Count));
                    }
                    else
                    {
                        _investManager.RightHandRenderer(false, gate.sprite, _handBaseControl, gate.gateMath, gate.GetAmount(_handBaseControl.moneyList.Count));
                    }
                }
            }

            Obstacle obstacle = other.GetComponent<Obstacle>();

            if (obstacle != null)
            {
                print("c");
                VibrationManager.Instance.VibratePop();
                _handControl.isMoneyTransfer = false;
                _handBaseControl.HitTheObstacle();
            }

            MoneyPack moneyPack = other.GetComponent<MoneyPack>();

            if (moneyPack != null)
            {
                print("b");
                VibrationManager.Instance.VibratePop();
                _moneySeparate.HandleMoney(_handBaseControl, GateMath.Addition, GameManager.Instance.moneyPackIncreaseCount, true);
                moneyPack.DestroyMoney();
            }
            MýknatýsScript ms = other.GetComponent<MýknatýsScript>();
            if (ms != null)
            {
                print("a");
                VibrationManager.Instance.VibratePop();
                _moneySeparate.HandleMoney2(_handBaseControl, ms.transform.GetChild(2).gameObject, GateMath.Minus, 2, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Obstacle obstacle = other.GetComponent<Obstacle>();

            if (obstacle != null)
            {
                _handControl.isMoneyTransfer = true;
            }
        }
    }
}

