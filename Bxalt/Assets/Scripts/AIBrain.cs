using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIBrain : MonoBehaviour
{
    public GameObject Mask, Shield, MedicHat, Canvas, panel, TayBanCircle, virusCircle, SotCircle, forceField, sani, gianCach;
    money playerWallet;
    public bool BiBenh, BiBan, daxetnghiem, haveJustDamaged = false, BiSot;
    public float phantramnguoiTayBan = 0.5f, phanTramNguoiBiSot = 0.3f, phanTramNguoiCoMask = 0.5f, phanTramNguoiCoShield = 0.5f, phanTramNguoiCoMedicHat = 0.5f, phantramnguoicoSani = .3f, phanTramNguoiBiBenh = .3f, phanTramNguoiBietGianCach = 0.4f, DoTayRua = 100f, detectRadius;
    public TextMeshProUGUI CoBiBanko, CoBiSotKhong, CoBiBenhKo;
    private float DoBaoVe = 0f, pri_SotTime;
    public bool DenkhuCachLy;
    public float DiemBaoVeCuaMask = .3f, DiemBaoVeCuaSani = .35f, DiemBaoVeCuaGianCach = .35f, DiemTruCuaTayBan = 0.35f, DiemTruCuaSot = 0.5f, SotTime;
    // Start is called before the first frame update
    void Start()
    {
        playerWallet = Transform.FindObjectOfType<money>();
        forceField.SetActive(false);
        Mask.SetActive(Random.value < phanTramNguoiCoMask ? true : false);
        MedicHat.SetActive(Random.value < phanTramNguoiCoMedicHat ? true : false);
        Shield.SetActive(Random.value < phanTramNguoiCoShield ? true : false);
        sani.SetActive(Random.value < phantramnguoicoSani ? true : false);
        gianCach.SetActive(Random.value < phanTramNguoiBietGianCach ? true : false);
        if (Random.value < phanTramNguoiBiSot)
        {
            CoBiSotKhong.text = "Bị sốt, cần đưa đi cách ly";
            BiSot = true;
        }
        else
        {
            CoBiSotKhong.text = "Không sốt";
            BiSot = false;
        }
        if (Random.value < phantramnguoiTayBan)
        {
            //Debug.Log("bibenh");
            CoBiBanko.text = "Tay bẩn, rửa bằng nước rửa tay";
            BiBan = true;
        }
        else
        {
            //Debug.Log("ko benh");
            BiBan = false;
            CoBiBanko.text = "Tay sạch";
        }
        if (Random.value < phanTramNguoiBiBenh)
        {
            Debug.Log("bibenh");
            CoBiBenhKo.text = "Đã nhiễm COVID-19, cần cách ly";
            BiBenh = true;
        }
        else
        {
            CoBiBenhKo.text = "Không nhiễm COVID-19";
            Debug.Log("ko benh");
            BiBenh = false;
        }
        panel.SetActive(false);
    }
    private void Update()
    {
        DoBaoVe = 0;
        if (Mask.activeInHierarchy == true || Shield.activeInHierarchy == true)
        {
            DoBaoVe += DiemBaoVeCuaMask;
        }
        if (sani.activeInHierarchy == true)
        {
            DoBaoVe += DiemBaoVeCuaSani;
        }
        if (gianCach.activeInHierarchy == true)
        {
            DoBaoVe += DiemBaoVeCuaGianCach;
        }
        if (BiBan == true)
        {
            DoBaoVe -= DiemTruCuaTayBan;
        }
        if (BiSot == true)
        {
            DoBaoVe -= DiemTruCuaSot;
        }
        Collider2D[] otherCivillan = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        for (int i = 0; i < otherCivillan.Length; i++)
        {
            if (otherCivillan != null && (otherCivillan[i].gameObject.tag == "Enemy" || otherCivillan[i].gameObject.tag == "Player"))
            {
                if (otherCivillan[i].GetComponent<AIBrain>())
                {
                    if (otherCivillan[i].GetComponent<AIBrain>().BiBenh && !BiBenh && Random.value > DoBaoVe)
                    {
                        BiBenh = true;
                        CoBiBanko.text = "Đã nhiễm COVID-19, cần cách ly";
                        virusCircle.GetComponent<Image>().fillAmount = 1;
                    }
                }
                if (otherCivillan[i].gameObject.tag == "Player" && BiBenh)
                {
                    if (GameObject.FindGameObjectWithTag("ShieldForPlayer").GetComponent<SpriteRenderer>().enabled == false)
                    {
                        if (haveJustDamaged == false)
                        {
                            haveJustDamaged = true;
                            otherCivillan[i].GetComponent<Enemy>().TakeDamage(20);
                        }
                        StartCoroutine(Forcefield(.3f));
                    }
                }
            }
        }
        Canvas.transform.LookAt(Canvas.transform.position + Vector3.forward);
        TayBanCircle.transform.LookAt(TayBanCircle.transform.position + Vector3.forward);
        virusCircle.transform.LookAt(virusCircle.transform.position + Vector3.forward);
        SotCircle.transform.LookAt(SotCircle.transform.position + Vector3.forward);
        if (daxetnghiem)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                panel.SetActive(true);
            }
            else
            {
                panel.SetActive(false);
            }
            if (BiBan == true)
            {
                TayBanCircle.SetActive(true);
            }
            else
            {
                TayBanCircle.SetActive(false);
            }
            if (BiBenh)
            {
                virusCircle.SetActive(true);
            }
            else
            {
                virusCircle.SetActive(false);
            }
            if (BiSot == true)
            {
                SotCircle.SetActive(true);
            }
            else
            {
                SotCircle.SetActive(false);
            }
        }
        else
        {
            TayBanCircle.SetActive(false);
            virusCircle.SetActive(false);
            SotCircle.SetActive(false);
        }
        if (DoTayRua <= 0)
        {
            DoTayRua = 0;
            BiBan = false;
            CoBiBanko.text = "Tay sạch";
            TayBanCircle.GetComponent<Image>().fillAmount = 0;
        }

        if (BiSot)
        {
            pri_SotTime += Time.deltaTime;
        }
        if (pri_SotTime >= SotTime)
        {
            pri_SotTime = 0;
            BiSot = false;
            BiBenh = true;
        }
    }
    // Update is called once per frame
    public void GotHitBy(string bulletName = "", float damage = 0f)
    {
        if (bulletName.Contains("thermo"))
        {
            if (daxetnghiem == false)
            {
                daxetnghiem = true;
                //CoBiBanko.gameObject.SetActive(true);
            }
        }
        if (bulletName.Contains("kt") && Mask.activeInHierarchy == false)
        {
            Mask.SetActive(true);
            playerWallet.CollectMoney(3);
        }
        if (bulletName.Contains("S") && Shield.activeInHierarchy == false)
        {
            Shield.SetActive(true);
            playerWallet.CollectMoney(5);
        }
        if (bulletName.Contains("sani") && BiBan)
        {
            DoTayRua -= damage;
            TayBanCircle.GetComponent<Image>().fillAmount -= (damage / 100);
        }
        if (bulletName.Contains("box") && sani.activeInHierarchy == false)
        {
            sani.SetActive(true);
            playerWallet.CollectMoney(5);
        }
        if (bulletName.Contains("gianCach") && gianCach.activeInHierarchy == false)
        {
            gianCach.SetActive(true);
            playerWallet.CollectMoney(7);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    IEnumerator Forcefield(float time)
    {
        forceField.SetActive(true);
        yield return new WaitForSeconds(time);
        forceField.SetActive(false);
        haveJustDamaged = false;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (GameObject.FindGameObjectWithTag("ShieldForPlayer").GetComponent<SpriteRenderer>().enabled == false)
            {
                if (GameObject.FindGameObjectWithTag("MaskForPlayer").GetComponent<SpriteRenderer>().enabled == false)
                {
                    Debug.Log("Cả hai đều không đeo mặt nạ, đáng ra không thể chạm được đến đây nhưng không sao");
                    if (other.gameObject.GetComponent<Enemy>() != null)
                    {
                        other.gameObject.GetComponent<Enemy>().health = -10;
                    }
                }
                else
                {
                    Debug.Log("Có khẩu trang nhưng không có tấm chắn, đáng ra không thể chạm được đến đây nhưng không sao");
                    // other.gameObject.GetComponent<Enemy>().TakeDamage(60);
                    // StartCoroutine(Forcefield(.3f));
                }
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("MaskForPlayer").GetComponent<SpriteRenderer>().enabled == false)
                {
                    Debug.Log("Có tấm chắn nhưng không có mặt nạ");
                    other.gameObject.GetComponent<Enemy>().TakeDamage(130);
                    StartCoroutine(Forcefield(.3f));
                }
                else
                {
                    Debug.Log("Có cả 2 cơ à, gắt nhỉ");
                    other.gameObject.GetComponent<Enemy>().TakeDamage(20);
                    StartCoroutine(Forcefield(.3f));
                }
            }
        }
    }
}
