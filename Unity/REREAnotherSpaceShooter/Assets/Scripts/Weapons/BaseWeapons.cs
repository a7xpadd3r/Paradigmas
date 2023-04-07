using UnityEngine;

public abstract class BaseWeapons : MonoBehaviour
{
    public int ammo = 0;
    public float recoilTime = 0.4f;

    public abstract void Fire(Vector2 spawnPosition);
    public abstract void UpdateThis(float delta);
    public abstract void AmmoGrab(int howMuch);
}
