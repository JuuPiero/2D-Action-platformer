using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static void ClearChild(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }


    public static List<T> GetChilds<T>(this Transform transform)  where T : Component
    {
        List<T> childs = new();
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            T t = transform.GetChild(i).GetComponent<T>();
            if (t != null) childs.Add(t);
        }
        return childs;
    }
}