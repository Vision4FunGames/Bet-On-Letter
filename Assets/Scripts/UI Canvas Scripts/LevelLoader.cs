using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public enum Level //write level names here
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
    Level7,
    Level8
    //Level9,
    // Level10

}

public class LevelLoader : Singleton<LevelLoader>
{
    public GameObject loadingScreen;
    public TextMeshProUGUI progressText;
    public Image progressBar;

    CanvasManager canvasManager;

    public List<string> levels = new List<string>();

    protected override void Awake()
    {
        base.Awake();
        canvasManager = CanvasManager.GetInstance();
    }

    public void LoadLevel(Level sceneName) => StartCoroutine(LoadAsynchronously(sceneName.ToString()));
    public void UnLoadLevel(Level sceneName) => StartCoroutine(UnLoadAsynchronously(sceneName.ToString()));


    IEnumerator LoadAsynchronously(string sceneName)
    {
        canvasManager.SwitchCanvas(CanvasType.LoadingScreen);
        sceneName = NameFromIndex(SceneIndexFromName(sceneName) + 0); //bir sonraki sahne adi (rollic sahnesi oldugu icin)
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        print("Yuklenecek level: " + sceneName);


        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            /*
            if (progressBar.fillAmount < progress)
            {
                progressBar.fillAmount += 0.1f;
                progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
            }
            */
            yield return null;
        }

        //progressBar.fillAmount = 0f;
        //progressText.text = 0f + "%";

        levels.Add(sceneName);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        canvasManager.SwitchCanvas(CanvasType.MainMenu);
    }

    IEnumerator UnLoadAsynchronously(string sceneName)
    {
        // canvasManager.SwitchCanvas(CanvasType.LoadingScreen);
        sceneName = NameFromIndex(SceneIndexFromName(sceneName) + 0); //bir sonraki sahne adi (rollic sahnesi oldugu icin)
        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneName);
        print("Silinecek level: " + sceneName);

        while (!operation.isDone)
        {
            /*
            float progress = Mathf.Clamp01(operation.progress / .9f);

            if (progressBar.fillAmount < progress)
            {
                progressBar.fillAmount += 0.1f;
                progressText.text = Mathf.RoundToInt(progress * 100f) + "%";
            }
            */

            yield return null;
        }
        levels.Remove(sceneName);

        //progressBar.fillAmount = 0f;
        //progressText.text = 0f + "%";
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        //anvasManager.SwitchCanvas(CanvasType.GameUI);
    }

    private int SceneIndexFromName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string testedScreen = NameFromIndex(i);
            //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return i;
        }
        return -1;
    }

    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }


}
