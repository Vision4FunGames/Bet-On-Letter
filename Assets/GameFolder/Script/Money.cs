using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MoneyTransfer
{
    public class Money : MonoBehaviour
    {
        [SerializeField] private Transform _child;
        private bool _isProgress = false;
        private bool _isIncreaseProgress;
        private bool _isDecreaseProgress;
        private ObjectPool _objectPool;
        public HandStackControl handStackControl;
        public bool isGetHit;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
        }

        public void JumpToTargetHand(HandStackControl fromHandBaseControl, HandStackControl toHandBaseControl, bool isRight)
        {
            if (!_isProgress)
            {
                _isProgress = true;
                Vector3 targetPosition = FindJumpPosition(toHandBaseControl);
                if (isRight) _child.DORotate(new Vector3(0, 0, -180), GameManager.Instance.transferDuration).SetRelative().OnComplete(() => transform.localEulerAngles = Vector3.zero);
                else _child.DORotate(new Vector3(0, 0, 180), GameManager.Instance.transferDuration).SetRelative().OnComplete(() => transform.localEulerAngles = Vector3.zero);
                handStackControl = toHandBaseControl;
                transform.DOLocalJump(targetPosition, GameManager.Instance.jumpPower, 0, GameManager.Instance.transferDuration).OnComplete(() =>
                 {
                     _isProgress = false;
                     if (!isGetHit)
                     {
                         toHandBaseControl.AddToList(this);
                     }
                     fromHandBaseControl.RemoveToList(this);
                 });
            }
        }

        public void JumpToFinishLine(FinishManager finishManager, int index)
        {
            Vector3 targetPoint = finishManager.targetPoint.position;
            Vector3 jumpPosition = Vector3.zero;
            if (index == 0) jumpPosition = finishManager.slotMachineEntry.position;
            else jumpPosition = finishManager.slotMachineEntry.position;
            _child.DORotate(new Vector3(180, 0, 90), GameManager.Instance.transferDurationForFinish).SetRelative();
            transform.DOJump(jumpPosition, GameManager.Instance.jumpPower, 0, GameManager.Instance.transferDurationForFinish).OnComplete(() =>
            {
                finishManager.currentHeight = transform.position.y;
                finishManager.slotMachineCount += 1;
                finishManager.slotMachineText.text = finishManager.slotMachineCount.ToString();
            });
        }

        Vector3 FindJumpPosition(HandStackControl handBaseControl)
        {
            Vector3 jumpPosition;
            if (handBaseControl.moneyList.Count == 0)
            {
                jumpPosition = handBaseControl.handPoint.localPosition;
            }
            else
            {
                jumpPosition = handBaseControl.moneyList[handBaseControl.moneyList.Count - 1].transform.localPosition;
            }

            return jumpPosition;
        }

        public void DestroyAndGoPool()
        {
            _objectPool.SendList(this);
        }


        public void DoScaleEffect(bool state)
        {
            //if (state) _child.DOScale(new Vector3(1, 1, 1) * 1.5f, .1f).OnComplete(() => _child.DOScale(Vector3.one, .1f));
            //else _child.DOScale(new Vector3(1, 1, 1) * .5f, .1f).OnComplete(() => _child.DOScale(Vector3.one, .1f));
        }

        public void ResetProperty()
        {
            _child.localEulerAngles = Vector3.zero;
            transform.position = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;
            isGetHit = false;
        }
    }
}

