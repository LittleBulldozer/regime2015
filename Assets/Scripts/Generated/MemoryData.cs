using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class MemoryData
{
	[SerializeField]
public int Approval = 80;
[SerializeField]
public int Fear = 0;
[SerializeField]
public int Fund = 10;
[SerializeField]
public int AME_Reputation = 50;
[SerializeField]
public int SOV_Reputation = 50;
[SerializeField]
public bool Phase_0 = true;
[SerializeField]
public bool Phase_1 = false;
[SerializeField]
public bool Phase_2 = false;

}