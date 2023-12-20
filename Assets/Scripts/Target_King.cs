using UnityEngine;
using UnityEngine.AI;

//~~~~~~    Скрипт цели-короля    ~~~~~~//
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
         agent.SetDestination(player.transform.position);  // Преследование игрока
    }

    //~~~~~~    Уменьшение здоровья    ~~~~~~//
    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Death();
    }

    //~~~~~~    Смерть    ~~~~~~//
    void Death()
    {
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonScript>().AddPoints(50);  // Увелечение очков, время существования объекта менее значимо

        //~~~~~~    Система частиц для смерти    ~~~~~~//
        ParticleSystem exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.Play();

        Destroy(exp.gameObject, 1f);  // Уничтожение системы частиц через секунду
        Destroy(agent.gameObject);  // Уничтожение основы

        //~~~~~~    Запуск генераторов мобов рядом со стартовым домом    ~~~~~~//
        for (int i = 0; i < gen.Length; i++)
        {
            TargetsGenerator tg = gen[i].transform.GetComponent<TargetsGenerator>();
            tg.StartGenerate();
        }

        //~~~~~~    Запуск генераторов мобов внутри города, если игрок находится внутри    ~~~~~~//
        if (borderCity.GetComponent<BorderCity>().PlayerInCity())
            for (int i = 0; i < cityGen.Length; i++)
            {
                CityTargetsGenerator tg = cityGen[i].transform.GetComponent<CityTargetsGenerator>();
                tg.StartGenerate();
            }
    }

    //~~~~~~    При попадании пуля уничттожается    ~~~~~~//
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
