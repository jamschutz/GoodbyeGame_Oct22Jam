using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_WakeUp_PlayerName : MonoBehaviour
{
    void Start()
    {
        var textMesh = GetComponent<TMPro.TMP_Text>();
        textMesh.text = $"{GameManager.PlayerName}...wake up!";
    }
}
