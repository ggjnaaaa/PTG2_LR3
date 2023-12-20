using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

//~~~~~~    Скрипт границ города (находится в переходе)    ~~~~~~//
public class BorderCity : MonoBehaviour
{
    GameObject[] gen;
    public int exit;

    private void Start()
    {
        gen = GameObject.FindGameObjectsWithTag("city generator");
        exit = 0;
    }

    //~~~~~~    Если игрок зашел в город когда король мёртв запускаются генераторы внутри города    ~~~~~~//
    private void OnTriggerEnter(Collider other)
    {
        if (exit % 2 == 0
            && GameObject.FindGameObjectsWithTag("King Target").Length == 0
            && other.tag == "Player")
                for (int i = 0; i < gen.Length; i++)
                    gen[i].transform.GetComponent<CityTargetsGenerator>().StartGenerate();

        exit++;
    }

    //~~~~~~    Находится ли игрок в городе    ~~~~~~//
    public bool PlayerInCity() => exit % 2 != 0;
}
