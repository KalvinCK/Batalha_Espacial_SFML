using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;


namespace My_Game
{
    class Tiros
    {
        public Texturas disparoLaser;
        public Sprite []laser;
        public Sprite []explosao;
        public Sprite []explosao1;
        private int []cont2;
        private bool []active;

        private Vector2f VEL_BALA;
        private bool ativarDisparo = false;
        private int contador = 0;
        private int bala1 = 0;
        private int bala2 = 1;
        private int cont = 10;
        private SoundBuffer buffer1;
        private Sound sound;
        private SoundBuffer buffer2;
        private Sound explosaoColisaoBalas;
        public Tiros()
        {
            disparoLaser = new Texturas("14.png");

            // o valor corresponde a classe satic
            laser = new Sprite[Posicao.staticBalasPersonagem.Length];

            for(int i = 0; i < laser.Length; i++)
            {
                laser[i] = new Sprite();
                laser[i].Texture = disparoLaser.texture;
                laser[i].Scale = new Vector2f(0.1f, 0.3f);
                // laser[i].Color = new Color(255, 100, 100, 255);
                laser[i].Position = new Vector2f(Posicao.staticPosPersonagem.X, Posicao.staticPosPersonagem.Y);
            }

            Texturas folhaExplosao = new Texturas("explosion 4.png");

            int x = 1290;
            int y = 255;
                        
            explosao = new Sprite[8];
            explosao1 = new Sprite[8];
            
            for(int i = 0; i < explosao.Length; i++)
            {
                explosao[i] = new Sprite(folhaExplosao.texture);
                explosao[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao[i].Scale = new Vector2f(0.5f, 0.5f);

                explosao1[i] = new Sprite(folhaExplosao.texture);
                explosao1[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao1[i].Scale = new Vector2f(0.5f, 0.5f);

            }

            cont2 = new int[2];

            active = new bool[2];

            for(int i = 0; i < cont2.Length; i++)
            {
                cont2[i] = 0;
                active[i] = false;
            }



            VEL_BALA = new Vector2f(0f, 20f);

            // buffer1 = new SoundBuffer(Posicao.path + "weaponfire6.wav");
            buffer1 = new SoundBuffer(Posicao.path + "drag_release.wav");
            sound = new Sound(buffer1);
            sound.Volume = 15f;

            buffer2 = new SoundBuffer(Posicao.path + "drag_release.wav");
            explosaoColisaoBalas = new Sound(buffer2);
            explosaoColisaoBalas.Volume = 30f;

        }
        private void update()
        {   
            if(this.contador == laser.Length * 5)
            {
                this.contador = 0;
                this.bala1 = 0;
                this.bala2 = 1;
            }
            
            for(int i = 0; i < laser.Length / 2; i++)
            {
                if ( contador == this.cont*i)
                {
                    this.sound.Play();

                    this.laser[this.bala1].Position = new Vector2f(Posicao.staticPosPersonagem.X - 38, Posicao.staticPosPersonagem.Y - 80);
                    this.laser[this.bala2].Position = new Vector2f(Posicao.staticPosPersonagem.X - 10, Posicao.staticPosPersonagem.Y - 80);

                    this.bala1 += 2;
                    this.bala2 += 2;

                    this.ativarDisparo = true;
                }

            }
            this.contador += 10;
        }
        public void balasMoving()
        {
            for(int i = 0; i < laser.Length; i++)
            {
                if(this.laser[i].Position.Y > -30)
                {
                    this.laser[i].Position -= this.VEL_BALA;

                } else {
                    this.laser[i].Position = new Vector2f(3000f, this.laser[i].Position.Y);
                }
                int diferencaX = -47;
                int diferencaY = -60;

                Posicao.staticBalasPersonagem[i] = new Vector4(this.laser[i].Position.X - diferencaX, 
                this.laser[i].Position.Y - diferencaY, 
                (this.laser[i].Position.X + this.laser[i].TextureRect.Width * this.laser[i].Scale.X) - diferencaX,
                (this.laser[i].Position.Y + this.laser[i].TextureRect.Height * this.laser[i].Scale.Y) - diferencaY);

            }
        }
        public void colisao()
        {
            // colisao das balas do personagem com as naves inimigas
            int diferencaX = 50;
            int diferencaY = 30;
            for(int i = 0; i < Posicao.staticBalasPersonagem.Length; i++)
            {
                for(int j = 0; j < Posicao.staticNavesInimigas.Length; j++)
                {
                    if(Posicao.colisoes(Posicao.staticBalasPersonagem[i], Posicao.staticNavesInimigas[j]))
                    {
                        for( int k = 0; k < explosao.Length; k++)
                        {
                            explosao[k].Position = new Vector2f(this.laser[i].Position.X - diferencaX, this.laser[i].Position.Y - diferencaY);
                        }
                        this.active[0] = true;
                        this.laser[i].Position = new Vector2f(3000f, this.laser[i].Position.Y);
                        Posicao.pontos += 1;
                    }
                }
            }
            // colisao das balas do personagem com as balas dos inimigos
            for(int i = 0; i < Posicao.staticBalasNaves.Length; i++)
            {
                for(int j = 0; j < Posicao.staticBalasPersonagem.Length; j++)
                {
                    if(Posicao.colisoes(Posicao.staticBalasNaves[i], Posicao.staticBalasPersonagem[j]))
                    {
                        for (int k = 0; k < explosao1.Length; k++)
                        {
                            explosao1[k].Position = new Vector2f(this.laser[j].Position.X - diferencaX, this.laser[j].Position.Y - diferencaY);
                        }
                        this.explosaoColisaoBalas.Play();
                        this.active[1] = true;
                        this.laser[j].Position = new Vector2f(3000f, this.laser[j].Position.Y);
                    }
                }
            }
        }
        public void draw(RenderTarget window)
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                this.update();
            }
            
            balasMoving();
            colisao();

            if(ativarDisparo == true )
            {
                for(int i = 0; i < this.laser.Length; i++)
                {
                    window.Draw(laser[i]);
                }
            }

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
        }
        public void destruir()
        {
            this.buffer1.Dispose();
            this.sound.Dispose();

            this.buffer2.Dispose();
            this.explosaoColisaoBalas.Dispose();
        }
    }
}