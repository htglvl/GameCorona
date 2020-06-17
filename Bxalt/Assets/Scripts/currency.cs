using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currency : MonoBehaviour
{
    public float TimeOnGround;
    void Start()
    {
        Destroy(this.gameObject, TimeOnGround);
    }
    public float HowManyMoneyGetWhenContact = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            money PlayerWallet = other.GetComponent<money>();
            PlayerWallet.CollectMoney(HowManyMoneyGetWhenContact);
            Destroy(this.gameObject);
            //transform.position = Vector2.Lerp(transform.position, other.transform.position, Time.deltaTime * 10f);
        }
    }

}
