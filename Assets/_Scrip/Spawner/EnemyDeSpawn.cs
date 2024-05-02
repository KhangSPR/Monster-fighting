using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDeSpawn : DespawnByTime
{
    public override void deSpawnObjParent()
    {
        EnemySpawner.Instance.Despawn(transform.parent);
    }
}
