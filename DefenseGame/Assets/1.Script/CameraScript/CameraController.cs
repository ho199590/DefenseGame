using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region 변수
    private bool doMovement = true;

    public float panSpeed = 45f;
    public float panBorderThickness = 10f;

    private float scrollSpeed = 5f;

    private float minY = 20f;
    private float maxY = 80f;
    private float maxZ = 100;
    private float minZ = -40;
    private float maxX = 115;
    private float minX = -25;


    #endregion
    #region 함수
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement; 
        }

        if (!doMovement){return;}

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            if(transform.position.z < maxZ)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            if(transform.position.x > minX)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            if (transform.position.x < maxX)
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            if(transform.position.z > minZ)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;

        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
    #endregion
}
