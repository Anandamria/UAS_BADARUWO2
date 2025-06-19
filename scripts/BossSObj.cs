using UnityEngine;

[CreateAssetMenu(fileName = "NewBossSObj", menuName = "Enemy/BossSObj")]
public class BossSObj : ScriptableObject
{
    [SerializeField] private float _bossHealth = 6f;
    [SerializeField] private RuntimeAnimatorController _bossAnimator;

    public float BossHealth
    {
        get => _bossHealth;
        set => _bossHealth = value;
    }

    public RuntimeAnimatorController BossAnimator => _bossAnimator;

    public void InitializeData()
{
     _bossHealth = 6; 
}

}
