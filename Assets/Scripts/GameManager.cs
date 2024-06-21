using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

class GameManager: MonoBehaviour
{
    private static List<GameObject> Bombs = new List<GameObject>();
    private static List<GameObject> Players = new List<GameObject>();
    public static int NumberOfPlayers;      
    private Vector3[] SpawnPositions;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PauseMenu;
    private bool Paused = false;

    private void Start()
    {
        SpawnPositions = new Vector3[] {new Vector3(-6.5f, -3.4f, 0f), new Vector3(6.5f, -3.4f, 0f), new Vector3(0f, 3.6f, 0f)};
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            Player.GetComponent<PlayerScript>().PlayerID = i + 1;
            Players.Add(Instantiate(Player, SpawnPositions[i], Quaternion.identity));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Paused)
            {
                Paused = true;
                PauseMenu.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                ResumeGame();
            }
        }
        if(NumberOfPlayers <= 1)
        {
            MainMenu();
        }
    }
    public static bool DeployBomb(GameObject bomb, Vector3 pos, PlayerScript player)
    {
        if (!CheckForBomb(pos))
        {
            var newBomb = BombScript.Deploy(bomb, pos);
            newBomb.GetComponent<BombScript>().Player = player;
            Bombs.Add(newBomb);
            return true;
        }
        return false;
    }
    public static void RemoveBomb(GameObject bomb) => Bombs.Remove(bomb);

    public void ResumeGame()
    {
        Paused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        Players.Clear();
        Bombs.Clear();
        SceneManager.LoadScene(0);
    }

    public static bool CheckForBomb(Vector3 pos)
    {
        foreach (GameObject Bomb in Bombs)
        {
            if (Bomb.transform.position == pos) return true;
        }
        return false;
    }
    public static void CheckForPlayers(int x, int y)
    {
        foreach(GameObject player in Players)
        {
            if (player.IsDestroyed()) continue;
            else if (Math.Floor(player.transform.position.x) == x && Math.Floor(player.transform.position.y) == y)
            {
                player.GetComponent<PlayerScript>().Die();
                NumberOfPlayers--;
            }
        }
    }

}
