using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerSlotView : ItemView, IPointerClickHandler
{
    public string id => itemData.id;
    private ItemDescription itemData;
    public Image backgroundImage;
    public int index {  get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Выбран {id}");
        GameManager.instance.SelectedAnswerSlot = index;
        GameManager.instance.RemoveSelectionAnswers();
        GameManager.instance.AddSelectionExpressions();
        backgroundImage.color  = new Color32(200, 255, 55, 255);
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public override void Attach(ItemDescription item)
    {
        base.Attach(item);
        itemData = item;
    }
}
