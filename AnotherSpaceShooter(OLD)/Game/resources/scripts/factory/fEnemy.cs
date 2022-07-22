namespace Game
{
    public enum EnemyTypes
    {
        Dummy, Mosquitoe
    }
    public static class fEnemy
    {
        public static iEnemy CreateEnemy(EnemyTypes type)
        {
            switch (type)
            {
                case EnemyTypes.Dummy:
                    return new eDummy();
                case EnemyTypes.Mosquitoe:
                    return new eMosquitoe();
                default:
                    return new eDummy();
            }
        }

    }
}
