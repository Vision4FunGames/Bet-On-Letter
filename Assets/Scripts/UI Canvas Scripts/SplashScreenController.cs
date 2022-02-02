using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
    bool isFirstLaunch = true;
    GameManager gameManager;

    void OnEnable()
    {
        if (!isFirstLaunch) return;
        isFirstLaunch = false;
        gameManager = GameManager.GetInstance();
        gameManager.LoadGame();

        int nextLevelIndex;
        print(gameManager.allLevelsFinished);
        if (!gameManager.allLevelsFinished)
            nextLevelIndex = ((gameManager.level - 1) % (SceneManager.sceneCountInBuildSettings - 2)) + 2;
        else
        {
            nextLevelIndex = gameManager.AllLevelsBeforeLoadArray[gameManager.alllevelsControl % gameManager.AllLevelsBeforeLoadArray.Length];

        }

        StartCoroutine(LoadLastScene(nextLevelIndex));//rollicin sahnesi oldugu icin gameManager.level + 1

    }


    IEnumerator LoadLastScene(int sceneIndex)
    {
        //baslangictaki level yukleme
        int nextLevelIndex = ((sceneIndex - 2) % (SceneManager.sceneCountInBuildSettings - 2)) + 2;//+1 => -1

        if (nextLevelIndex < 2) nextLevelIndex = 2;//2. sahneden basliyor
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevelIndex, LoadSceneMode.Additive);


        LevelLoader.GetInstance().levels.Add(NameFromIndex(nextLevelIndex));
        print("Yuklenecek ilk level: " + NameFromIndex(nextLevelIndex));
        /*
        while (!operation.isDone)
        {
            yield return null;
        }
        */

        yield return null;
        //yield return new WaitForSecondsRealtime(1f);
        //SceneManager.SetActiveScene(SceneManager.GetSceneAt(sceneName));
        //Debug.Log("Active scene is: " + SceneManager.GetActiveScene());

        CanvasManager.GetInstance().SwitchCanvas(CanvasType.MainMenu);
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