using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//~~~~~~    Скрипт камеры    ~~~~~~//
public class Camera : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public GameObject pic;

    float xSens = 300f;  // Чувствительность мыши по оси X
    float ySens = 300f;  // Чувствительность мыши по оси Y
    static bool cursorLock;

    Quaternion center;  // Начальный поворот камеры

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
            //~~~~~~    Фиксация курсора и открытие картинки    ~~~~~~//
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pic.SetActive(true);

            //~~~~~~    Поворот по Y    ~~~~~~//
            float mouseY = Input.GetAxis("Mouse Y") * ySens * Time.deltaTime;
            Quaternion yRot = cam.localRotation * Quaternion.AngleAxis(mouseY, -Vector3.right);
            if (Quaternion.Angle(center, yRot) < 75f)
                cam.localRotation = yRot;

            //~~~~~~    Поворот по X    ~~~~~~//
            float mouseX = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
            Quaternion xRot = player.localRotation * Quaternion.AngleAxis(mouseX, Vector3.up);
            player.localRotation = xRot;

            if (Input.GetMouseButtonDown(1))
                cursorLock = false;
        }
        else
        {
            //~~~~~~    Камера не движется если курсор не зафиксирован    ~~~~~~//
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pic.SetActive(false);
            if (Input.GetMouseButtonDown(1))
                cursorLock = true;
        }
    }
}
