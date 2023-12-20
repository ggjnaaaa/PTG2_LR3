using UnityEngine;
using UnityEngine.AI;

//~~~~~~    ������ ����-������    ~~~~~~//
public class Target_King: MonoBehaviour
{
    public ParticleSystem explosion;
    public GameObject player;
    public NavMeshAgent agent;
    public GameObject borderCity;
    GameObject[] gen;
    GameObject[] cityGen;
    float hp;

    void Start()
    {
        gen = GameObject.FindGameObjectsWithTag("generator");
        cityGen = GameObject.FindGameObjectsWithTag("city generator");
        hp = 50;
    }

    void Update()
    {
         agent.SetDestination(player.transform.position);  // ������������� ������
    }

    //~~~~~~    ���������� ��������    ~~~~~~//
    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Death();
    }

    //~~~~~~    ������    ~~~~~~//
    void Death()
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonScript>().AddPoints(50);  // ���������� �����, ����� ������������� ������� ����� �������

        //~~~~~~    ������� ������ ��� ������    ~~~~~~//
        ParticleSystem exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.Play();

        Destroy(exp.gameObject, 1f);  // ����������� ������� ������ ����� �������
        Destroy(agent.gameObject);  // ����������� ������

        //~~~~~~    ������ ����������� ����� ����� �� ��������� �����    ~~~~~~//
        for (int i = 0; i < gen.Length; i++)
        {
            TargetsGenerator tg = gen[i].transform.GetComponent<TargetsGenerator>();
            tg.StartGenerate();
        }

        //~~~~~~    ������ ����������� ����� ������ ������, ���� ����� ��������� ������    ~~~~~~//
        if (borderCity.GetComponent<BorderCity>().PlayerInCity())
            for (int i = 0; i < cityGen.Length; i++)
            {
                CityTargetsGenerator tg = cityGen[i].transform.GetComponent<CityTargetsGenerator>();
                tg.StartGenerate();
            }
    }

    //~~~~~~    ��� ��������� ���� �������������    ~~~~~~//
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
