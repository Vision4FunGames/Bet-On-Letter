using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MoneyTransfer
{
    public class CameraResize : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new Vector3(0, 16, -50);
        [SerializeField] private HandStackControl leftHand;
        [SerializeField] private HandStackControl rightHand;
        [SerializeField] private Transform playerLeft;
        [SerializeField] private Transform playerRight;
        [SerializeField] private float smoothTime = 0.5f;
        private Vector3 velocity;
        [SerializeField] private float minZoom = 40f;
        [SerializeField] private float maxZoom = 10f;
        [SerializeField] private float zoomLimiter = 50;
        private bool isChange = true;
        private bool isProgress;
        [SerializeField] Transform rePositionPoint;
        [SerializeField] private float rePositionDuration;

        private Camera cam;

        private void OnEnable()
        {
            Observer.OnChangeForPano.AddListener(OnChangeCameraForInvest);
            Observer.OnChangeForInitial.AddListener(OnChangeCameraForInitial);
        }

        private void OnDisable()
        {
            Observer.OnChangeForPano.RemoveListener(OnChangeCameraForInvest);
            Observer.OnChangeForInitial.RemoveListener(OnChangeCameraForInitial);
        }

        private void OnChangeCameraForInvest()
        {
            isChange = false;
            Vector3 movePosition = rePositionPoint.position;
            transform.DOMove(movePosition, rePositionDuration);
        }

        private void OnChangeCameraForInitial()
        {
            isChange = true;
        }

        private void Start()
        {
            cam = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            if (isChange)
            {
                //Move();
               // Zoom();
            }
        }

        void Zoom()
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }

        float GetGreatestDistance()
        {
            var bounds = new Bounds(playerLeft.position, Vector3.zero);
            if (leftHand.moneyList.Count > 0) bounds.Encapsulate(leftHand.moneyList[leftHand.moneyList.Count - 1].transform.position);
            if (rightHand.moneyList.Count > 0) bounds.Encapsulate(rightHand.moneyList[rightHand.moneyList.Count - 1].transform.position);
            bounds.Encapsulate(playerRight.position);
            return bounds.size.y;
        }

        void Move()
        {
            Vector3 centerPoint = GetCenterPoint();
            Vector3 newPosition = centerPoint + offset;
            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }

        Vector3 GetCenterPoint()
        {
            var bounds = new Bounds(playerLeft.position, Vector3.zero);
            if (leftHand.moneyList.Count > 0) bounds.Encapsulate(leftHand.moneyList[leftHand.moneyList.Count - 1].transform.position);
            if (rightHand.moneyList.Count > 0) bounds.Encapsulate(rightHand.moneyList[rightHand.moneyList.Count - 1].transform.position);
            bounds.Encapsulate(playerRight.position);
            return bounds.center;
        }
    }
}

