namespace Game
{
    public class TheRendererer
    {
        public Transform renderTransform;
        public Texture renderTexture { get; set; }

        public static void RenderDraw(Texture newTexture, Transform nT, float newRadius)
        {
            Engine.Draw(newTexture, nT.position.X, nT.position.Y, nT.scale.X, nT.scale.Y, nT.angle, newRadius);
        }
    }
}
