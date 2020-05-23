using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceexplosion : MonoBehaviour
{
    public float ObstacleDamage, damageRange;
    public LayerMask whoIsPlayer;
    public bool NeedDamage = true, DestroyOrActiveFalse = true;
    public float timeForceExist = 0.01f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (NeedDamage)
        {
            Collider2D[] ThingsGotDamage = Physics2D.OverlapCircleAll(transform.position, damageRange, whoIsPlayer);
            for (int i = 0; i < ThingsGotDamage.Length; i++)
            {
                if (ThingsGotDamage.Length > 0 && !ThingsGotDamage[i].gameObject.GetComponent<forceexplosion>())
                {
                    float distanceFromExplo = Vector2.Distance(transform.position, ThingsGotDamage[i].transform.position);
                    int damageCal = Mathf.RoundToInt(
                          (ObstacleDamage - (((distanceFromExplo < damageRange) ? (distanceFromExplo / damageRange) : 1) * ObstacleDamage)));
                    if (ThingsGotDamage[i].GetComponent<Enemy>())
                    {
                        ThingsGotDamage[i].GetComponent<Enemy>().TakeDamage(damageCal);
                    }
                }
            }
        }
    }
    void OnEnable()
    {
        if (timeForceExist != 0)
        {
            if (DestroyOrActiveFalse)
            {
                Destroy(this.gameObject, timeForceExist);
            }
            else
            {
                StartCoroutine(SetActiveFalseDelay(timeForceExist));
            }
        }
    }
    IEnumerator SetActiveFalseDelay(float Time)
    {
        yield return new WaitForSeconds(Time);
        gameObject.SetActive(false);
    }
    // Update is called once per frame

}
