using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Projectile laserPrefab;
    private bool _laserActive;
    public float speed = 3.0f;
    private Camera mainCamera;
    private float minX,
        maxX;

    void Start()
    {
        mainCamera = Camera.main;

        CalculateBounds();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);
        transform.position += movement;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            transform.position.y,
            transform.position.z
        );
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void CalculateBounds()
    {
        float halfPlayerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfScreenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        minX = -halfScreenWidth + halfPlayerWidth;
        maxX = halfScreenWidth - halfPlayerWidth;
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            float playerHeight = GetComponent<SpriteRenderer>().bounds.size.y;

            float yOffset = playerHeight / 2.0f;

            Vector3 shootPosition = transform.position + new Vector3(0, yOffset, 0);

            Projectile projectile = Instantiate(
                this.laserPrefab,
                shootPosition,
                Quaternion.identity
            );
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    
}
