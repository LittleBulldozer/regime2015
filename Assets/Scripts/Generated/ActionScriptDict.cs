using UnityEngine;

class ActionScriptDict
{
    static void S_0(int turn, MemoryData memory)
{
memory.Fear += 5;}
static void S_1(int turn, MemoryData memory)
{
Basic_000.priority = 10;
Basic_001.priority = 10;
Basic_002.priority = 10;}


    public static void RunAction(int id, int nrTurn)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            case 0:
S_0(nrTurn, memoryData);
break;
case 1:
S_1(nrTurn, memoryData);
break;


            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}