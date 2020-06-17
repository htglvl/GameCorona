using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject item;
        public int dropRarity, HowManyDrop;
    }

    public List<DropCurrency> loottable = new List<DropCurrency>();
    public int dropChance;
    public void caclculateLoot()
    {
        int cacl_dropChance = Random.Range(1, 101);
        if (cacl_dropChance > dropChance)
        {
            return;
        }
        if (cacl_dropChance <= dropChance)
        {
            int itemweight = 0;
            for (int i = 0; i < loottable.Count; i++)
            {
                itemweight += loottable[i].dropRarity;
            }
            int RandomValue = Random.Range(0, itemweight);
            for (int j = 0; j < loottable.Count; j++)
            {
                if (RandomValue <= loottable[j].dropRarity)
                {
                    for (int i = 0; i < loottable[j].HowManyDrop; i++)
                    {
                        Instantiate(loottable[j].item, transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)), Quaternion.identity);
                    }
                    return;
                }
                RandomValue -= loottable[j].dropRarity;
            }
        }
    }
}
