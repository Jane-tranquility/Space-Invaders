using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class Movement : Command
    {
        public static int counting=0;
        public Movement(Composite root, SpriteBatch pSpriteBatch, SpriteBatch pCollisionSpriteBatch, Composite pTree)
        {
            // initialized the sprite animation is attached to
            this.grid = (Composite)root.GetFirstChild();
            this.pSpriteBatch = pSpriteBatch;
            this.pCollisionSpriteBatch = pCollisionSpriteBatch;
            this.pTree = pTree;
        }
        public override void Execute(float deltaTime, TimeEvent.Name name)
        {
            //if (Component.right > 896 || Component.left < 0.0f)
            //{
            //    Component.speed *= -1.0f;
            //    grid.MoveDown();
            //}
            //Component.right += Component.speed;
            //Component.left += Component.speed;
            grid.MoveGrid();

            IrrKlang.ISoundEngine pSndEngine = SpaceInvaders.GetInstance().sndEngine;
            pSndEngine.SoundVolume = 0.2f;
            if (counting % 4 == 0)
            {
                pSndEngine.Play2D("fastinvader1.wav");
            }
            else if(counting % 4 == 1)
            {
                pSndEngine.Play2D("fastinvader2.wav");
            }
            else if (counting % 4 == 2)
            {
                pSndEngine.Play2D("fastinvader3.wav");
            }
            else if (counting % 4 == 3)
            {
                pSndEngine.Play2D("fastinvader4.wav");
            }
            counting++;

            //1, using ForwardIterator
            ForwardIterator pIterator = new ForwardIterator(grid);
            Random rnd = new Random();
            int num = rnd.Next(0, 11);
            Debug.WriteLine("        random number generated: {0}", num);
            Component pColumn = null;
            bool flag = false;
            for (int i = 0; i <= num; i++)
            {
                if (pColumn == null && flag==true)
                {
                    break;
                }
                pColumn = pIterator.Next();
                flag = true;
                if (pColumn == null)
                {
                    break;
                }
                while (pColumn.holder != Component.Container.COMPOSITE)
                {
                    pColumn = pIterator.Next();
                    if (pColumn == null)
                    {
                        break;
                    }
                }
            }



            //2, using Iterator
            //Component pColumn = Iterator.GetChild(grid);
            //pColumn = Iterator.GetSibling(pColumn);
            if (pColumn != null)
            {
                //Debug.WriteLine("  3,     child type: {0}", pColumn);
                Bomb pBomb=((AlienColumn)pColumn).ActivateBomb(rnd);
                pBomb.ActivateGameSprite(this.pSpriteBatch);
                pBomb.ActivateCollisionSprite(this.pCollisionSpriteBatch);
                pTree.Add(pBomb);
            }


            // Add itself back to timer
            TimerMan.Add(name, this, deltaTime - 0.003f);

        }
        public Composite grid;
        private SpriteBatch pSpriteBatch;
        private SpriteBatch pCollisionSpriteBatch;
        private Composite pTree;
    }
}