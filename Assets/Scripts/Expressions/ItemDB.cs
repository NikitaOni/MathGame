using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Game/Database")]
public class ItemDB : ScriptableObject
{
    [SerializeField] private ItemDescription[] innerData;
    public readonly Dictionary<string, ItemDescription> items = new();

    public void Init()
    {
        foreach (var item in innerData)
        {
            items[item.id] = item;
        }
    }

    public ItemDescription GetRandomItem()
    {
        var index = Random.Range(0, innerData.Length);
        return innerData[index];
    } 
}
