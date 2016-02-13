using UnityEngine;

class ActionScriptDict
{
    

    public static void RunAction(int id)
    {
        MemoryData memoryData = TheWorld.memory.data;

        switch (id)
        {
            

            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}