using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* @author Connor Burnside 33394927  
* @date 4/04/2021
* The class for the menu manager object, used to navigate through menus and menu history
*/

public class MenuManager : MonoBehaviour
{
    /**
    * The current menu panel, this is the only panel that is shown at any given time
    */
    public Panel currentPanel;

    /**
    * The history of panels that the user has clicked through
    */
    private List<Panel> panelHistory = new List<Panel>();

	/**
	 * The menu managers canvas component which allows the menu to be opened or closed with the menu or escape key
	 */
	private Canvas canvas;

    /**
    * Calls SetupPanels on start
    */
    private void Start() 
    {
        SetupPanels();
		canvas = GetComponent<Canvas>();
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            canvas.enabled = true;
        }
    }

    /**
    * Current way of checking if back button is pressed to go to previous panel, should be moved to a centralised input class
	* also opens the menu if the start or esc key are pressed
    */
    private void Update() 
    {
        if(OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            GoToPrevious();
        }

        if((OVRInput.GetDown(OVRInput.Button.Start) || Input.GetKeyDown(KeyCode.Escape)) && SceneManager.GetActiveScene().name != "MainMenu" )
		{
			if(canvas.enabled)
			{
				canvas.enabled = false;
                //timescale pauses or resumes the gameplay
                Time.timeScale = 1;
			}
			else
			{
				canvas.enabled = true;
                Time.timeScale = 0;
			}
		}
    }

    /**
    * Puts all menu panels into an array and then calls the panel.Setup function to initialise them one by one,
    * then shows current panel
    */
    private void SetupPanels()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach (Panel panel in panels)
        {
            panel.Setup(this);
        }

        currentPanel.Show();
    }

    /**
    * Can be called to go to the previous menu panel in the panel history, 
    * called when back button on controller is pressed or when a back button on a panel is pressed
    */
    public void GoToPrevious()
    {
        int lastIndex = panelHistory.Count - 1;

        if(panelHistory.Count == 0)
        {
            OVRManager.PlatformUIConfirmQuit();
            return;
        }

        SetCurrent(panelHistory[lastIndex]);
        panelHistory.RemoveAt(lastIndex);
    }

    /**
    * Sets current panel from the panel history
    * @param newPanel The new panel that will become the current panel and be added to the panel History
    */
    public void SetCurrentWithHistory(Panel newPanel)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newPanel);
    }

    /**
    * Hides the previous panel and Shows the current panel
    * @param newPanel the new panel that will become the current panel
    */
    private void SetCurrent(Panel newPanel)
    {
        currentPanel.Hide();

        currentPanel = newPanel;
        currentPanel.Show();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}