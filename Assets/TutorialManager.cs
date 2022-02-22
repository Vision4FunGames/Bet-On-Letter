using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialHarf;
    public GameObject tutorialTestere;
    public GameObject tutorialmagnet;

    public GameObject tutorialObj;
    void Start()
    {
        if(!PlayerPrefs.HasKey("tuto"))
        {
            tutorialObj.SetActive(true);
            PlayerPrefs.SetInt("tuto", 1);
        }
    }

    void Update()
    {
        
    }

    public void tutoriallarikapat()
    {
        tutorialHarf.SetActive(false);
        tutorialTestere.SetActive(false);
        tutorialmagnet.SetActive(false);
        Time.timeScale = 1;
    }
}
