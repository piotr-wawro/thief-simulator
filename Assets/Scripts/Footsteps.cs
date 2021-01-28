using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public new Camera camera;

    public float normalFootstepDelay = 0.4f;
    public float runFootstepDelay = 0.3f;

    private AudioSource audioSource;
    private AudioClip[] woodFootsteps;
    private AudioClip[] carpetFootsteps;
    private AudioClip[] tilesFootsteps;
    private new CapsuleCollider collider;

    private bool isOnCarpet = false;
    private bool isOnTiles = false;

    private float nextFootstep = 0f;

    private void Awake()
    {
        woodFootsteps = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Wood");
        carpetFootsteps = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Carpet");
        tilesFootsteps = Resources.LoadAll<AudioClip>("Sounds/Footsteps/Tiles");

        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (IsMovingOnGround())
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                CheckGroundUnderPlayer();

                AudioClip clip;
                if (isOnCarpet)
                {
                    clip = carpetFootsteps[Random.Range(0, carpetFootsteps.Length)];
                }
                else if (isOnTiles)
                {
                    clip = tilesFootsteps[Random.Range(0, tilesFootsteps.Length)];
                }
                else
                {
                    clip = woodFootsteps[Random.Range(0, woodFootsteps.Length)];
                }
                audioSource.PlayOneShot(clip);
                if (IsRunning())
                {
                    nextFootstep += runFootstepDelay;
                }
                else
                {
                    nextFootstep += normalFootstepDelay;
                }
            }
        }
    }


    private bool IsGrounded() => Physics.CheckCapsule(collider.bounds.min, new Vector3(collider.bounds.min.x, collider.bounds.min.y - 0.01f, collider.bounds.min.z), 0.1f);

    private bool IsMovingOnGround() => (IsGrounded() && (Input.GetKey(KeyCode.A) ||
                                                        Input.GetKey(KeyCode.S) ||
                                                        Input.GetKey(KeyCode.D) ||
                                                        Input.GetKey(KeyCode.W)));

    private bool IsRunning() => Input.GetKey(KeyCode.LeftShift);

    private void CheckGroundUnderPlayer()
    {
        Vector3 position = camera.transform.position;
        Vector3 target = position + Vector3.down * 4f;
        RaycastHit hit;

        isOnCarpet = false;
        isOnTiles = false;
        if (Physics.Linecast(position, target, out hit))
        {
            if (hit.collider.tag == "Carpet")
            {
                isOnCarpet = true;
            }
            else if (hit.collider.tag == "Tiles")
            {
                isOnTiles = true;
            }
        }
    }
}
