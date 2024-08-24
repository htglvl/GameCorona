using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DaynightCycle : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.Rendering.Universal.Light2D Sun;
    public float minIntensity = .4f, maxIntensity = 1.4f, Intensity, timeMultiplyer = 1;

    public float hour, day, month, year;
    void Start()
    {
        hour = DateTime.Now.Hour;
        day = DateTime.Now.Day;
        month = DateTime.Now.Month;
        year = DateTime.Now.Year;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TinhNgay();
        //Debug.Log(hour + "Gio, " + day + "/" + month + "/" + year);

        if (hour >= 0 && hour < 12) //SangDan
        {

            Sun.intensity = Mathf.Lerp(minIntensity, maxIntensity, hour / 12f);//hour/ 12 de cho nam o 0 va 1
        }
        else            //ToiDan
        {
            Sun.intensity = Mathf.Lerp(maxIntensity, minIntensity, (hour - 12) / 12f);
        }

        //IntensityByHour(hour);
    }
    IEnumerator startNightCycle()
    {
        yield return null;
    }

    IEnumerator startDayCycle()
    {
        yield return null;
    }
    void IntensityByHour(float Hour)
    {
        if (Hour >= 0 && Hour < 12) //SangDan
        {
            Sun.intensity = Mathf.Lerp(minIntensity, maxIntensity, 12f);
        }
        else            //ToiDan
        {
            Sun.intensity = Mathf.Lerp(maxIntensity, minIntensity, 12f);
        }
    }
    void TinhNgay()
    {
        hour += Time.deltaTime * timeMultiplyer;
        if (hour >= 24)
        {
            hour = 0;
            day++;
        }
        if (month == 2)
        {
            if (year % 4 == 0) // nam nhuan
            {
                if (day >= 29)
                {
                    day = 1;
                    month++;
                }
            }
            else
            {
                if (day >= 28)
                {
                    day = 1;
                    month++;
                }
            }
        }
        else
        {
            if (month % 2 == 0) //so chan
            {
                if (day >= 30)
                {
                    day = 1;
                    month++;
                }
            }
            else
            {
                if (day >= 31)
                {
                    month++;
                    day = 1;
                }
            }
        }
        if (month >= 12)
        {
            month = 1;
            year++;
        }
    }
}
