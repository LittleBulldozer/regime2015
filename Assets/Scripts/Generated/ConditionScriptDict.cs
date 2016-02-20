using UnityEngine;
using System.Linq;

class ConditionScriptDict
{
    

    public static bool TestCondition(int id)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            

            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}