using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Octopus : AlienCategory
    {
        public Octopus(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        // Data: ---------------
        ~Octopus()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("     ~GreenBird():{0}", this.GetHashCode());
#endif
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an GreenBird
            // Call the appropriate collision reaction            
            other.VisitOctopus(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Bird vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Bird vs Missile
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Bird
            Debug.WriteLine("-------> Done  <--------");

        }

        public override void Update()
        {
            //   Debug.WriteLine("update: {0}", this);
            base.Update();
        }
    }
}
