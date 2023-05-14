using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{   //public means that our object method can be called from anywhere,whether outside or inside the class itself.
    //private variables and methods can only be accessed from within class.

    [Header("Stats")]
    public int currentHP;
    public int maxHP;

    [Header("Movement")]
    public float moveSpeed=4; 
    public int jumpForce=6;

    [Header("Camera")]

    public float lookSensitivity;            //how fast does the camera rotate around
    public float maxLookX;                   //highest x rotation of the camera
    public float minLookX;                   //lowest down we can look
    private float rotX;                      //current x rotation of the camera
    
    private Camera cam;
    private Rigidbody rig;
    private Weapon weapon;
    public CharacterController controller;

    public Image image;
    public float flashSpeed;
    private Coroutine fadeAway;

    private AudioSource audioSource; //hurt sound
    public AudioClip PlayerHurtSFX;

    private void Awake()
    {
        //get the components
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();

        audioSource = GetComponent<AudioSource>();
        //disable cursor maus playde ekranda kalsýn diye
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Start()
    {
        //initialize the UI
        UIManager.instance.UpdateHealthBar(currentHP, maxHP);
        UIManager.instance.UpdateScoreText(0);
        UIManager.instance.UpdateAmmoText(weapon.currentAmmo, weapon.maxAmmo);
    }
    public void Update()
    {
        //dont do anything if the game pauses 
        if (GameManager.instance.gamePaused == true)
        {
            return;
        }
        Move();
        if (Input.GetButtonDown("Jump")) // if condition true block of code be executed if the codition is
            TryJump();
        if (Input.GetButton("Fire1"))
        {
            if (weapon.CanShoot())
            {
                weapon.Shoot();
            }
                
        }
        if(Cursor.lockState==CursorLockMode.Locked)
        CameraLook();
    }
    void Move()
    {
        // -1 left and down
        // +1 up and right 
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;
        controller.Move(dir);  // çok hýzlý hareket etti olmadý. yukarýdaki dir çözümü
        
        dir.y = rig.velocity.y;
        rig.velocity = dir;

    }

    void CameraLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        // clamps the given value between the given minimum float and maximum float values.Returns the given value if it is within the min and max range.
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);

        cam.transform.localRotation = Quaternion.Euler(-rotX, 0, 0); //y = 0 because we want to apply it to the player // - çünkü maus ters calýsýyodu
        transform.eulerAngles+= Vector3.up* y;    // represents rotation in word space
    }

    void TryJump()
    {
        // ray user(zýplayýnca) ve ground arasýndaki aralýk 
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.1f))
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //AddForce= add an instant force impluse to the rigidbody ,using its mass
        }
    }
    public void TakeDamage(int damage)
    {
        audioSource.PlayOneShot(PlayerHurtSFX);
        currentHP -= damage;
        UIManager.instance.UpdateHealthBar(currentHP, maxHP);
        flashDamage();
        if (currentHP <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        GameManager.instance.LoseGame();
    }

    public void GiveHealth(int amountToGive)
    {
        //ctr +r+r toplu deðiþme 
        currentHP = Mathf.Clamp(currentHP + amountToGive, 0, maxHP);

        UIManager.instance.UpdateHealthBar(currentHP, maxHP);
    }

    public void GiveAmmo(int amountToGive)
    {
        weapon.currentAmmo = Mathf.Clamp(weapon.currentAmmo + amountToGive, 0, weapon.maxAmmo);
        UIManager.instance.UpdateAmmoText(weapon.currentAmmo,weapon.maxAmmo);
    }

    public void flashDamage()
    {
        if(fadeAway != null)
        {
            StopCoroutine(fadeAway);
        }
        image.enabled = true;
        image.color = Color.white;
        fadeAway = StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float a = 1.0f;

        while (a > 0.0f)
        {
            //decrease a during time by the rate of flash speed =means how much we will be decrease it per frame
            a -= (1.0f / flashSpeed) * Time.deltaTime;
            image.color = new Color(1.0f, 1.0f, 1.0f,a);

            yield return null;
        }
        image.enabled = false;
    }
}
