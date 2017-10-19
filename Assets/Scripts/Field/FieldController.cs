using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldController : MonoBehaviour {

    public static FieldController instance;
    public FieldUIController UIController;
    public Camera MainCamera;
    public GameObject PlayingField;
    public EnemySpawnController EnemySpawnController;

    public List<Server> Servers;

    public BuildingData SelectedBuilding;

    private void Awake()
    {
        instance = this;
        FindServers();
        UIController = GameObject.FindObjectOfType<FieldUIController>();
        EnemySpawnController = GetComponent<EnemySpawnController>();
    }

    private void Start()
    {
        SetScrap(100);
        UIController.ChangeScrap(GameController.instance.Scrap);
    }

    void FindServers()
    {
        Servers = new List<Server>();

        GameObject[] servers = GameObject.FindGameObjectsWithTag("Server");

        foreach (GameObject x in servers)
        {
            Servers.Add(x.GetComponent<Server>());
        }
    }

    public void AddScrap(int amount)
    {
        GameController.instance.Scrap += amount;
        UIController.ChangeScrap(GameController.instance.Scrap);
    }

    public void SetScrap(int amount)
    {
        GameController.instance.Scrap = amount;
        UIController.ChangeScrap(GameController.instance.Scrap);
    }

    public void WinWave()
    {
        SceneManager.LoadScene("WorkShop");
    }
}
