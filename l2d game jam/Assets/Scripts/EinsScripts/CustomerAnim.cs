using System.Collections;
using UnityEngine;

public class CustomerAnim : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void customerHappy()
    {
        anim.SetTrigger("Happy");
    }
    public void customerSad()
    {
        anim.SetTrigger("Sad");
    }
    public void startsTalkingForSeconds(float seconds)
    {
        anim.SetTrigger("StartsTalking");
        StartCoroutine(FinishTalking(seconds));
    }
    public void dissapearsAfterSeconds(float seconds)
    {
        anim.SetTrigger("Dissapear");
        StartCoroutine(Dissapear(seconds));
    }
    private IEnumerator FinishTalking(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetTrigger("FinishTalking");
    }
    private IEnumerator Dissapear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
