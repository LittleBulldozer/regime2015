using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxUI : MonoBehaviour
{
    public MyBar approvalBar;
    public MyBar fearBar;
    public Text fundTxt;

    int cachedFund = 0;

    void Start()
    {
        StartCoroutine(WatchGameMemory());
    }

    IEnumerator WatchGameMemory()
    {
        var data = TheWorld.memory.data;

        while (true)
        {
            var approval = data.Approval;
            if (approvalBar.progress.Value != approval)
            {
                approvalBar.progress.Value = approval;
            }

            var fear = data.Fear;
            if (fearBar.progress.Value != fear)
            {
                fearBar.progress.Value = fear;
            }

            var fund = data.Fund;
            if (cachedFund != fund)
            {
                fundTxt.text = string.Format("{0:n0} $", fund);
                cachedFund = fund;
            }

            yield return null;
        }
    }
}
