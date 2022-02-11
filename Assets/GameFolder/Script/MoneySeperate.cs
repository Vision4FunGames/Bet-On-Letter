using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoneyTransfer;
using UnityEngine.Events;

namespace MoneyTransfer
{
    public class MoneySeperate : MonoBehaviour
    {
        [SerializeField] private float spawnCountForLeft = 20;
        [SerializeField] private float spawnCountForRight = 20;
        [SerializeField] List<Money> managerMoneyList = new List<Money>();
        [SerializeField] private HandStackControl leftHandControl;
        [SerializeField] private HandStackControl rightHandControl;
        private WaitForSeconds delay = new WaitForSeconds(.05f);
        private WaitForSeconds delayForFinish = new WaitForSeconds(.05f);
        private bool _isLeftHandProgress = false;
        private bool _isRightHandProgress = false;
        private ObjectPool _objectPool;
        private FinishManager finishManager;

        private void OnEnable()
        {
            Observer.OnGameFinish.AddListener(ObserverListener);
        }

        private void OnDisable()
        {
            Observer.OnGameFinish.RemoveListener(ObserverListener);
        }

        private void Start()
        {
            Initialize();
        }

        private void ObserverListener()
        {
            SendToFinishTarget();
        }

        private void Initialize()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
            finishManager = FindObjectOfType<FinishManager>();
            Invoke(nameof(SpawnMoneyToHand), .1f);
        }

        private void SpawnMoneyToHand()
        {
            for (int i = 0; i < spawnCountForLeft; i++)
            {
                Money moneyForLeft = _objectPool.GetMoney();
                leftHandControl.AddToList(moneyForLeft);
                moneyForLeft.handStackControl = leftHandControl;
                AddToManagerList(moneyForLeft);
            }

            for (int i = 0; i < spawnCountForRight; i++)
            {
                Money moneyForRight = _objectPool.GetMoney();
                rightHandControl.AddToList(moneyForRight);
                moneyForRight.handStackControl = rightHandControl;
                AddToManagerList(moneyForRight);
            }
        }


        public void AddToManagerList(Money money)
        {
            managerMoneyList.Add(money);
            CheckMoneyCount();
        }

        public void RemoveManagerList(Money money)
        {
            managerMoneyList.Remove(money);
            CheckMoneyCount();
        }

        private void CheckMoneyCount()
        {
            if (managerMoneyList.Count == 0)
            {
                Observer.OnGameLose?.Invoke();
            }
        }

        #region MoneyMathematic
        public void HandleMoney(HandStackControl handBaseControl, GateMath gateMath, float amount, bool isPack = false)
        {
            if (handBaseControl.moneyList.Count > 0 || isPack == true)
            {
                if (gateMath == GateMath.Addition || gateMath == GateMath.Multiplication)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        Money money = _objectPool.GetMoney();
                        money.handStackControl = handBaseControl;
                        money.transform.localPosition = new Vector3(handBaseControl.handPoint.localPosition.x, handBaseControl.currentHeight, handBaseControl.handPoint.localPosition.z);
                        AddToManagerList(money);
                        if (money != null) handBaseControl.AddToList(money);
                    }
                }
                else
                {
                    handBaseControl.DetectNegativeGate((int)amount);
                }
            }
        }
        public void HandleMoney2(HandStackControl handBaseControl, GameObject target, GateMath gateMath, float amount, bool isPack = false)
        {
            if (handBaseControl.moneyList.Count > 0 || isPack == true)
            {
                if (gateMath == GateMath.Addition || gateMath == GateMath.Multiplication)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        Money money = _objectPool.GetMoney();
                        money.handStackControl = handBaseControl;
                        money.transform.localPosition = new Vector3(handBaseControl.handPoint.localPosition.x, handBaseControl.currentHeight, handBaseControl.handPoint.localPosition.z);
                        AddToManagerList(money);
                        if (money != null) handBaseControl.AddToList(money);
                    }
                }
                else
                {
                    handBaseControl.DetectNegativeGate2((int)amount, target);
                }
            }
        }
        #endregion

        #region SendMoneyToLeftHand
        public void SendMoneyToLeftHand(int count)
        {
            if (!_isLeftHandProgress)
            {
                _isLeftHandProgress = true;
                StartCoroutine(SendMoneyLeftHandByDelay(count));
            }
        }

        IEnumerator SendMoneyLeftHandByDelay(int count)
        {
            List<Money> tempMoneyList = rightHandControl.GetMoneyContainer(count);

            if (tempMoneyList != null)
            {
                for (int i = 0; i < tempMoneyList.Count; i++)
                {
                    Money tempMoney = tempMoneyList[i];
                    rightHandControl.RemoveToList(tempMoney);
                    tempMoney.JumpToTargetHand(rightHandControl, leftHandControl, false);
                    yield return delay;
                }
            }

            _isLeftHandProgress = false;
        }
        #endregion

        #region SendMoneyToRightHand
        public void SendMoneyToRightHand(int count)
        {
            if (!_isRightHandProgress)
            {
                _isRightHandProgress = true;
                StartCoroutine(SendMoneyToRightHandByDelay(count));
            }
        }

        IEnumerator SendMoneyToRightHandByDelay(int count)
        {
            List<Money> tempMoneyList = leftHandControl.GetMoneyContainer(count);
            if (tempMoneyList != null)
            {
                for (int i = 0; i < tempMoneyList.Count; i++)
                {
                    Money tempMoney = tempMoneyList[i];
                    leftHandControl.RemoveToList(tempMoney);
                    tempMoney.JumpToTargetHand(leftHandControl, rightHandControl, true);
                    yield return delay;
                }
            }

            _isRightHandProgress = false;
        }
        #endregion

        #region SendToFinish
        private void SendToFinishTarget()
        {
            StartCoroutine(SendToFinishTargetWithDelay());
        }

        IEnumerator SendToFinishTargetWithDelay()
        {
            yield return new WaitUntil(() => finishManager.isReachedToTarget == true);
            int currentIndex = 0;
            List<Money> finishMoneyList = new List<Money>();

            for (int i = 0; i < managerMoneyList.Count; i++)
            {
                if (currentIndex < leftHandControl.moneyList.Count)
                {
                    Money money = leftHandControl.moneyList[i];
                    finishMoneyList.Add(money);
                }

                if (currentIndex < rightHandControl.moneyList.Count)
                {
                    Money money = rightHandControl.moneyList[i];
                    finishMoneyList.Add(money);
                }

                currentIndex++;
            }


            for (int i = 0; i < finishMoneyList.Count; i++)
            {
                if (i == finishMoneyList.Count - 1)
                {
                    SlotMachineScript sc = FindObjectOfType<SlotMachineScript>();
                    sc.StartRotating(i);
                    //finishManager.slotMachineText
                }
                Money money = finishMoneyList[i];
                money.handStackControl.RemoveToList(money);
                finishManager.AddToFinishList(money);
                money.JumpToFinishLine(finishManager, i);
                yield return delayForFinish;
            }
            //finishManager.MoneyTranferProcessDone();
        }

        #endregion
    }
}



