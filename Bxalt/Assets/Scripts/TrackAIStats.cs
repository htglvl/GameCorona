using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackAIStats : MonoBehaviour
{
    AIBrain[] AIBrains;
    public float SoAI, SonguoiDcBaoVe, SonguoiCanTiemPhong, SoNguoiBiBenh;
    public TextMeshProUGUI SoAIText, SonguoiDcBaoVeText, SonguoiCanTiemPhongText, SoNguoiBiBenhText;
    bool DoneLateStart;
    // Start is called before the first frame update
    void Start()
    {
        DoneLateStart = false;
        StartCoroutine(LateStart(.5f));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (DoneLateStart)
        {
            SoAIText.text = cal("SoAI").ToString();
            SonguoiDcBaoVeText.text = cal("SonguoiDcBaoVe").ToString();
            SonguoiCanTiemPhongText.text = cal("SonguoiCanTiemPhong").ToString();
            SoNguoiBiBenhText.text = cal("SoNguoiBiBenh").ToString();

        }
    }
    public float cal(string goiGi)
    {
        SoAI = 0;
        SonguoiDcBaoVe = 0;
        SonguoiCanTiemPhong = 0;
        SoNguoiBiBenh = 0;

        SoAI = AIBrains.Length;
        foreach (AIBrain AI in AIBrains)
        {
            if ((AI.MedicHat.activeSelf && AI.Shield.activeSelf) || (!AI.MedicHat.activeSelf && (AI.Mask.activeSelf || AI.Shield.activeSelf)))
            {
                SonguoiDcBaoVe++;
            }
            if (AI.NeedTiemPhong)
            {
                SonguoiCanTiemPhong++;
            }
            if (AI.BiBenh)
            {
                SoNguoiBiBenh++;
            }
        }
        if (goiGi == "SoAI")
        {
            return SoAI;
        }
        else if (goiGi == "SonguoiDcBaoVe")
        {
            return SonguoiDcBaoVe;
        }
        else if (goiGi == "SonguoiCanTiemPhong")
        {
            return SonguoiCanTiemPhong;
        }
        else if (goiGi == "SoNguoiBiBenh")
        {
            return SoNguoiBiBenh;
        }
        else
        {
            return 0f;
        }
    }
    IEnumerator LateStart(float second)
    {
        yield return new WaitForSeconds(second);
        AIBrains = Transform.FindObjectsOfType<AIBrain>();
        DoneLateStart = true;
    }
}
