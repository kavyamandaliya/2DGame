using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private Collider2D coll;
    public AudioSource fxSound;
    public RectTransform gameWin;
    public RectTransform gameLose;
    [SerializeField]private LayerMask ground;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int hearts = 0;
    [SerializeField] private Text heartText;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        fxSound = GetComponent<AudioSource>();
        heartText.text = hearts.ToString();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Tilemap") || col.gameObject.name.Equals("Tilemap1"))
            this.transform.parent = col.transform;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Tilemap") || col.gameObject.name.Equals("Tilemap1"))
            this.transform.parent = null;
    }

    private void onGameWin()
    {

        gameWin.gameObject.SetActive(true);

    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        float hDirection = Input.GetAxis("Horizontal");
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (rb.position.y < -10)
        {
            print("Died");
            onGameLose();
            onGameOverEnd();
        }
        

    }

    private void onGameLose()
    {
        gameLose.gameObject.SetActive(true);
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Heart")
        {
            //Destroy(collision.gameObject);
            hearts++;
            heartText.text = hearts.ToString();
            fxSound.Play();

        }
        if(collision.tag == "FullEnd")
        {
            onGameWin();
            print("You win");
            onGameEnd();
        }
        collision.gameObject.SetActive(false);
        yield return new WaitForSeconds(4);
        collision.gameObject.SetActive(true);
    }

    private void onGameEnd()
    {
        print("in game end");
        StartCoroutine(EnableGameoverPanel());

    }

    private IEnumerator EnableGameoverPanel()
    {
        yield return new WaitForSeconds(1);
        gameWin.gameObject.SetActive(false);
        rb.position = new Vector2(3, 4);
        hearts = 0;
        heartText.text = hearts.ToString();
    }

    private void onGameOverEnd()
    {
        print("in game end");
        StartCoroutine(EnableGameoverEndPanel());

    }

    private IEnumerator EnableGameoverEndPanel()
    {
        yield return new WaitForSeconds(1);
        gameLose.gameObject.SetActive(false);
        rb.position = new Vector2(3, 4);
        hearts = 0;
        heartText.text = hearts.ToString();
    }
}
