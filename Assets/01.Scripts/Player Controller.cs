using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

// PlayerController�� �÷��̾� ĳ���ͷμ� Player ���� ������Ʈ�� ������
public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; // ��� �� ����� ����� Ŭ��
    public float jumpForec = 400f; // ���� ��

    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isGrounded = false; // �ٴڿ� ��Ҵ��� ��Ÿ��
    private bool isDead = false; // ��� ����

    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator animator; // ����� �ִϸ����� ������Ʈ
    private AudioSource playerAudio; // ����� ����� �ҽ� ������Ʈ

    private void Start()
    {
        // �ʱ�ȭ
        // ���� ������Ʈ�κ��� ����� ������Ʈ�� ������ ������ �Ҵ�
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead)
        {
            // ����� �Է��� �����ϰ� �����ϴ� ó��
            // ��� �� ó���� �� �̻� �������� �ʰ� ����
            return;
        }

        // ���콺 ���� ��ư�� �������� && �ִ� ���� Ƚ��(2)�� �������� �ʾҴٸ� 
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            // ���� Ƚ�� ����
            jumpCount++;
            // ���� ������ �ӵ��� ���������� ����(0, 0)�� ����
            playerRigidbody.linearVelocity = Vector2.zero;
            // ������ٵ� �������� ���� �ֱ�
            playerRigidbody.AddForce(new Vector2(0, jumpForec));
            // ����� �ҽ� ���
            playerAudio.Play();

        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.linearVelocity.y > 0)
        {
            // ���콺 ���� ��ư���� ���� ���� ���� && �ӵ��� y ���� ������(���� ��� ��)
            // ���� �ӵ��� �������� ����
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity * 0.5f;
        }

        // �ִϸ������� Grounded �Ķ���͸� isGrounded ������ ����
        animator.SetBool("Grounded", isGrounded);
    }
    private void Die()
    {
        // ��� ó��
        // �ִϸ������� Die Ʈ���� �Ķ���͸� ��
        animator.SetTrigger("Die");

        //����� �ҽ��� �Ҵ�� ����� Ŭ���� deathClip���� ����
        playerAudio.clip = deathClip;

        //��� ȿ���� ���
        playerAudio.Play();

        // �ӵ��� ����(0, 0)�� ����
        playerRigidbody.linearVelocity = Vector2.zero;
        //��� ���¸� true�� ����
        isDead = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���� �ݶ��̴��� ���� ��ֹ����� �浹 ����
        if (other.tag == "DEAD" && !isDead)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������� �����ϴ� ó��
        // � �ݶ��̴��� �������, �浹 ǥ���� ������ ���� ������
        if (collision.contacts[0].normal.y > 0.7f)
        {
            // isGround�� true�� �����ϰ�, ���� ���� Ƚ���� 0���� ����
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // �ٴڿ��� ������� �����ϴ� ó��
        // � �ݶ��̴����� ������ ��� isGrounded�� false�� ����
        isGrounded= false;
    }
}