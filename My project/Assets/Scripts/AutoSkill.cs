using UnityEngine;

public class AutoSkill : MonoBehaviour
{
    public GameObject skillPrefab;
    public float interval = 2f;
    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            FireAtClosestEnemy();
            timer = interval;
        }
    }

    void FireAtClosestEnemy()
    {
        GameObject target = FindClosestEnemy();
        if (target != null)
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(dir);
            Instantiate(skillPrefab, transform.position + dir * 1.5f, rotation);
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float minDist = float.MaxValue;
        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < minDist)
            {
                closest = e;
                minDist = dist;
            }
        }
        return closest;
    }
}
