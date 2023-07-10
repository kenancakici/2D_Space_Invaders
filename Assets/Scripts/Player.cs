using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject bulletPrefab; // Player mnesnesi
    private const float minX = -2.15f; // Player'�n gidebilece�i minimum X pozisyonu. (sol)
    private const float maxX = 2.15f; // Player'�n gidebilece�i maximum X pozisyonu. (Sa�)
    private float speed = 3f; // Player'�n h�z�
    private bool isShooting; // Default false'dir
    private float cooldown = 0.3f; // iki mermi at��� aras�ndaki bekleme s�resi

    // ObjectPool'a eri�mek i�in referans olu�turuyoruz.

    [SerializeField] private ObjectPool objectPool = null;

    void Update()
    {
 #if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > minX )  // A tu�una bas�ld�ysa ve player'in pozisyonu minX'den b�y�kse 
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed); // sola gitmeye devam etsin
        }
        if (Input.GetKey(KeyCode.D) && transform.position.x < maxX) // D tu�una bas�ld�ysa ve player'in pozisyonu maxX'den k���kse 
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed); // sa�a gitmeye devam etsin
        }
        if(Input.GetKey(KeyCode.Space) && !isShooting ) // bo�luk �ubu�una bas�ld�ysa ve isShooting = false ise
        {
            StartCoroutine(Shoot()); // Mermi olu�turma Fonksiyonu �a�r�l�yor
        }
 #endif
    }

    private IEnumerator Shoot()
    {
        isShooting = true;

        //Instantiate(bulletPrefab,transform.position, Quaternion.identity); // Bu scriptin ba�l� oldu�u objenin pozisyounda(transform.position) bir bulletPrefab(mermi) olu�tur.
        
        // Havuz olu�turdu�umuz i�in yeniden olu�turmuyoruz art�k.
        // Havuzdan �a��r�yoruz.
        GameObject obj =  objectPool.GetPooedObject();
        //obj.transform.position = gameObject.transform.position;
        obj.transform.position = new Vector2(gameObject.transform.position.x + 0.03f, gameObject.transform.position.y); // Merminin pozisyonunu tam ortalamak i�in yaz�ld�.
        
        yield return new WaitForSeconds(cooldown);
        isShooting = false; 


        
    }

}
