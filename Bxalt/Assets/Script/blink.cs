using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    public bool haveLight, IsStunEnemy;
    public float lightTime, flashRange, stunTime = 3f;
    public LayerMask whoGetFlashed;
    public bool HaveSound;
    public string Sound;
    audiomanager audiomanager;

    private void Awake()
    {
        audiomanager[] audiomanagers = FindObjectsOfType<audiomanager>();
        foreach (var audio in audiomanagers)
        {
            if (audio.OnlyOneCanBe == false)
            {
                audiomanager = audio;
            }
        }
    }
    void OnEnable()
    {
        if (HaveSound)
        {
            audiomanager.Play(Sound);
        }
        if (haveLight)
        {
            StartCoroutine(Flash());
        }
        if (IsStunEnemy)
        {
            Collider2D[] ThingsGotDamage = Physics2D.OverlapCircleAll(transform.position, flashRange, whoGetFlashed);
            for (int i = 0; i < ThingsGotDamage.Length; i++)
            {
                if (ThingsGotDamage[i].GetComponent<EnemyAttack>())
                {
                    ThingsGotDamage[i].GetComponent<EnemyAttack>().GotKnocked(0, stunTime);
                }
            }
        }
    }
    IEnumerator Flash()
    {
        yield return new WaitForSeconds(lightTime);
        if (gameObject.transform.childCount > 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
