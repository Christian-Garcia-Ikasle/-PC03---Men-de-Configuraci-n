using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    //The variable to manage the state of the pause screen
    private bool isPaused; 
    public GameObject pauseMenu ;
    public PlayerController playerController;
    public PlayerControls playerControls;
    void Start(){
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Floor.Pause.performed += ctx => PausePerformed(ctx);
    }

    public void PausePerformed(InputAction.CallbackContext callbackContext){
         isPaused = !isPaused;
            if (isPaused)
            {
                PauseGame();
                playerController.enabled = false;
            }
            else
            {
                ResumeGame();
                playerController.enabled = true;
            } 
    }

    public void PauseGame()
    {
        // This sets Time.timeScale to 0 to pause gameplay
        Time.timeScale = 0;
        // This makes the PauseMenu panel visible
        pauseMenu.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        //This sets moves the cursor to the character when resuming the game
        Vector2 mcPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Mouse.current.WarpCursorPosition(mcPos);
        // This sets Time.timeScale back to 1 to resume gameplay
        Time.timeScale = 1;
        //This hides the PauseMenu panel 
        pauseMenu.gameObject.SetActive(false);
        //Hide the cursor
        Cursor.visible = false;
    }

    //This quits the game
    public void QuitGame(){
        Application.Quit();
    }
}