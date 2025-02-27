using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPanel : MonoBehaviour
{
    public StatsPlayer player;
    public void NextLevel()
    {
        player.levelPlayer++;
        GameManager.instance.UpdateLevel();
        GameManager.instance.HideWinScreen();
    }

    public void RepeatLevel()
    {
        GameManager.instance.UpdateLevel();
        GameManager.instance.HideWinScreen();
    }
}
