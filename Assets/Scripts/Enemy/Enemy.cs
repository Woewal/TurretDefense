using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Linq;


public class Enemy : MonoBehaviour
{
    public enum Target { Server, Player };

    public float Health;
    public float MaxHealth = 30f;

    public Image HealthBar;

    private List<Server> Targets = new List<Server>();

    EnemyNav EnemyNav;

    private void Start()
    {
        EnemyNav = GetComponent<EnemyNav>();

        if(EnemyNav == null)
        {
            Debug.LogError("WTf");
        }

        Targets = FieldController.instance.Servers;

        Health = MaxHealth;

        FindTarget();
    }

    public void Reevaluate()
    {

    }
    

    void FindTarget()
    {
        Targets = Targets.OrderBy(
            x => Vector3.Distance(this.transform.position, x.transform.position)
        ).ToList();

        EnemyNav.SetTarget(Targets[0].gameObject, Target.Server);
    }

    public void Damage(float amount)
    {
        StartCoroutine(ChangeHealthBar(Health, Health - amount));
        Health -= amount;
        CheckHealth();
    }

    IEnumerator ChangeHealthBar(float oldHealth, float newHealth)
    {
        float currentTime = 0;
        float endTime = 0.2f;

        while (currentTime < endTime)
        {
            HealthBar.fillAmount = Mathf.Lerp(oldHealth / MaxHealth, newHealth / MaxHealth, currentTime / endTime);
            currentTime += Time.deltaTime;
            yield return null;
        }

    }

    void CheckHealth()
    {
        if (Health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        FieldController.instance.EnemySpawnController.Enemies.Remove(this);
        Destroy(gameObject);
        FieldController.instance.EnemySpawnController.CheckIfAllEnemiesAreDead();
    }

    public void SetTarget(Target Target)
    {
        if(Target == Target.Player)
        {

        }
        else if(Target == Target.Server)
        {

        }
    }
}
