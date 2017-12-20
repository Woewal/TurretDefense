using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FieldController : MonoBehaviour {

    public static FieldController instance;
    public Camera MainCamera;
    public GameObject PlayingField;
    public EnemySpawnController EnemySpawnController;

    public List<Server> Servers;

    public BuildingData SelectedBuilding;

    private void Awake()
    {
        instance = this;
        FindServers();
        EnemySpawnController = GetComponent<EnemySpawnController>();
    }

    private void Start()
    {
        SetScrap(100);
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
