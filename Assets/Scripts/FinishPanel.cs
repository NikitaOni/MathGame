using System.Collections;
using UnityEngine;

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

    public IEnumerator AppearanceAnimation(float duration)
    {
        transform.localScale = Vector3.zero;

        var elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
