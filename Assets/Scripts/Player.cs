using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;
    public float width;
    private float speed = 3f; // Player'ýn hýzý

    public GameObject bulletPrefab; // Player mnesnesi
    //private const float minX = -2.15f; // Player'ýn gidebileceði minimum X pozisyonu. (sol)
    //private const float maxX = 2.15f; // Player'ýn gidebileceði maximum X pozisyonu. (Sað)

    private bool isShooting; // Default false'dir
    private float cooldown = 0.3f; // iki mermi atýþý arasýndaki bekleme süresi

    // ObjectPool'a eriþmek için referans oluþturuyoruz.

    [SerializeField] private ObjectPool objectPool = null;

    private void Awake()
    {
        cam = Camera.main;
        width = ((1 / (cam.WorldToViewportPoint(new Vector3(1, 1, 0)).x - .5f) / 2) - 0.25f);
    }

    void Update()
    {
 #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > -width )  // A tuþuna basýldýysa ve player'in pozisyonu minX'den büyükse 
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed); // sola gitmeye devam etsin
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < width) // D tuþuna basýldýysa ve player'in pozisyonu maxX'den küçükse 
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed); // saða gitmeye devam etsin
        }
        if(Input.GetKey(KeyCode.Space) && !isShooting ) // boþluk çubuðuna basýldýysa ve isShooting = false ise
        {
            StartCoroutine(Shoot()); // Mermi oluþturma Fonksiyonu çaðrýlýyor
        }
 #endif
    }

    private IEnumerator Shoot()
    {
        isShooting = true;
        
        //Instantiate(bulletPrefab,transform.position, Quaternion.identity); // Bu scriptin baðlý olduðu objenin pozisyounda(transform.position) bir bulletPrefab(mermi) oluþtur.
        
        // Havuz oluþturduðumuz için yeniden oluþturmuyoruz artýk.
        // Havuzdan çaðýrýyoruz.
        GameObject obj =  objectPool.GetPooledObject();
        //obj.transform.position = gameObject.transform.position;
        obj.transform.position = new Vector2(gameObject.transform.position.x + 0.03f, gameObject.transform.position.y); // Merminin pozisyonunu tam ortalamak için yazýldý.
        
        yield return new WaitForSeconds(cooldown);
        isShooting = false; 

        
        
    }

}
