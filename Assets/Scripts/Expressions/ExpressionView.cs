using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ExpressionView : MonoBehaviour
{
    private BaseExpression _expression;
    public List<ExpressionSlotView> expressionSlots;
    private readonly List<int> _answerSlotIndexes = new(); 
    private Coroutine _coroutine;
    public bool isReady = false;

    public void InitExpression(BaseExpression expression, in List<ItemDescription> answersList)
    {
        _expression = expression;
        for (int i = 0; i < expressionSlots.Count; i++)
        {
            if (expression.TryGetShownData(i, out var result))
            {
                expressionSlots[i].Attach(result);
                expressionSlots[i].Lock();
            }
            else
            {
                expressionSlots[i].ResetSlot();
                answersList.Add(result);
            }

            expressionSlots[i].onPut += SetSlot;
        }
    }

    private IEnumerator StartAnimation(ExpressionSlotView inputSlot, AnswerSlotView answerSlot)
    {
        inputSlot.transform.localScale = Vector3.zero;
        yield return StartCoroutine(answerSlot.Animation(0.1f, true));
        GameManager.instance.SetAnswerVisible(answerSlot.index, false);
        StartCoroutine(inputSlot.Animation(0.1f, false));
        _coroutine = null;
    }

    private void DoTransition(ExpressionSlotArgs arg)
    {
        if (_coroutine == null)
        {
            var answerSlot = GameManager.instance.answerSlots[arg.index];
            _coroutine = StartCoroutine(StartAnimation(arg.slot, answerSlot));
        }
    }

    private void SetSlot(ExpressionSlotArgs arg)
    {
        if (!arg.slot.CanSet(arg.index))
        {
            return;
        }
        arg.slot.Set(arg.index);

        _answerSlotIndexes.Add(arg.index);        

        foreach (var slot in expressionSlots)
        {
            if (!slot.islocked)
            {
                DoTransition(arg);
                return;
            }
        }
        var result = _expression.CheckExpression(expressionSlots);
        GameManager.instance.RemoveSelectionAnswers();
        if (result)
        {
            isReady = true;
            DoTransition(arg);
            GameManager.instance.CheckWin();
        }
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
