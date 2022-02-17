using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] public GameObject winPanel;
    [SerializeField] private Transform tipsPanel;
    [SerializeField] private GameObject pausePanel;

    private void OnEnable()
    {
        Observer.OnGameLose.AddListener(OnGameLose);
        Observer.OnGameWin.AddListener(OnGameWin);
        Observer.OnGameStart.AddListener(OnGameStartListen);
    }

    private void OnDisable()
    {
        Observer.OnGameLose.RemoveListener(OnGameLose);
        Observer.OnGameWin.RemoveListener(OnGameWin);
        Observer.OnGameStart.RemoveListener(OnGameStartListen);
    }

    void OnGameStartListen()
    {
        tipsPanel.gameObject.SetActive(false);
    }

    void OnGameWin()
    {
        winPanel.SetActive(true);
    }

    void OnGameLose()
    {
        losePanel.SetActive(true);
        SlotMachineScript sc = FindObjectOfType<SlotMachineScript>();
        sc.loseGame();
    }

    public void OpenPauseMenu()
    {
        Observer.OnGamePause?.Invoke();
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Observer.OnGameContinue.Invoke();
        pausePanel.SetActive(false);
    }
}
