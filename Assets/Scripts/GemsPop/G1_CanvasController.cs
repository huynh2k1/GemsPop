using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class G1_CanvasController : MonoBehaviour
{
    public static G1_CanvasController instance;
    public TMP_Text scoreText;
    public TMP_Text targetScoreText;
    public TMP_Text levelText;
    private void Awake()
    {
        instance = this;
    }
    //cập nhật điểm hiện tại
    public void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
    //cập nhật điểm mục tiêu
    public void UpdateTargetScoreText(int targetScore)
    {
        targetScoreText.text = "Target Score: " + targetScore.ToString();  
    }
    //cập nhật text level
    public void UpdateLevelText(int level)
    {
        levelText.text = "Level : " + level.ToString();  
    }    
}
