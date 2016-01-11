using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Action : MonoBehaviour
{
    public GameObject actor;

    [System.NonSerialized]
    public float time = 3;

    [System.NonSerialized]
    public List<ActionBlock> blocks;

    public void CacheBlocks()
    {
        if (blocks == null)
        {
            blocks = new List<ActionBlock>();
        }

        blocks.Clear();

        foreach (var B in GetComponentsInChildren<ActionBlock>())
        {
            blocks.Add(B);
        }
    }

    void Awake()
    {
        time = 0;

        CacheBlocks();
    }

    void Start()
    {
        StartCoroutine(UpdateBlocks());
    }

    IEnumerator UpdateBlocks()
    {
        int index = 0;
        bool binit = false;

        while (true)
        {
            var dt = Time.deltaTime;

            while (dt > 0f && index < blocks.Count)
            {
                var B = blocks[index];

                if (binit == false)
                {
                    if (B.Begin(this) == false)
                    {
                        index++;
                        continue;
                    }
                    binit = true;
                }

                var bdt = dt;
                if (time + bdt > B.duration)
                {
                    bdt = B.duration - time;
                }
                dt -= bdt;

                if (time >= B.duration)
                {
                    if (B.End() == false)
                    {
                        time = 0;
                        continue;
                    }

                    index++;
                    time = 0;
                    binit = false;
                }
                else
                {
                    if (B.UpdateTime(time, bdt) == false)
                    {
                        index++;
                        time = 0;
                        binit = false;
                        continue;
                    }

                    time += bdt;
                }
            }

            yield return null;
        }
    }
}
