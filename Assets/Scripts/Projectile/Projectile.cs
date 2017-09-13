using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    GameObject Target;
    public float Speed;

    public float Damage = 10f;

    private Coroutine FireCoroutine;

    public void SetTarget(GameObject target)
    {
        Target = target;
        Fire();
    }

    public abstract void Fire();

    public IEnumerator FireInStaticDirection()
    {
        if(Target == null)
        {
            Destroy(this);
            yield break;
        }
        transform.LookAt(Target.transform.position);

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        while (true)
        {
            this.transform.position += this.transform.forward * Speed * 0.1f;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().Damage(10f);
            Destroy(this.gameObject);
        }
    }
}
