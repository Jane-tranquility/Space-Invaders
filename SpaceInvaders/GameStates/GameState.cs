using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameState
    {
        // state()
        public abstract void Handle();

        // strategy()
        public abstract void LoadContent();
        public abstract void Update(float time);
        public abstract void Draw();
        public abstract void UnLoadContent();

    }
}