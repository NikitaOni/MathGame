using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Game/LevelDB")]
public class LevelDB : ScriptableObject
{
    public LevelDescription[] innerData;
}
