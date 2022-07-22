using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Game
{
    public class GenericBullet
    {
        public static SoundPlayer sfx = new SoundPlayer("resources/gulp.wav");
        float _speed = 800;
        float _posX = 0;
        float _posY = 0;
        float _rot = 0;
        int _type = 1;
        Texture gfx = new Texture("resources/p_genericproyectile.png"); // Default texture
        float lifeTime = 1;
        float timer = 0;

        bool draw = true;

        public bool Draw => draw;

        public GenericBullet(float posX, float posY, float rot, int type)
        {
            _posX = posX;
            _posY = posY;
            _rot = rot;
            _type = type;
            switch (_type)
            {
                case 1:
                    _speed = 700;
                    gfx = new Texture("resources/p_proyectile1.png");
                    break;
                case 2:
                    _speed = 1000;
                    gfx = new Texture("resources/p_proyectile2.png");
                    break;
            }
        }

        public void Update()
        {
            _posY -= _speed * Program.deltaTime;
            timer += Program.deltaTime;

            if (timer >= lifeTime)
            {
                draw = false;
            }
        }

        public void DrawBullet()
        {
            if (draw)
                Engine.Draw(gfx, _posX, _posY, 2, 2, _rot, 145.5f, 86.5f);
        }

    }
}
