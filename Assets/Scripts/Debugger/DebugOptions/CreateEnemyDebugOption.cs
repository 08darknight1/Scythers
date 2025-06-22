using UnityEngine;

public class CreateEnemyDebugOption : DebugOption
{
    public GameObject EnemyPrefab;

    public override void ExecuteOption()
    {
        Instantiate(EnemyPrefab);
        SetOptionActive(false);
    }
}
