using System.Collections.Generic;
using UnityEngine;

public abstract class BaseExpression : ScriptableObject
{
    public ItemDescription[] inputs;
    public ItemDescription output;

    public abstract bool TryGetShownData(int slotIndex, out ItemDescription result);

    public abstract bool CheckExpression(List<ExpressionSlotView> expressionSlotsView);
}

