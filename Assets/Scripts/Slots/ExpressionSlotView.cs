using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ExpressionSlotView : ItemView, IPointerClickHandler
{
    public ItemDescription currentItem;
    public int answerSlotIndex;
    public Image backgroundImage;

    public bool islocked => _islocked;
    private bool _islocked;

    public event Action<ExpressionSlotArgs> onPut = delegate { };

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManager.instance.selectedItem == null)
        {
            return;
        }
        onPut(new ExpressionSlotArgs(GameManager.instance.selectedItem, this, GameManager.instance.SelectedAnswerSlot));
        GameManager.instance.RemoveSelectionExpressions();
    }

    public bool CanSet(int slotIndex) 
    {
        var item = GameManager.instance.selectedItem;

        if (item == null) 
        {
            return false;
        }

        if (_islocked)
        {
            return false;
        }

        return true;
    }

    public void Set(int slotIndex)
    {
        var item = GameManager.instance.selectedItem;

        Attach(item);
        Lock();
        answerSlotIndex = slotIndex;

        GameManager.instance.SelectedAnswerSlot = -1;
    }

    public void Lock() => _islocked = true;
    public void UnLock() => _islocked = false;

    public override void Attach(ItemDescription item)
    {
        base.Attach(item);
        currentItem = item;
    }

    public ItemDescription fallbackDescription => GameManager.instance.database.items["question"];

    public void ResetSlot()
    {
        Attach(fallbackDescription);
        UnLock();
        GameManager.instance.SetAnswerVisible(answerSlotIndex, true);
        transform.localScale = Vector3.one;
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(GameManager.instance.answerSlots[answerSlotIndex].Animation(0.1f, false));
        }   
    }
}
public class ExpressionSlotArgs 
{
    public readonly ItemDescription item;
    public readonly ExpressionSlotView slot;
    public readonly int index;

    public ExpressionSlotArgs(ItemDescription item, ExpressionSlotView slot, int index)
    {
        this.item = item;
        this.slot = slot;
        this.index = index;
    }
}
