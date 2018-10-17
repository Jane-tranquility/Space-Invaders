using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RightUFO : UFOCategory
    {

        public RightUFO(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 2.0f;
        }

        //~RightUFO()
        //{
        //}

        public override void Update()
        {
            base.Update();
            this.x += delta;
        }

        public override void Remove(SpriteBatchMan pSpriteBatchMan)
        {
            // Since the Root object is being drawn
            // 1st set its size to zero
            this.poColObj.poColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (UFO group)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Remove(pSpriteBatchMan);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Missile
            // Call the appropriate collision reaction            
            other.VisitRightUFO(this);
        }

        public override void VisitMissile(Missile m)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        // Data
        public float delta;
    }
}
