using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SpaceInvaders : Azul.Game
    {
        

        public enum State
        {
            MenuState,
            GamePlayingState,
            EndGameState
        }
        

        private SpaceInvaders()
        {
            this.pMenuState = new MenuState();
            this.pGamePlayingState = new GamePlayingState();
            this.pEndGameState = new EndGameState();
            this.scoreOne = 0;
            this.scoreTwo = 0;
            this.scoreHigh = 0;
            this.sndEngine = new IrrKlang.ISoundEngine(); ;
        }


        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("->Space Invader<-");
            this.SetWidthHeight(896, 1024);
            this.SetClearColor(0f, 0f, 0f, 0f);
            Simulation.Create();
            
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            pInstance.pGameState.LoadContent();
        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------

        public override void Update()
        {
            pInstance.pGameState.Update(this.GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------

        public override void Draw()
        {
            pInstance.pGameState.Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            pInstance.pGameState.UnLoadContent();
        }

        public static SpaceInvaders Create()
        {
            // make sure its the first time
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new SpaceInvaders();
            }

            Debug.Assert(pInstance != null);
            pInstance.SetState(SpaceInvaders.State.MenuState);
            return pInstance;

        }
        public void SetState(SpaceInvaders.State inState)
        {
            switch (inState)
            {
                case SpaceInvaders.State.MenuState:
                    this.pGameState= this.pMenuState;
                    break;

                case SpaceInvaders.State.GamePlayingState:
                    this.pGameState = this.pGamePlayingState;
                    break;

                case SpaceInvaders.State.EndGameState:
                    this.pGameState = this.pEndGameState;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }
        }

        public static SpaceInvaders GetInstance()
        {
            Debug.Assert(pInstance != null);

            return pInstance;
        }


        private static SpaceInvaders pInstance = null;

        public int scoreOne;
        public int scoreTwo;
        public int scoreHigh;
        private GameState pGameState;

        private MenuState pMenuState;
        private GamePlayingState pGamePlayingState;
        private EndGameState pEndGameState;
        public IrrKlang.ISoundEngine sndEngine;

    }
}
