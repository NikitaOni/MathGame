using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public ExpressionView[] expressionViews;
    public LevelDescription currentLevel;
    public ItemDescription questionSlot;
    public AnswerSlotView[] answerSlots;
    public List<ItemDescription> answersList = new();
    public ItemDB database;
    [SerializeField]private GameObject _finishPanel;

    public StatsPlayer player;
    public LevelDB levelDB;

    //public static ItemDescription SelectedAnswerSlot
    //{
    //    get 
    //    { 
    //        var item = _selectedItem; 
    //        _selectedItem = null; 
    //        return item; 
    //    }
    //    set
    //    {
    //        _selectedItem = value;        
    //    }
    //}

    public int SelectedAnswerSlot { get; set; } = -1;

    public ItemDescription selectedItem => SelectedAnswerSlot == -1 ? null : answersList[SelectedAnswerSlot];
    private void Awake()
    {
        if (instance != null) 
        { 
            Destroy(gameObject);
        }
        instance = this;
        InitLevel();

        database.Init();
        InitExpressions();
        InitAnswers();
    }

    public void InitLevel()
    {
        currentLevel = levelDB.innerData[player.levelPlayer - 1];  
    }

    public void UpdateLevel()
    {
        ResetAnswerSlot();
        InitLevel();
        InitExpressions();
        InitAnswers();
    }

    private void InitExpressions()
    {
        answersList.Clear();

        for (int i = 0; i < expressionViews.Length; i++) 
        {
            var isValid = i < currentLevel.expressions.Length;

            if (isValid)
            {
                expressionViews[i].InitExpression(currentLevel.expressions[i], answersList);
            }

            expressionViews[i].SetVisible(isValid);
        }
    }

    public void InitAnswers()
    {
        for(int i = 0; i < currentLevel.garbageAnswers; i++)
        {
            answersList.Add(database.GetRandomItem());
        }

        Shuffle(answersList);

        for (int i = 0; i < answerSlots.Length; i++)
        {
            if (i < answersList.Count)
            {
                answerSlots[i].Attach(answersList[i]);
                answerSlots[i].SetIndex(i);
            }
            else
            {
                answerSlots[i].SetVisible(false);
            }
        }

        foreach(var answer in answersList)
        {
            Debug.Log(answer.name);
        }
    }

    public void ResetAnswerSlot()
    {
        for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].SetVisible(true);
            answerSlots[i].transform.localScale = Vector3.one;
        }
    }

    public void RemoveSelectionExpressions() 
    {
        for (int i = 0; i < expressionViews.Length; i++)
        {
            for (int j = 0; j < expressionViews[i].expressionSlots.Count; j++)
            {            
                expressionViews[i].expressionSlots[j].backgroundImage.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void CheckWin()
    {
        for (int i = 0; i < expressionViews.Length; i++)
        {
            if (!expressionViews[i].isReady && expressionViews[i].gameObject.activeSelf)
            {
                Debug.Log("Ne vse");
                return;
            }  
        }
        ShowWinScreen();

        for (int i = 0; i < expressionViews.Length; i++)
        {
            expressionViews[i].ResetExpression();
        }
    }

    public void ShowWinScreen()
    {
        _finishPanel.SetActive(true);
    }

    public void HideWinScreen()
    {
        _finishPanel.SetActive(false);
    }

    public void AddSelectionExpressions()
    {
        for (int i = 0; i < expressionViews.Length; i++)
        {
            for (int j = 0; j < expressionViews[i].expressionSlots.Count; j++)
            {
                if (expressionViews[i].expressionSlots[j].currentItem == database.items["question"])
                {
                    expressionViews[i].expressionSlots[j].backgroundImage.color = new Color32(177, 247, 147, 255);
                }
            }
        }
    }
    public void RemoveSelectionAnswers()
    {
        for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].backgroundImage.color = new Color(255, 255, 255, 1);
        }
        RemoveSelectionExpressions();
    }

    private void Shuffle<ItemDescription>(List<ItemDescription> list)
    {
        for (int i = list.Count - 1; i > 0; i--) 
        {
            int randomIndex = Random.Range(0, i + 1);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void SetAnswerVisible(int index, bool isVisible)
    {
        answerSlots[index].SetVisible(isVisible);
    }
}
