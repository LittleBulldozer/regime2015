using UnityEngine;
using System.Collections;

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

    CardPool.CardContext cardSelected = null;
    RectTransform turnAlertPrefab;
    int _nrTurn = 1;

    void Awake()
    {
        TheWorld.gameFlow = this;
        turnAlertPrefab = Resources.Load<RectTransform>("prefabs/TurnAlert");
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
            turnAlert.SetParent(TheWorld.eventCanvas.transform, false);

            TheWorld.triggerMgr.RunAllTriggers(_nrTurn);

            var contexts = TheWorld.cardPool.DrawCards(4);
            TheWorld.cardCanvas.ApplyView(contexts);

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
