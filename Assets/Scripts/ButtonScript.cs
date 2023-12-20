using System;
using UnityEngine;
using UnityEngine.UI;

//~~~~~~    Скрипт канваса    ~~~~~~//
public class ButtonScript : MonoBehaviour
{
    public Text score;  // Текст с количеством очков

    public void Quit() => Application.Quit();  // Выход из игры
    public void AddPoints(int points) => score.text = Convert.ToString(Convert.ToInt32(score.text) + points);  // Увеличение количества очков
}
