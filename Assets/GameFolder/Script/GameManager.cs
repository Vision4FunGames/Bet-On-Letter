using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace MoneyTransfer
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;
        [Range(0f, 1f)]
        public float high; 
        public float jumpPower = 1; 
        public int seperateMoneyPerObstacle; 
        public float characterOffsetForFinish = 1; 
        public int moneyPackIncreaseCount; 
        private bool isStart = false;
        [Header("Transfer Duration For hand to hand")]
        public float transferDuration;
        public float transferDurationForFinish;

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

            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            Observer.OnGameLose.AddListener(OnLoseConditionListener);
            Observer.OnGameWin.AddListener(OnWinConditionListener);
            Observer.OnGameContinue.AddListener(GameContinue);
            Observer.OnGamePause.AddListener(GamePause);
        }

        private void OnDisable()
        {
            Observer.OnGameLose.RemoveListener(OnLoseConditionListener);
            Observer.OnGameWin.RemoveListener(OnWinConditionListener);
            Observer.OnGameContinue.RemoveListener(GameContinue);
            Observer.OnGamePause.RemoveListener(GamePause);
        }

        private void Update()
        {
            if (!isStart)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isStart = true;
                    Observer.OnGameStart?.Invoke();
                }
            }
        }

        private void OnLoseConditionListener()
        {
            print("Lose");
        }

        private void OnWinConditionListener()
        {
            print("Win");
        }

        private void GamePause()
        {
            Time.timeScale = 0;
        }

        private void GameContinue()
        {
            Time.timeScale = 1;
        }
    }


}
