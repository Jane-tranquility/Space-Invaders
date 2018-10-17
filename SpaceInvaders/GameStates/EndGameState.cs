using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class EndGameState : GameState
    {
        SpriteBatchMan pSpriteBatchMan;
        public override void Handle()
        {

        }

       
        public override void LoadContent()
        {
            pSpriteBatchMan = new SpriteBatchMan(1, 1);
            //TimerMan.Create(3, 1);
            TextureMan.Create(2, 1);
            GlyphMan.Create(3, 1);
            FontMan.Create(1, 1);

            TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            FontMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            SpriteBatch pSB_Texts = pSpriteBatchMan.Add(SpriteBatch.Name.Texts);
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "SCORE<1>  HIGH-SCORE  SCORE<2>", Glyph.Name.Consolas36pt, 200, 680);
            FontMan.Add(pSpriteBatchMan, Font.Name.ScoreOne, SpriteBatch.Name.Texts, "00", Glyph.Name.Consolas36pt, 240, 650);
            FontMan.Add(pSpriteBatchMan, Font.Name.HighestScore, SpriteBatch.Name.Texts, "00", Glyph.Name.Consolas36pt, 440, 650);
            FontMan.Add(pSpriteBatchMan, Font.Name.ScoreTwo, SpriteBatch.Name.Texts, "00", Glyph.Name.Consolas36pt, 650, 650);
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "GAME OVER!!", Glyph.Name.Consolas36pt, 350, 400);
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "Press 3 to go back to the menu...", Glyph.Name.Consolas36pt, 200, 280);

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetKeyThreeSubject();
            pInputSubject.Attach(new MoveToMenuObserver());

            Simulation.SetState(Simulation.State.Realtime);
            //CycleBackToMenuState pCylce = new CycleBackToMenuState();
            //TimerMan.Add(TimeEvent.Name.CycleBackToMenu, pCylce, 20f);
        }
        public override void Update(float time)
        {

            InputMan.Update();

            SpaceInvaders pSI = SpaceInvaders.GetInstance();
            Font pScoreOne = FontMan.Find(Font.Name.ScoreOne);
            Debug.Assert(pScoreOne != null);
            pScoreOne.UpdateMessage("" + pSI.scoreOne);

            Font pScoreTwo = FontMan.Find(Font.Name.ScoreTwo);
            Debug.Assert(pScoreTwo != null);
            pScoreTwo.UpdateMessage("" + pSI.scoreTwo);


            Font pScoreMax = FontMan.Find(Font.Name.HighestScore);
            Debug.Assert(pScoreMax != null);
            pScoreMax.UpdateMessage("" + pSI.scoreHigh);

            //Simulation.Update(time);
            //if (Simulation.GetTimeStep() > 0.0f)
            //{
            //    // Fire off the timer events
            //    TimerMan.Update(Simulation.GetTotalTime());

            //}
        }
        public override void Draw()
        {
            pSpriteBatchMan.Draw();
        }
        public override void UnLoadContent()
        {
            //pSpriteBatchMan.Destroy();
            TextureMan.Destroy();
            GlyphMan.Destroy();
            //TimerMan.Destroy();

            FontMan.Destroy();
            GameObjectMan.Destroy();
            //ColPairMan.Destroy();
            //ShipMan.Destroy();
            
        }
    }
}

