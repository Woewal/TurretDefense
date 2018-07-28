using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enables a building component to be compatible to be combined with targeting components
/// </summary>
public interface ITargetable {

    void OnTargetUpdate(Enemy enemy);

    void OnUntargetUpdate();

}
