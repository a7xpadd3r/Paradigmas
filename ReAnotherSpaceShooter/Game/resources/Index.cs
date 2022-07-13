using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Index // Índice de funciones
    {
        public sMainMenu menu_principal;
        public GameManager nivel_jugable_Y_manager_Y_singleton_Y_eventos;
        public sGameOver victoria_derrota;
        public Animation animaciones;
        public Collider colisiones;
        public Player herencia;
        public iWeapon factory = fWeapons.CreateWeapon(WeaponTypes.BlueRail);
        public fyPoolDay poolgenerico;
        public Transform componente_transform;
        public Renderer componente_renderer;
        public iScenes interface1;
        public iWeapon interface2;
        public iShip interface3;
    }
}
