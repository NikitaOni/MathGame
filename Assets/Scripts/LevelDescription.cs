using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Game/Create NewLevel")]
public class LevelDescription : ScriptableObject
{
    public BaseExpression[] expressions;
    public int garbageAnswers;
}
