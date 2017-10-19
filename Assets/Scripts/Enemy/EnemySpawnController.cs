using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class EnemySpawnController : MonoBehaviour
{
    public List<Enemy> Enemies;
    public List<Enemy> SpawnableEnemies;
    public GameObject[] SpawnLocations;

    private bool AllEnemiesSpawned = false;

    public List<EnemyTypes> EnemyTypes;

    public TextAsset Waves;

    XmlDocument xmlDoc;

    private void Start()
    {
        Enemies = new List<Enemy>();
        SpawnableEnemies = new List<Enemy>();
        SpawnLocations = GameObject.FindGameObjectsWithTag("EnemySpawn");
        LoadDocument();
        LoadWave(1);
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        while (SpawnableEnemies.Count != 0)
        {
            Enemy newEnemy = Instantiate(SpawnableEnemies[0]);
            Enemies.Add(newEnemy);
            newEnemy.transform.position = SpawnLocations[Random.Range(0, SpawnLocations.Length)].transform.position;
            SpawnableEnemies.RemoveAt(0);

            yield return new WaitForSeconds(1f);
        }
        AllEnemiesSpawned = true;
    }
    
    void LoadWave(int ID)
    {
        SpawnableEnemies.Clear();

        XmlNodeList waves = xmlDoc.GetElementsByTagName("Wave");

        XmlNode wave = null;
        foreach (XmlNode w in waves)
        {
            if (int.Parse(w.Attributes["id"].Value) == ID)
            {
                wave = w;
            }
        }

        if (wave != null)
        {
            foreach (XmlNode enemy in wave.ChildNodes)
            {
                for (int i = 0; i < int.Parse(enemy.Attributes["amount"].Value); i++)
                {
                    Enemy newEnemy = EnemyTypes.Find(x => x.Name == enemy.Attributes["name"].Value).EnemyType;
                    SpawnableEnemies.Add(newEnemy);
                }  
            }
        }
        

    }

    void LoadDocument()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Waves.text);   
    }

    public void CheckIfAllEnemiesAreDead()
    {
        if (Enemies.Count == 0 && AllEnemiesSpawned)
        {
            FieldController.instance.WinWave();
        }
    }
}
