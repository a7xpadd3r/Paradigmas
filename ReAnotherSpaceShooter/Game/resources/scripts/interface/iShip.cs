namespace Game
{
    public interface iShip
    {
        bool ShieldActive { get; set; }
        float ShieldDuration { get; set; }
        void Invincibility();
    }
}
