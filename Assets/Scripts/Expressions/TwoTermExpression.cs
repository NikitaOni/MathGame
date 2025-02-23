using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Два слагаемых
/// </summary>

[CreateAssetMenu(fileName = "NewExpression", menuName = "Game/Expression/Two terms Expression")]
public class TwoTermExpression : BaseExpression
{
    private const int ANSWER_SLOT_INDEX = 2;
    public override bool CheckExpression(List<ExpressionSlotView> expressionSlotsView)
    {
        var isValid = expressionSlotsView[ANSWER_SLOT_INDEX].currentItem == output;

        if (!isValid)
        {
            expressionSlotsView[ANSWER_SLOT_INDEX].ResetSlot();
        }
        return isValid;
    }

    public override bool TryGetShownData(int slotIndex, out ItemDescription result)
    {
        if (slotIndex > 1)
        {
            result = output;
            return false;
        }

        result = inputs[slotIndex];
        return true;
    }
}
