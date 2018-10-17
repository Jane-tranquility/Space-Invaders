using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Bomb : BombCategory
    {
        public Bomb(GameObject.Name name, GameSprite.Name spriteName, FallStrategy strategy, float posX, float posY)
            : base(name, spriteName, BombCategory.Type.Bomb)
        {
            this.x = posX;
            this.y = posY;
            this.delta = 4.0f;

            Debug.Assert(strategy != null);
            this.pStrategy = strategy;

            this.pStrategy.Reset(this.y);

            this.poColObj.pColSprite.SetLineColor(1, 1, 0);
        }


        public void Reset()
        {
            this.y = 700.0f;
            this.pStrategy.Reset(this.y);
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

        public override void Update()
        {
            base.Update();
            this.y -= delta;

            // Strategy
            this.pStrategy.Fall(this);
        }

        public float GetBoundingBoxHeight()
        {
            return this.poColObj.poColRect.height;
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }
        ~Bomb()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Bomb():{0}", this.GetHashCode());
#endif
        }


        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate collision reaction            
            other.VisitBomb(this);
        }

        public void SetPos(float xPos, float yPos)
        {
            this.x = xPos;
            this.y = yPos;
        }

        public void MultiplyScale(float sx, float sy)
        {
            Debug.Assert(this.poProxySprite != null);

            this.poProxySprite.sx *= sx;
            this.poProxySprite.sy *= sy;
        }


        // Data
        public float delta;
        private FallStrategy pStrategy;
    }
}