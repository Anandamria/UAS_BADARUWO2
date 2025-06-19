using UnityEngine;

public class GunderuwoController : MonoBehaviour
{
    public BossSObj bossData;

    private Animator animator;
    private float currentHP;
    private bool isDefeated = false;

    void Start()
    {
        animator = GetComponent<Animator>(); 

    if (animator != null && bossData != null)
    {
        animator.runtimeAnimatorController = bossData.BossAnimator;
        animator.ResetTrigger("Fall"); 
    }

    currentHP = bossData.BossHealth;
    }


    public void TakeHit()
    {
        if (isDefeated) return;

        currentHP--;
        Debug.Log("Gunderuwo terkena ramuan! Sisa nyawa: " + currentHP);

        if (currentHP <= 0)
        {
            isDefeated = true;

            if (animator != null)
            {
                Debug.Log("Gunderuwo kalah! Memicu animasi jatuh.");
                animator.SetTrigger("Fall");
                GameManager.Instance.TriggerGameWin();
            }
            else
            {
                Debug.LogWarning("Animator belum di-assign di Inspector.");
            }
        }
    }
}
