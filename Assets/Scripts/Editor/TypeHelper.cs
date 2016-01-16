using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public class TypeHelper : MonoBehaviour
{
    public static System.Type[] GetAllSubTypes(System.Type ParentType)
    {
        List<System.Type> list = new List<System.Type>();
        foreach (Assembly a in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (System.Type type in a.GetTypes())
            {
                if (type.IsSubclassOf(ParentType))
                {
                    list.Add(type);
                }
            }
        }
        return list.ToArray();
    }
}
