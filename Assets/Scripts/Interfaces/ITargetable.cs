using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable {

    void OnTargetUpdate(Enemy enemy);

    void OnUntargetUpdate();

}
