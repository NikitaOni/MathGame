using UnityEngine;
using UnityEngine.EventSystems;

public class AnswerSlotView : ItemView, IPointerClickHandler
{
    public string id => itemData.id;
    private ItemDescription itemData;
    public int index {  get; private set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Выбран {id}");
        GameManager.instance.SelectedAnswerSlot = index;
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
