using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// The main component of the game, Player.
/// The player can be controlled either by keyboard or by the audio source.
/// </summary>
public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite[] sprites;
    private int spriteIndex;
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;

    private AudioSource audioSource;
    private AudioClip micClip;
    public float micThreshold = 0.1f;
    private bool micInitialized = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        this.audioSource = GetComponent<AudioSource>();

        // Try initializing microphone input
        if (Microphone.devices.Length > 0)
        {
            this.micClip = Microphone.Start(null, true, 10, 44100); // Default microphone
            if (micClip != null)
            {
                audioSource.clip = micClip;
                audioSource.loop = true;

                // Wait until the microphone starts recording
                while (!(Microphone.GetPosition(null) > 0)) { }

                audioSource.Play();
                micInitialized = true;
            }
            else
            {
                Debug.LogError("Failed to initialize microphone!");
            }
        }
        else
        {
            Debug.LogError("Microphone not found!");
        }
    }

    public void OnEnable()
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
            direction = Vector3.up * strength;

            // Add a Game Logger here and move ahead.
            GameLogger.LogAction("Moving the Game.");
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }

        // Adding Microphone Control
        if (micInitialized && CheckMicInput())
        {
            direction = Vector3.up * strength;
            GameLogger.LogAction("Moving the Game via Microphone");
        }

        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    // Checking for the sounds in the microphone input.
    private bool CheckMicInput()
    {
        if (audioSource == null || !audioSource.isPlaying || micClip == null)
        {
            // AudioSource or micClip is not initialized or playing yet.
            return false;
        }

        float[] samples = new float[256];
        audioSource.GetOutputData(samples, 0); // Safely getting the data.

        float sum = 0f;
        for (int i = 0; i < samples.Length; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }

        float avgVolume = sum / samples.Length;

        return avgVolume > micThreshold;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
