using UnityEngine;
using DG.Tweening;

public class SeeThrough : MonoBehaviour
{
	private void OnTriggerEnter(Collider other) => 	other.GetComponent<Renderer>().material.DOFade(.2f,.2f);
}