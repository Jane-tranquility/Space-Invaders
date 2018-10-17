using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class CheckPlayerStatusObserver: ColObserver
    {
        public CheckPlayerStatusObserver(SpriteBatchMan pSpriteBatchMan)
        {
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public CheckPlayerStatusObserver(CheckPlayerStatusObserver c)
        {
            this.pSpriteBatchMan = c.pSpriteBatchMan;
        }

        public override void Notify()
        {
            if (GamePlayingState.playLives > 1)
            {
                GamePlayingState.playLives -= 1;
                //Debug.Assert(ShipMan.GetShip() == null);
                ShipMan.Attach(pSpriteBatchMan);
            }
            else
            {

                CheckPlayerStatusObserver pCheck = new CheckPlayerStatusObserver(this);
                DelayedObjectMan.Attach(pCheck);
                //SpaceInvaders pSI = SpaceInvaders.GetInstance();
                //pSI.UnLoadContent();

                //pSI.SetState(SpaceInvaders.State.EndGameState);

                //pSI.LoadContent();
                //if (pSI.scoreOne > pSI.scoreTwo)
                //{
                //    if (pSI.scoreOne > pSI.scoreHigh)
                //    {
                //        pSI.scoreHigh = pSI.scoreOne;
                //    }
                //}
                //else
                //{
                //    if (pSI.scoreTwo > pSI.scoreHigh)
                //    {
                //        pSI.scoreHigh = pSI.scoreTwo;
                //    }
                //}


                //MoveToEndStateEvent pMoveToEndEvent = new MoveToEndStateEvent(pSI);
                //TimerMan.Add(TimeEvent.Name.MoveToEndState, pMoveToEndEvent, 0.5f);


            }
        }

        public override void Execute()
        {
            SpaceInvaders pSI = SpaceInvaders.GetInstance();
            pSI.UnLoadContent();

            pSI.SetState(SpaceInvaders.State.EndGameState);

            pSI.LoadContent();
            if (pSI.scoreOne > pSI.scoreTwo)
            {
                if (pSI.scoreOne > pSI.scoreHigh)
                {
                    pSI.scoreHigh = pSI.scoreOne;
                }
            }
            else
            {
                if (pSI.scoreTwo > pSI.scoreHigh)
                {
                    pSI.scoreHigh = pSI.scoreTwo;
                }
            }

        }

        //data
        private SpriteBatchMan pSpriteBatchMan;
    }
}