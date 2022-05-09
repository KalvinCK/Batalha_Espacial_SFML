using SFML.Graphics;
using SFML.Window;
using SFML.Audio;


namespace My_Game
{
    public class Game
    {
        public const int WIDTH = 1920;
        public const int HEIGHT = 1080;
        public const int FPS = 60;
        public const string TITULO = "Batalha Espacial C#";

        private RenderWindow window;
        private VideoMode Display = new VideoMode(WIDTH, HEIGHT);


        // Entidades resposaveis pelo game
        private Player player;
        private Background background;
        private Tiros tiros;
        private Inimigos inimigos;
        private Pontuacao poeira;
        // private SoundBuffer buffer;
        private Music music;

        public Game()
        {
            this.window = new RenderWindow(Display, TITULO, Styles.Fullscreen);
            this.window.SetFramerateLimit(FPS);
            this.window.SetVerticalSyncEnabled(true);

            this.window.Closed += (sender, args) => {
                this.window.Close();
                this.DESTRUIDOR();
            };

            
            background = new Background();
            player = new Player();
            tiros = new Tiros();
            inimigos = new Inimigos();
            poeira = new Pontuacao();

            // buffer = new SoundBuffer(Posicao.path + "music_theme.ogg");
            // music = music(Posicao.path + "music_theme.ogg");
            music = new Music(Posicao.path + "music_theme.ogg");
            music.Volume = 20f;
            music.Loop = true;

            music.Play();

            
        }
        public void Run()
        {
            while(this.window.IsOpen)
            {
                window.DispatchEvents();
                if(Keyboard.IsKeyPressed(Keyboard.Key.Escape)) // ESC encerra o game como nao a nada de render vai aqui
                {
                    this.window.Close();
                    this.DESTRUIDOR();

                }
                // code here..
                window.Clear(Color.Black); // limpa toda a janela com a cor escolhida
                background.draw(window);


                player.draw(window);

                tiros.draw(window);

                inimigos.draw(window);

                poeira.draw(window);


                window.Display(); // Renderiza o frame atual
            }
        }
        public void DESTRUIDOR()
        {
            // this.buffer.Dispose();
            this.music.Dispose();

            tiros.destruir();
            player.destruir();
            inimigos.destruir();


        }
    }
}