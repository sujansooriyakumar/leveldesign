using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public void Hit(Vector3 attackDirection)
    {
        hp--;
        //GetComponent<Animator>().SetTrigger("hit");
        if(hp<=0) Destroy(gameObject);
        transform.position -= attackDirection * .1f;
    }
}
