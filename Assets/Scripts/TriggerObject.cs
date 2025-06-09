using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    [Header("Stage")]
    public bool isStageName = false;
    public string stageName;
    public float stageNameTime = 3f;
    public GameObject stageNamePanel;
    public TextMeshProUGUI stageNameText;

    public void UpdateStageNameUI()
    {
        if (isStageName)
        {
            stageNamePanel.SetActive(true);
            stageNameText.text = stageName;
            StartCoroutine(UIActiveTime());
        }
    }

    IEnumerator UIActiveTime()
    {
        yield return new WaitForSeconds(stageNameTime);
        stageNamePanel.SetActive(false);
    }
}