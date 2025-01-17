using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;


public class gunsettings : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float firerate = 15f;

    public int MaxAmmo = 12;
    private int curAmmo;
    //public float swapTime = 1f;
    //private bool isreloading = false;
    //public Animator animator;
    public GameObject hitmark;

    public Camera fpscamera;
    public GameObject muzzleflash;
    public GameObject impact;
    public GameObject firePoint;
    public AudioClip shootAud;
    public AudioClip emptyAud;
    public swap currentWeapon;
    public PlayerInventory inventory;
    private Animator anim;

    private float Fr = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        
        
        currentWeapon = GameObject.FindGameObjectWithTag("Holster").GetComponent<swap>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        anim = GameObject.FindGameObjectWithTag("Holster").GetComponent<Animator>();
        if (hitmark != null)
        {
            hitmark.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isreloading)
            return;
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Swap());
            return;
        }
        */
        if (Input.GetButtonDown("Fire1") && Time.time >= Fr && anim.GetBool("CanShoot"))
        {
            /*
            Fr = Time.time + 1f / firerate;
            Shoot();
            */
            
            if (currentWeapon.Active_Weapon == 0 && inventory.arAmmo > 0)
            {
                inventory.usingArAmmo();
                Fr = Time.time + 1f / firerate;
                Shoot();
            }
            else if(currentWeapon.Active_Weapon == 0 && inventory.arAmmo == 0)
            {
                AudioSource.PlayClipAtPoint(emptyAud, transform.position);
            }
            
            if (currentWeapon.Active_Weapon == 1 && inventory.pistolAmmo > 0)
            {
                inventory.usingPistolAmmo();
                Fr = Time.time + 1f / firerate;
                Shoot();
            }
            else if (currentWeapon.Active_Weapon == 1 && inventory.pistolAmmo == 0)
            {
                AudioSource.PlayClipAtPoint(emptyAud, transform.position);
            }
            
            
        }
        if (Input.GetKeyDown(KeyCode.R) && currentWeapon.Active_Weapon == 0)
        {
            Reload();
            inventory.ReloadArAmmo();
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentWeapon.Active_Weapon == 1)
        {
            Reload();
            inventory.ReloadPistolAmmo();
        }
    }
    void Shoot ()
        {
        

        RaycastHit hit;
        int layerMask = ~(1 << LayerMask.NameToLayer("DoorTrigger"));
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, Range, layerMask))
            {
                Debug.Log(hit.transform.name);

                

                Vector3 flashDirection = firePoint.transform.forward;
                muzzleflash.transform.position = firePoint.transform.position;
                muzzleflash.transform.rotation = Quaternion.LookRotation(flashDirection);

                GameObject fireParticle = Instantiate(muzzleflash, muzzleflash.transform.position, muzzleflash.transform.rotation);
                GameObject hitParticle = Instantiate(impact, hit.point, Quaternion.identity);
                AudioSource.PlayClipAtPoint(shootAud, transform.position);

                if (hit.transform.CompareTag("Enemy"))
                {
                EnemyStatistics enemyStats = hit.transform.GetComponent<EnemyStatistics>();

                print("Enemy was hit");
                if (hit.collider.CompareTag("Head"))
                {
                    print("Headshot");
                    enemyStats.health -= 10;

                }

                if (hit.collider.CompareTag("Torso"))
                {
                    print("Shot in torso");
                    enemyStats.health -= 5;
                }

                else
                {
                    print("Shot in arms or legs");
                    enemyStats.health -= 2;
                }

                }

                Hitmarker();
                Destroy(fireParticle, 1);
                Destroy(hitParticle, 1);


            }
        }

    /*
    IEnumerator Swap()
    {
        //isreloading = true;
        animator.SetBool("Swap", true);
        
        yield return new WaitForSeconds(swapTime);
        animator.SetBool("Swap", false);
        //isreloading = false;

    }
    */

    void Reload()
    {
        anim.SetTrigger("RifleReload");
    }

    void Hitmarker()
    
        {
            hitmark.SetActive(true);
            Invoke("DeactivateHitmarker", 0.5f); // Use Invoke to call the deactivation method after 0.3 seconds
        }

        // New method to deactivate hitma
        void DeactivateHitmarker()
        {
            hitmark.SetActive(false);
        }
    
}
