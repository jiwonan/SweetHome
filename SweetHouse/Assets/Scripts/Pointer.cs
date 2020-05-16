using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f; // 길이
    public LineRenderer m_LineRenderer = null; // 레이저
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    // 보물상자 비밀번호에 쓰이는 변수들
    public GameObject[] BoxgameObjects = new GameObject[4];
    public GameObject[] BoxgameObjects2 = new GameObject[4];
    public GameObject locker;
    public GameObject BoxPwUI;
    public GameObject chestopen;
    int cnt = 0;
    int pw = -1;
    private int[] boxpw = { 1, 2, 3, 4 };
    private int[] boxpw_user = new int[4];

    private Transform m_CurrentOrigin = null;
    private GameObject m_CurrentObject = null;

    private void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchpadDown += ProcessTouchpadDown;
    }

    private void Strat()
    {
        SetLineColor();
    }

    private void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchpadDown -= ProcessTouchpadDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        m_CurrentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, m_CurrentObject);
    }

    private Vector3 UpdateLine()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(m_EverythingMask); // 충돌된 객체

        // Default end
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_Distance);

        // Check hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
    {

        // Set origin of pointer
        m_CurrentOrigin = controllerObject.transform;

        // Is the laser visible?
        if (controller == OVRInput.Controller.Touchpad)
        {
            m_LineRenderer.enabled = false;
        }
        else
        {
            m_LineRenderer.enabled = true;
        }

    }

    private GameObject UpdatePointerStatus()
    {
        // Create ray

        RaycastHit hit = CreateRaycast(m_InteractableMask);

        // Check hit
        if (hit.collider)
            return hit.collider.gameObject;

        // Return
        return null;
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_Distance, layer);

        return hit;
    }

    private void SetLineColor()
    {
        if (!m_LineRenderer)
            return;

        Color endColor = Color.white;
        endColor.a = 0.0f;

        m_LineRenderer.endColor = endColor;
    }

    private void ProcessTouchpadDown()
    {
        if (!m_CurrentObject)
            return;

        if (m_CurrentObject.name.Equals("locker") && cnt!=2)
        {
            BoxPwUI.SetActive(true);
        }

        Boxbtninput();
    }
    private void Boxbtninput()
    {

        if (cnt == 0)
        {
            for (int i = 0; i <= 3; ++i)
            {
                BoxgameObjects2[i].SetActive(false);
                cnt = 1;
            }
        }

        if (pw > 3)
        {
            
        }
        else
        {

            if (m_CurrentObject.name.Equals("boxpwbtn1"))
            {
                boxpw_user[++pw] = 1;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn2"))
            {
                boxpw_user[++pw] = 2;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn3"))
            {
                boxpw_user[++pw] = 3;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn4"))
            {
                boxpw_user[++pw] = 4;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn5"))
            {
                boxpw_user[++pw] = 5;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn6"))
            {
                boxpw_user[++pw] = 6;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn7"))
            {
                boxpw_user[++pw] = 7;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn8"))
            {
                boxpw_user[++pw] = 8;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn9"))
            {
                boxpw_user[++pw] = 9;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn0"))
            {
                boxpw_user[++pw] = 0;
            }
        }

        if (m_CurrentObject.name.Equals("cancle"))
        {
            if (pw < 0)
            {
                pw = -1;
            }
            else
            {
                BoxgameObjects[pw].SetActive(true);
                BoxgameObjects2[pw].SetActive(false);
                boxpw_user[pw--] = -1;
            }
        }

        for (int i = 0; i <= pw; ++i)
        {
            BoxgameObjects[i].SetActive(false);
            BoxgameObjects2[i].SetActive(true);
        }

        if (m_CurrentObject.name.Equals("fin_01"))
        {
            for(int i = 0; i < 4; ++i)
            {
                if (boxpw[i] != boxpw_user[i])
                {
                    BoxPwUI.SetActive(false);
                    cnt = 0;
                    return;
                }
                chestopen.SetActive(false);
            }
            BoxPwUI.SetActive(false);
            cnt = 2;
        }
    }
}
