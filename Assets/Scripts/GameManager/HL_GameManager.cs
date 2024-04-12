using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HL_GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EOverlays
    {
        OVERLAY_MENU = 0,
        OVERLAY_PAUSED,
        OVERLAY_INGAMEUI,
        OVERLAY_GAMEOVER,
        OVERLAY_OPTIONS,
        OVERLAY_MAX,
    }

    public EOverlays CurrentOverlay = EOverlays.OVERLAY_MENU;
    private EOverlays PreviousOverlay = EOverlays.OVERLAY_MENU;

    public GameObject MainMenu;
    public GameObject PauseScreen;
    public GameObject InGameOverlay;
    public GameObject GameOverOverlay;
    public GameObject OptionsOverlay;
    public GameObject LocalPlayer;
    public bool bOverrideCursor = false;

    private HL_PlayerController LocalPlayerController;

    bool bPaused = false;
    void Start()
    {
        LocalPlayerController = LocalPlayer.GetComponent<HL_PlayerController>();
        LocalPlayerController.SetLocalPlayerState(false);
        LocalPlayer.SetActive(false);
    }

    void UpdateOverlay(EOverlays Overlay)
    {
 
        PreviousOverlay = CurrentOverlay;
        CurrentOverlay = Overlay;
        MainMenu.SetActive(Overlay == EOverlays.OVERLAY_MENU);
        PauseScreen.SetActive(Overlay == EOverlays.OVERLAY_PAUSED);
        InGameOverlay.SetActive(Overlay == EOverlays.OVERLAY_INGAMEUI);
        GameOverOverlay.SetActive(Overlay == EOverlays.OVERLAY_GAMEOVER);
        OptionsOverlay.SetActive(Overlay == EOverlays.OVERLAY_OPTIONS);
        LocalPlayer.SetActive(Overlay == EOverlays.OVERLAY_INGAMEUI || Overlay == EOverlays.OVERLAY_PAUSED
             || (PreviousOverlay == EOverlays.OVERLAY_PAUSED && Overlay == EOverlays.OVERLAY_OPTIONS));

        if (Overlay == EOverlays.OVERLAY_INGAMEUI)
        {
            LocalPlayerController.SetLocalPlayerState(true);
        }
        else if (Overlay == EOverlays.OVERLAY_PAUSED)
        {
            LocalPlayerController.SetLocalPlayerState(true);
            LocalPlayerController.ControllerActive = false;
        }
        else if (Overlay == EOverlays.OVERLAY_PAUSED)
        {
            LocalPlayerController.SetLocalPlayerState(false);
        }

    }

    public void OpenOptionsScreen()
    {
        UpdateOverlay(EOverlays.OVERLAY_OPTIONS);
    }
    public void SetOverlayToPrevious()
    {
        UpdateOverlay(PreviousOverlay);
    }
    public void OnMenuLoad()
    {
        UpdateOverlay(EOverlays.OVERLAY_MENU);
    }
    public void OnGameOver()
    {
        UpdateOverlay(EOverlays.OVERLAY_GAMEOVER);
    }
    public void OnLevelLoad()
    {
        UpdateOverlay(EOverlays.OVERLAY_INGAMEUI);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    void Update()
    {
        if (bPaused || CurrentOverlay == EOverlays.OVERLAY_INGAMEUI)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                bPaused = !bPaused;

                if (bPaused)
                    UpdateOverlay(EOverlays.OVERLAY_PAUSED);
                else
                    UpdateOverlay(EOverlays.OVERLAY_INGAMEUI);
            }

        switch (CurrentOverlay)
        {
            case EOverlays.OVERLAY_INGAMEUI:
                {
                    Cursor.visible = false;
                    break;
                }
            case EOverlays.OVERLAY_MENU:

                {
                    Cursor.visible = true;
                    LocalPlayer.SetActive(false);
                    break;
                }
            case EOverlays.OVERLAY_PAUSED:
            case EOverlays.OVERLAY_GAMEOVER:
            case EOverlays.OVERLAY_OPTIONS:
                {
                    Cursor.visible = true;
                    break;
                }
            default:
                {
                    Application.Quit(); // How did this even end up like this?
                    break;
                }
        }

        if (bOverrideCursor)
            Cursor.visible = true;
    }
}
