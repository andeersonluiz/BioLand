

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class PhasesManager : MonoBehaviour
{

    public GameObject[] phasesList;
    public int value;
    private PlayerManager scenesManager;
    public Sprite star1;
    public Sprite star2;
    public Sprite star3;
    private Dictionary<string, int> phList;
    private void Awake()
    {
        phList = PlayerManager.phaseList;
        foreach (var entry in phList)
        {
            updatePhases(int.Parse(entry.Key), entry.Value);
        }
        for (int i = (phList.Count + 1); i < phasesList.Length; i++)
        {
            disablePhase(phasesList[i]);
        }

    }
    public void updatePhases(int phaseIndex, int score)
    {
        phasesList[phaseIndex].transform.GetChild(0).GetComponent<Text>().alignment = TextAnchor.LowerCenter;
        GameObject grandChild = phasesList[phaseIndex].transform.GetChild(0).GetChild(0).gameObject;
        grandChild.SetActive(true);
        grandChild.GetComponent<Image>().sprite = getStar(score);
    }

    private void disablePhase(GameObject phase)
    {
        phase.GetComponent<Button>().interactable = false;
    }

    private Sprite getStar(int score)
    {
        if (score < 300)
        {
            return star1;
        }
        else if (score < 600)
        {
            return star2;
        }
        else
        {
            return star3;

        }
    }

    public void onMouseEnter(int index)
    {
        if (phList[index.ToString()] != 0)
        {
            GameObject bestScoreQuad = phasesList[index].transform.GetChild(1).gameObject;
            bestScoreQuad.SetActive(true);
            bestScoreQuad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = phList[index.ToString()].ToString();
        }

    }

    public void onMouseExit(int index)
    {
        phasesList[index].transform.GetChild(1).gameObject.SetActive(false);
    }
}