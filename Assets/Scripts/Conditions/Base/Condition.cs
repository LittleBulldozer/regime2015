﻿using UnityEngine;
using System.Collections;

public abstract class Condition : ScriptableObject
{
	public abstract bool IsSatisfied(int nrTurn);
}