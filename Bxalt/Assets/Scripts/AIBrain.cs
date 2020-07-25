using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIBrain : MonoBehaviour
{
    public GameObject Mask, Shield, MedicHat, Canvas, panel, shieldCircle, vacxinCircle, virusCircle, forceField;
    money playerWallet;
    public bool BiBenh, VirusRuaBangSoapTrueSaniFalse, NeedTiemPhong, daxetnghiem, haveJustDamaged = false, BiSot;
    public int BenhTiemPhong;
    public float phantramnguoiBiBenh = 0.5f, phanTramNguoiBiSot = 0.3f, phanTramNguoiCoMask = 0.5f, phanTramNguoiCoShield = 0.5f, phanTramNguoiCoMedicHat = 0.5f, phantramnguoicanTiemPhong = 0.5f, PercentSickCleanBySoapOtherUseSani = 0.5f, TiemBaoNhieu = 100f, DoTayRua = 100f, detectRadius, TimeMacBenh = 60.0f;
    public string[] TenBenhTiemPhong;
    public TextMeshProUGUI TextTiemPhong, CoBiVirusKo, CoBiSotKhong, DTR, TBN;

    public bool paidForVacxinOnce = true;
    public bool paidForVirusOnce = true;
    private float Pri_TimeMacBenh = 0.0f;
    public bool DenkhuCachLy;

    // Start is called before the first frame update
    void Start()
    {
        Pri_TimeMacBenh = 0;
        playerWallet = Transform.FindObjectOfType<money>();
        forceField.SetActive(false);
        Mask.SetActive(Random.value < phanTramNguoiCoMask ? true : false);
        MedicHat.SetActive(Random.value < phanTramNguoiCoMedicHat ? true : false);
        Shield.SetActive(Random.value < phanTramNguoiCoShield ? true : false);
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
        if (Random.value < phantramnguoiBiBenh)
        {
            //Debug.Log("bibenh");
            CoBiVirusKo.text = "Đã nhiễm virus: ";
            BiBenh = true;
            if (Random.value < PercentSickCleanBySoapOtherUseSani)
            {
                //Debug.Log("useSoap");
                CoBiVirusKo.text += "Diệt bằng xà phòng";
                VirusRuaBangSoapTrueSaniFalse = true;
            }
            else
            {
                //Debug.Log("useSani");
                CoBiVirusKo.text += "Diệt bằng nước rửa tay";
                VirusRuaBangSoapTrueSaniFalse = false;
            }
        }
        else
        {
            //Debug.Log("ko benh");
            BiBenh = false;
            CoBiVirusKo.text = "Không nhiễm virus";
        }
        if (Random.value < phantramnguoicanTiemPhong)
        {
            //Debug.Log("canTiemPhong");
            TextTiemPhong.text = "Cần tiêm phòng ";
            NeedTiemPhong = true;
            BenhTiemPhong = Random.Range(1, 9);
        }
        else
        {
            //Debug.Log("koCanTiemPhong");
            TextTiemPhong.text = "Không cần tiêm phòng ";
            NeedTiemPhong = false;
        }
        for (int i = 0; i < TenBenhTiemPhong.Length; i++)
        {
            if (i + 1 == BenhTiemPhong)
            {
                TextTiemPhong.text += TenBenhTiemPhong[i];
            }
        }
        TextTiemPhong.gameObject.SetActive(false);
        TextTiemPhong.gameObject.SetActive(false);
        panel.SetActive(false);
    }
    private void Update()
    {
        Collider2D[] otherCivillan = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        for (int i = 0; i < otherCivillan.Length; i++)
        {
            if (otherCivillan != null && (otherCivillan[i].gameObject.tag == "Enemy" || otherCivillan[i].gameObject.tag == "Player"))
            {
                if (otherCivillan[i].GetComponent<AIBrain>())
                {
                    if (otherCivillan[i].GetComponent<AIBrain>().BiBenh && !BiBenh && !shieldCircle.activeInHierarchy)
                    {
                        BiBenh = true;
                        CoBiVirusKo.text = "Đã nhiễm virus: ";
                        DoTayRua = 100;
                        paidForVirusOnce = true;
                        virusCircle.GetComponent<Image>().fillAmount = 1;
                        if (VirusRuaBangSoapTrueSaniFalse)
                        {
                            CoBiVirusKo.text += "Diệt bằng xà phòng";
                        }
                        else
                        {
                            CoBiVirusKo.text += "Diệt bằng nước rửa tay";
                        }
                    }
                }
                if (otherCivillan[i].gameObject.tag == "Player" && BiBenh)
                {
                    if (GameObject.FindGameObjectWithTag("ShieldForPlayer").GetComponent<SpriteRenderer>().enabled == false)
                    {
                        if (!haveJustDamaged)
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
        shieldCircle.transform.LookAt(shieldCircle.transform.position + Vector3.forward);
        virusCircle.transform.LookAt(virusCircle.transform.position + Vector3.forward);
        vacxinCircle.transform.LookAt(vacxinCircle.transform.position + Vector3.forward);
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
            if ((MedicHat.activeSelf && Shield.activeSelf) || (!MedicHat.activeSelf && (Mask.activeSelf || Shield.activeSelf)))
            {
                shieldCircle.SetActive(true);
            }
            else
            {
                shieldCircle.SetActive(false);
            }
            if (BiBenh)
            {
                virusCircle.SetActive(true);
            }
            else
            {
                virusCircle.SetActive(false);
            }
            if (NeedTiemPhong)
            {
                vacxinCircle.SetActive(true);
            }
            else
            {
                vacxinCircle.SetActive(false);
            }
        }
        else
        {
            shieldCircle.SetActive(false);
            virusCircle.SetActive(false);
            vacxinCircle.SetActive(false);
        }

        if (DoTayRua <= 0)
        {
            DoTayRua = 0;
            BiBenh = false;
            if (paidForVirusOnce)
            {
                playerWallet.CollectMoney(12);
                paidForVirusOnce = false;
                GetComponent<randomDestinationAI>().quarantine = true;
            }
            CoBiVirusKo.text = "Đã hết nhiễm virus";
            virusCircle.GetComponent<Image>().fillAmount = 0;

        }
        if (TiemBaoNhieu <= 0)
        {
            TiemBaoNhieu = 0;
            NeedTiemPhong = false;
            if (paidForVacxinOnce)
            {
                playerWallet.CollectMoney(8);
                paidForVacxinOnce = false;
            }
            TextTiemPhong.text = "Không cần tiêm phòng";
            vacxinCircle.GetComponent<Image>().fillAmount = 0;
        }

        if (BiSot == true)
        {
            Pri_TimeMacBenh += Time.deltaTime;
        }
        if (Pri_TimeMacBenh >= TimeMacBenh)
        {
            Pri_TimeMacBenh = 0f;
            BiBenh = true;
            CoBiVirusKo.text = "Đã nhiễm virus: ";
            DoTayRua = 100;
            paidForVirusOnce = true;
            virusCircle.GetComponent<Image>().fillAmount = 1;
            if (VirusRuaBangSoapTrueSaniFalse)
            {
                CoBiVirusKo.text += "Diệt bằng xà phòng";
            }
            else
            {
                CoBiVirusKo.text += "Diệt bằng nước rửa tay";
            }
        }
        if (DenkhuCachLy == true)
        {
            BiSot = false;
            CoBiSotKhong.text = "Không sốt";
            Pri_TimeMacBenh = 0;
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
                TextTiemPhong.gameObject.SetActive(true);
                CoBiVirusKo.gameObject.SetActive(true);
            }
        }
        if (bulletName.Contains("kt") && Mask.activeSelf == false)
        {
            Mask.SetActive(true);
            playerWallet.CollectMoney(3);
        }
        if (bulletName.Contains("S") && Shield.activeSelf == false)
        {
            Shield.SetActive(true);
            playerWallet.CollectMoney(5);
        }
        if (bulletName.Contains("sy") && NeedTiemPhong)
        {
            for (int i = 0; i < bulletName.Length; i++)
            {
                if (char.IsDigit(bulletName[i]) && bulletName[i].ToString() == (BenhTiemPhong).ToString())
                {
                    TiemBaoNhieu -= damage;
                    vacxinCircle.GetComponent<Image>().fillAmount -= (damage / 100);
                }
            }
        }
        if (bulletName.Contains("soap") && VirusRuaBangSoapTrueSaniFalse && BiBenh)
        {
            DoTayRua -= damage;
            virusCircle.GetComponent<Image>().fillAmount -= (damage / 100);
        }
        if (bulletName.Contains("sani") && VirusRuaBangSoapTrueSaniFalse == false && BiBenh)
        {
            DoTayRua -= damage;
            virusCircle.GetComponent<Image>().fillAmount -= (damage / 100);
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
