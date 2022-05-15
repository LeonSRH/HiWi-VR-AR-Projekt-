using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{
    //points for the completed quests
    int playerPoints;
    int questsCompleted;
    int zombiesHealed;
    public Text playerPointsText;
    public Text playerQuestCompletedText;

    public GameObject scoreImage;


    private void LateUpdate()
    {
        playerPointsText.text = "" + playerPoints;
        playerQuestCompletedText.text = "" + questsCompleted;
    }

    void Start()
    {
        playerPoints = 0;
        questsCompleted = 0;
        zombiesHealed = 0;
    }

    //add an amount of points
    public void addPoints(int points)
    {
        playerPoints = playerPoints + points;
        iTween.ScaleFrom(scoreImage, new Vector3(3, 3, 0), 3);
    }

    //add completed quest
    public void addCompletedQuest()
    {
        questsCompleted = questsCompleted + 1;
        iTween.ScaleFrom(scoreImage, new Vector3(3, 3, 0), 3);
    }

    public void addZombiesHealed()
    {
        zombiesHealed++;
    }

    public int getZombiesHealed() => zombiesHealed;

    public int getPoints() => playerPoints;

    public int getQuestsCompleted() => questsCompleted;
}