using UnityEngine;

//~~~~~~    ������ ������    ~~~~~~//
public class Player : MonoBehaviour
{
    //~~~~~~    ������    ~~~~~~//
    float walkSpeed = 500f;
    float runSpeed = 700f;
    float mSpeed;

    Rigidbody rb;
    UnityEngine.Camera cam;

    //~~~~~~    ���� ������ ������    ~~~~~~//
    float baseFov;
    float sprintFov;

    //~~~~~~    ������    ~~~~~~//
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

        float xMove = Input.GetAxisRaw("Vertical");  // �������� ������� ������ w, s, up, down
        float zMove = -Input.GetAxisRaw("Horizontal");  // �������� ������� ������ a, d, left, right

        mSpeed = sprint && xMove > 0 ? runSpeed : walkSpeed;
        cam.fieldOfView = sprint && xMove > 0 ? Mathf.Lerp(cam.fieldOfView, baseFov * sprintFov, Time.deltaTime) : Mathf.Lerp(cam.fieldOfView, baseFov, Time.deltaTime);

        Vector3 dir = new(xMove, 0, zMove);  // ����������� �������� � ��� xz
        dir.Normalize();  // ������������ �������
        Vector3 vec = transform.TransformDirection(dir) * mSpeed * Time.fixedDeltaTime;
        vec.y = rb.velocity.y;
        rb.velocity = vec;
    }
}
