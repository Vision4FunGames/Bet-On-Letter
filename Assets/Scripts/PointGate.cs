using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PointType { plusMinus, multiplication, division }

public class PointGate : MonoBehaviour
{

    public int point;

    public bool isUsed;
    public PointType pointType;
    public ParticleSystem particle;

    public void PlayParticle()
    {
        if (particle != null)
        {
            // if (((pointType == PointType.multiplication) && point != 1 || (pointType == PointType.division && point != 1) || pointType == PointType.plusMinus)
            // && GameManager.GetInstance().playerController.MoneyParents.moneyNumber > 0)
            particle.Play();

        }

    }
}
