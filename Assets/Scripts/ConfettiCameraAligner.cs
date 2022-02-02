using UnityEngine;
using System.Collections;

public class ConfettiCameraAligner : MonoBehaviour
{
    Transform cam;
    private void OnEnable()
    {
        StartCoroutine(SelfDestruct());
        FindCam();
    }

    private void FixedUpdate()
    {
        if (cam == null) FindCam();
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);
        
    }

    private void FindCam()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}