using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pointer : MonoBehaviour
{
    public float m_Distance = 10.0f; // 길이
    public LineRenderer m_LineRenderer = null; // 레이저
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    // 보물상자 비밀번호
    public GameObject[] BoxgameObjects = new GameObject[4];
    public GameObject[] BoxgameObjects2 = new GameObject[4];
    public GameObject locker;
    public GameObject BoxPwUI;
    public GameObject chestopen;
    int Boxcnt = 0;
    int Boxpwcnt = -1;
    private int[] boxpw = { 0, 3, 1, 3 };
    private int[] boxpw_user = new int[4];

    // 휴대폰 비밀번호
    public GameObject[] PhonegameObjects = new GameObject[4];
    public GameObject[] PhonegameObjects2 = new GameObject[4];
    public GameObject phone;
    public GameObject PhonePwUI;
    public GameObject messageimg;
    int Phonecnt = 0;
    int Phonepwcnt = -1;
    private int[] phonepw = { 0, 6, 0, 6 };
    private int[] phonepw_user = new int[4];
    public GameObject phonelight;

    // 탈출
    private bool enddingKey = false;

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

        BoxPassword boxpassword = m_CurrentObject.GetComponent<BoxPassword>();

        if (m_CurrentObject.name.Equals("locker") && Boxcnt!=2)
        {
            BoxPwUI.SetActive(true);
        }
        Boxbtninput();

        if (m_CurrentObject.name.Equals("Phone") && Phonecnt != 2)
        {
            phonelight.SetActive(true);
            PhonePwUI.SetActive(true);
        }
        Phonebtninput();

        if (m_CurrentObject.name.Equals("Enddoor") && enddingKey)
        {
            toEndding();
        }
    }

    public void Boxbtninput()
    {

        if (Boxcnt == 0)
        {
            for (int i = 0; i <= 3; ++i)
            {
                BoxgameObjects2[i].SetActive(false);
                Boxcnt = 1;
            }
        }

        if (Boxpwcnt > 3)
        {

        }
        else
        {

            if (m_CurrentObject.name.Equals("boxpwbtn1"))
            {
                boxpw_user[++Boxpwcnt] = 1;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn2"))
            {
                boxpw_user[++Boxpwcnt] = 2;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn3"))
            {
                boxpw_user[++Boxpwcnt] = 3;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn4"))
            {
                boxpw_user[++Boxpwcnt] = 4;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn5"))
            {
                boxpw_user[++Boxpwcnt] = 5;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn6"))
            {
                boxpw_user[++Boxpwcnt] = 6;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn7"))
            {
                boxpw_user[++Boxpwcnt] = 7;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn8"))
            {
                boxpw_user[++Boxpwcnt] = 8;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn9"))
            {
                boxpw_user[++Boxpwcnt] = 9;
            }
            else if (m_CurrentObject.name.Equals("boxpwbtn0"))
            {
                boxpw_user[++Boxpwcnt] = 0;
            }
        }

        if (m_CurrentObject.name.Equals("boxcancle"))
        {
            if (Boxpwcnt < 0)
            {
                Boxpwcnt = -1;
            }
            else
            {
                BoxgameObjects[Boxpwcnt].SetActive(true);
                BoxgameObjects2[Boxpwcnt].SetActive(false);
                boxpw_user[Boxpwcnt--] = -1;
            }
        }

        for (int i = 0; i <= Boxpwcnt; ++i)
        {
            BoxgameObjects[i].SetActive(false);
            BoxgameObjects2[i].SetActive(true);
        }

        if (m_CurrentObject.name.Equals("boxfin_01"))
        {
            for (int i = 0; i < 4; ++i)
            {
                if (boxpw[i] != boxpw_user[i])
                {
                    BoxPwUI.SetActive(false);
                    Boxcnt = 0;
                    return;
                }
            }
            chestopen.SetActive(false);
            BoxPwUI.SetActive(false);
            Boxcnt = 2;
            enddingKey = true;
        }
    }

    public void Phonebtninput()
    {



        if (Phonecnt == 0)
        {
            for (int i = 0; i <= 3; ++i)
            {
                PhonegameObjects2[i].SetActive(false);
                Phonecnt = 1;
            }
        }

        if (Phonepwcnt > 3)
        {

        }
        else
        {

            if (m_CurrentObject.name.Equals("phonepwbtn1"))
            {
                phonepw_user[++Phonepwcnt] = 1;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn2"))
            {
                phonepw_user[++Phonepwcnt] = 2;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn3"))
            {
                phonepw_user[++Phonepwcnt] = 3;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn4"))
            {
                phonepw_user[++Phonepwcnt] = 4;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn5"))
            {
                phonepw_user[++Phonepwcnt] = 5;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn6"))
            {
                phonepw_user[++Phonepwcnt] = 6;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn7"))
            {
                phonepw_user[++Phonepwcnt] = 7;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn8"))
            {
                phonepw_user[++Phonepwcnt] = 8;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn9"))
            {
                phonepw_user[++Phonepwcnt] = 9;
            }
            else if (m_CurrentObject.name.Equals("phonepwbtn0"))
            {
                phonepw_user[++Phonepwcnt] = 0;
            }
        }

        if (m_CurrentObject.name.Equals("phonecancle"))
        {
            if (Phonepwcnt < 0)
            {
                Phonepwcnt = -1;
            }
            else
            {
                PhonegameObjects[Phonepwcnt].SetActive(true);
                PhonegameObjects2[Phonepwcnt].SetActive(false);
                phonepw_user[Phonepwcnt--] = -1;
            }
        }

        for (int i = 0; i <= Phonepwcnt; ++i)
        {
            PhonegameObjects[i].SetActive(false);
            PhonegameObjects2[i].SetActive(true);
        }

        if (m_CurrentObject.name.Equals("phonefin_01"))
        {
            for (int i = 0; i < 4; ++i)
            {
                if (phonepw[i] != phonepw_user[i])
                {
                    PhonePwUI.SetActive(false);
                    Phonecnt = 0;
                    return;
                }
            }
            messageimg.SetActive(true);
            PhonePwUI.SetActive(false);
            Phonecnt = 2;
        }
    }

    public GameObject getCurrentObject()
    {
        return m_CurrentObject;
    }

    public void toEndding()
    {
        SceneManager.LoadScene("RankingScene");
    }


}
