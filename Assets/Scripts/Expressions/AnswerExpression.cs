using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Один ответ
/// </summary>
[CreateAssetMenu(fileName = "NewExpression", menuName = "Game/Expression/Answer Expression")]
public class AnswerExpression : BaseExpression
{
    private const int INPUT_SLOT_COUNT = 2;  
    private readonly List<ItemDescription> _validInputs = new List<ItemDescription>();

    public override bool CheckExpression(List<ExpressionSlotView> expressionSlotsView)
    {
        var isValid = true;

        _validInputs.Clear();
        _validInputs.AddRange(inputs);

        for (int i = 0; i < INPUT_SLOT_COUNT; i++)
        {
            var item = expressionSlotsView[i].currentItem;
            if (!_validInputs.Contains(item))
            {
                isValid = false;
                expressionSlotsView[i].ResetSlot();
            }
            _validInputs.Remove(item);
        }

        return isValid;
    }

    public override bool TryGetShownData(int slotIndex, out ItemDescription result)
    {
        if(slotIndex > 1)
        {
            result = output;
            return true;
        }
        result = inputs[slotIndex];
        return false;
    }
}
