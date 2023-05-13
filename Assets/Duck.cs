using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Duck : MonoBehaviour
{
    [SerializeField, Range(0, 1)] float moveDuration;
    [SerializeField, Range(0, 1)] float jumpHeight;
    [SerializeField] int leftMoveLimit;
    [SerializeField] int rightMoveLimit;
    [SerializeField] int backMoveLimit;

    public UnityEvent<Vector3> OnJumpEnd;
    public UnityEvent<int> OnGetCoin;
    public UnityEvent OnCarCollution;
    public UnityEvent OnDie;
    private bool isMoveable = false;
    void Update()
    {
        if (isMoveable == false)
            return;

        if (DOTween.IsTweening(transform))
            return;


        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector3.left;
        }

        if (direction == Vector3.zero)
        {
            return;
        }
        Move(direction);

    }
    public void Move(Vector3 direction)
    {
        var targetPosition = transform.position + direction;

        if (targetPosition.x < leftMoveLimit ||
            targetPosition.x > rightMoveLimit ||
            targetPosition.z < backMoveLimit ||
            Tree.AllPositions.Contains(targetPosition))
        {
            targetPosition = transform.position;
        }
        transform.DOJump(targetPosition, jumpHeight, 1, moveDuration).onComplete = BroadCastPositionOnJumpEnd;

        transform.forward = direction;
    }

    public void SetMoveable(bool value)
    {
        isMoveable = value;
    }

    public void UpdateMoveLimit(int horizontalSize, int backLimit)
    {
        leftMoveLimit = -horizontalSize / 2;
        rightMoveLimit = horizontalSize / 2;
        backMoveLimit = backLimit;
    }

    private void BroadCastPositionOnJumpEnd()
    {
        OnJumpEnd.Invoke(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            if (transform.localScale.y == 0.1f)
                return;

            transform.DOScale(new Vector3(2, 0.1f, 2), 0.2f);

            isMoveable = false;
            OnCarCollution.Invoke();
            Invoke("Die", 3);
        }

        else if (other.CompareTag("Coin"))
        {
            var coin = other.GetComponent<Coin>();
            OnGetCoin.Invoke(coin.Value);
            coin.Collected();

        }

        else if (other.CompareTag("Ufo"))
        {
            if (isGameOver) return;
            if (this.transform != other.transform)
            {
                this.transform.SetParent(other.transform);
                Invoke("Die", 3);
            }
        }
    }
    public static bool isGameOver = false;
    private void Die()
    {
        OnDie.Invoke();
        this.transform.SetParent(null);
        isGameOver = true;
    }
}
