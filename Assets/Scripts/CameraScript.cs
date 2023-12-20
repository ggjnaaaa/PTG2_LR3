using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//~~~~~~    ������ ������    ~~~~~~//
public class Camera : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public GameObject pic;

    float xSens = 300f;  // ���������������� ���� �� ��� X
    float ySens = 300f;  // ���������������� ���� �� ��� Y
    static bool cursorLock;

    Quaternion center;  // ��������� ������� ������

    void Start()
    {
        center = cam.localRotation;
        cursorLock = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorLock)
        {
            //~~~~~~    �������� ������� � �������� ��������    ~~~~~~//
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pic.SetActive(true);

            //~~~~~~    ������� �� Y    ~~~~~~//
            float mouseY = Input.GetAxis("Mouse Y") * ySens * Time.deltaTime;
            Quaternion yRot = cam.localRotation * Quaternion.AngleAxis(mouseY, -Vector3.right);
            if (Quaternion.Angle(center, yRot) < 75f)
                cam.localRotation = yRot;

            //~~~~~~    ������� �� X    ~~~~~~//
            float mouseX = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
            Quaternion xRot = player.localRotation * Quaternion.AngleAxis(mouseX, Vector3.up);
            player.localRotation = xRot;

            if (Input.GetMouseButtonDown(1))
                cursorLock = false;
        }
        else
        {
            //~~~~~~    ������ �� �������� ���� ������ �� ������������    ~~~~~~//
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pic.SetActive(false);
            if (Input.GetMouseButtonDown(1))
                cursorLock = true;
        }
    }
}
