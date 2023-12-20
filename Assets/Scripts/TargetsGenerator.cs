using UnityEngine;

//~~~~~~    Скрипт генератора у дома    ~~~~~~//
public class TargetsGenerator : MonoBehaviour
{
    //~~~~~~    Основной объект для генерации и его копия с анимацией появления    ~~~~~~//
    public GameObject target;
    public GameObject targetAnim;

    int targetsOnScene;  // Количество объектов на сцене
    int maxTargets;  // Максимальное количество объектов на сцене
    float nextTarget;  // Во сколько должен появиться следующий объект
    int secondsForNewTarget;  // Через сколько появится новый объект
    bool genIsStarted;  // Заработал ли генератор

    void Start()
    {
        //~~~~~~    Разница между городским и обычным генераторами - время на появление моба, количество мобов и материал    ~~~~~~//
        targetsOnScene = 0;
        maxTargets = 6;
        nextTarget = 0f;
        secondsForNewTarget = 8;
        genIsStarted = false;
    }

    void FixedUpdate()
    {
        targetsOnScene = GameObject.FindGameObjectsWithTag("target").Length;
        if (genIsStarted) StartGenerate();
    }

    //~~~~~~    Начало создания    ~~~~~~//
    public void StartGenerate()
    {
        genIsStarted = true;
        if (targetsOnScene <= maxTargets && nextTarget <= Time.time)
        {
            nextTarget = Time.time + secondsForNewTarget;
            System.Random rnd = new();
            AddTarget(rnd.Next(2, 4));
        }
    }

    //~~~~~~    Создание объекта    ~~~~~~//
    public void AddTarget(int scale)
    {
        //~~~~~~    Создание объекта с анимацией и включение его анимации    ~~~~~~//
        GameObject targetAnimEx = Instantiate(targetAnim, transform.position, transform.rotation);
        Transform child = targetAnimEx.transform.Find("Target");
        Animator anim = child.GetComponent<Animator>();
        anim.SetInteger("exit", scale);

        //~~~~~~    Создание моба    ~~~~~~//
        Vector3 vec = transform.position;
        vec.x -= 10;  // Отодвигает на 10 т.к. анимация сдвигает объект на 10
        GameObject newTarget = Instantiate(target, vec, transform.rotation);
        Transform go = newTarget.transform.Find("Sphere");;
        go.localScale = new Vector3(scale, scale, scale);
        go.GetComponent<Target>().GetAnim(targetAnimEx);

        Destroy(newTarget, 60f);  // Удаление объекта через минуту
    }
}
