using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using MoneyTransfer;
using DG.Tweening;

namespace MoneyTransfer
{
    public class HandControl : MonoBehaviour
    {
        [SerializeField] private float thresHold = 40f;
        [SerializeField] private float timeThresHold = 1f;
        [SerializeField] private float normalSpeed;  // normal hiz
        [SerializeField] private float decreaseSpeedForGate; // kapiya gelince dusecek hiz
        [SerializeField] private float durationForDecrease; // kapiya gelince yavaslama suresi
        [SerializeField] private float durationForIncrease; // kapidan gectikten sonraki hizlanma suresi
        private float _time;
        private bool _isStackProgress;
        private bool _isAllProgress;
        private bool _isActive = false;
        [HideInInspector]  
        public bool isMoneyTransfer = true;
        [HideInInspector]
        public float mouseX;
        private Vector3 _initialPosition;
        private MoneySeperate _moneySeperate;
        private float initialSpeed;


        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            initialSpeed = normalSpeed;
            _moneySeperate = GetComponent<MoneySeperate>();
        }

        public void SetSpeed(bool state)
        {
            if (state)
            {
                isMoneyTransfer = false;
                DOTween.To(() => normalSpeed, x => normalSpeed = x, decreaseSpeedForGate, durationForDecrease);
            }
            else
            {
                DOTween.To(() => normalSpeed, x => normalSpeed = x, initialSpeed, durationForIncrease).OnComplete(() => isMoneyTransfer = true);
            }
        }

        private void OnEnable()
        {
            Observer.OnGameFinish.AddListener(ObserverListener);
            Observer.OnGameLose.AddListener(ObserverListener);
            Observer.OnGameStart.AddListener(EnableControl);
        }

        private void OnDisable()
        {
            Observer.OnGameFinish.RemoveListener(ObserverListener);
            Observer.OnGameLose.RemoveListener(ObserverListener);
            Observer.OnGameStart.RemoveListener(EnableControl);
        }

        private void Update()
        {
            if (_isActive)
            {
                if (isMoneyTransfer)
                {
                    FingerControl();
                }              
                Movement();
            }
        }

        private void EnableControl()
        {
            _isActive = true;
        }

        private void ObserverListener()
        {
            _isActive = false;
            ResetParameters();
        }

        private void Movement()
        {
            transform.Translate(Vector3.forward * normalSpeed * Time.deltaTime);
        }

        private void FingerControl()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _initialPosition = Input.mousePosition;
                _isStackProgress = true;
                _isAllProgress = true;
            }

            if (Input.GetMouseButton(0))
            {
                mouseX = Input.mousePosition.x - _initialPosition.x;

                if (_isStackProgress)
                {
                    if (mouseX > thresHold)
                    {
                        _isStackProgress = false;
                        _moneySeperate.SendMoneyToRightHand(10);
                    }
                    else if (mouseX < -thresHold)
                    {
                        _isStackProgress = false;
                        _moneySeperate.SendMoneyToLeftHand(10);
                    }
                }

                if (_isAllProgress == true)
                {
                    _time += Time.deltaTime;
                    if (_time > timeThresHold)
                    {
                        if (mouseX > thresHold)
                        {
                            _moneySeperate.SendMoneyToRightHand(100);
                        }
                        else if (mouseX < -thresHold)
                        {
                            _moneySeperate.SendMoneyToLeftHand(100);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                ResetParameters();
            }
        }

        private void ResetParameters()
        {
            _time = 0;
            _isStackProgress = false;
            _isAllProgress = false;
        }
    }
}
