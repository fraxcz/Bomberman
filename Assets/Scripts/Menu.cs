using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class Menu: MonoBehaviour
{
    [SerializeField] private TMP_Dropdown NumberOfPlayersDropdown;
    [SerializeField] private TMP_Dropdown Terrain;


    public void LoadLevel()
    {
        GameManager.NumberOfPlayers = int.Parse(NumberOfPlayersDropdown.options[NumberOfPlayersDropdown.value].text);
        SceneManager.LoadScene(Terrain.value);
    }
    public void Quit()
    {
        Application.Quit();
    }

}
