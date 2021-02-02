using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum Status { Pause = 0, Play = 1, Speed = 2 };

public class ButtonVelocityManager : MonoBehaviour
{
    public Button[] listButtons;
    private Status _status;
    private Enviroment ev;
    private Parameters parameters;
    private CloudScript cloudScript;
    private SkyColors skyColors;
    private static ButtonVelocityManager bm_instance;
    public int lastButtonUsedIndex;
    public static ButtonVelocityManager Instance { get { return bm_instance; } }
    void Awake()
    {
        lastButtonUsedIndex = 1;
        ev = FindObjectOfType<Enviroment>();
        cloudScript = FindObjectOfType<CloudScript>();
        skyColors = FindObjectOfType<SkyColors>();
        parameters = new Parameters();
        ev.isPaused = true;
        status = Status.Pause;
        bm_instance = this;
    }

    public void selectButton()
    {
        GameObject go = EventSystem.current.currentSelectedGameObject;
        Button btn = go.GetComponent<Button>();
        int index = -1;
        for (int i = 0; i < listButtons.Length; i++)
        {

            if (btn.name == listButtons[i].name)
            {
                listButtons[i].interactable = false;
                index = i;
                lastButtonUsedIndex = index;
            }
            else
            {
                listButtons[i].interactable = true;
            }
        }
        setStatus(index);
    }


    public void setStatus(int value)
    {
        switch (value)
        {
            case 0:
                status = Status.Pause;
                ev.isPaused = true;
                break;
            case 1:
                status = Status.Play;
                ev.isPaused = false;
                ev.timeDay = parameters.timeDay;
                cloudScript.respawnCloud = parameters.respawnCloud;
                cloudScript.speedCloud = parameters.speedCloud;
                skyColors.valueAlpha = parameters.valueAlpha;
                break;
            case 2:
                status = Status.Speed;
                ev.isPaused = false;
                ev.timeDay = parameters.timeSpeed;
                cloudScript.respawnCloud = parameters.respawnCloud / 2;
                cloudScript.speedCloud = parameters.speedCloud * 2;
                skyColors.valueAlpha = parameters.valueAlpha * 5;


                break;
        }
        updateButtons();
    }
    public void updateButtons()
    {

        for (int i = 0; i < 3; i++)
        {

            if (i == (int)status)
            {
                listButtons[i].interactable = false;
            }
            else
            {
                listButtons[i].interactable = true;

            }
        }
    }

    public Status status { get; set; }
}
