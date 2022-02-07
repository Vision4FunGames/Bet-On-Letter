using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoneyTransfer
{
    public class HandPropertyControl : MonoBehaviour
    {
        public WaitForSeconds coroutineDelay = new WaitForSeconds(0.005f);
        public float seperateOffsetX = 3;
        public float seperateOffsetZ = 3;
        public float maxYMovement = 20;
        public float moveProcessDuration = 5;
        public float rotateProcessDuration = 0.5f;
        [Header("Hit by Negative Gate")]
        public float gateSeperateOffsetX;
        public float gateSeperateOffsetZ;
        public float gateSeperateOffsetY;



        public Collider FamousCheck()
        {
            Collider collider = null;
            int bitmask = 1 << 8;
            Ray ray = new Ray(transform.position + new Vector3(0, GameManager.Instance.characterOffsetForFinish, 0), transform.forward * 10);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50, bitmask))
            {
                collider = hit.collider;
                if (hit.collider.tag == "HarfGate")
                {
                    print("a");
                }
            }

            return collider;
        }
        public void RaycastHarfGate()
        {
            Collider collider = null;
            int bitmask = 1 << 8;
            Ray ray = new Ray(transform.position + new Vector3(0, GameManager.Instance.characterOffsetForFinish, 0), transform.forward * 10);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50, bitmask))
            {
                
            }
           
        }
       
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * 10);
        }
    }
}

