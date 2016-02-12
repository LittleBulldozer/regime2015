using UnityEngine;

class ActionScriptDict
{
    static void S_0(MemoryData memory)
{
memory.test2++;}


    public static void RunAction(int id)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            case 0:
S_0(memoryData);
break;


            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}