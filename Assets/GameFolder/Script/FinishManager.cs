using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoneyTransfer;
using DG.Tweening;

namespace MoneyTransfer
{
    public class FinishManager : MonoBehaviour
    {
        [HideInInspector]
        public List<Money> finishMoneyList = new List<Money>();
        public Transform targetPoint;
        [SerializeField] Transform player;
        private const float high = 0.1f;
        private WaitForSeconds delay = new WaitForSeconds(0.01f);
        [HideInInspector]
        public float currentHeight = 0;
        public bool isReachedToTarget;
        [SerializeField] private float maxHeight;
        private bool isMaxHeight;
        private HandPropertyControl handPropertyControl;
        public float finishHigh;

        private void Start()
        {
            handPropertyControl = FindObjectOfType<HandPropertyControl>();
        }


        private void OnTriggerEnter(Collider other)
        {
            MoneySeperate moneySeperate = other.GetComponent<MoneySeperate>();
            if (moneySeperate != null)
            {
                Observer.OnGameFinish?.Invoke();
                MoveToTarget();
            }
        }

        public void AddToFinishList(Money money)
        {
            money.transform.SetParent(null);
            finishMoneyList.Add(money);
            RefreshList(money);
        }

        public void RefreshList(Money money)
        {
            if (isMaxHeight)
            {
                money.gameObject.SetActive(false);
            }
            
            //Vector3 targetPosition = targetPoint.position;
            //if (finishMoneyList.Count == 0)
            //{
            //    money.transform.position = targetPoint.position;
            //}
            //else
            //{
            //    float heightOffset = finishMoneyList[finishMoneyList.Count - 1].transform.position.y + GameManager.Instance.high;
            //    money.transform.position = new Vector3(targetPosition.x, heightOffset, targetPosition.z);
            //}
            money.DoScaleEffect(true);
        }

        private void Update()
        {
            if (isReachedToTarget)
            {
                RefreshPlayerPosition();
            }
        }

        private void MoveToTarget()
        {
            player.transform.DOMoveZ(targetPoint.position.z, 1f).SetEase(Ease.Linear).OnComplete(() => isReachedToTarget = true);
        }

        public void MoneyTranferProcessDone()
        {
            Collider collider = handPropertyControl.FamousCheck();
            StartCoroutine(Finish(collider.transform));
        }

        IEnumerator Finish(Transform famousObject)
        {
            famousObject.transform.DOScale(Vector3.one * .3f, .3f).SetLoops(6, LoopType.Yoyo);
            yield return new WaitForSeconds(1);
            Observer.OnGameWin?.Invoke();
        }

        private void RefreshPlayerPosition()
        {
            if (finishMoneyList.Count == 0) return;
            Vector3 targetPosition = new Vector3(targetPoint.position.x, GameManager.Instance.characterOffsetForFinish + currentHeight, targetPoint.position.z);
            player.position = Vector3.Lerp(player.position, targetPosition, 3 * Time.deltaTime);
            player.position = new Vector3(player.position.x, Mathf.Clamp(player.position.y, -maxHeight, maxHeight), player.position.z);
            if(player.position.y + 0.25f >= maxHeight)
            {
                isMaxHeight = true;
            }
            else
            {
                isMaxHeight = false;
            }
        }
    }
}
