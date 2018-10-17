using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class MenuState:GameState
    {
        SpriteBatchMan pSpriteBatchMan; 
        public override void Handle()
        {

        }

        public override void LoadContent()
        {

            pSpriteBatchMan = new SpriteBatchMan(1, 1); 
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
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "SPACE INVADERS", Glyph.Name.Consolas36pt, 320, 500);
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "Choose 1 or 2 to play...", Glyph.Name.Consolas36pt, 240, 450);
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "1, Single Player", Glyph.Name.Consolas36pt, 300, 350);
            FontMan.Add(pSpriteBatchMan, Font.Name.Title, SpriteBatch.Name.Texts, "2, Two Player", Glyph.Name.Consolas36pt, 310, 250);

            InputSubject pInputSubject;
            pInputSubject = InputMan.GetKeyOneSubject();
            pInputSubject.Attach(new MoveToPlayingStateObserver());

            pInputSubject = InputMan.GetKeyTwoSubject();
            pInputSubject.Attach(new MoveToPlayingStateObserver());

            Simulation.SetState(Simulation.State.Realtime);
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
            Debug.Assert(pScoreMax!= null);
            pScoreMax.UpdateMessage("" + pSI.scoreHigh);
        }
        public override void Draw()
        {
            pSpriteBatchMan.Draw();
        }
        public override void UnLoadContent()
        {
            pSpriteBatchMan.Destroy();
            TextureMan.Destroy();
            GlyphMan.Destroy();
            FontMan.Destroy();
        }
    }
}
