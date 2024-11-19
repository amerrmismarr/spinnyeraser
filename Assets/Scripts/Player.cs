using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 180f;

    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    private int spriteIndex;

    public AudioClip clickSound;
    public AudioClip hittingSound;

    public AudioClip scoreSound;

    // Assign the audio clip in the Inspector
    public AudioSource audioSource;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        // Get the AudioSource component from the GameObject
        audioSource = GetComponent<AudioSource>();

        // Assign the AudioClip to the AudioSource
        //audioSource.clip = clickSound;
    }


    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            audioSource.PlayOneShot(clickSound);
            Debug.Log("Hello World");
            direction = Vector3.up * strength;

        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        if (spriteIndex < sprites.Length && spriteIndex >= 0)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    private bool canScore = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            audioSource.PlayOneShot(hittingSound);
            GameManager.Instance.GameOver();
        }
        else if (other.gameObject.CompareTag("Scoring") && canScore)
        {
            GameManager.Instance.IncreaseScore();
            audioSource.PlayOneShot(scoreSound);
            canScore = false;
            StartCoroutine(ResetScoreCooldown());
        }
    }

    IEnumerator ResetScoreCooldown()
    {
        yield return new WaitForSeconds(0.75f); // Adjust this delay as needed
        canScore = true;
    }

}
