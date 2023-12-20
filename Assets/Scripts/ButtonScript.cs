using System;
using UnityEngine;
using UnityEngine.UI;

//~~~~~~    ������ �������    ~~~~~~//
public class ButtonScript : MonoBehaviour
{
    public Text score;  // ����� � ����������� �����

    public void Quit() => Application.Quit();  // ����� �� ����
    public void AddPoints(int points) => score.text = Convert.ToString(Convert.ToInt32(score.text) + points);  // ���������� ���������� �����
}
