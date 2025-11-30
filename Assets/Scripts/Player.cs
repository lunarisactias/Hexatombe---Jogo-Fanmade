using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 12f;
    public float maxSpeed = 30f;
    public float deadZone = 0.03f;
    public bool autoCalibrateOnStart = true;

    Rigidbody2D rb;
    Vector2 calib;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (autoCalibrateOnStart)
            CalibrateNow();
    }

    void FixedUpdate()
    {
        Vector2 tilt = ReadTiltXY() - calib;

        if (tilt.magnitude < deadZone)
        {
            tilt = Vector2.zero;
        }

        Vector3 force = new Vector3(tilt.x, tilt.y, 0f) * speed;
        rb.AddForce(force, ForceMode2D.Force);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && touch.tapCount == 2)
            {
                CalibrateNow();
            }
        }
    }

    Vector2 ReadTiltXY()
    {
        Vector3 a = Input.acceleration;
        return new Vector2(a.x, a.y);
    }

    public void CalibrateNow() => calib = ReadTiltXY();

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger with: " + other.gameObject.name);

        if (other.CompareTag("Sacrificio"))
        {
            GameManager.Instance.sacrificios += 1;
            Destroy(other.gameObject);
            GameManager.Instance.OpenFinalDoor();
        }

        if (other.CompareTag("Coroa"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
