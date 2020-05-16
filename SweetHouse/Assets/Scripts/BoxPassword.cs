using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BoxPassword : MonoBehaviour
{
    public GameObject[] gameObjects = new GameObject[4];
    public int pw = -1;
    public GameObject BoxPwUI;
    public GameObject ptr;

    private GameObject m_CurrentObject;

    private void Boxbtninput()
    {
        Pointer p = ptr.GetComponent<Pointer>();
        // m_CurrentObject = p.Getm_CurrentObject();

        if (m_CurrentObject.name.Equals("boxpwbtn1"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn2"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn3"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn4"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn5"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn6"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn7"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn8"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn9"))
        {
            pw++;
        }
        else if (m_CurrentObject.name.Equals("boxpwbtn0"))
        {
            pw++;
        }

        if (pw > 3)
        {
            pw = 3;
        }

        if (m_CurrentObject.name.Equals("cancle"))
        {
            if (pw < 0)
            {
                pw = -1;
            }
            else
            {
                gameObjects[pw].SetActive(true);
                pw--;
            }
        }

        for (int i = 0; i <= pw; ++i)
        {
            gameObjects[i].SetActive(false);
        }

        if (m_CurrentObject.name.Equals("fin_01"))
        {
            BoxPwUI.SetActive(false);
        }
    }
}
