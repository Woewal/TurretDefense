using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    public static LevelController instance;
    public Camera MainCamera;
    public GameObject PlayingField;
    public EnemySpawnController EnemySpawnController;

    public List<Server> Servers;

    public BuildingData SelectedBuilding;

    private void Awake()
    {
        instance = this;
        EnemySpawnController = GetComponent<EnemySpawnController>();
    }

    private void Start()
    {
        SetScrap(100);
    }

    public void AddScrap(int amount)
    {
        GameController.instance.Scrap += amount;
    }

    public void SetScrap(int amount)
    {
        GameController.instance.Scrap = amount;
    }

    public void WinWave()
    {
        SceneManager.LoadScene("WorkShop");
    }
}
