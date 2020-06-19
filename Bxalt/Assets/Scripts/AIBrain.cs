using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AIBrain : MonoBehaviour
{
    public GameObject Mask, Shield, MedicHat, Canvas;
    public bool BiBenh, VirusRuaBangSoapTrueSaniFalse, NeedTiemPhong, daxetnghiem;
    public int BenhTiemPhong;
    public float phantramnguoiBiBenh = 0.5f, phanTramNguoiCoMask = 0.5f, phanTramNguoiCoShield = 0.5f, phanTramNguoiCoMedicHat = 0.5f, phantramnguoicanTiemPhong = 0.5f, PercentSickCleanBySoapOtherUseSani = 0.5f, TiemBaoNhieu = 100f, DoTayRua = 100f;
    public string[] TenBenhTiemPhong;
    public TextMeshProUGUI TextTiemPhong, CoBiVirusKo;
    // Start is called before the first frame update
    void Start()
    {
        Mask.SetActive(Random.value < phanTramNguoiCoMask ? true : false);
        MedicHat.SetActive(Random.value < phanTramNguoiCoMedicHat ? true : false);
        Shield.SetActive(Random.value < phanTramNguoiCoShield ? true : false);
        if (Mask.activeInHierarchy)
        {
            Debug.Log("Co KT");
        }
        else
        {
            Debug.Log("Ko KT");
        }
        if (Shield.activeInHierarchy)
        {
            Debug.Log("Co Mat na");
        }
        else
        {
            Debug.Log("Ko Mat na");
        }
        if (MedicHat.activeInHierarchy)
        {
            Debug.Log("Co Mu");
        }
        else
        {
            Debug.Log("Ko Mu");
        }
        if (Random.value < phantramnguoiBiBenh)
        {
            Debug.Log("bibenh");
            CoBiVirusKo.text = "Đã nhiễm virus: ";
            BiBenh = true;
            if (Random.value < PercentSickCleanBySoapOtherUseSani)
            {
                Debug.Log("useSoap");
                CoBiVirusKo.text += "Chỉ diệt được bằng xà phòng";
                VirusRuaBangSoapTrueSaniFalse = true;
            }
            else
            {
                Debug.Log("useSani");
                CoBiVirusKo.text += "Chỉ diệt được bằng nước rửa tay";
                VirusRuaBangSoapTrueSaniFalse = false;
            }
        }
        else
        {
            Debug.Log("ko benh");
            BiBenh = false;
            CoBiVirusKo.text = "Không nhiễm virus";
        }
        if (Random.value < phantramnguoicanTiemPhong)
        {
            Debug.Log("canTiemPhong");
            TextTiemPhong.text = "Cần tiêm phòng ";
            NeedTiemPhong = true;
            BenhTiemPhong = Random.Range(1, 9);
        }
        else
        {
            Debug.Log("koCanTiemPhong");
            TextTiemPhong.text = "Không cần tiêm phòng ";
            NeedTiemPhong = false;
        }
        for (int i = 1; i < TenBenhTiemPhong.Length; i++)
        {
            if (i == BenhTiemPhong)
            {
                TextTiemPhong.text += TenBenhTiemPhong[i];
            }
        }
        TextTiemPhong.gameObject.SetActive(false);
        TextTiemPhong.gameObject.SetActive(false);
        Canvas.SetActive(false);
    }
    private void Update()
    {
        Canvas.transform.LookAt(Canvas.transform.position + Vector3.forward);

        if (daxetnghiem && Input.GetKey(KeyCode.Space))
        {
            Canvas.SetActive(true);
        }
        else
        {
            Canvas.SetActive(false);
        }
        if (DoTayRua <= 0)
        {
            BiBenh = false;
            CoBiVirusKo.text = "Đã hết nhiễm virus";
        }
        if (TiemBaoNhieu <= 0)
        {
            NeedTiemPhong = false;
            TextTiemPhong.text = "Không cần tiêm phòng ";
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
        }
        if (bulletName.Contains("S") && Shield.activeSelf == false)
        {
            Shield.SetActive(true);
        }
        if (bulletName.Contains("sy") && NeedTiemPhong)
        {
            for (int i = 0; i < bulletName.Length; i++)
            {
                if (char.IsDigit(bulletName[i]) && bulletName[i].ToString() == BenhTiemPhong.ToString())
                {
                    TiemBaoNhieu -= damage;
                }
            }
        }
        if (bulletName.Contains("soap") && VirusRuaBangSoapTrueSaniFalse)
        {
            DoTayRua -= damage;
        }
        if (bulletName.Contains("sani") && VirusRuaBangSoapTrueSaniFalse == false)
        {
            DoTayRua -= damage;
        }
    }
}
