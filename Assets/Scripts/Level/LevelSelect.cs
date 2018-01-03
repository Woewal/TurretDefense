using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] LevelData availableLevels;
    [SerializeField] Button levelButtonPrefab;

    private void Start()
    {
        LoadLevels();
    }

    private void LoadLevels()
    {
        foreach (string levelName in availableLevels.scenes)
        {
            Button newButton = Instantiate(levelButtonPrefab, transform);
            newButton.onClick.AddListener(delegate () { SceneManager.LoadScene(levelName); });
        }
    }
}
