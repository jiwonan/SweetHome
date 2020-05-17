using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

public class BoxPassword : MonoBehaviour
{
    public GameObject[] BoxgameObjects = new GameObject[4];
    public GameObject[] BoxgameObjects2 = new GameObject[4];
    public GameObject locker;
    public GameObject BoxPwUI;
    public GameObject chestopen;
    public GameObject ptr;
    public int cnt = 0;
    int pw = -1;
    private int[] boxpw = { 1, 2, 3, 4 };
    private int[] boxpw_user = new int[4];

    public void Boxbtninput(GameObject m_CurrentObject)
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
            for (int i = 0; i < 4; ++i)
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
