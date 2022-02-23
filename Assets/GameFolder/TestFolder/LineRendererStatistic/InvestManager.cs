using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace MoneyTransfer
{
    public class InvestManager : MonoBehaviour
    {
        [SerializeField] private Transform investPanelHolder;

        [Header("Left Hand Invest Panel")]
        [SerializeField] private LineRenderer leftRenderer;
        [SerializeField] private Transform[] leftlowPoint;
        [SerializeField] private Transform[] leftHighPoint;
        [SerializeField] private Image LeftInvestImage;
        [SerializeField] private TextMeshProUGUI leftInvestHighText;
        [SerializeField] private TextMeshProUGUI leftInvestLowText;
        [SerializeField] private SpriteRenderer leftInvestArrow;

        //-----------------------------------------------------------//

        [Header("Right Hand Invest Panel")]
        [SerializeField] private LineRenderer rightRenderer;
        [SerializeField] private Transform[] rightLowPoint;
        [SerializeField] private Transform[] rightHighPoint;
        [SerializeField] private Image rightInvestImage;
        [SerializeField] private TextMeshProUGUI rightInvestHightText;
        [SerializeField] private TextMeshProUGUI rightInvestLowText;
        [SerializeField] private SpriteRenderer rightInvestArrow;

        //--------------------------------------------------------------//
        [Header("Process Timing Parameter")]
        [SerializeField] private float moveDuration = 0f;
        [SerializeField] private float waitAfterProcess = 0f;


        private bool isPanoMovement = false;
        //-----------------------------------------------------
        private Vector3 initLocalRightRenderer;
        private Vector3 initLocalLeftRenderer;
        [Header("Invest Panel Setting")]
        [SerializeField] private Transform showPos;
        [SerializeField] private Transform hidePos;
        [SerializeField] private float duration;
        private HandControl handControl;
        private MoneySeperate moneySeperate;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            handControl = FindObjectOfType<HandControl>();
            moneySeperate = FindObjectOfType<MoneySeperate>();
        }


        private void OnEnable()
        {
            initLocalLeftRenderer = leftInvestArrow.transform.localPosition;
            initLocalRightRenderer = rightInvestArrow.transform.localPosition;
        }

        private void OnDisable()
        {
            ResetProperty();
        }

        public void OpenInvestPanel(bool state)
        {
            if (state)
            {
                handControl.SetSpeed(true);
                // Observer.OnChangeForPano?.Invoke();
                isPanoMovement = true;
                //investPanelHolder.DOMove(showPos.position, duration).OnComplete(() => isPanoMovement = true).SetUpdate(UpdateType.Normal);
            }
            else
            {
                handControl.SetSpeed(false);
                //Observer.OnChangeForInitial?.Invoke();
                rightRenderer.positionCount = 0;
                leftRenderer.positionCount = 0;
                //investPanelHolder.DOMove(hidePos.position, duration).OnComplete(() => ResetProperty()).SetUpdate(UpdateType.Normal);
            }
        }

        public void RightHandRenderer(bool isIncrease, Sprite sprite, HandStackControl handStackControl, GateMath gateMath, float amount)
        {
            if (isIncrease)
            {
                StartCoroutine(RightHandInvestProcess(rightHighPoint, rightInvestHightText.gameObject, handStackControl, gateMath, amount));
            }
            else
            {
                StartCoroutine(RightHandInvestProcess(rightLowPoint, rightInvestLowText.gameObject, handStackControl, gateMath, amount));
            }

            RightHandSetProperty(isIncrease, sprite);
        }

        private void RightHandSetProperty(bool isIncrease, Sprite sprite)
        {
            rightInvestImage.sprite = sprite;

            if (isIncrease)
            {
                rightRenderer.material.color = Color.green;
                rightInvestArrow.color = Color.green;
                //ightInvestHightText.text = textInfo;
                rightInvestHightText.gameObject.SetActive(false);
            }
            else
            {
                rightRenderer.material.color = Color.red;
                rightInvestArrow.color = Color.red;
                //ightInvestLowText.text = textInfo;
                rightInvestLowText.gameObject.SetActive(false);
            }
        }

        IEnumerator RightHandInvestProcess(Transform[] path, GameObject textComponent, HandStackControl handStackControl, GateMath gateMath, float amount)
        {
            Transform rightHand = rightInvestArrow.transform;
            //yield return new WaitUntil(() => isPanoMovement == true);
            rightRenderer.positionCount = 1;
            rightRenderer.SetPosition(0, path[0].localPosition);

            //for (int i = 0; i < path.Length; i++)
            //{
            //    bool isDone = false;

            //    if (i != 0)
            //    {
            //        rightRenderer.positionCount++;
            //        rightRenderer.SetPosition(rightRenderer.positionCount - 1, rightHand.localPosition);
            //        Transform targetPath = path[i];
            //        rightHand.rotation = Quaternion.Euler(new Vector3(0, 0, FindAngle(targetPath.position, rightHand.position)));
            //        rightHand.DOLocalMove(targetPath.localPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() => isDone = true).OnUpdate(() => rightRenderer.SetPosition(i, rightHand.localPosition));
            //        yield return new WaitUntil(() => isDone == true);
            //    }
            //}
            textComponent.SetActive(true);
            yield return new WaitForSeconds(waitAfterProcess);
            OpenInvestPanel(false);
            SendMoneyToHandler(handStackControl, gateMath, amount);
        }


        public void LeftHandRenderer(bool isIncrease, Sprite sprite, HandStackControl handStackControl, GateMath gateMath, float amount)
        {
            if (isIncrease)
            {
                StartCoroutine(LeftHandInvestProcess(leftHighPoint, leftInvestHighText.gameObject, handStackControl, gateMath, amount));
            }
            else
            {
                StartCoroutine(LeftHandInvestProcess(leftlowPoint, leftInvestLowText.gameObject, handStackControl, gateMath, amount));
            }

            LeftHandSetProperty(isIncrease, sprite);
        }

        private void LeftHandSetProperty(bool isIncrease, Sprite sprite)
        {
            LeftInvestImage.sprite = sprite;

            if (isIncrease)
            {
                leftRenderer.material.color = Color.green;
                leftInvestArrow.color = Color.green;
                //leftInvestHighText.text = textInfo;
                leftInvestHighText.gameObject.SetActive(false);
            }
            else
            {
                leftRenderer.material.color = Color.red;
                leftInvestArrow.color = Color.red;
                //leftInvestHighText.text = textInfo;
                leftInvestHighText.gameObject.SetActive(false);
            }
        }

        IEnumerator LeftHandInvestProcess(Transform[] path, GameObject textComponent, HandStackControl handStackControl, GateMath gateMath, float amount)
        {
            Transform leftHand = leftInvestArrow.transform;
            //yield return new WaitUntil(() => isPanoMovement == true);
            leftRenderer.positionCount = 1;
            leftRenderer.SetPosition(0, path[0].localPosition);

            //for (int i = 0; i < path.Length; i++)
            //{
            //    bool isDone = false;

            //    if (i != 0)
            //    {
            //        leftRenderer.positionCount++;
            //        leftRenderer.SetPosition(leftRenderer.positionCount - 1, leftHand.localPosition);
            //        Transform targetPath = path[i];
            //        leftHand.rotation = Quaternion.Euler(new Vector3(0, 0, FindAngle(targetPath.position, leftHand.position)));
            //        leftHand.DOLocalMove(targetPath.localPosition, moveDuration).SetEase(Ease.Linear).OnComplete(() => isDone = true).OnUpdate(() => leftRenderer.SetPosition(i, leftHand.localPosition));
            //        yield return new WaitUntil(() => isDone == true);
            //    }
            //}
            textComponent.SetActive(true);
            yield return new WaitForSeconds(waitAfterProcess);
            OpenInvestPanel(false);
            SendMoneyToHandler(handStackControl, gateMath, amount);
        }

        float FindAngle(Vector3 targetPath, Vector3 mainObject)
        {
            targetPath.x = targetPath.x - mainObject.x;
            targetPath.y = targetPath.y - mainObject.y;
            float angle = Mathf.Atan2(targetPath.y, targetPath.x) * Mathf.Rad2Deg;
            return angle;
        }

        private void SendMoneyToHandler(HandStackControl handStackControl, GateMath gateMath, float amount)
        {
            moneySeperate.HandleMoney(handStackControl, gateMath, amount);
        }

        #region TRASH
        //public void LowInvestCoroutineStarter(HandStackControl handStackControl, GateMath gateMath, float amount, Sprite sprite)
        //{
        //    StartCoroutine(StartLowInvestProcess(handStackControl, gateMath, amount, sprite));
        //}

        //IEnumerator StartLowInvestProcess(HandStackControl handStackControl, GateMath gateMath, float amount, Sprite sprite)
        //{
        //    Transform mainInvest = lowInvest.transform;
        //    yield return new WaitUntil(() => isPanoMovement == true);
        //    lowInvestImage.sprite = sprite;
        //    lowInvest.positionCount = 1;
        //    lowInvest.SetPosition(0, lowInvestArray[0].position);

        //    for (int i = 0; i < lowInvestArray.Length; i++)
        //    {
        //        bool isDone = false;
        //        if (i != 0)
        //        {
        //            lowInvest.positionCount++;
        //            lowInvest.SetPosition(lowInvest.positionCount - 1, lowInvest.transform.position);
        //            Transform lowInvestPos = lowInvestArray[i];
        //            Vector3 targetPos = lowInvestPos.position;
        //            Vector3 thisPos = mainInvest.position;
        //            targetPos.x = targetPos.x - thisPos.x;
        //            targetPos.y = targetPos.y - thisPos.y;
        //            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        //            mainInvest.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //            mainInvest.DOMove(lowInvestPos.position, .5f).SetEase(Ease.Linear).OnComplete(() => isDone = true).OnUpdate(() => { lowInvest.SetPosition(i, mainInvest.position + Vector3.forward * 0.05f); });
        //            yield return new WaitUntil(() => isDone == true);
        //        }
        //    }

        //    CalculateInvest(lowInvestText, handStackControl.moneyList.Count, amount);
        //    yield return new WaitForSeconds(0.5f);
        //    OpenInvestPanel(false);
        //    moneySeperate.HandleMoney(handStackControl, gateMath, amount);
        //}

        //public void HightInvestCoroutineStarter(HandStackControl handStackControl, GateMath gateMath, float amount, Sprite sprite)
        //{
        //    StartCoroutine(StartHighInvestProcess(handStackControl, gateMath, amount, sprite));
        //}

        //IEnumerator StartHighInvestProcess(HandStackControl handStackControl, GateMath gateMath, float amount, Sprite sprite)
        //{
        //    Transform mainInvest = highInvest.transform;
        //    yield return new WaitUntil(() => isPanoMovement == true);
        //    hightInvestImage.sprite = sprite;
        //    highInvest.positionCount = 1;
        //    highInvest.SetPosition(0, highInvestArray[0].position);

        //    for (int i = 0; i < highInvestArray.Length; i++)
        //    {
        //        bool isDone = false;
        //        if (i != 0)
        //        {
        //            highInvest.positionCount++;
        //            highInvest.SetPosition(highInvest.positionCount - 1, highInvest.transform.position);
        //            Transform lowInvestPos = highInvestArray[i];
        //            Vector3 targetPos = lowInvestPos.position;
        //            Vector3 thisPos = mainInvest.position;
        //            targetPos.x = targetPos.x - thisPos.x;
        //            targetPos.y = targetPos.y - thisPos.y;
        //            float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        //            mainInvest.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //            mainInvest.DOMove(lowInvestPos.position, .5f).SetEase(Ease.Linear).OnComplete(() => isDone = true).OnUpdate(() => { highInvest.SetPosition(i, mainInvest.position + Vector3.forward * 0.05f); });
        //            yield return new WaitUntil(() => isDone == true);
        //        }
        //    }

        //    CalculateInvest(highInvestText, handStackControl.moneyList.Count, amount);
        //    yield return new WaitForSeconds(0.5f);
        //    OpenInvestPanel(false);
        //    moneySeperate.HandleMoney(handStackControl, gateMath, amount);
        //}

        //private void CalculateInvest(TextMeshProUGUI textMesh, float previousAmount, float nextAmount)
        //{
        //    float amount = 0;

        //    if (previousAmount != 0)
        //    {
        //        float final = previousAmount + nextAmount;
        //        float dividePart = (final - previousAmount) / previousAmount;
        //        amount = dividePart * 100;
        //    }
        //    textMesh.gameObject.SetActive(true);
        //    textMesh.text = "%" + Mathf.FloorToInt(amount).ToString();
        //} 
        #endregion

        private void ResetProperty()
        {
            rightInvestArrow.transform.localPosition = initLocalRightRenderer;
            leftInvestArrow.transform.localPosition = initLocalLeftRenderer;
            leftInvestArrow.transform.eulerAngles = Vector3.zero;
            rightInvestArrow.transform.eulerAngles = Vector3.zero;
            #region TextMeshProEnableFalse
            leftInvestHighText.gameObject.SetActive(false);
            leftInvestLowText.gameObject.SetActive(false);
            rightInvestLowText.gameObject.SetActive(false);
            rightInvestHightText.gameObject.SetActive(false);
            #endregion
            isPanoMovement = false;
        }
    }
}

