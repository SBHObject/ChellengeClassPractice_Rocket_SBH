using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 0.02f;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;
    
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void ApplyMovement(float inputX)
    {
        Rotate(inputX);
    }

    public void ApplyBoost()
    {
        _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
    }

    private void Rotate(float inputX)
    {
        Vector3 targetVector = new Vector3(-inputX, 0, 0);
        float rotZ = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg - 90;
        Quaternion targetQ = Quaternion.Euler(0, 0, rotZ);
        Quaternion dir = Quaternion.Slerp(transform.rotation, targetQ, ROTATIONSPEED);
        Vector3 rotate = dir.eulerAngles;

        transform.rotation = Quaternion.Euler(0, 0, rotate.z);
    }
}