using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Слагаемое на первом месте и один ответ
/// </summary>

[CreateAssetMenu(fileName = "NewExpression", menuName = "Game/Expression/First With Answer Expression")]
public class FirstWithAnswerExpression : BaseExpression
{
    private const int SECOND_SLOT_INDEX = 1;
    public override bool CheckExpression(List<ExpressionSlotView> expressionSlotsView)
    {
        var isValid = expressionSlotsView[SECOND_SLOT_INDEX].currentItem == inputs[SECOND_SLOT_INDEX];

        if (!isValid) 
        {
            expressionSlotsView[SECOND_SLOT_INDEX].ResetSlot();
        }
        return isValid;
    }

    public override bool TryGetShownData(int slotIndex, out ItemDescription result)
    {
        if (slotIndex == 0)
        {
            result = inputs[slotIndex];
            return true;
        }

        if (slotIndex == 2)
        {
            result = output;
            return true;
        }

        result = inputs[slotIndex];
        return false;
    }
}
