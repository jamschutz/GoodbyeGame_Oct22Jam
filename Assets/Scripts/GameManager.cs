using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Items")]
    public int Disc_Captain = 0;

    public static string PlayerName { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPlayerName(string playerName)
    {
        GameManager.PlayerName = playerName;
        Debug.Log($"set player name to: {GameManager.PlayerName}");
    }


    public void GetPlayerNameFromText(string gameObjectName)
    {
        var textMesh = GameObject.Find(gameObjectName).GetComponent<TMPro.TMP_Text>();
        SetPlayerName(textMesh.text);
    }
}
