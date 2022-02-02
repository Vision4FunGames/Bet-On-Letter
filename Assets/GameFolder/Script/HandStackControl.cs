using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System.Linq;

namespace MoneyTransfer
{
    public class HandStackControl : MonoBehaviour
    {
        public List<Money> moneyList = new List<Money>();
        [SerializeField] private CountDisplay countDisplay;
        public Transform handPoint;
        public bool isRefresh = false;
        [HorizontalLine(5)] [SerializeField] private int testCount = 10;
        private HandPropertyControl _handPropertyControl;
        [HideInInspector]
        public float currentHeight;
        private MoneySeperate _moneySeperate;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            RefreshListIndex();
        }


        private void Initialize()
        {
            _handPropertyControl = FindObjectOfType<HandPropertyControl>();
            _moneySeperate = FindObjectOfType<MoneySeperate>();
            currentHeight = handPoint.transform.position.y;
        }

        public void AddToList(Money money)
        {
            moneyList.Add(money);
            currentHeight += GameManager.Instance.high;
            RefreshList(true);
            SendToCountDisplay();
        }

        public void RemoveToList(Money money)
        {
            moneyList.Remove(money);
            currentHeight -= GameManager.Instance.high;
            RefreshList(false);
            SendToCountDisplay();
        }

        private void SendToCountDisplay()
        {
            countDisplay.SetToText(moneyList.Count);
        }

        public List<Money> GetMoneyContainer(int count)
        {
            List<Money> tempMoneyList = null;
            int leftIndex = 0;
            int currentListCount = moneyList.Count;
            if (currentListCount != 0)
            {
                tempMoneyList = new List<Money>();

                int listCount = currentListCount - 1;
                int loopCount = listCount - count;

                for (int i = listCount; i > loopCount; i--)
                {
                    if (leftIndex < currentListCount)
                    {
                        leftIndex++;
                        Money tempMoney = moneyList[i];
                        tempMoneyList.Add(tempMoney);
                      //  RemoveToList(tempMoney);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return tempMoneyList;
        }

        private void RefreshListIndex()
        {
            Vector3 handPosition = handPoint.localPosition;
            for (int i = 0; i < moneyList.Count; i++)
            {
                Money money = moneyList[i];

                if (i == 0)
                {
                    money.transform.localPosition = handPoint.localPosition;
                }
                else
                {
                    float heightOffset = handPosition.y + (i * GameManager.Instance.high);
                    money.transform.localPosition = new Vector3(handPosition.x, heightOffset, handPosition.z);
                }
            }
        }

        public void RefreshList(bool isPositive)
        {
            StartCoroutine(RefresListWithDelay(isPositive));
        }

        IEnumerator RefresListWithDelay(bool isPositive)
        {
            for (int i = 0; i < moneyList.Count; i++)
            {
                Money money = moneyList[i];
                money.DoScaleEffect(isPositive);
                yield return _handPropertyControl.coroutineDelay;
            }
        }


        public void DetectNegativeGate(int amount)
        {
            List<Money> moneyList = GetMoneyContainer(amount);
            StartCoroutine(DetectNegativeGate(moneyList));
        }

        IEnumerator DetectNegativeGate(List<Money> moneyList)
        {
            if (moneyList != null)
            {
                for (int i = 0; i < moneyList.Count; i++)
                {
                    Money tempMoney = moneyList[i];
                    tempMoney.isGetHit = true;
                    RemoveToList(tempMoney);
                    _moneySeperate.RemoveManagerList(tempMoney);
                    tempMoney.transform.SetParent(null);
                }

                for (int i = 0; i < moneyList.Count; i++)
                {
                    yield return _handPropertyControl.coroutineDelay;
                    Money money = moneyList[i];
                    Vector3 movePosition = new Vector3(Random.Range(-_handPropertyControl.gateSeperateOffsetX, _handPropertyControl.gateSeperateOffsetX),
                        _handPropertyControl.gateSeperateOffsetY, Random.Range(0, -_handPropertyControl.gateSeperateOffsetZ));
                    money.transform.DOMove(movePosition, _handPropertyControl.moveProcessDuration).SetRelative().SetEase(Ease.OutQuart).OnComplete(() => money.DestroyAndGoPool());
                    Vector3 randomRotate = new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                    money.transform.DORotate(randomRotate * 360, _handPropertyControl.rotateProcessDuration, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear).SetLoops(10, LoopType.Restart);
                }
            }
        }


        public void HitTheObstacle()
        {
            List<Money> moneyForDestroy = GetMoneyContainer(GameManager.Instance.seperateMoneyPerObstacle);
            if (moneyForDestroy != null) StartCoroutine(HitTheObstacleWithDelay(moneyForDestroy));
        }

        IEnumerator HitTheObstacleWithDelay(List<Money> moneyForDestroy)
        {
            if (moneyForDestroy != null)
            {
                for (int i = 0; i < moneyForDestroy.Count; i++)
                {
                    Money money = moneyForDestroy[i];
                    money.isGetHit = true;
                    RemoveToList(money);
                    _moneySeperate.RemoveManagerList(money);
                    money.transform.SetParent(null);
                }

                for (int i = 0; i < moneyForDestroy.Count; i++)
                {
                    yield return _handPropertyControl.coroutineDelay;
                    Money money = moneyForDestroy[i];
                    Vector3 movePosition = new Vector3(Random.Range(-_handPropertyControl.seperateOffsetX, _handPropertyControl.seperateOffsetX), _handPropertyControl.maxYMovement, Random.Range(-_handPropertyControl.seperateOffsetZ, _handPropertyControl.seperateOffsetZ));
                    money.transform.DOMove(movePosition, _handPropertyControl.moveProcessDuration).SetRelative().SetEase(Ease.OutQuart).OnComplete(() => money.DestroyAndGoPool());
                    Vector3 randomRotate = new Vector3(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
                    money.transform.DORotate(randomRotate * 360, _handPropertyControl.rotateProcessDuration, RotateMode.FastBeyond360).SetRelative().SetEase(Ease.Linear).SetLoops(10, LoopType.Restart);
                }
            }
        }
    }
}
