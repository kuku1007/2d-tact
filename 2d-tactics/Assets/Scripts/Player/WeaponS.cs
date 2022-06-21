using UnityEngine;

public class WeaponS : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    private float bulletSpeed = 20f;

    void Start()
    {
        CharacterS.OnPlayerShoot += Shoot;
    }

    private void Shoot(float shootAngle, Vector3 shoorDirection) { // TODO: create bullet script 
        GameObject bulletO = Instantiate(bullet, firePoint.position, Quaternion.identity);
        bulletO.transform.eulerAngles = new Vector3(0, 0, shootAngle);
        Destroy(bulletO, 0.5f); // destroy after 0.5s
        Rigidbody2D rb2 = bulletO.GetComponent<Rigidbody2D>();

        rb2.AddForce(shoorDirection * bulletSpeed, ForceMode2D.Impulse);
    }
}
