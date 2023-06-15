using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class G1_GameController : MonoBehaviour
{
    public static G1_GameController instance;
    public int score;
    public int targetScore;
    public int level;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ResetStat();
        G1_CanvasController.instance.UpdateScoreText(score);
        G1_CanvasController.instance.UpdateTargetScoreText(targetScore);
        G1_CanvasController.instance.UpdateLevelText(level);
    }
    public void ResetStat()
    {
        score = 0;
        targetScore = 0;
        level = 1;
    }
    public void CheckIsLevelUp()
    {
        if(score >= targetScore)
        {
            
        }    
    }
    public void LoseGame()
    {

    }
}
