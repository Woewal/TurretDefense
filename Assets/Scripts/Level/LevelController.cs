using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
    public CameraController cameraController;

    [SerializeField] LevelUIController uiControllerPrefab;
    [HideInInspector] public EnemyManager enemyManager;

    public Server server;

    public int scrap;

    private void Start()
    {
        Initiate();
    }

    public void Initiate()
    {
        enemyManager = GetComponent<EnemyManager>();
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

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLevel();
        }
    }
}
