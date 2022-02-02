using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyTransfer
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject moneyPrefab;
        [SerializeField] private int spawnCount = 200;
        [HideInInspector] private readonly List<Money> moneylist = new List<Money>();
        [SerializeField] private Transform container;

        private void Start()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Money moneyObject = Instantiate(moneyPrefab, transform.position, Quaternion.identity, container).GetComponent<Money>();
                moneyObject.gameObject.SetActive(false);
                moneylist.Add(moneyObject);
            }
        }

        public Money GetMoney()
        {
            Money money = null;
            for (int i = 0; i < moneylist.Count; i++)
            {
                Money tempMoney = moneylist[i];
                if (!tempMoney.gameObject.activeInHierarchy)
                {
                    money = tempMoney;
                    break;
                }
            }

            if(money == null)
            {
                Money moneyObject = Instantiate(moneyPrefab, transform.position, Quaternion.identity, container).GetComponent<Money>();
                moneyObject.gameObject.SetActive(false);
                moneylist.Add(moneyObject);
                money = moneyObject;
            }

            if (money != null) money.gameObject.SetActive(true);
            return money;
        }

        public void SendList(Money money)
        {
            if (money != null)
            {
                money.gameObject.SetActive(false);
                money.transform.SetParent(container);
                money.ResetProperty();
            }
        }
    }
}

