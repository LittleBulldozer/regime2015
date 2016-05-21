using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public int nrTurn
    {
        get
        {
            return _nrTurn;
        }
    }

    public void NotifyCardSelected()
    {
        cardSelected = TheWorld.cardCanvas.selectedCard;
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

            if (_nrTurn > 1)
            {
                yield return new WaitForSeconds(1f);

                TheWorld.cardCanvas.gameObject.SetActive(true);
                TheWorld.cardCanvas.scaleGroup.ShowWithDelap();
            }

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
}
