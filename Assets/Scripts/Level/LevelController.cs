using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public EnemySpawnController enemySpawnController;
    public CameraController cameraController;

    [SerializeField] LevelUIController uiControllerPrefab;

    public Server server;
    public List<Enemy> remainingEnemies = new List<Enemy>();

    public int scrap;

    private void Start()
    {
        Initiate();
    }

    private void Initiate()
    {
        LevelUIController uiController = Instantiate(uiControllerPrefab);
        uiController.SetScrap(scrap = GlobalController.instance.gameSettings.defaultScrap);
    }
    
    public void AddScrap(int amount)
    {
        scrap += amount;
        LevelUIController.instance.SetScrap(scrap);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("TestLevel");
        Initiate();
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("Main menu");
    }
    
    public void CheckRemainingEnemies()
    {
        if (remainingEnemies.Count == 0)
        {
            Debug.Log("Won!");
        }
    }
}
