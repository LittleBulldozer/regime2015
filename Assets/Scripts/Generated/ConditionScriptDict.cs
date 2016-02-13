using UnityEngine;
using System.Linq;

class ConditionScriptDict
{
    static bool S_0(MemoryData memory)
{
int b = 1 + 1;
int c = b + 2;return b > 3;}


    public static bool TestCondition(int id)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            case 0:
return S_0(memoryData);


            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}