[System.Serializable]
public class PlayerData
{
    public int level;
    public int score;
    public bool allLevelsFinished;

    public PlayerData(GameManager gameManager)
    {
        level = gameManager.level;
        score = gameManager.playerScore;
        allLevelsFinished = gameManager.allLevelsFinished;
    }
}