using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private static TMP_Dropdown NumberOfPlayersDropdown;
    [SerializeField] private static TMP_Dropdown Terrain;

    public static int PlayerCount()
    {
        return int.Parse(NumberOfPlayersDropdown.itemText.ToString());
    }

    private static void TerrainOption()
    {
        SceneManager.LoadScene(Terrain.value);
    }

    public static void OnClick()
    {
        TerrainOption();
    }
}
