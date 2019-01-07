using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Warfare.Util;

public class ScoreList : MonoBehaviour {

    [SerializeField]
    List<ScoreWidget> ScoreWidgets;

    private void Start()
    {
        MessageHandler.instance.PlayerRegisterCompleted += PlayerRegisterCompleted;
        MessageHandler.instance.PlayerRemove += ShowUpdatedScore;
        MessageHandler.instance.PlayerScoreChange += PlayerScoreChange;
        //Test();
    }

    void PlayerRegisterCompleted(string openID)
    {
        ShowUpdatedScore();
    }

    void PlayerScoreChange(string openID)
    {
        ShowUpdatedScore();
    }

    void ShowUpdatedScore()
    {

        List<ScoreInfo> infoList = new List<ScoreInfo>();

        foreach (string openID in GameController.instance.scoreTable.Keys)
        {
            infoList.Add(new ScoreInfo(GameController.instance.GetNickName(openID), GameController.instance.scoreTable[openID]));
        }

        infoList.Sort(new ScoreInfo());

        int size = Mathf.Min(infoList.Count, ScoreWidgets.Count);

        for(int i = 0; i < ScoreWidgets.Count; i++)
        {
            ScoreWidgets[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < size; i++)
        {
            ScoreWidgets[i].gameObject.SetActive(true);
            ScoreWidgets[i].SetScoreInfo(infoList[i]);
        }
    }

    //void Test()
    //{
    //    List<ScoreInfo> infoList = new List<ScoreInfo>();
    //    infoList.Add(new ScoreInfo("aaa", 3));
    //    infoList.Add(new ScoreInfo("c", 4));
    //    infoList.Add(new ScoreInfo("bb", 5));
    //    infoList.Add(new ScoreInfo("y", 1));
    //    infoList.Add(new ScoreInfo("x", 2));
    //    infoList.Add(new ScoreInfo("zzz", 2));
    //    infoList.Sort(new ScoreInfo());
    //    for(int i = 0; i < infoList.Count; i++)
    //    {
    //        Debug.LogError(infoList[i].nickname + ":" + infoList[i].score);
    //    }
    //}

}
