using System;
using UnityEngine;
public class ManagerBase<T> : MonoBehaviour where T : class
{
    private static T _instance = null;
    public static void Set(T instance)
    {
        _instance = instance;
    }
    public static T Get()
    {
        if(_instance == null)
        {
            Debug.LogError("Manger インスタンスが無い、Setし忘れたか、削除されたか確認してください.");
        }

        return _instance;
    }
}
