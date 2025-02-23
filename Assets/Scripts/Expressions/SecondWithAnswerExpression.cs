using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Слагаемое на втором месте и один ответ
/// </summary>

[CreateAssetMenu(fileName = "NewExpression", menuName = "Game/Expression/Second With Answer Expression")]
public class SecondWithAnswerExpression : BaseExpression
{
    private const int FIRST_SLOT_INDEX = 0;
    public override bool CheckExpression(List<ExpressionSlotView> expressionSlotsView)
    {
        var isValid = expressionSlotsView[FIRST_SLOT_INDEX].currentItem == inputs[FIRST_SLOT_INDEX];

        if (!isValid)
        {
            expressionSlotsView[FIRST_SLOT_INDEX].ResetSlot();
        }
        return isValid;
    }

    public override bool TryGetShownData(int slotIndex, out ItemDescription result)
    {
        if (slotIndex == 1)
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
