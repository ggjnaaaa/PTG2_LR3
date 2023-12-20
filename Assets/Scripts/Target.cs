using System;
using UnityEngine;
using UnityEngine.AI;

//~~~~~~    ������ ��� ��������� � ������� �����    ~~~~~~//
public class Target : MonoBehaviour
{
    public ParticleSystem explosion;  // ������� ������ ��� ������
    GameObject player;  // �����
    NavMeshAgent agent;  // ������ ��������
    GameObject targetAnimEx;  // ������ ��� �������� ���������
    float hp;  // ��������
    float active;  // ����� �� �������� ���� �� ����� ������������ �������� ���������
    float scale;  // ������ �� ������ ��������
    bool death;  // ���� �� ������
    bool firstActive;  // ������ �� update ����� ����������� ��������

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = transform.GetComponentInParent<NavMeshAgent>();
        hp = 30;
        active = Time.time + 4;
        scale = transform.localScale.x;
        death = false;
        firstActive = true;

        transform.localScale = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (active < Time.time)
        {
            if (firstActive)
                SetActiveComponents();
            if (!death)
                agent.SetDestination(player.transform.position);
        }
    }

    //~~~~~~    ��������� ������� � ��������� ���������    ~~~~~~//
    public void GetAnim(GameObject go) => targetAnimEx = go;

    //~~~~~~    �������������� ���� ����� ��������    ~~~~~~//
    private void SetActiveComponents()
    {
        Destroy(targetAnimEx);  // ����������� ������� � ���������
        agent.enabled = true;  // ��������� ������ ������
        transform.GetComponent<Animator>().enabled = true;  // ��������� ��������� ����
        transform.localScale = new Vector3(scale, scale, scale);  // �������������� ��������
    }

    //~~~~~~    ����    ~~~~~~//
    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Death();
    }

    //~~~~~~    ������    ~~~~~~//
    void Death()
    {
        //~~~~~~    ������� �����    ~~~~~~//
        int scalePoints = transform.localScale.x == 2 ? 4 : 2;  // �� ������� ������ ����� ������ ������
        int timePoints = (60 - Convert.ToInt32(Time.time)) / 10;  // ��� ������ ���� ������ - ��� ������ �� ���� ����� �����
        int typePoints = transform.tag == "target" ? 3 : 5;  // �� �������, ��������������� � ������ ����� ������ �����
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonScript>().AddPoints(typePoints * scalePoints + timePoints);  // ����� ������������� ������� ����� �������

        ParticleSystem exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.Play();

        Destroy(exp.gameObject, 1f);
        Destroy(agent.gameObject);

        death = true;
    }

    //~~~~~~    ����������� ���� � ����� � ��������� � ����    ~~~~~~//
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
