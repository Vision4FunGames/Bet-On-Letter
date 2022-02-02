using UnityEngine;
using System.Collections;

public class CameraFocusController : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] Vector3 focusOffset = new Vector3(0, 0, 5f);
    bool followActive = true;

    private void Awake() {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void LateUpdate()
    {
        if (!followActive) return;
    }

/*
    public void UpdatePosition(){
        followActive = false;
        LeanTween.move(gameObject, PlayerController.coasters[(int)Mathf.Floor(PlayerController.coasters.Count * .5f)].transform.position, .3f).setEaseInOutSine();
        if (resetUpdatePosition!=null) StopCoroutine(resetUpdatePosition);
        resetUpdatePosition = StartCoroutine(ResetUpdatePosition());
    }

    Coroutine resetUpdatePosition;
    IEnumerator ResetUpdatePosition(){
        yield return new WaitForSeconds(.3f);
        followActive = true;
    }
    */
}