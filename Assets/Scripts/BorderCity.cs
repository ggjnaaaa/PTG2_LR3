using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

//~~~~~~    Скрипт границ города    ~~~~~~//
public class BorderCity : MonoBehaviour
{
    GameObject[] gen;
    bool enter;

    private void Start()
    {
        gen = GameObject.FindGameObjectsWithTag("city generator");
        enter = false;
    }

    //~~~~~~    Если игрок зашел в город когда король мёртв запускаются генераторы внутри города    ~~~~~~//
    private void OnTriggerEnter(Collider other)
    {
        enter = true;
        if (GameObject.FindGameObjectsWithTag("King Target").Length == 0
            && other.tag == "Player"
            && gen != null)
        {
            for (int i = 0; i < gen.Length; i++)
                gen[i].transform.GetComponent<CityTargetsGenerator>().StartGenerate();
            gen = null;
        }
    }

    private void OnTriggerExit(Collider other) => enter = false;

    //~~~~~~    Находится ли игрок в городе    ~~~~~~//
    public bool PlayerInCity() => enter;
}
