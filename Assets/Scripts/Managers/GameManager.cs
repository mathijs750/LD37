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
    [Header("Controllers")]
    [SerializeField]
    private CameraController cameraController;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private DrillManager drillManager;
    [SerializeField]
    private BackgroundSwitchController switchController;

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

    public void nextLayer()
    {
        Debug.Log("Next Layer!");
        globalState = GlobalState.Cutscene;
        openOverlay(OverlayType.DepthMeter);
        switchController.NextLayer();
    }


    public void openOverlay(OverlayType type)
    {
        drillManager.gameObject.SetActive(false);
        playerController.gameObject.SetActive(false);

        if (type == OverlayType.Periscope)
        {
            cameraController.ChangeMode(MovementMode.Periscope);
        }
        else
        {
            cameraController.ChangeMode(MovementMode.Overlay);
        }

        if (type != OverlayType.DepthMeter)
        {
            uiController.ShowOverlay(type,true);
        }
        else
        {
            uiController.ShowOverlay(type, false);
        }
    }

    public void closeOverlay()
    {
        cameraController.ChangeMode(MovementMode.Drill);
        uiController.CloseActiveOverlay();
        drillManager.gameObject.SetActive(true);
        playerController.gameObject.SetActive(true);
    }
}
