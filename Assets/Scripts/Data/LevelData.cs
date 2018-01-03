using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Level data", order = 1)]
public class LevelData : ScriptableObject {
    public List<string> scenes;
}
