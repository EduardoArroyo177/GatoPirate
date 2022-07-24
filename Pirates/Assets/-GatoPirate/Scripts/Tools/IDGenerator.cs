using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDGenerator : SceneSingleton<IDGenerator>
{
    public string GetGeneratedID(string _idBase)
    {
        long time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        return $"{_idBase.Replace(" ", "")}{time}";
    }

    public string GetGeneratedID()
    {
        long time = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        return time.ToString();
    }
}
