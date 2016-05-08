using UnityEngine;

class ActionScriptDict
{
    static void S_0(int turn, MemoryData memory)
{
memory.Fear += 5;}


    public static void RunAction(int id, int nrTurn)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            case 0:
S_0(nrTurn, memoryData);
break;


            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}