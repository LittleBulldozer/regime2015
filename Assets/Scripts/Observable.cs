using UnityEngine;
using System.Collections.Generic;

public class Observable<T>
{
#if UNITY_EDITOR
    public bool dbg = false;
#endif

    private T obj;
    private int idCount = 0;
    private Dictionary<int, System.Action<T>> dict = new Dictionary<int, System.Action<T>>();
    private List<System.Action<T>> disposables = new List<System.Action<T>>();
    private bool isDictIterating = false;
    private List<int> deathIds = new List<int>();

    public Observable()
    {
    }

    public Observable(T initObj)
    {
        obj = initObj;
#if UNITY_EDITOR
        if (dbg)
        {
            Debug.Log("observable init with " + initObj);
        }
#endif
    }


    public T Value
    {
        get
        {
#if UNITY_EDITOR
            if (dbg)
            {
                Debug.Log("observable get " + obj);
            }
#endif
            return obj;
        }

        set
        {
            obj = value;

#if UNITY_EDITOR
            if (dbg)
            {
                Debug.Log("observable set " + obj);
            }
#endif

            isDictIterating = true;
            foreach (var item in dict)
            {
                item.Value(obj);
            }
            isDictIterating = false;

            if (deathIds.Count > 0)
            {
                foreach (int id in deathIds)
                {
                    dict.Remove(id);
                }
                deathIds.Clear();
            }

            foreach (var callback in disposables)
            {
                callback(obj);
            }
            disposables.Clear();
        }
    }

    public int Listen(System.Action<T> callback)
    {
        int id = idCount++;
        dict.Add(id, callback);
        return id;
    }

    public void ListenOnce(System.Action<T> callback)
    {
        disposables.Add(callback);
    }

    public void StopListen(int id)
    {
        if (dict.ContainsKey(id) == false)
        {
            throw new System.Exception("no such id : " + id);
        }

        if (isDictIterating)
        {
            deathIds.Add(id);
        }
        else
        {
            dict.Remove(id);
        }
    }

    public delegate T COMP1<K>(K obj);
    public static Observable<T> Compute<K>(Observable<K> a, COMP1<K> func)
    {
        Observable<T> ret = new Observable<T>();

        a.Listen(x =>
        {
            ret.Value = func(x);
        });

        return ret;
    }
}