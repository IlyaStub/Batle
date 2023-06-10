using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector2 dir;
    private Vector2 mouse;
    private Rigidbody2D rb;
    public Camera cum;
    public float chargeStamina = 1f;

    public Image HeathBar;
    public Image StaminaBar1;
    public Image StaminaBar2;
    public static float heath = 100f; // �������� �� � ���������
    public static float stamina = 100f;
    public Vector3 Spawn;
    public static Vector3 SpawnPoint;
    static bool isDead = false;
    private static Transform transform;

    private bool shifting = false;
    private static bool hitting = false;
    void shifts(bool s)
    {
        if (s)
        {
            if (stamina > 0)
            {
                StaminaBar1.enabled = true;
                StaminaBar2.enabled = true;
                speed = 30f;
                stamina -= chargeStamina;          
            }
            else
            {
                stamina = 0;
                speed = 15f;
            }
            StaminaBar1.fillAmount = stamina / 100f;
            StaminaBar2.fillAmount = stamina / 100f;
        }
        else
        {
            speed = 15f;
            if (stamina < 100)
            {
                StaminaBar1.enabled = true;
                StaminaBar2.enabled = true;
                stamina += chargeStamina * 2;
                StaminaBar1.fillAmount = stamina / 100f;
                StaminaBar2.fillAmount = stamina / 100f;
            }
            else
            {
                stamina = 100f;
                StaminaBar1.enabled = false;
                StaminaBar2.enabled = false;
            }
        }
    }


    void Start()
    {
        StaminaBar1.enabled = false;
        StaminaBar2.enabled = false;
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        SpawnPoint = Spawn;
    }

    public static void Damage(float d)
    {
        hitting = true;
        heath -= d;
        if (heath <= 0f) { isDead = true; }
    }

    public void Respawn()
    {
        transform.position = SpawnPoint; 
        heath = 100f; 
        HeathBar.fillAmount = 1;
        isDead = false;
    }

    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");
        mouse = cum.ScreenToWorldPoint(Input.mousePosition);

        //HeathBar.fillAmount = heath / 100f; // ����������� �������� �� �� ������ (���������� � ����� �������)
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            shifting = true;
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            shifting = false;
            
        }
        shifts(shifting);

        if (hitting)
        {
            if (HeathBar.fillAmount * 100 > heath)
            {
                HeathBar.fillAmount -= 0.001f;
            }
            else
            {
                hitting = false;
            }
        }

        if (isDead)
        {
            Respawn();
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
        Vector2 look = mouse - rb.position;
        float ang = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
        rb.rotation = ang;

    }
}
