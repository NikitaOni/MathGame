using UnityEngine;

[CreateAssetMenu(fileName = "NewGameItem", menuName = "Game/Create New Item")]
public class ItemDescription : ScriptableObject
{
    public new string name;
    public string id;
    public Sprite icon;
}
