using UnityEngine;
using System.Collections;
//using MoreMountains.NiceVibrations;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource audioSource;
    public AudioClip winSound, loseSound;
    public AudioClip addCoasterSound, loseCoasterSound;

    public bool audioOn=true;

    [SerializeField] GameObject soundOffMain,soundOffPause;
	[SerializeField] GameObject soundOnMain, soundOnPause;
    
	// Awake is called when the script instance is being loaded.
	protected override void Awake()
	{
		base.Awake();
	/////	MMNViOS.iOSInitializeHaptics();
	}
	
	// This function is called when the behaviour becomes disabled () or inactive.
	//protected void OnDisable() =>	MMNViOS.iOSReleaseHaptics();


    public void ToggleAudioOnOff()
    {
	    audioOn = !audioOn;
        audioSource.volume = audioOn ? 1f : 0f;
        soundOffMain.SetActive(!audioOn);
        soundOnMain.SetActive(audioOn);
        soundOffPause.SetActive(!audioOn);
        soundOnPause.SetActive(audioOn);
    }

    public void PlayWinSound()
    {
	    //audioSource.pitch = 1f;
	   //// MMVibrationManager.Haptic(HapticTypes.Success);
        if (winSound!=null) audioSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
	  ////  MMVibrationManager.Haptic(HapticTypes.Failure);
	    //audioSource.pitch = 1f;
        if (loseSound != null) audioSource.PlayOneShot(loseSound);
    }

	//bool canPlayAddCoasterSound=true, canPlayLoseCoasterSound=true;
	public void PlayCoasterSound(bool add)
    {
        if (add)
        {
	        //if (!canPlayAddCoasterSound) return;
	      // /// MMVibrationManager.Haptic(HapticTypes.MediumImpact);
	        if (addCoasterSound!=null) audioSource.PlayOneShot(addCoasterSound);
	        //canPlayAddCoasterSound = false;
	        //if (resetCanPlayAddCubeSoundCoroutine != null) StopCoroutine(resetCanPlayAddCubeSoundCoroutine);
	        //resetCanPlayAddCubeSoundCoroutine = StartCoroutine(ResetCanPlayAddCubeSound());
        }
        else
        {
	        //if (!canPlayLoseCoasterSound) return;
	      // /// MMVibrationManager.Haptic(HapticTypes.RigidImpact);
	        if (loseCoasterSound!=null) audioSource.PlayOneShot(loseCoasterSound);
	        //canPlayLoseCoasterSound = false;
	        //if (resetCanPlayLoseCubeSoundCoroutine != null) StopCoroutine(resetCanPlayLoseCubeSoundCoroutine);
	        //resetCanPlayLoseCubeSoundCoroutine = StartCoroutine(ResetCanPlayLoseCubeSound());
        }
    }
	/*
    Coroutine resetCanPlayAddCubeSoundCoroutine;
    IEnumerator ResetCanPlayAddCubeSound()
    {
        yield return new WaitForSeconds(.1f);
        canPlayAddCoasterSound = true;
    }
    Coroutine resetCanPlayLoseCubeSoundCoroutine;
    IEnumerator ResetCanPlayLoseCubeSound()
    {
        yield return new WaitForSeconds(.1f);
        canPlayLoseCoasterSound = true;
    }
	*/
}