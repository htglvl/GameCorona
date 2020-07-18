using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scopeSize, scopeZoomSpeed;
    private float scopeNormalSize;
    Vector3 CameraPos = new Vector3(0, 0, -10);
    private Transform PlayerPos;
    public bool scope;
    [Range(0.0f, 1.0f)]

    public float TiLeGiuaCameraVsPlayerVaCursorVsplayer;
    bool DoneLateStart;

    private void Start()
    {
        DoneLateStart = false;
        StartCoroutine(LateStart(.5f));

    }
    void FixedUpdate()
    {
        if (DoneLateStart)
        {
            if (scope)
            {
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, scopeSize, scopeZoomSpeed * Time.deltaTime);
            }
            else
            {
                gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, scopeNormalSize, scopeZoomSpeed * Time.deltaTime);
            }
            if (PlayerPos != null)
            {
                Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.transform.position.z);
                CameraPos.x = PlayerPos.position.x + (mousePosition.x - PlayerPos.position.x) * TiLeGiuaCameraVsPlayerVaCursorVsplayer;
                CameraPos.y = PlayerPos.position.y + (mousePosition.y - PlayerPos.position.y) * TiLeGiuaCameraVsPlayerVaCursorVsplayer;
                Camera.main.transform.position = CameraPos;
            }
        }
    }
    IEnumerator LateStart(float second)
    {
        yield return new WaitForSeconds(second);
        scopeNormalSize = gameObject.GetComponent<Camera>().orthographicSize;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
        DoneLateStart = true;
    }
}
