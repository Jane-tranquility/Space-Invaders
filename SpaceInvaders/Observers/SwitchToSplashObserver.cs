using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SwitchToSplashObserver : ColObserver
    {

        public SwitchToSplashObserver(GameSprite.Name gameSpriteName, SpriteBatchMan pSpriteBatchMan)
        {
            this.pAlien = null;
            this.gameSpriteName = gameSpriteName;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public SwitchToSplashObserver(SwitchToSplashObserver b, SpriteBatchMan pSpriteBatchMan)
        {
            Debug.Assert(b != null);
            this.pAlien = b.pAlien;
            this.pSpriteBatchMan = pSpriteBatchMan;
        }

        public override void Notify()
        {
            // Delete missile
            //Debug.WriteLine("RemoveBrickObserver: {0} {1}", this.pSubject.pObjA, this.pSubject.pObjB);
            RemoveSplash pRemoveSplash;
            switch (this.gameSpriteName)
            {
                case GameSprite.Name.Splash:
                    this.pAlien = this.pSubject.pObjB;
                    switch (this.pAlien.name)
                    {
                        case GameObject.Name.UFO:
                            break;
                        default:
                            GamePlayingState.numOfAliens -= 1;
                            Debug.WriteLine("NUMBER OF ALIENS: {0}", GamePlayingState.numOfAliens);
                            break;
                    }
                    this.pAlien.poProxySprite.Set(this.gameSpriteName);
                    this.pAlien.Update();
                    Debug.Assert(this.pAlien != null);
                    pRemoveSplash = new RemoveSplash(this.pAlien, pSpriteBatchMan);
                    TimerMan.Add(TimeEvent.Name.RemoveSplash, pRemoveSplash, 0.3f);
                    break;

                case GameSprite.Name.PlayerEnd:
                    this.pAlien = this.pSubject.pObjB;
                    this.pAlien.poProxySprite.Set(this.gameSpriteName);
                    this.pAlien.Update();
                    Debug.Assert(this.pAlien != null);
                    pRemoveSplash = new RemoveSplash(this.pAlien, pSpriteBatchMan);
                    TimerMan.Add(TimeEvent.Name.RemoveSplash, pRemoveSplash, 0.3f);
                    break;

                case GameSprite.Name.TopSplash:
                    this.pAlien = this.pSubject.pObjA;
                    this.pAlien.poProxySprite.Set(this.gameSpriteName);
                    this.pAlien.Update();
                    Debug.Assert(this.pAlien != null);
                    break;

        }

    }

   

    


        // data
        private GameObject pAlien;
        private GameSprite.Name gameSpriteName;
        private SpriteBatchMan pSpriteBatchMan;
    }
}
