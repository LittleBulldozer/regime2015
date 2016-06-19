using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public float delay;

    public int nrTurn
    {
        get
        {
            return _nrTurn;
        }
    }

    public void NotifyCardSelected()
    {
        StartCoroutine(BeginCardSelectedProc());
    }

    public void BackToTitle()
    {
        SceneManager.LoadSceneAsync("title");
    }

    CardPool.CardContext cardSelected = null;
    UITurnAlert turnAlertPrefab;
    int _nrTurn = 1;

    void Awake()
    {
        TheWorld.gameFlow = this;
        turnAlertPrefab = Resources.Load<UITurnAlert>("prefabs/TurnAlert");
    }

    void Start ()
    {
        StartCoroutine(MainFlow());
	}
	
    IEnumerator MainFlow()
    {
        while (true)
        {
            Debug.Log("Beginning of the turn " + _nrTurn + ".");

            var turnAlert = Instantiate(turnAlertPrefab);
            turnAlert.transform.SetParent(TheWorld.eventCanvas.transform, false);
            var nowdate = TheWorld.timer.dateTime.Value;
            turnAlert.SetString(nowdate.Year, nowdate.Month, nowdate.Day
                , _nrTurn);

            TheWorld.triggerMgr.RunAllTriggers(_nrTurn);

            var contexts = TheWorld.cardPool.DrawCards(4);
            TheWorld.cardCanvas.ApplyView(contexts);

            yield return new WaitForSeconds(delay);

            TheWorld.cardCanvas.gameObject.SetActive(true);
            TheWorld.cardCanvas.scaleGroup.ShowWithDelap();

            while (true)
            {
                if (cardSelected != null)
                {
                    cardSelected.RunAction(_nrTurn);
                    cardSelected = null;
                    break;
                }

                yield return null;
            }

            _nrTurn++;
        }
    }


    IEnumerator BeginCardSelectedProc()
    {
        var selectedOne = TheWorld.cardCanvas.selectedCard;
        selectedOne.transform.SetSiblingIndex(selectedOne.transform.parent.childCount);
        yield return selectedOne.GoAway();
        TheWorld.cardCanvas.gameObject.SetActive(false);
        cardSelected = selectedOne.cx;
    }
}
