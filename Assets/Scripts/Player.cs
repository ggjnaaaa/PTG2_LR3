using UnityEngine;

//~~~~~~    Скрипт игрока    ~~~~~~//
public class Player : MonoBehaviour
{
    //~~~~~~    Ходьба    ~~~~~~//
    float walkSpeed = 500f;
    float runSpeed = 700f;
    float mSpeed;

    Rigidbody rb;
    UnityEngine.Camera cam;

    //~~~~~~    Угол обзора камеры    ~~~~~~//
    float baseFov;
    float sprintFov;

    //~~~~~~    Прыжок    ~~~~~~//
    float jumpForce;
    public LayerMask ground;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("camera").GetComponent<UnityEngine.Camera>();
        baseFov = cam.fieldOfView;
        sprintFov = 1.25f;
        jumpForce = 20f;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && Physics.Raycast(transform.position, Vector3.down, 2f, ground))
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        float xMove = Input.GetAxisRaw("Vertical");  // Проверка нажатия кнопок w, s, up, down
        float zMove = -Input.GetAxisRaw("Horizontal");  // Проверка нажатия кнопок a, d, left, right

        mSpeed = sprint && xMove > 0 ? runSpeed : walkSpeed;
        cam.fieldOfView = sprint && xMove > 0 ? Mathf.Lerp(cam.fieldOfView, baseFov * sprintFov, Time.deltaTime) : Mathf.Lerp(cam.fieldOfView, baseFov, Time.deltaTime);

        Vector3 dir = new(xMove, 0, zMove);  // Направление движения в оси xz
        dir.Normalize();  // Нормализация вектора
        Vector3 vec = transform.TransformDirection(dir) * mSpeed * Time.fixedDeltaTime;
        vec.y = rb.velocity.y;
        rb.velocity = vec;
    }
}
