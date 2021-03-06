using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    [SerializeField] Sprite stick = default;
    [SerializeField] Sprite stickBig = default;

    public static bool isPerfect = false;
    public static bool isSuccess = false;
    public static bool isMiss = false;

    private float speed;
    private float time170 = 0.70588235f;
    private float totalTime = 0f;
    private float deathTime;

    private float startTime;
    private float endTime;
    private float successTime;
    private float perfectSTime;
    private float perfectETime;

    private Vector2 vector;
    private Rigidbody2D rb;
    private SpriteRenderer sprRend;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprRend = GetComponent<SpriteRenderer>();

        deathTime = time170 * 2500f;
        speed = 6000f / deathTime;

        startTime = deathTime * 0.55f;
        endTime = deathTime * 0.77f;
        successTime = deathTime * 0.65f;
        perfectSTime = deathTime * 0.69f;
        perfectETime = deathTime * 0.73f;

        sprRend.enabled = false;

        //Debug.Log(successTime);
        //Debug.Log(endTime);
    }

    private void Update()
    {
        totalTime += Time.deltaTime * 1000;

        if (totalTime > endTime) {
            Debug.Log("MISS");
            isMiss = true;
            Destroy(gameObject);
        } else if (totalTime >= successTime) {
            sprRend.sprite = stickBig;
        }

        if (Input.GetKeyDown("space")){
            if (totalTime >= perfectSTime && totalTime <= perfectETime) {
                isPerfect = true;
                Debug.Log("NICE!!");
                
            } else if (totalTime >= successTime) {
                isSuccess = true;
                Debug.Log("OK!");
            } else if (totalTime >= startTime) {
                isMiss = true;
                Debug.Log("MISS");
            } else {
                return;
            }

            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        vector.x = -speed;
        rb.velocity = vector;
    }
}
