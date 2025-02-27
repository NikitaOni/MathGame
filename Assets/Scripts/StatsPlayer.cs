using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPlayer : MonoBehaviour
{
    public int levelPlayer = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
