using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DetectGateHarf : MonoBehaviour
{
    public TextMeshProUGUI harfText;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HarfGate"))
        {
            harfText.text = other.GetComponent<HarfGate>().SetText();
            harfText.transform.parent.GetComponent<Image>().rectTransform.DOAnchorPos(new Vector2(0, -72), 0.5f);
        }
        HarfGateChil hf = other.GetComponent<HarfGateChil>();
        if(hf != null)
        {
            hf.ShowText();
        }
    }
}
