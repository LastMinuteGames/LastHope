using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button start;
    public Button help;
    public Button credits;
    public Button exit;
    public Text startText;
    public Text helpText;
    public Text creditsText;
    public Text exitText;
    public int focus;
    //Confirm exit menu
    public bool isConfirmExit = false;
    public Canvas confirmExit;
    public Button yes;
    public Button no;
    public Text yesText;
    public Text noText;
    public int focusExit;
    //Help
    public bool isHelp = false;
    public Canvas helpCanvas;
    //Credits
    public bool isCredits = false;
    public Canvas creditsCanvas;

    public Color unselectedBGColor;
    public Color selectedBGColor;
    public Color unselectedTextColor;
    public Color selectedTextColor;

    //Input
    bool upInUse = false;
    bool downInUse = false;
    bool leftInUse = false;
    bool rightInUse = false;

    // Use this for initialization
    void Start () {

        unselectedBGColor = new Color(0.529F, 0.407F, 0.239F, 1);
        selectedBGColor = new Color(0.364F, 0.305F, 0.227F, 1);
        //unselectedTextColor = new Color(0.333F, 0.239F, 0.086F, 1);
        //selectedTextColor = new Color(0.176F, 0.156F, 0.086F, 1);
        unselectedTextColor = new Color(0F, 0F, 0F, 1);
        selectedTextColor = new Color(1F, 1F, 1F, 1);

        //Boton Iniciar Partida - Initial state: Selected
        start = start.GetComponent<Button>();
        start.image.color = selectedBGColor;
        startText = startText.GetComponent<Text>();
        startText.color = selectedTextColor;

        //Boton Level Select - Initial state: Unselected
        help = help.GetComponent<Button>();
        help.image.color = unselectedBGColor;
        helpText = helpText.GetComponent<Text>();
        helpText.color = unselectedTextColor;

        //Boton Controles - Initial state: Unselected
        credits = credits.GetComponent<Button>();
        credits.image.color = unselectedBGColor;
        creditsText = creditsText.GetComponent<Text>();
        creditsText.color = unselectedTextColor;

        //Boton Salida - Initial state: Unselected
        exit = exit.GetComponent<Button>();
        exit.image.color = unselectedBGColor;
        exitText = exitText.GetComponent<Text>();
        exitText.color = unselectedTextColor;

        //focus manages button focus logic - initiate 0
        focus = 0;

        //Manages the confirm exit canvas (sets it to invisible)
        confirmExit = confirmExit.GetComponent<Canvas>();
        confirmExit.gameObject.SetActive(false);

        //Flag isConfirmExit to false
        isConfirmExit = false;

        //Boton SI salir
        yes = yes.GetComponent<Button>();
        yes.image.color = unselectedBGColor;
        yesText = yesText.GetComponent<Text>();
        yesText.color = unselectedTextColor;

        //Boton NO salir
        no = no.GetComponent<Button>();
        no.image.color = unselectedBGColor;
        noText = noText.GetComponent<Text>();
        noText.color = unselectedTextColor;

        //focus exit menu
        focusExit = 1;

        //HelpCanvas
        helpCanvas = helpCanvas.GetComponent<Canvas>();
        helpCanvas.gameObject.SetActive(false);

        //Flag isHelp to false
        isHelp = false;

        //CreditCanvas
        creditsCanvas = creditsCanvas.GetComponent<Canvas>();
        creditsCanvas.gameObject.SetActive(false);

        //Flag isHelp to false
        isCredits = false;

    }

    //Move focus up
    public int MoveFocusUp(int focus)
    {

        if (focus != 0)
        {
            focus = focus - 1;
        }

        return focus;
    }

    //Move focus down
    public int MoveFocusDown(int focus)
    {

        if (focus != 3)
        {
            focus = focus + 1;
        }

        return focus;
    }

    //Move focus right in exit menu
    public int MoveFocusRight(int focus)
    {

        if (focus == 0)
        {
            focus = 1;
        }

        return focus;
    }

    //Move focus left in exit menu
    public int MoveFocusLeft(int focus)
    {

        if (focus == 1)
        {
            focus = 0;
        }

        return focus;
    }

    //Redraw the buttons if focus has changed
    void UpdateFocus(int focus)
    {

        switch (focus)
        {
            case 0:
                start.image.color = selectedBGColor;
                help.image.color = unselectedBGColor;
                credits.image.color = unselectedBGColor;
                exit.image.color = unselectedBGColor;

                startText.color = selectedTextColor;
                helpText.color = unselectedTextColor;
                creditsText.color = unselectedTextColor;
                exitText.color = unselectedTextColor;
                break;
            case 1:
                start.image.color = unselectedBGColor;
                help.image.color = selectedBGColor;
                credits.image.color = unselectedBGColor;
                exit.image.color = unselectedBGColor;

                startText.color = unselectedTextColor;
                helpText.color = selectedTextColor;
                creditsText.color = unselectedTextColor;
                exitText.color = unselectedTextColor;
                break;
            case 2:
                start.image.color = unselectedBGColor;
                help.image.color = unselectedBGColor;
                credits.image.color = selectedBGColor;
                exit.image.color = unselectedBGColor;

                startText.color = unselectedTextColor;
                helpText.color = unselectedTextColor;
                creditsText.color = selectedTextColor;
                exitText.color = unselectedTextColor;
                break;
            case 3:
                start.image.color = unselectedBGColor;
                help.image.color = unselectedBGColor;
                credits.image.color = unselectedBGColor;
                exit.image.color = selectedBGColor;

                startText.color = unselectedTextColor;
                helpText.color = unselectedTextColor;
                creditsText.color = unselectedTextColor;
                exitText.color = selectedTextColor;
                break;
        }
    }
    //Redraw the buttons of the confirm exit menu if focus has changed
    void UpdateFocusExit(int focus)
    {

        switch (focus)
        {
            case 0:
                yes.image.color = selectedBGColor;
                no.image.color = unselectedBGColor;

                yesText.color = selectedTextColor;
                noText.color = unselectedTextColor;
                break;
            case 1:
                yes.image.color = unselectedBGColor;
                no.image.color = selectedBGColor;

                yesText.color = unselectedTextColor;
                noText.color = selectedTextColor;
                break;
        }
    }

    //When Enter is pressed, we've selected an item
    /*
		0: Start
		1: Help
		2: Credits
		3: Exit
	*/
    void EnterPress(int focus)
    {
        if (!isConfirmExit)
        {
            switch (focus)
            {
                case 0:
                    SceneManager.LoadScene("CityMap");
                    break;
                case 1:
                    openHelp();
                    break;
                case 2:
                    openCredits();
                    break;
                case 3:
                    openExitMenu();
                    break;
            }
        }
        else
        {
            switch (focus)
            {
                case 0:
                    Debug.Log("Application quit");
                    Application.Quit();
                    break;
                case 1:
                    closeExitMenu();
                    break;
            }
        }
        
    }

    //Open the confirm exit menu
    void openExitMenu()
    {
        confirmExit.gameObject.SetActive(true);
        isConfirmExit = true;
        focusExit = 1;
        UpdateFocusExit(focusExit);
    }

    //Open the confirm exit menu
    void closeExitMenu()
    {
        confirmExit.gameObject.SetActive(false);
        isConfirmExit = false;
        focusExit = 1;
    }

    //Open Help
    void openHelp()
    {
        helpCanvas.gameObject.SetActive(true);
        isHelp = true;
        start.gameObject.SetActive(false);
        help.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    //Close Help
    void closeHelp()
    {
        helpCanvas.gameObject.SetActive(false);
        isHelp = false;
        start.gameObject.SetActive(true);
        help.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }

    //Open Credits
    void openCredits()
    {
        creditsCanvas.gameObject.SetActive(true);
        isCredits = true;
        start.gameObject.SetActive(false);
        help.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    //Close Credits
    void closeCredits()
    {
        creditsCanvas.gameObject.SetActive(false);
        isCredits = false;
        start.gameObject.SetActive(true);
        help.gameObject.SetActive(true);
        credits.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        if (isHelp || isCredits)
        {
            //Go back to main menu
            if (InputManager.Pause())
            {
                if (isHelp) closeHelp();
                if (isCredits) closeCredits();
            }
        }
        else
        {
            if (!isConfirmExit)
            {
                //Main menu
                if (InputManager.LeftJoystickUp() || Input.GetKeyDown("up"))
                {
                    if (!upInUse)
                    {
                        upInUse = true;
                        focus = MoveFocusUp(focus);
                        UpdateFocus(focus);
                    }
                }
                else
                {
                    upInUse = false;
                }
                if (InputManager.LeftJoystickDown() || Input.GetKeyDown("down"))
                {
                    if (!downInUse)
                    {
                        downInUse = true;
                        focus = MoveFocusDown(focus);
                        UpdateFocus(focus);
                    }
                }
                else
                {
                    downInUse = false;
                }
            }
            else
            {
                //Exit menu
                if (InputManager.LeftJoystickLeft() || Input.GetKeyDown("left"))
                {
                    if (!leftInUse)
                    {
                        leftInUse = true;
                        focusExit = MoveFocusLeft(focusExit);
                        UpdateFocusExit(focusExit);
                    }
                }
                else
                {
                    leftInUse = false;
                }

                if (InputManager.LeftJoystickRight() || Input.GetKeyDown("right"))
                {
                    if (!rightInUse)
                    {
                        rightInUse = true;
                        focusExit = MoveFocusRight(focusExit);
                        UpdateFocusExit(focusExit);
                    }
                }
                else
                {
                    rightInUse = false;
                }
            }
        }
        if (InputManager.Dodge() || Input.GetKeyDown("return"))
        {
            if (isConfirmExit)
            {
                EnterPress(focusExit);
            }
            else
            {
                EnterPress(focus);
            }
        }
    }
}
