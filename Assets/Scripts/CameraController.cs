using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Transform followTarget;
    public Transform aimTarget;

    public Transform finishTarget;

    public Vector3 followOffset;

    public Vector3 finishFollowOffset;

    private Vector3 defaultPos;
    private Quaternion defaultRot;

    private void Start()
    {
        defaultPos = transform.position;
        defaultRot = transform.rotation;
    }

    public bool inFinishMode = false;
    private void FixedUpdate()
    {
        if (followTarget == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null) return;
            followTarget = GameObject.FindGameObjectWithTag("Player").transform;

        }
        if (aimTarget == null)
        {
            if (GameObject.FindGameObjectWithTag("CameraAimFocus") == null) return;
            aimTarget = GameObject.FindGameObjectWithTag("CameraAimFocus").transform;
        }

        if (finishTarget == null)
        {
            if (GameObject.FindGameObjectWithTag("Cone/Parent") == null) return;
            finishTarget = GameObject.FindGameObjectWithTag("Cone/Parent").transform;
        }

        if (followTarget == null || aimTarget == null) return;

        transform.position = Vector3.Lerp(transform.position, (inFinishMode ? finishTarget.position : followTarget.position) + (inFinishMode ? finishFollowOffset : followOffset), Time.fixedDeltaTime);

        var q = Quaternion.LookRotation((inFinishMode ? finishTarget.position + new Vector3(0, 2, 0) : aimTarget.position) - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.fixedDeltaTime);
        //print(followTarget.gameObject.name + " " + followOffset + " " + aimTarget.gameObject.name);
    }

    public void Blend(bool toFinish)
    {
        if (!toFinish) transform.SetPositionAndRotation(defaultPos, defaultRot);
        inFinishMode = toFinish;
    }
}