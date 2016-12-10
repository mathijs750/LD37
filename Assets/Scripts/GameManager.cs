using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GlobalState
{
    MainMenu,
    GameStartCutscene,
    Cutscene,
    GameEndCutscene,
    Gameplay
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private DrillManager drillManager;

    public static GameManager instance = null;
    public delegate void OnGlobalStateChange(GlobalState currentState, GlobalState oldState);
    public static event OnGlobalStateChange OnStateChanged;

    private GlobalState _globalState;
    public GlobalState globalState
    {
        get { return _globalState; }
        set
        {
            GlobalState oldState = _globalState;
            _globalState = value;
            if (OnStateChanged != null) { OnStateChanged(_globalState, oldState); }
        }
    }

    void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        // TEMP
        _globalState = GlobalState.Gameplay;
        // _globalState = GlobalState.MainMenu;
    }

    public void openOverlay(OverlayType type)
    {
        drillManager.gameObject.SetActive(false);
        uiController.ShowOverlay(type);
        if (type == OverlayType.Periscope)
        {
            cameraController.ChangeMode(MovementMode.Periscope);
        }
        else
        {
            cameraController.ChangeMode(MovementMode.Overlay);
        }
    }

    public void closeOverlay()
    {
        cameraController.ChangeMode(MovementMode.Drill);
        uiController.CloseActiveOverlay();
        drillManager.gameObject.SetActive(true);
    }
}
