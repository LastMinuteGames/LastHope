using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{

    public Button continueGame;
    public Button exit;
    public Text continueGameText;
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
    void Start()
    {
        unselectedBGColor = new Color(0.529F, 0.407F, 0.239F, 1);
        selectedBGColor = new Color(0.364F, 0.305F, 0.227F, 1);
        unselectedTextColor = new Color(0F, 0F, 0F, 1);
        selectedTextColor = new Color(1F, 1F, 1F, 1);

        //Boton Iniciar Partida - Initial state: Selected
        continueGame = continueGame.GetComponent<Button>();
        continueGame.image.color = selectedBGColor;
        continueGameText = continueGameText.GetComponent<Text>();
        continueGameText.color = selectedTextColor;

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

        if (focus != 1)
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
                continueGame.image.color = selectedBGColor;
                exit.image.color = unselectedBGColor;

                continueGameText.color = selectedTextColor;
                exitText.color = unselectedTextColor;
                break;
            case 1:
                continueGame.image.color = unselectedBGColor;
                exit.image.color = selectedBGColor;

                continueGameText.color = unselectedTextColor;
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
                    this.GetComponentInParent<GameController>().closeMenu();
                    break;
                case 1:
                    openExitMenu();
                    break;
            }
        }
        else
        {
            switch (focus)
            {
                case 0:
                    GetComponentInParent<GameController>().isMenu = false;
                    SceneManager.LoadScene("Menu");
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
        continueGame.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
    }

    //Open the confirm exit menu
    void closeExitMenu()
    {
        confirmExit.gameObject.SetActive(false);
        isConfirmExit = false;
        focusExit = 1;
        continueGame.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
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
