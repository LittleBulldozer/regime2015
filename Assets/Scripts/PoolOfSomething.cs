using UnityEngine;
using System.Collections.Generic;

public class PoolOfSomething<T>
{
    public bool IsEmpty()
    {
        return candidates.Count == 0;
    }

    public void PutOne(T t)
    {
        candidates.Push(t);
    }

    public T GetOne()
    {
        return candidates.Pop();
    }

    Stack<T> candidates = new Stack<T>();
}
