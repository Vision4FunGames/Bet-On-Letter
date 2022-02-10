using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using ElephantSDK;

namespace MoneyTransfer
{
    public class LevelManager : Singleton<LevelManager>
    {
        public bool isActive;
        private int levelCount;
        private List<LevelScriptable> levelList = new List<LevelScriptable>();
        private float firstTime;
        private float lastTime;
        [HideInInspector]
        public LevelScriptable currentLevelScriptable;
        [Header("Which level do you want the spawn")]
        public bool isManuel;
        public int levelIndex;

        private void Awake()
        {
            if (isActive)
            {
                Initialize();
            }
        }

        //-----------------------------------WARN?NG-----?F YOU WANT TO ED?T TH?S PROJECT ON W?NDOWS PC YOU HAVE TO PUT ?N COMM?T L?NE THAN WHEN YOU WANT TO SEND TEST FLY JUST TAKE OUT OF COMM?T------------------

        [ContextMenu("Reset Key")]
        void ResetKey()
        {
            PlayerPrefs.DeleteKey("Level");
            PlayerPrefs.DeleteKey("FirstTime");
        }

        void Initialize()
        {

            //if (ElephantIOS.getConsentStatus() == "Authorized")
            //{
            //    GameAnalytics.Initialize();
            //}


            GetLevelInResources();

            SetLevelCount();
            LoadLevel();
            firstTime = Time.time;
        }

        void SetLevelCount()
        {
            if (PlayerPrefs.GetInt("FirstTime") == 0)
            {
                PlayerPrefs.SetInt("FirstTime", 1);
                PlayerPrefs.SetInt("Level", 1);
            }


            levelCount = PlayerPrefs.GetInt("Level");

            //if (ElephantIOS.getConsentStatus() == "Authorized")
            //{
         //   Elephant.LevelStarted(levelCount);
            //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level", levelCount);
            //}
        }


        void GetLevelInResources()
        {
            LevelScriptable[] levelScriptable = Resources.LoadAll<LevelScriptable>("Levels");

            for (int i = 0; i < levelScriptable.Length; i++)
            {
                levelList.Add(levelScriptable[i]);
            }
        }

        void LoadLevel()
        {
            if (isManuel)
            {
                foreach (LevelScriptable levelScriptable in levelList)
                {
                    if (levelScriptable.levelId == levelIndex)  //  this is just for the manuel level load 
                    {
                        currentLevelScriptable = levelScriptable;
                        Instantiate(levelScriptable.levelPrefab);
                    }
                }
            }
            else
            {
                foreach (LevelScriptable levelScriptable in levelList) // this is for the automatic level load
                {
                    if (levelScriptable.levelId == levelCount)
                    {
                        currentLevelScriptable = levelScriptable;
                        Instantiate(levelScriptable.levelPrefab);
                    }
                }
            }
        }

        public void NextLevel()
        {
            lastTime = Time.time - firstTime;
            if (Application.isEditor)
            {
                print(lastTime);
            }

            //if (ElephantIOS.getConsentStatus() == "Authorized")
            //{
        //    Elephant.LevelCompleted(levelCount);
            //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level", levelCount);
            //    GameAnalytics.StopTimer(lastTime.ToString() + "Win Time");
            //}


            if (levelCount != levelList.Count)
            {
                levelCount++;
                PlayerPrefs.SetInt("Level", levelCount);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                levelCount = 1;
                PlayerPrefs.SetInt("Level", levelCount);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void RestartCurrentLevel()
        {
            lastTime = Time.time - firstTime;

            //if (ElephantIOS.getConsentStatus() == "Authorized")
            //{
            //    GameAnalytics.StopTimer(lastTime.ToString() + "Failed Time");
            //    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level", levelCount);
          //  Elephant.LevelFailed(levelCount);
            //}

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
