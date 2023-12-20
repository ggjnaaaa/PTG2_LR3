using UnityEngine;

//~~~~~~    Скрипт ружья    ~~~~~~//
public class Gun : MonoBehaviour
{
    //~~~~~~    Стрельба    ~~~~~~//
    float damage;
    float range;
    float fireRate;
    float nextShot;

    public UnityEngine.Camera cam;
    public ParticleSystem flash;

    public ParticleSystem onHit;
    public GameObject bullet;
    public GameObject bulletGenerator;
    float bulletSpeed;

    private void Start()
    {
        damage = 10f;
        range = 1000f;

        fireRate = 5f;
        nextShot = 0f;

        bulletSpeed = 120f;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextShot)
        {
            nextShot = Time.time + 1 / fireRate;
            Shot();
        }
    }

    //~~~~~~    Встрел    ~~~~~~//
    void Shot()
    {
        flash.Play();

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward,out hit, range))
        {
            //~~~~~~    Если попал по любой цели, уменьшается hp у цели    ~~~~~~//
            if (hit.transform.CompareTag("target") || hit.transform.CompareTag("city target"))
            {
                Target t = hit.transform.GetComponent<Target>();
                t.Hit(damage);
            }
            else if (hit.transform.CompareTag("King Target"))
            {
                Target_King t = hit.transform.GetComponent<Target_King>();
                t.Hit(damage);
            }

            //~~~~~~    Создание пули и придание ей движения через rigidbody    ~~~~~~//
            GameObject newBullet = Instantiate(bullet, bulletGenerator.transform.position, bulletGenerator.transform.rotation);
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            rb.velocity = rb.transform.forward * bulletSpeed;
            Destroy(newBullet, 15f);

            //~~~~~~    Система частиц для попадания пули    ~~~~~~//
            ParticleSystem hitEffect = Instantiate(onHit, hit.point, Quaternion.LookRotation(hit.normal));
            hitEffect.Play();

            Destroy(hitEffect.gameObject, 1f);  // Отключение эффекта через секунду
        }
    }
}
