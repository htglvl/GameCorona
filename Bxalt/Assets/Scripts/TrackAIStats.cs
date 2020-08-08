using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrackAIStats : MonoBehaviour
{
    AIBrain[] AIBrains;
    public float SoAI, SonguoiDcBaoVe, SoNguoiBiBenh, SoNguoiBiBan;
    public TextMeshProUGUI SoAIText, SonguoiDcBaoVeText, SoNguoiBiBenhText, SoNguoiBiBanText;
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
            SoNguoiBiBenhText.text = cal("SoNguoiBiBenh").ToString();
            SoNguoiBiBanText.text = cal("SoNguoiBiBan").ToString();
        }
    }
    public float cal(string goiGi)
    {
        SoAI = 0;
        SonguoiDcBaoVe = 0;
        SoNguoiBiBenh = 0;
        SoNguoiBiBan = 0;

        SoAI = AIBrains.Length;
        foreach (AIBrain AI in AIBrains)
        {
            if ((AI.Shield.activeInHierarchy || AI.Mask.activeInHierarchy) && AI.sani.activeInHierarchy && AI.gianCach.activeInHierarchy)
            {
                SonguoiDcBaoVe++;
            }
            if (AI.BiBenh)
            {
                SoNguoiBiBenh++;
            }
            if (AI.BiBan)
            {
                SoNguoiBiBan++;
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
        else if (goiGi == "SoNguoiBiBenh")
        {
            return SoNguoiBiBenh;
        }
        else if (goiGi == "SoNguoiBiBan")
        {
            return SoNguoiBiBan;
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
