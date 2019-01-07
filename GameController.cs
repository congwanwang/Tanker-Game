using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Warfare.Util;

public class GameController : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject playerPrefab;
    public Vector3 spawnValue;
    public int score;

    public static GameController instance;

    public Dictionary<string, RegisterPackage> registerPlayerTable = new Dictionary<string, RegisterPackage>();

    public Dictionary<string, PlayerController> playerController = new Dictionary<string, PlayerController>();

    public Dictionary<string, int> scoreTable = new Dictionary<string, int>();

    public Dictionary<GameObject, GameObject> bulletList = new Dictionary<GameObject, GameObject>();

    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        MessageHandler.instance.PlayerEnterHandler += AddPlayer;
	}

    void AddPlayer(RegisterPackage package)
    {
        if (registerPlayerTable.ContainsKey(package.openId))
        {
            Debug.Log(package.nickName + " is already in game!");
            return;
        }
        registerPlayerTable.Add(package.openId, package);
        scoreTable.Add(package.openId, 0);
        PlayerController controller = spawnCharacter().GetComponent<PlayerController>();
        controller.setOpenID(package.openId);
        controller.GetComponentInChildren<Text>().text = package.nickName;
        playerController.Add(package.openId, controller);
        MessageHandler.instance.OnPlayerRegisterCompleted(package.openId);
    }

    GameObject spawnCharacter()
    {
        Vector3 spawnPosition = new Vector3(-spawnValue.x, Random.Range(-spawnValue.y, spawnValue.y), 0);
        Quaternion spawnRotation = Quaternion.identity;
        return Instantiate(playerPrefab,spawnPosition, spawnRotation);
    }

    public void playerGameOver(string openID)
    {
        if (!registerPlayerTable.ContainsKey(openID))
        {
            Debug.LogError(openID + " not found. Something is wrong!");
            return;
        }
        MessageHandler.instance.OnPlayerGameOver(openID);
        registerPlayerTable.Remove(openID);
        int score = scoreTable[openID];
        scoreTable.Remove(openID);
        GameObject playerModel = playerController[openID].gameObject;
        Destroy(playerModel);
        playerController.Remove(openID);
        ExitProxy.instance.PlayerFailedToExit(openID, score);
        MessageHandler.instance.OnPlayerRemove();

    }

    public bool IsPlayerInGame(string openID)
    {
        return registerPlayerTable.ContainsKey(openID);
    }

    public void PlayerOperate(OperationPackage operation)
    {
        string openID = operation.openId;
        PlayerController controller = playerController[openID];
        controller.Move(operation.X, operation.Y);
        if (operation.button1)
        {
            controller.PressButtonOne();
        }
    }

    void AllPlayerQuit()
    {
        List<string> QuitList = new List<string>();

        foreach (string openID in registerPlayerTable.Keys)
        {
            QuitList.Add(openID);
        }

        for(int i = 0; i < QuitList.Count; i++)
        {
            playerGameOver(QuitList[i]);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("All player quit.");
        AllPlayerQuit();
    }

    public void AddScore(string openID, int score)
    {
        scoreTable[openID] += score;
        MessageHandler.instance.OnPlayerScoreChange(openID);
    }

    public string GetNickName(string openID)
    {
        if (registerPlayerTable.ContainsKey(openID))
        {
            return registerPlayerTable[openID].nickName;
        }
        else
        {
            Debug.LogError(openID + " not in the game!");
            return "";
        }
    }

}
