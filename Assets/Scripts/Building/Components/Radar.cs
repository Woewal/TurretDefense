using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Radar : BuildingComponent, ITargetable
{
    private List<Enemy> targetedEnemies = new List<Enemy>();

    private List<ITargetable> targetables = new List<ITargetable>();

    public override void Initialize()
    {
        building.OnComponentUpdate += UpdateTargetables;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetedEnemies.Count > 0)
        {
            if(targetedEnemies[0] == null)
            {
                SortTargets();
                return;
            }
            
            foreach (var targetable in targetables)
            {
                targetable.OnTargetUpdate(targetedEnemies[0]);
            }
        }
        else
        {
            foreach (var targetable in targetables)
            {
                targetable.OnUntargetUpdate();
            }
        }
    }

    /// <summary>
    /// Detects whenever an enemy enters the range of the radar
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            var enemy = other.gameObject.GetComponent<Enemy>();

            if (!targetedEnemies.Contains(enemy))
                targetedEnemies.Add(enemy);
        }
    }

    /// <summary>
    /// Detects whenever an enemy leaves the range of the radar
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            targetedEnemies.Remove(other.gameObject.GetComponent<Enemy>());
        }
    }

    /// <summary>
    /// Sorts the targets based on the distance from the radar
    /// </summary>
    public void SortTargets()
    {
        targetedEnemies.RemoveAll(item => item == null);

        targetedEnemies = targetedEnemies.OrderBy(
           x => Vector3.Distance(this.transform.position, x.transform.position)
          ).ToList();
    }

    /// <summary>
    /// Finds the components in the building that are compatible with the radar
    /// </summary>
    private void UpdateTargetables()
    {
        targetables.Clear();

        foreach (var component in building.components)
        {
            if (component is ITargetable)
            {
                targetables.Add((ITargetable)component);
            }
        }
    }

    /// <summary>
    /// Points at the target
    /// </summary>
    /// <param name="enemy"></param>
    public void OnTargetUpdate(Enemy enemy)
    {
        Quaternion targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    public void OnUntargetUpdate()
    {
        transform.localEulerAngles = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * rotationSpeed * 10, transform.localEulerAngles.z)).eulerAngles;
    }

}

