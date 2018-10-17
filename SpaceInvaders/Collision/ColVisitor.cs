using System;
using System.Diagnostics;


namespace SpaceInvaders
{

    abstract public class ColVisitor : DLink
    {
        public virtual void VisitShieldRoot(ShieldRoot s)
        {
            Debug.WriteLine("Visit by ShieldRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldGrid(ShieldGrid s)
        {
            Debug.WriteLine("Visit by ShieldGrid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldColumn(ShieldColumn s)
        {
            Debug.WriteLine("Visit by ShieldColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShieldBrick(ShieldBrick s)
        {
            Debug.WriteLine("Visit by ShieldBrick not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienGrid(AlienGrid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by AlienGrid not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienColumn(AlienColumn b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by AlienColumn not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitOctopus(Octopus b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by RedBird not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitCrab(Crab b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by YellowBird not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitSquid(Squid b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Squid not implemented");
            Debug.Assert(false);
        }


        //public virtual void VisitWhiteBird(WhiteBird b)
        //{
        //    // no differed to subcass
        //    Debug.WriteLine("Visit by Octopus not implemented");
        //    Debug.Assert(false);
        //}

        public virtual void VisitMissile(Missile m)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Missile not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitMissileGroup(MissileGroup m)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by MissileGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitNullGameObject(NullGameObject n)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by NullGameObject not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallGroup(WallGroup w)
        {
            Debug.WriteLine("Visit by WallGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallBottom(WallBottom w)
        {
            Debug.WriteLine("Visit by WallBottom not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallRight(WallRight w)
        {
            Debug.WriteLine("Visit by WallRight not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitWallLeft(WallLeft w)
        {
            Debug.WriteLine("Visit by WallLeft not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitWallTop(WallTop w)
        {
            Debug.WriteLine("Visit by WallTop not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBomb(Bomb b)
        {
            // no differed to subcass
            Debug.WriteLine("Visit by Bomb not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitBombRoot(BombRoot b)
        {
            Debug.WriteLine("Visit by BombRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitShip(Ship s)
        {
            Debug.WriteLine("Visit by Ship not implemented");
            Debug.Assert(false);
        }
        public virtual void VisitShipRoot(ShipRoot s)
        {
            Debug.WriteLine("Visit by ShipRoot not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitUFOGroup(UFOGroup s)
        {
            Debug.WriteLine("Visit by UFOGroup not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitRightUFO(RightUFO s)
        {
            Debug.WriteLine("Visit by Right Moving UFO not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitLeftUFO(LeftUFO s)
        {
            Debug.WriteLine("Visit by Left Moving UFO not implemented");
            Debug.Assert(false);
        }

        public virtual void VisitAlienRoot(AlienRoot s)
        {
            Debug.WriteLine("Visit by AlienRoot not implemented");
            Debug.Assert(false);
        }
        abstract public void Accept(ColVisitor other);
    }

}