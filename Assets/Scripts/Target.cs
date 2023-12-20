using System;
using UnityEngine;
using UnityEngine.AI;

//~~~~~~    Скрипт для городских и обычных мобов    ~~~~~~//
public class Target : MonoBehaviour
{
    public ParticleSystem explosion;  // Система частиц для смерти
    GameObject player;  // Игрок
    NavMeshAgent agent;  // Навмеш родителя
    GameObject targetAnimEx;  // Объект для анимации генерации
    float hp;  // Здоровье
    float active;  // Время от создания моба до конца проигрывания анимации генерации
    float scale;  // Размер на момент создания
    bool death;  // Умер ли объект
    bool firstActive;  // Первый ли update после проигывания анимации

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

    //~~~~~~    Получение объекта с анимацией появления    ~~~~~~//
    public void GetAnim(GameObject go) => targetAnimEx = go;

    //~~~~~~    Восстановление моба после анимации    ~~~~~~//
    private void SetActiveComponents()
    {
        Destroy(targetAnimEx);  // Уничтожение объекта с анимацией
        agent.enabled = true;  // Включение навмеш агента
        transform.GetComponent<Animator>().enabled = true;  // Включение аниматора моба
        transform.localScale = new Vector3(scale, scale, scale);  // Восстановление размеров
    }

    //~~~~~~    Урон    ~~~~~~//
    public void Hit(float damage)
    {
        hp -= damage;
        if (hp <= 0) Death();
    }

    //~~~~~~    Смерть    ~~~~~~//
    void Death()
    {
        //~~~~~~    Подсчёт очков    ~~~~~~//
        int scalePoints = transform.localScale.x == 2 ? 4 : 2;  // За меньший объект даётся больше баллов
        int timePoints = (60 - Convert.ToInt32(Time.time)) / 10;  // Чем меньше живёт объект - тем больше за него даётся очков
        int typePoints = transform.tag == "target" ? 3 : 5;  // За объекты, сгенерированные в городе даётся больше очков
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<ButtonScript>().AddPoints(typePoints * scalePoints + timePoints);  // Время существования объекта менее значимо

        ParticleSystem exp = Instantiate(explosion, transform.position, transform.rotation);
        exp.Play();

        Destroy(exp.gameObject, 1f);
        Destroy(agent.gameObject);

        death = true;
    }

    //~~~~~~    Уничтожение пули с лучае её попадания в цель    ~~~~~~//
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
