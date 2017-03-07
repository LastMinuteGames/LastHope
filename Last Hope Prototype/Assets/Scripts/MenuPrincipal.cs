using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour {

    public Button start;
    public Button help;
    public Button credits;
    public Button exit;
    public Text startText;
    public Text helpText;
    public Text creditsText;
    public Text exitText;
    public int focus;

    public Color unselectedBGColor;
    public Color selectedBGColor;
    public Color unselectedTextColor;
    public Color selectedTextColor;

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

    //When Enter is pressed, we've selected an item
    /*
		0: Start
		1: Help
		2: Credits
		3: Exit
	*/
    void EnterPress(int focus)
    {

        switch (focus)
        {
            case 0:
                SceneManager.LoadScene("1");
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                Application.Quit();
                break;
        }
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("up"))
        {
            focus = MoveFocusUp(focus);
            UpdateFocus(focus);
        }

        if (Input.GetKeyDown("down"))
        {
            focus = MoveFocusDown(focus);
            UpdateFocus(focus);
        }

        if (Input.GetKeyDown("return"))
        {
            EnterPress(focus);
        }
    }
}
