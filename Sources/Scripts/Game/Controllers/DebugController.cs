using System;
using System.Collections.Generic;
using System.Linq;
using Game.Entities;
using Game.Enums;
using Ludiq.OdinSerializer.Utilities;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public static DebugController Instance { get; private set; }

    [Header("General")]
    public bool ShowDebugLayout;

    public bool ShowSingleSelectionLayout;

    [Header("Layouts")]
    public bool LineOfSight;

    public bool Range;
    public bool AggroList;
    public bool Target;

    [Header("Other")]
    public bool DebugClicks;

    /*------------------------------------------------------------------*\
    |*							PROPS
    \*------------------------------------------------------------------*/

    public Selection Selection => Selection.Instance;
    public GameController GameController => GameController.Instance;

    /*------------------------------------------------------------------*\
    |*							HOOKS
    \*------------------------------------------------------------------*/

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        SubscribeToInputEvents();
        SubscribeToGUIEvents();
    }

    private void Update() {
        DrawEntitiesLayouts();
    }

    /*------------------------------------------------------------------*\
    |*							SUBSCRIPTIONS
    \*------------------------------------------------------------------*/

    private void SubscribeToInputEvents() {
        InputsManager.Instance.OnEntityLeftClicked += EntityLeftClickedHandler;
        InputsManager.Instance.OnEntityRightClicked += EntityRightClickedHandler;
        InputsManager.Instance.OnGroundLeftClicked += GroundLeftClickedHandler;
        InputsManager.Instance.OnGroundRightClicked += GroundRightClickedHandler;
    }

    private void SubscribeToGUIEvents() {
        // GUISelectionPanel.Instance.OnDebugCheckBoxClicked += DebugCheckBoxClickedHandler;
    }


    /*------------------------------------------------------------------*\
    |*							ENTITY DEBUG HANDLERS
    \*------------------------------------------------------------------*/

    private void DebugCheckBoxClickedHandler(bool value) => Selection.Selected[0].debug = value;

    /*------------------------------------------------------------------*\
    |*							CLICK HANDLERS
    \*------------------------------------------------------------------*/
    private void EntityLeftClickedHandler(Entity entity) => Log($"EntityLeft: \t{entity.Meta.Class}");
    private void EntityRightClickedHandler(Entity entity) => Log($"EntityRight: \t{entity.Meta.Class}");
    private void GroundLeftClickedHandler(Vector3 position) => Log($"GroundLeft: \t{position}");
    private void GroundRightClickedHandler(Vector3 position) => Log($"GroundRight: \t{position}");

    private void Log(string message) {
        if (!DebugClicks) return;
        Debug.Log(message);
    }

    /*------------------------------------------------------------------*\
    |*							PRIVATE METHODES
    \*------------------------------------------------------------------*/

    private void DrawEntitiesLayouts() {
        if (ShowDebugLayout) {
            GameController.entities.Where(i => i.debug).ForEach(DebugFunctions);

            var count = Selection.Selected.Count;

            if (ShowSingleSelectionLayout && count == 1 && !Selection.Selected.First().debug) {
                DebugFunctions(Selection.Selected.First());
            }
        }
    }

    private void DebugFunctions(Entity entity) {
        if (entity.Meta.Type != EntityType.Character) return;

        if (LineOfSight) EntityDebug.DrawLineOfSight(entity);
        if (Range) EntityDebug.DrawRange(entity);
        if (AggroList) EntityDebug.DrawAggroList(entity);
    }
}
