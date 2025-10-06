using System.Collections;
using UnityEngine;

public class CustomerAnim : MonoBehaviour
{
    public static CustomerAnim Instance { get; private set; }
    public Animator anim;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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
    public void nextCustomer()
    {
        Debug.Log("Generating next customer (CustomerAnime script)");
        anim.SetTrigger("NewPlayer");
        Debug.Log("Next Customer Generated");
    }

}
