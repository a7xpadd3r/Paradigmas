using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IScene
    {
        void Initialize();
        void Finish();
        void Update();
        void Render();
    }
}
