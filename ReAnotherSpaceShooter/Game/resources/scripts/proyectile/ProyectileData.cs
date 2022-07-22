namespace Game
{
    public class ProyectileData
    {
        public float MaxSpeed;
        public float Damage;
        public float Recoil;
        public Animation Animation;
        public DoubleVector2 colliderVectors;
        public DoubleVector2 beamStats;

        public ProyectileData(float newRecoil, float newMaxSpeed, float newDamage, Animation newAnim, DoubleVector2 newColliderVectors, DoubleVector2 newBeamSize = null)
        {
            this.Recoil = newRecoil;
            this.MaxSpeed = newMaxSpeed;
            this.Damage = newDamage;
            this.Animation = newAnim;
            this.colliderVectors = newColliderVectors;
            this.beamStats = newBeamSize;
        }
    }
}
