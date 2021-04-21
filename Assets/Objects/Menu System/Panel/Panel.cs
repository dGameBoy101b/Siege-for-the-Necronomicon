using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* @author Connor Burnside 33394927  
* @date 4/04/2021
* The class for the Panel object, these are unity canvases used to display the different menu screens
* which can then be switched between
*/

public class Panel : MonoBehaviour
{

    [Tooltip("Stores the canvas object this script is attached to so the show property can be changed")]
    private Canvas canvas;

    [Tooltip("Stores the menu manager that this panel should be a child of")]
    private MenuManager menuManager;

    /**
    * Sets the canvas variable as soon as the script is loaded
    */
    private void Awake() 
    {
        canvas = GetComponent<Canvas>();
    }

    /**
    * Sets up the panel by setting the menu manager and hiding the panel 
    */
    public void Setup(MenuManager mm)
    {
        menuManager = mm;
        Hide();
    }

    /**
    * A function that allows the menu manager to edit the enabled variable of the canvas
    */
    public void Show()
    {
        canvas.enabled = true;
    }
    
    /**
    * A function that allows the menu manager to edit the enabled variable of the canvas
    */
    public void Hide()
    {
        canvas.enabled = false;
    }
}
