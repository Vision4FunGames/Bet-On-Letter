using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
//using MoreMountains.NiceVibrations;
using Lean.Touch;
using UnityEngine.EventSystems;
//using Cinemachine;
//using ElephantSDK;


public enum GAMESEQUENCE { GO, FINISH }
public class GameManager : Singleton<GameManager>
{
    //In Game Variables
    public int playerScore;
    public int level = 1;

    //Level States
    public bool isGamePaused;
    bool GamePaused
    {
        set
        {
            isGamePaused = value;
            //FindObjectOfType<DrawManager>().paused = value;
        }
    }
    public bool allLevelsFinished;

    //UI
    CanvasManager canvasManager;
    public Slider progressBar;

    //Game

    public GameObject portalAPrefabHead;
    public GameObject portalAPrefabBody;
    public GameObject portalBPrefab;
    bool levelStarted, failed;
    public bool levelFinished;
    //public CameraRotate cameraRotate;

    public GAMESEQUENCE gameSequence;

    public PlayerController playerController;

    public GameObject hooliganPrefab;
    int maskIndex = 4000;
    //public CinemachineVirtualCamera vcam;
    //public DynamicJoystick joystick;
    GameObject confetti;


    //EndGame



    [SerializeField] TextMeshProUGUI tmpLevel;
    [SerializeField] TextMeshProUGUI tmpFeedback;
    public TextMeshProUGUI tmpInfo;
    public TextMeshProUGUI tmpKillCount;
    public GameObject uiHitRed;
    [SerializeField] GameObject uiGreenFrame;
    public GameObject gestureTutorialVertical;

    public AudioManager audioManager;

    [SerializeField] GameObject vfxWin;

    [SerializeField] int[] allLevelsBeforeLoadArray2 = new int[] { 4, 5, 6, 7, 8, 9 };
    [HideInInspector] public int alllevelsControl = 0;
    public int[] AllLevelsBeforeLoadArray => allLevelsBeforeLoadArray2;
    int normalizedLevel;
    bool restartLoadLevel;
    protected override void Awake()
    {

        base.Awake();
        Time.timeScale = 1;

        //LoadGame();
        canvasManager = CanvasManager.GetInstance();
        audioManager = AudioManager.GetInstance();


        if (level == 0) level = 1;
        tmpLevel.text = "Level " + level.ToString();
    }

    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    protected void Start()
    {
        if (SceneManager.sceneCount > 1) SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetSaveData();
            RestartLevel();

        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            EndLevel(true);

        }
    }

    public void StartLevel()
    {


        //if (SceneManager.sceneCount > 1) SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        //Elephant.LevelStarted(level);
        playerScore = 0;
        canvasManager.SwitchCanvas(CanvasType.GameUI);
        print("start level");
        Image flashImage = uiHitRed.GetComponent<Image>();
        Color color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
        flashImage.color = color;
        levelStarted = true;
        gameSequence = GAMESEQUENCE.GO;
        //Time.timeScale=1;
    }



    public void EndLevel(bool finished)
    {
        //string info;

        if (finished)
        {
            //info = "Level " + level.ToString() + " at buildIndex " + SceneManager.GetActiveScene().buildIndex + " finished";
            //Elephant.LevelCompleted(level);
            StartCoroutine(ProcessWinScreen());
            //audioManager.PlayWinSound();
        }
        else
        {
            //HitRedFlash(false);
            //info = "Lost at level " + level.ToString() + " with buildIndex: " + SceneManager.GetActiveScene().buildIndex;
            //Elephant.LevelFailed(level);
            StartCoroutine(ProcessLoseScreen());
            //Time.timeScale = 0;

            //audioManager.PlayLoseSound();
        }
        levelFinished = true;

        //joystick.gameObject.SetActive(false);

    }

    IEnumerator ProcessLoseScreen()
    {
        canvasManager.SwitchCanvas(CanvasType.LoseScreen);
        yield return null;
    }

    [SerializeField] TextMeshProUGUI tmpScoreEndLevel;
    IEnumerator ProcessWinScreen()
    {
        // if (vfxWin != null) confetti = Instantiate(vfxWin, new Vector3(playerController.transform.position.x, vcam.transform.position.y, playerController.transform.position.z), Quaternion.identity);

        //WaitForSeconds _wait = new WaitForSeconds(.001f);
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ShowInstruction("nice build!");
        yield return new WaitForSeconds(1f);
        canvasManager.SwitchCanvas(CanvasType.WinScreen);
        //tmpScoreEndLevel.text = playerScore.ToString() + "0 BLOCKS";
        /*
        int displayedPlateCount = 0;
        while (displayedPlateCount < playerScore)
        {
            displayedPlateCount ++;
            tmpScoreEndLevel.text = displayedPlateCount.ToString() + " BLOCKS!";
            yield return _wait;
	    }
	    */
    }

    [SerializeField] float cooldown = 2f;
    //WaitForSeconds _cooldown = new WaitForSeconds(1.5f);

    #region PauseResumeSystem
    public void Pause()
    {
        Time.timeScale = 0f;
        canvasManager.SwitchCanvas(CanvasType.PauseMenu);
        GamePaused = true;
    }

    public void PauseWithoutCanvasChange()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        canvasManager.SwitchCanvas(CanvasType.GameUI);
        GamePaused = false;
    }
    #endregion

    #region SaveLoadSystemMethods


    public void ResetSaveData()
    {
        //GameManager resetData = new GameManager();
        playerScore = 0;
        level = 0;
        allLevelsFinished = false;

        PlayerPrefs.SetInt("loadLevel", 1);
        PlayerPrefs.SetInt("isAllLevelFinished", allLevelsFinished == false ? 0 : 1);

        Debug.Log("Player data reset complete.");
    }

    public void LoadGame()
    {

        //playerScore = loadData.score;

        level = PlayerPrefs.GetInt("loadLevel", 1);
        allLevelsFinished = PlayerPrefs.GetInt("isAllLevelFinished", 0) == 0 ? false : true;
        alllevelsControl = PlayerPrefs.GetInt("alllevelsControl", 5);
        Debug.Log("Player data loaded from file.");

    }
    public void SaveGame()
    {
        PlayerPrefs.SetInt("loadLevel", level);
        PlayerPrefs.SetInt("isAllLevelFinished", allLevelsFinished == false ? 0 : 1);
        PlayerPrefs.SetInt("alllevelsControl", alllevelsControl);

    }
    private void OnApplicationQuit() => SaveGame();
    #endregion

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        if (audioManager.audioSource.isPlaying) audioManager.audioSource.Stop();
        //TinySauce.OnGameFinished(50);
        levelFinished = false;
        ResetRedFlash();
        string targetSceneName = SceneManager.GetActiveScene().name;
        Level targetLevel = (Level)System.Enum.Parse(typeof(Level), targetSceneName); //converts string to enum
        LevelLoader.GetInstance().UnLoadLevel(targetLevel);

        LevelLoader.GetInstance().LoadLevel(targetLevel);
    }
    /*
    public void LoadNextLevel()
    {
        if (audioManager.audioSource.isPlaying) audioManager.audioSource.Stop();
        ResetRedFlash();
        Debug.Log("Load next level.");
        levelFinished = false;
        tmpLevel.text = "Level " + level.ToString();
        allLevelsFinished = level > SceneManager.sceneCountInBuildSettings - 1;
        int nextLevelIndex = allLevelsFinished ? Random.Range(2, SceneManager.sceneCountInBuildSettings) : level;
        while (nextLevelIndex == SceneManager.GetActiveScene().buildIndex || nextLevelIndex < 2 || nextLevelIndex == 3) //ayni bolum tekrar gelmesin. ilk bolum de tekrar gelmesin.
        {
            nextLevelIndex = Random.Range(2, SceneManager.sceneCountInBuildSettings - 1);
        }
        LoadLevelByIndex(nextLevelIndex);



    }
    */



    public void LoadNextLevel()
    {
        //if (audioManager.audioSource.isPlaying) audioManager.audioSource.Stop();
        //ResetRedFlash();
        //vcam.enabled = true;

        Destroy(confetti);
        level++;
        Debug.Log("Load next level.");
        tmpLevel.text = "Level " + (level);

        allLevelsFinished = level > SceneManager.sceneCountInBuildSettings - 2;
        int nextLevelIndex = 0;
        //if (level % 7 == 0) { alllevelsControl = 4; }

        print("allLevelsFinished " + allLevelsFinished + "allLevelsBeforeLoadArray countt: " + allLevelsBeforeLoadArray2.Length);
        if (!allLevelsFinished)
        {
            nextLevelIndex = ((level - 1) % (SceneManager.sceneCountInBuildSettings - 2)) + 2;
            alllevelsControl = 5;//4
            normalizedLevel = nextLevelIndex;
        }
        else
        {

            alllevelsControl++;
            nextLevelIndex = allLevelsBeforeLoadArray2[alllevelsControl % 6];
            // nextLevelIndex = allLevelsBeforeLoadArray[alllevelsControl % 6];
            // normalizedLevel = allLevelsBeforeLoadArray[alllevelsControl % 6];

            // alllevelsControl = ((level - 8) % 6);

        }
        print("nextLevelIndex: " + nextLevelIndex + " level " + level + "all finished: " + allLevelsFinished + " alllevelsControl " + alllevelsControl);
        LoadLevelByIndex(nextLevelIndex);


    }
    public void LoadLevelByIndex(int index)
    {
        string pathToScene, pathToSceneBefore = "";

        print("index " + index);
        if (SceneUtility.GetScenePathByBuildIndex(index) != "")
        {

            pathToScene = SceneUtility.GetScenePathByBuildIndex(index); //unity bug at returning a scene by build index if that scene hasn't been loaded before. this way works                                                        
            if (index == 2 || ((allLevelsFinished) && index == 4))//LevelLoader.GetInstance().levels[0] == "Level8"
            {//8
                pathToSceneBefore = SceneUtility.GetScenePathByBuildIndex(9); //unity bug at returning a scene by build index if that scene hasn't been loaded before. this way works
                print("+++ayarlandi " + index);

                restartLoadLevel = false;
            }
            else if (index > 2)//
                pathToSceneBefore = SceneUtility.GetScenePathByBuildIndex(index - 1); //unity bug at returning a scene by build index if that scene hasn't been loaded before. this way works
        }
        else
        {
            Debug.Log("All levels finished!");
            allLevelsFinished = true;
            LoadNextLevel();
            return;
        }
        string targetSceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
        string targetSceneNameBefore = System.IO.Path.GetFileNameWithoutExtension(pathToSceneBefore);

        print("targetLevelBefore " + targetSceneNameBefore + " targetLevel " + targetSceneName + " index " + index);

        Level targetLevel = (Level)System.Enum.Parse(typeof(Level), targetSceneName); //converts string to enum
        Level targetLevelBefore = (Level)System.Enum.Parse(typeof(Level), targetSceneNameBefore); //converts string to enum


        if (LevelLoader.GetInstance().levels.Contains(targetLevelBefore.ToString()))
        {
            print("level kaldir");
            LevelLoader.GetInstance().UnLoadLevel(targetLevelBefore);
        }
        LevelLoader.GetInstance().LoadLevel(targetLevel);

        SaveGame();


    }
    public bool hapticOn;
    [SerializeField] GameObject hapticOnMain, hapticOffMain;
    [SerializeField] GameObject hapticOnPause, hapticOffPause;

    public void ToggleHapticOnOff()
    {
        hapticOn = !hapticOn;
        //MMVibrationManager.SetHapticsActive(hapticOn);
        hapticOnMain.SetActive(hapticOn);
        hapticOffMain.SetActive(!hapticOn);
        hapticOnPause.SetActive(hapticOn);
        hapticOffPause.SetActive(!hapticOn);
    }

    Coroutine redFlashCoroutine;
    public void HitRedFlash(bool flashBack) //use this to flash red when hit a negative object
    {
        if (!uiHitRed.activeSelf) uiHitRed.SetActive(true);
        if (redFlashCoroutine != null) StopCoroutine(redFlashCoroutine);
        redFlashCoroutine = StartCoroutine(HitRedFlashCoroutine(flashBack));
    }
    float _alpha;

    IEnumerator HitRedFlashCoroutine(bool flashBack)
    {
        WaitForSeconds shortDuration = new WaitForSeconds(.02f);
        Image flashImage = uiHitRed.GetComponent<Image>();
        Color color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
        flashImage.color = color;
        _alpha = 0;
        while (flashImage.color.a < 1)
        {
            _alpha += 0.2f;
            flashImage.color = new Color(color.r, color.g, color.b, _alpha);
            yield return shortDuration;
        }
        if (flashBack)
        {
            while (flashImage.color.a > 0)
            {
                _alpha -= 0.2f;
                flashImage.color = new Color(color.r, color.g, color.b, _alpha);
                yield return shortDuration;
            }
        }
    }

    private void ResetRedFlash()
    {
        Image flashImage = uiHitRed.GetComponent<Image>(); //reset red flash color
        Color color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
        flashImage.color = color;
        uiHitRed.SetActive(false);
    }




}