using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public InputAction attackAction;
    private Animator anim;

    public Transform hitboxCenter;
    public float hitboxRadius;
    public LayerMask hitLayers;
    public bool hitboxActive;
    public CinemachineBasicMultiChannelPerlin noise;
    bool attacking = false;

    public UnityEvent OnHit;
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        attackAction.Enable();
        attackAction.performed += Attack_Performed;
    }

    private void Attack_Performed(InputAction.CallbackContext c)
    {
        if (attacking == true) return;
        anim.SetTrigger("punch");
        attacking = true;
    }


    public void ActivateHitbox()
    {
        hitboxActive = true;
    }

    public void DeactivateHitbox()
    {
        hitboxActive = false;
        attacking = false;
    }

    private void FixedUpdate()
    {
        if (!hitboxActive) return;
        Collider2D[] hit = Physics2D.OverlapCircleAll(hitboxCenter.position, hitboxRadius, hitLayers);
        if (hit.Length > 0)
        {
            foreach (Collider2D col in hit)
            {
                if (col.gameObject.TryGetComponent<Enemy>(out Enemy e))
                {
                    hitboxActive = false;
                    OnHit?.Invoke();

                     e.Hit(transform.position - col.transform.position);
                     transform.position += (transform.position - col.transform.position) * 0.1f ;
                     StartCoroutine(HitStop());
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitboxCenter.position, hitboxRadius);
    }

    private IEnumerator HitStop()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
        StartCoroutine(CameraShake());
    }

    private IEnumerator CameraShake()
    {
        noise.enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        noise.enabled = false;
    }

}
