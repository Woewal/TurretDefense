using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public static FieldController instance;
    public FieldUIController UIController;
    public Camera MainCamera;
    public GameObject PlayingField;
    public EnemySpawnController EnemySpawnController;

    public List<Server> Servers;

    private void Awake()
    {
        instance = this;
        FindServers();
        UIController = GameObject.FindObjectOfType<FieldUIController>();
        EnemySpawnController = GetComponent<EnemySpawnController>();
    }

    private void Start()
    {
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

    public void AddScrap(int Amount)
    {
        GameController.instance.Scrap += Amount;
        UIController.ChangeScrap(GameController.instance.Scrap);
    }
}
