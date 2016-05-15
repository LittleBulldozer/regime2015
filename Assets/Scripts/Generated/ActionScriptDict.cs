using UnityEngine;

class ActionScriptDict
{
    static void S_0(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Approval += 5;;}
static void S_1(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
cardPool.GetCard("Basic_000").priority = 5;
cardPool.GetCard("Basic_001").priority = 5;
cardPool.GetCard("Basic_002").priority = 5;
cardPool.GetCard("Basic_003").priority = 5;;}
static void S_2(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fund += 5;}
static void S_3(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fear += 5;;}
static void S_4(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Approval +=5;
memory.Fear +=5;;}
static void S_5(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fear +=5;}
static void S_6(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fear +=10;;}
static void S_7(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fear = 100;;}
static void S_8(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fear +=20;;}
static void S_9(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Approval +=20;;}
static void S_10(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Fear +=15;;}
static void S_11(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
memory.Approval +=10;;}
static void S_12(int turn, MemoryData memory, CardPool cardPool, GameFlow gameFlow)
{
cardPool.GetCard("Adv_000").priority = 30;
cardPool.GetCard("Adv_001").priority = 30;
cardPool.GetCard("Adv_002").priority = 30;
cardPool.GetCard("Adv_003").priority = 30;;}


    public static void RunAction(int id, int nrTurn)
    {
        MemoryData memoryData = TheWorld.memory.data;
		CardPool cardPool = TheWorld.cardPool;
		GameFlow gameFlow = TheWorld.gameFlow;

        switch (id)
        {
            case 0:
S_0(nrTurn, memoryData, cardPool, gameFlow);
break;
case 1:
S_1(nrTurn, memoryData, cardPool, gameFlow);
break;
case 2:
S_2(nrTurn, memoryData, cardPool, gameFlow);
break;
case 3:
S_3(nrTurn, memoryData, cardPool, gameFlow);
break;
case 4:
S_4(nrTurn, memoryData, cardPool, gameFlow);
break;
case 5:
S_5(nrTurn, memoryData, cardPool, gameFlow);
break;
case 6:
S_6(nrTurn, memoryData, cardPool, gameFlow);
break;
case 7:
S_7(nrTurn, memoryData, cardPool, gameFlow);
break;
case 8:
S_8(nrTurn, memoryData, cardPool, gameFlow);
break;
case 9:
S_9(nrTurn, memoryData, cardPool, gameFlow);
break;
case 10:
S_10(nrTurn, memoryData, cardPool, gameFlow);
break;
case 11:
S_11(nrTurn, memoryData, cardPool, gameFlow);
break;
case 12:
S_12(nrTurn, memoryData, cardPool, gameFlow);
break;


            default:
                throw new System.Exception("Unhandled action id : " + id);
        }
    }
}