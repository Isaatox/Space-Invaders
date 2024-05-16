using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    public Invader[] prefabs = new Invader[3];
    public Projectile missilePrefab;
    public int rows = 3;
    public int columns = 5;
    public float speed = 0.5f;
    public int ammountKilled { get; private set; }
    public int ammountAlive => totalInvaders - ammountKilled;
    public int totalInvaders;
    private Vector3 _direction = Vector2.right;

    private void Start()
    {
        InvokeRepeating(nameof(Attack), 0.5f, 0.5f);
    }

    private void Awake()
    {
        totalInvaders = rows * columns;

        float invaderSpacing = 0.5f;
        float width = 1.0f * (columns - 1);
        float height = 1.0f * (rows - 1);
        Vector2 center = new Vector2(-width / 8.0f, -height / 20.0f);
        Vector3 rowPosition = new Vector3(center.x, center.y + (rows * invaderSpacing), 0);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                Invader invader = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform);
                int rank = r;
                invader.killed += () => InvaderKilled(rank);
                Vector3 position = rowPosition;
                position.x += c * invaderSpacing;
                invader.transform.position = position;
            }
            rowPosition.y -= invaderSpacing;
        }
    }

    private void Update()
    {
        float step = _direction.x * speed * Time.deltaTime;
        transform.position += new Vector3(step, 0, 0);

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        float playerHeight = FindObjectOfType<PlayerBehaviour>().transform.position.y;
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.5f))
            {
                _direction = Vector3.left;
                AdvanceRow();
            }
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.5f))
            {
                _direction = Vector3.right;
                AdvanceRow();
            }
            if (invader.position.y <= playerHeight + 0.25f)
            {
                SceneManager.LoadScene("EndScene");
            }
        }
        if (ammountAlive == 0)
        {
            SceneManager.LoadScene("StartGameScene");
        }
    }

    private void AdvanceRow()
    {
        Vector3 position = transform.position;
        position.y -= 0.25f;
        transform.position = position;
    }

    private void Attack()
    {
        foreach (Transform invader in transform)
        {
            if (!invader.gameObject)
            {
                continue;
            }
            if (Random.value < (0.1f / ammountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled(int invaderRank)
    {
        ammountKilled++;
        GameManager.instance.IncreaseScore(invaderRank);
    }
}
