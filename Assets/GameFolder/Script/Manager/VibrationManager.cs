using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    [SerializeField] private GameObject onVibrate, offVibrate;
    public static VibrationManager Instance;
    private bool isActive = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void OffVibrate()
    {
        onVibrate.SetActive(false);
        offVibrate.SetActive(true);
        isActive = false;
    }
    public void OnVibrate()
    {
        onVibrate.SetActive(true);
        offVibrate.SetActive(false);
        isActive = true;
    }


    private void Start()
    {
        Vibration.Init();
    }

    public void VibratePop()
    {
        if (isActive)
        {
         Vibration.VibratePop();
        }
    }
}
