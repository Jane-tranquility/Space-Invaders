using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Missile : MissileCategory
    {
        public Missile(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 15.0f;
        }

        //~Missile()
        //{
        //}

        public override void Update()
        {
            base.Update();
            this.y += delta;
        }

        public override void Remove(SpriteBatchMan pSpriteBatchMan)
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove(pSpriteBatchMan);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitMissile(this);
        }


        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public override void VisitAlienGrid(AlienGrid a)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitAlienColumn(AlienColumn a)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(a);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitSquid(Squid a)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(this, a);
            pColPair.NotifyListeners();
        }

        public override void VisitCrab(Crab a)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(this,a);
            pColPair.NotifyListeners();
        }

        public override void VisitOctopus(Octopus a)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(this, a);
            pColPair.NotifyListeners();
        }
        // Data
        public float delta;
    }
}