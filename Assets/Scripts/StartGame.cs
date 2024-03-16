using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StartGame : MonoBehaviour
{
    public TMP_Dropdown NumberOfPlayers;
    public TMP_Dropdown Terrain;

    void PlayerCount()
    {
        Debug.Log(NumberOfPlayers.value);
    }

    void TerrainOption()
    {
        Debug.Log(Terrain.value);
    }

    public void OnClick()
    {
        PlayerCount();
        TerrainOption();
    }
}
