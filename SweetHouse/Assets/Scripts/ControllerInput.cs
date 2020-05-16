using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{

    public int speedForward = 12; // 전진 속도
    public int speedSide = 6; // 옆걸음 속도

    private Transform tr;
    private float dirX = 0;
    private float dirZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    // 플레이어 이동
    void MovePlayer()
    {
        dirX = 0; // 좌우 이동(왼쪽: -1, 오른쪽: 1)
        dirZ = 0; // 전진 후진(후진: -1, 전진: 1)
        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            Vector2 coord = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad,
                                         OVRInput.Controller.RTrackedRemote);

            var absX = Mathf.Abs(coord.x);
            var absY = Mathf.Abs(coord.y);
            if (absX > absY)
            {
                // Right
                if (coord.x > 0)
                    dirX = +1;
                //Left
                else
                    dirX = -1;
            }
            else
            {
                // Up
                if (coord.y > 0)
                    dirZ = +1;
                // Down
                else
                    dirZ = -1;
            }
        }
        // 이동 방향 설정 후 이동
        Vector3 moveDir = new Vector3(dirX * speedSide, 0, dirZ * speedForward);
        transform.Translate(moveDir * Time.smoothDeltaTime);

    }
}
