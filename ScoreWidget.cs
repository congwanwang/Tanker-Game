using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWidget : MonoBehaviour {

    [SerializeField]
    Text nameText;

    [SerializeField]
    Text scoreText;

    public void SetScoreInfo(ScoreInfo info)
    {
        nameText.text = info.nickname;
        scoreText.text = info.score.ToString();
    }

}

public struct ScoreInfo : IComparer<ScoreInfo>
{
    public string nickname;
    public int score;

    public ScoreInfo(string nickname, int score)
    {
        this.nickname = nickname;
        this.score = score;
    }

    public int Compare(ScoreInfo x, ScoreInfo y)
    {
        if(x.score == y.score)
        {
            return 0;
        }
        if(x.score > y.score)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}