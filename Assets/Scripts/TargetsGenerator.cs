using UnityEngine;

//~~~~~~    ������ ���������� � ����    ~~~~~~//
public class TargetsGenerator : MonoBehaviour
{
    //~~~~~~    �������� ������ ��� ��������� � ��� ����� � ��������� ���������    ~~~~~~//
    public GameObject target;
    public GameObject targetAnim;

    int targetsOnScene;  // ���������� �������� �� �����
    int maxTargets;  // ������������ ���������� �������� �� �����
    float nextTarget;  // �� ������� ������ ��������� ��������� ������
    int secondsForNewTarget;  // ����� ������� �������� ����� ������
    bool genIsStarted;  // ��������� �� ���������

    void Start()
    {
        //~~~~~~    ������� ����� ��������� � ������� ������������ - ����� �� ��������� ����, ���������� ����� � ��������    ~~~~~~//
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

    //~~~~~~    ������ ��������    ~~~~~~//
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

    //~~~~~~    �������� �������    ~~~~~~//
    public void AddTarget(int scale)
    {
        //~~~~~~    �������� ������� � ��������� � ��������� ��� ��������    ~~~~~~//
        GameObject targetAnimEx = Instantiate(targetAnim, transform.position, transform.rotation);
        Transform child = targetAnimEx.transform.Find("Target");
        Animator anim = child.GetComponent<Animator>();
        anim.SetInteger("exit", scale);

        //~~~~~~    �������� ����    ~~~~~~//
        Vector3 vec = transform.position;
        vec.x -= 10;  // ���������� �� 10 �.�. �������� �������� ������ �� 10
        GameObject newTarget = Instantiate(target, vec, transform.rotation);
        Transform go = newTarget.transform.Find("Sphere");;
        go.localScale = new Vector3(scale, scale, scale);
        go.GetComponent<Target>().GetAnim(targetAnimEx);

        Destroy(newTarget, 60f);  // �������� ������� ����� ������
    }
}
