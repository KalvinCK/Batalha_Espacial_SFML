using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

namespace My_Game
{
    class Player
    {
        public Sprite personagen;
        private Sprite []propulsor;
        private Sprite []propulsor1;
        private const float vel_player = 10f;
        public Sprite []explosao;
        public Sprite []explosao1;
        private int []cont2;
        private bool []active;
        private SoundBuffer buffer;
        private Sound explosaopersonagem;
        private int contador = 0;
        public Player()
        {
            // load texture
            Texturas player = new Texturas("F5S2.png");

            // Sprites
            personagen = new Sprite();
            personagen.Texture = player.texture;
            personagen.Scale = new Vector2f(0.4f, 0.4f);
            personagen.Position = new Vector2f(800f, 900f);


            // Explosao
            Texturas folhaExplosao = new Texturas("explosion 4.png");

            int x = 1290;
            int y = 255;
                        
            explosao = new Sprite[8];
            explosao1 = new Sprite[8];
            
            for(int i = 0; i < explosao.Length; i++)
            {
                explosao[i] = new Sprite(folhaExplosao.texture);
                explosao[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao[i].Scale = new Vector2f(0.8f, 0.8f);

                explosao1[i] = new Sprite(folhaExplosao.texture);
                explosao1[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao1[i].Scale = new Vector2f(0.8f, 0.8f);

            }

            cont2 = new int[2];
            active = new bool[2];

            for(int i = 0; i < cont2.Length; i++)
            {
                cont2[i] = 0;
                active[i] = false;
            }

            // propulsores
            
            Texturas folhaPropulsor = new Texturas("Aura38.png"); 
            propulsor = new Sprite[32];
            propulsor1 = new Sprite[32];

            int tamSprite = 128;
            int contar = 0;
            for(int i = 0; i < 4; i += 1)
            {
                for(int  j = 0; j < 8; j++)
                {
                    propulsor[contar] = new Sprite(folhaPropulsor.texture);
                    propulsor[contar].TextureRect = new IntRect(j * tamSprite, i * tamSprite, tamSprite, tamSprite);
                    propulsor[contar].Scale = new Vector2f(0.2f, 1f);
                    propulsor[contar].Rotation = 180;

                    propulsor1[contar] = new Sprite(folhaPropulsor.texture);
                    propulsor1[contar].TextureRect = new IntRect(j * tamSprite, i * tamSprite, tamSprite, tamSprite);
                    propulsor1[contar].Scale = new Vector2f(0.2f, 1f);
                    propulsor1[contar].Rotation = 180;

                    
                    contar++;

                }
                
            }

            buffer = new SoundBuffer(Posicao.path + "weaponfire1.wav");
            explosaopersonagem = new Sound(buffer);
            explosaopersonagem.Volume = 30f;

        }
        public void Update()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.W) && this.personagen.Position.Y > 0)
            {
                this.personagen.Position -= new Vector2f(0f, vel_player);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S) && this.personagen.Position.Y < Posicao.HEIGHT - this.personagen.TextureRect.Height * this.personagen.Scale.Y)
            {
                this.personagen.Position += new Vector2f(0f, vel_player);
            }
            if(Keyboard.IsKeyPressed(Keyboard.Key.A) && this.personagen.Position.X > 0)
            {
                this.personagen.Position -= new Vector2f(vel_player ,0f);
            }
            if(Keyboard.IsKeyPressed(Keyboard.Key.D) && this.personagen.Position.X < Posicao.WIDTH - this.personagen.TextureRect.Width * this.personagen.Scale.X)
            {
                this.personagen.Position += new Vector2f(vel_player ,0f);
            }
            int diferencaX = -50;
            int diferencaY = -58;
            Posicao.staticPosPersonagem = new Vector4(this.personagen.Position.X - diferencaX, 
            this.personagen.Position.Y - diferencaY,
             (this.personagen.Position.X + this.personagen.TextureRect.Width  * personagen.Scale.X) - diferencaX, 
             (this.personagen.Position.Y + this.personagen.TextureRect.Height * personagen.Scale.Y) - diferencaY);


        }
        public void updatePropulsores()
        {
            int diferencaX = 41;
            int diferencaY = 182;

            int diferenca1X = 52;

            for(int i = 0; i < this.propulsor.Length; i++)
            {
                this.propulsor[i].Position = new Vector2f(this.personagen.Position.X + diferencaX, this.personagen.Position.Y + diferencaY);

                this.propulsor1[i].Position = new Vector2f(this.personagen.Position.X + diferenca1X, this.personagen.Position.Y + diferencaY);
            }
        }
        public void colisao()
        {
            // detectamos a colisao das naves inimigas com o nosso personagem e resetando a posicao dele
            for (int i = 0; i < Posicao.staticNavesInimigas.Length; i++)
            {
                int diferencaX = 60;
                int diferencaY = 20;
                if(Posicao.colisoes(Posicao.staticNavesInimigas[i], Posicao.staticPosPersonagem))
                {
                    // explosao do personagem
                    for(int j = 0; j < explosao.Length; j++)
                    {
                        explosao[j].Position = new Vector2f(this.personagen.Position.X - diferencaX, this.personagen.Position.Y - diferencaY);
                    }
                    explosaopersonagem.Play();
                    this.active[0] = true;
                    this.personagen.Position = new Vector2f(800f, 1000f);
                    Posicao.pontos = 0;
                }
                // colisao das balas dos inimigos 
                if(Posicao.colisoes(Posicao.staticBalasNaves[i], Posicao.staticPosPersonagem))
                {
                    // explsao das baslas do personagem
                    for(int j = 0; j < explosao1.Length; j++)
                    {
                        explosao1[j].Position = new Vector2f(this.personagen.Position.X - diferencaX, this.personagen.Position.Y - diferencaY);

                    }
                    explosaopersonagem.Play();
                    this.active[1] = true;
                    this.personagen.Position = new Vector2f(800f, 1000f);
                    Posicao.pontos = 0;
                }
            }
        }
        public void draw(RenderTarget window)
        {
            this.Update();
            this.updatePropulsores();
            this.colisao();

            window.Draw(this.personagen);

            if(active[0] == true)
            {
                window.Draw(this.explosao[cont2[0]]);

                this.cont2[0] += 1;
                if(this.cont2[0] == 7)
                {
                    this.cont2[0] = 0;
                    this.active[0] = false;
                }
            }
            if(active[1] == true)
            {
                window.Draw(this.explosao1[cont2[1]]);

                this.cont2[1] += 1;
                if(this.cont2[1] == 7)
                {
                    this.cont2[1] = 0;
                    this.active[1] = false;
                }
            }
            if(contador == 32)
            {
                contador = 0;
            }
            window.Draw(propulsor[contador]);
            window.Draw(propulsor1[contador]);
            contador++;
        }
        public void destruir()
        {
            this.buffer.Dispose();
            this.explosaopersonagem.Dispose();
        }
    }
   
}