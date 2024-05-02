using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootAbleObject", menuName = "SO/ShootAbleObject")]
public class ShootAbleObjectSO : ScriptableObject
{
    [Header("Object")]
    public string ObjName = "ShootAble Object";
    public ObjectType objectType;
    public int hpMax;
    public int damage;
    [Header("Card")]
    public float speed = 1.5f;
    public float attackRange;

    [Header("Bulleet")]
    public float speedFly;
    //public float speed = 1;
    //public List<Droprate> drop
    //List;
}
