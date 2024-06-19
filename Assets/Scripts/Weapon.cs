using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
  
[RequireComponent(typeof(LineRenderer))]
public class Weapon : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunRange = 50f;
    public float fireRate = 0.003f;
    public float laserDuration = 0.05f;
    public ParticleSystem muzzleFlash;
    public AudioSource audioSource;
 
    LineRenderer laserLine;
    float fireTimer;
 
    void Awake()
    {
        laserLine = GetComponent<LineRenderer>();
        muzzleFlash.Stop();
        audioSource.Stop();
    }

    void Start()
    {
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");
        playerCamera = cameraObject.GetComponent<Camera>();
    }
 
    void Update()
    {
        fireTimer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && fireTimer > fireRate)
        {
            muzzleFlash.Play();
            audioSource.Play();
            fireTimer = 0;
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            Debug.Log("Ray Origin: " + rayOrigin);
            Debug.DrawRay(rayOrigin, playerCamera.transform.forward * gunRange, Color.green); // Debug raycast

            RaycastHit hit;
            if(Physics.Raycast(rayOrigin, playerCamera.transform.forward, out hit, gunRange))
            {
                laserLine.SetPosition(1, hit.point);
                GameObject targetObject = hit.collider.gameObject;
                if (targetObject.CompareTag("Target")) 
                {
                    Destroy(targetObject);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (playerCamera.transform.forward * gunRange));
            }
            StartCoroutine(ShootLaser());
        }
    }
 
    IEnumerator ShootLaser()
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }
}