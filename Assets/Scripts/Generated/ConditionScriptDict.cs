using UnityEngine;
using System.Linq;

class ConditionScriptDict
{
    static bool S_0(int turn, MemoryData memory)
{

return Random.Range(0, 100) >= 50;}
static bool S_1(int turn, MemoryData memory)
{

return memory.Phase_0;}


    public static bool TestCondition(int id, int nrTurn)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            case 0:
return S_0(nrTurn, memoryData);
case 1:
return S_1(nrTurn, memoryData);


            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}