using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Obstacle : MonoBehaviour
    {

        public ParticleSystem particle;
        // Start is called before the first frame update
        public int point;

        public void PlayParticle()
        {
            if (particle != null)
            {
                // if (point != 0 && GameManager.GetInstance().playerController.MoneyParents.moneyNumber > 0)
                //   particle.Play();

            }

        }
    }


