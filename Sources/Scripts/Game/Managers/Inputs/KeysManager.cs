using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

public class KeysManager
{
    private static KeysManager instance;
    public static KeysManager Instance => instance ?? (instance = new KeysManager());

    private bool bars = true;

    private readonly TimeManager TimeManager;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    private GameController GameController => GameController.Instance;
    private Selection Selection => Selection.Instance;

    /*------------------------------------------------------------------*\
    |*							CONSTRUCTORS
    \*------------------------------------------------------------------*/

    private KeysManager() {
        TimeManager = GameController.GetComponent<TimeManager>();
    }

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    public void Update() {
        BarsHandler();
        TimeScaleHandler();
        EntitiesHandler();
        GroupsHandler();
    }


    /*------------------------------------------------------------------*\
    |*							GROUPS
    \*------------------------------------------------------------------*/

    private List<Entity>[] groups = new List<Entity>[9];

    private void GroupsHandler() {
        for (int i = 1; i <= 9; i++) {
            if (Input.GetKeyDown(i.ToString())) {
                if (Input.GetKey(KeyCode.LeftAlt)) {
                    groups[i - 1] = new List<Entity>(Selection.Selected);
                    Debug.Log($"group {i} bound ({groups[i - 1].Count})");
                }
                else {
                    Debug.Log($"select group {i} ({groups[i - 1].Count})");
                    Selection.AddRange(groups[i - 1]);
                }
            }
        }
    }

    /*------------------------------------------------------------------*\
    |*							ENTITIES
    \*------------------------------------------------------------------*/

    private void EntitiesHandler() {
        DeleteIfSelected();
    }

    private void DeleteIfSelected() {
        if (Input.GetKeyDown(KeyCode.Delete) && Selection.Selected.Count > 0) {
            Selection.Selected.ToList().ForEach(i => i.Disable(soft: false));
        }
    }

    /*------------------------------------------------------------------*\
    |*							TIME SCALE
    \*------------------------------------------------------------------*/

    private void TimeScaleHandler() {

        if (Input.GetKey(KeyCode.Slash) || Input.GetKey(KeyCode.K)) {
            TimeManager.ResetTimeScale();
        }

        if (Input.GetKey(KeyCode.Comma) || Input.GetKey(KeyCode.J)) {
            TimeManager.DecrementTimeScale();
        }

        if (Input.GetKey(KeyCode.Period)|| Input.GetKey(KeyCode.L)) {
            TimeManager.IncrementTimeScale();
        }
    }

    /*------------------------------------------------------------------*\
    |*							BARS
    \*------------------------------------------------------------------*/

    private void BarsHandler() {
        if (Input.GetKeyDown(KeyCode.B)) {
            ToggleBars();
        }
    }

    private void ToggleBars() {
        bars = !bars;

        GameController.entities.ForEach(
            e => e.extensions.Find("HealthBar").gameObject.SetActive(bars)
        );
    }
}
