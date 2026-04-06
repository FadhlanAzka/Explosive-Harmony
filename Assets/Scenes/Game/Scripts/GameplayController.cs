using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public TimerController timerController;
    public StrikeController strikeController;
    public ModuleWires moduleWires;
    public ModulePassword modulePassword;
    public ModuleColors moduleColors;
    public ModuleSymbol moduleSymbol;
    public ModuleButton moduleButton;

    public GameObject Defused;
    public GameObject Exploded;
    public TextMeshProUGUI DefusedTimeRemaining;
    public TextMeshProUGUI ExplodedTimeRemaining;

    private void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "GameBeginner" && moduleButton.ButtonSuccess && moduleWires.WireSuccess && modulePassword.PasswordSuccess)
        {
            HandleDefused();
        }
        else if (sceneName == "GameIntermediate" && moduleButton.ButtonSuccess && moduleWires.WireSuccess && modulePassword.PasswordSuccess && moduleColors.ColourSuccess)
        {
            HandleDefused();
        }
        else if (sceneName == "GameExpert" && moduleButton.ButtonSuccess && moduleWires.WireSuccess && modulePassword.PasswordSuccess && moduleColors.ColourSuccess && moduleSymbol.SymbolSuccess)
        {
            HandleDefused();
        }

        if (strikeController.StrikeLimit == 4)
        {
            timerController.StopTimer();
            Exploded.SetActive(true);
            ExplodedTimeRemaining.text = "" + timerController.remainingTime;
        }
    }

    private void HandleDefused()
    {
        timerController.StopTimer();
        Defused.SetActive(true);
        DefusedTimeRemaining.text = "" + timerController.remainingTime;
    }
}