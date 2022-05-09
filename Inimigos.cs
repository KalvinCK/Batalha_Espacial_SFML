using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;


namespace My_Game
{
    class Inimigos
    {
        public Sprite []navesInimigas;
        public Sprite []tiros;
        // public Vector2f disparo;
        public Random rdn;
        public Sprite []explosao;
        public Sprite []explosao1;
        public Sprite []explosao2;
        public Sprite []explosao3;
        private int []cont2;
        private bool []active;
        private float []contadorY;
        private SoundBuffer buffer1;
        private SoundBuffer buffer2;
        private Sound explosaoNaveIninimiga;
        private Sound tirosInimigos;
        public Inimigos()
        {
            Texturas folha = new Texturas("ships006.png");  

            // PEQUENOS
            navesInimigas = new Sprite[160];
            for (int i = 0; i < navesInimigas.Length; i += 2)
            {
                navesInimigas[i] = new Sprite(folha.texture);
                navesInimigas[i].TextureRect = new IntRect(621, 0, 66, 100);
                navesInimigas[i].Rotation = 180f;
                navesInimigas[i].Scale = new Vector2f(0.7f, 0.7f);

                navesInimigas[i + 1] = new Sprite(folha.texture);
                navesInimigas[i + 1].TextureRect = new IntRect(696, 0, 66, 100);
                navesInimigas[i + 1].Rotation = 180f;
                navesInimigas[i + 1].Scale = new Vector2f(0.7f, 0.7f);

            }

            // POSIÇOES
            contadorY = new float[20];

            for(int i = 0; i < contadorY.Length; i++)
            {
                contadorY[i] = -170 * (i + 1);

            }
            initPositions();

            // DISPAROS
            Texturas dispLaser = new Texturas("09.png");
            tiros = new Sprite[navesInimigas.Length];
            for(int i = 0; i < tiros.Length; i++)
            {

                tiros[i] = new Sprite(dispLaser.texture);
                tiros[i].Scale = new Vector2f(0.1f, 0.3f);
                tiros[i].Position = new Vector2f(100f, 2000f);
                tiros[i].Color = new Color(0, 255, 55);

            }

            Texturas folhaExplosao = new Texturas("explosion 4.png");

            int x = 1290;
            int y = 255;

            // Explosoes
            explosao  = new Sprite[8];
            explosao1 = new Sprite[8];
            explosao2 = new Sprite[8];
            explosao3 = new Sprite[8];
            
            for(int i = 0; i < explosao.Length; i++)
            {
                explosao[i] = new Sprite(folhaExplosao.texture);
                explosao[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao[i].Scale = new Vector2f(0.8f, 0.8f);

                explosao1[i] = new Sprite(folhaExplosao.texture);
                explosao1[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao1[i].Scale = new Vector2f(0.8f, 0.8f);

                explosao2[i] = new Sprite(folhaExplosao.texture);
                explosao2[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao2[i].Scale = new Vector2f(0.4f, 0.4f);
                explosao2[i].Color = new Color(73, 255, 255, 90);

                
                explosao3[i] = new Sprite(folhaExplosao.texture);
                explosao3[i].TextureRect = new IntRect(x, y * i, y, y);
                explosao3[i].Scale = new Vector2f(0.4f, 0.4f);
                explosao3[i].Color = new Color(73, 255, 255, 90);

            }

            cont2 = new int[4];

            active = new bool[4];

            for(int i = 0; i < cont2.Length; i++)
            {
                cont2[i] = 0;
                active[i] = false;
            }

            rdn = new Random();

            buffer1 = new SoundBuffer(Posicao.path + "explosion4.wav");
            explosaoNaveIninimiga = new Sound(buffer1);
            explosaoNaveIninimiga.Volume = 30f;

            buffer2 = new SoundBuffer(Posicao.path + "weaponfire9.wav");
            tirosInimigos = new Sound(buffer2);
            tirosInimigos.Volume = 15f;


        }
        public void initPositions()
        {
            int contadorX = 1;
            int cont = 0;

            for(int i = 0; i < navesInimigas.Length; i++)
            {
                if(contadorX == 21)
                {
                    contadorX = 1;
                    cont++;
                }
                this.navesInimigas[i].Position = new Vector2f(93f * contadorX,  this.contadorY[cont]);

                contadorX++;
            }
        }
        // atualiza as posicoes
        public void resetarAll(int inicio, int indice)
        {
            int contadorX = 1;
            int cont = 0;

            for(int i = inicio; i < indice; i++)
            {
                if(contadorX == 21)
                {
                    contadorX = 1;
                    cont++;
                }
                this.navesInimigas[i].Position = new Vector2f(93f * contadorX,  this.contadorY[cont]);

                contadorX++;
            }
        }
        // Movimento dos inimigos
        public void update()
        {
            for(int i = 0; i < this.navesInimigas.Length; i += 20)
            {
                if(this.navesInimigas[i].Position.Y > 1220)
                {
                    this.resetarAll(i, i + 20);

                }
            }


            Vector2f velocidade = new Vector2f(0f, 1.5f);

            for(int i = 0; i < this.navesInimigas.Length; i++)
            {
                this.navesInimigas[i].Position += velocidade;

                // Set Positions na class static
                Posicao.staticNavesInimigas[i] = new Vector4(this.navesInimigas[i].Position.X, this.navesInimigas[i].Position.Y, 
                this.navesInimigas[i].Position.X + this.navesInimigas[i].TextureRect.Width *  this.navesInimigas[i].Scale.X,
                this.navesInimigas[i].Position.Y + this.navesInimigas[i].TextureRect.Height * this.navesInimigas[i].Scale.Y);
            }


        }
        public void updateDisparos(int inicio, int indice)
        {
            // var rand = this.rdn.Next() % 3;
            var rand1 = 2 + (this.rdn.Next() % 5);
            tirosInimigos.Play();

            for(int i = inicio; i < indice; i += rand1)
            {
                this.tiros[i].Position = new Vector2f(this.navesInimigas[i].Position.X - 30, this.navesInimigas[i].Position.Y - 20);
            }

        }
        public void disparos()
        {
            // Disparos
            Vector2f vel1 = new Vector2f(1f, 4f);

            Vector2f vel2 = new Vector2f(-1f, 4f);

            for(int i = 0; i < this.navesInimigas.Length; i += 20)
            {
                // Console.WriteLine(i);
                if(this.navesInimigas[i].Position.Y < 30f && this.navesInimigas[i].Position.Y > 20f || this.navesInimigas[i].Position.Y < 510f && this.navesInimigas[i].Position.Y > 500f)
                {
                    updateDisparos(i, i + 20);

                }
            }

            // Setando as posicoes na classe estatica
            int diferencaX = 50;
            int diferencaY = 58;

            int contador = 0;
            // int contador1 = 1;
            for(int i = 0; i < this.navesInimigas.Length; i++)
            {
                if(contador == this.navesInimigas.Length)
                {
                    contador = 0;
                }
                this.tiros[contador].Position += vel1;
                this.tiros[contador + 1].Position += vel2;

                Posicao.staticBalasNaves[i] = new Vector4(this.tiros[i].Position.X + diferencaX,
                this.tiros[i].Position.Y + diferencaY,
                (this.tiros[i].Position.X + this.tiros[i].TextureRect.Width *  this.tiros[i].Scale.X) + diferencaX,
                (this.tiros[i].Position.Y + this.tiros[i].TextureRect.Height * this.tiros[i].Scale.Y) + diferencaY);

                contador += 2;

            }
            
        }
        public void colisao()
        {
            // detectando colisoes das nossa balas com as naves inimigas
            for(int i = 0; i <  Posicao.staticBalasPersonagem.Length; i++)
            {
                for(int j = 0; j < Posicao.staticNavesInimigas.Length; j++)
                {
                    
                    if(Posicao.colisoes(Posicao.staticBalasPersonagem[i], Posicao.staticNavesInimigas[j]))
                    {
                        // explosoes das naves inimigas
                        for(int k = 0; k < explosao.Length; k++)
                        {   
                            int diferencaX = 100;
                            int diferencaY = 100;
                            this.explosao[k].Position = new Vector2f(this.navesInimigas[j].Position.X - diferencaX, this.navesInimigas[j].Position.Y - diferencaY);
                        }
                        this.explosaoNaveIninimiga.Play();
                        this.active[0] = true;
                        this.navesInimigas[j].Position = new Vector2f(-3000, this.navesInimigas[j].Position.Y);
                    }
                }
            }
            
            // detectando colisoes das naves inimigas com o personagem
            for (int i = 0; i < Posicao.staticNavesInimigas.Length; i++)
            {
                // colisao das naves inimigas com o personagem
                if(Posicao.colisoes(Posicao.staticNavesInimigas[i], Posicao.staticPosPersonagem))
                {
                    // explosoes
                    for(int j = 0; j < explosao.Length; j++)
                    {
                        int diferenca1X = 115;
                        int diferenca1Y = 120;
                        this.explosao1[j].Position =  new Vector2f(this.navesInimigas[i].Position.X - diferenca1X, this.navesInimigas[i].Position.Y - diferenca1Y);
                    }
                    this.explosaoNaveIninimiga.Play();
                    this.active[1] = true;
                    this.navesInimigas[i].Position = new Vector2f(-3000, this.navesInimigas[i].Position.Y);
                    this.initPositions();

                }
                // colisao das balas dos inimigos com a nossa nave
                if(Posicao.colisoes(Posicao.staticBalasNaves[i], Posicao.staticPosPersonagem))
                {
                    // explosoes
                    for(int j = 0; j < explosao.Length; j++)
                    {
                        int diferenca2X = 40;
                        int diferenca2Y = 35;
                        this.explosao2[j].Position = new Vector2f(this.tiros[i].Position.X - diferenca2X, this.tiros[i].Position.Y - diferenca2Y);
                    }
                    this.active[2] = true;
                    this.tiros[i].Position = new Vector2f(-3000, this.tiros[i].Position.Y);
                    this.initPositions();

                }
            }
            // colisao das balas do personagem com as do inimigo
            for(int i = 0; i < Posicao.staticBalasNaves.Length; i++)
            {
                for(int j = 0; j < Posicao.staticBalasPersonagem.Length; j++)
                {
                    if(Posicao.colisoes(Posicao.staticBalasNaves[i], Posicao.staticBalasPersonagem[j]))
                    {
                        for (int k = 0; k < explosao3.Length; k++)
                        {
                            int diferenca3X = 40;
                            int diferenca3Y = 35;
                            explosao3[k].Position = new Vector2f(this.tiros[i].Position.X - diferenca3X, this.tiros[i].Position.Y - diferenca3Y);
                        }
                        this.active[3] = true;
                        this.tiros[i].Position = new Vector2f(-3000, this.tiros[i].Position.Y);
                    }
                }
            }
        }
        public void draw(RenderTarget window)
        {
            this.update();
            this.disparos();
            this.colisao();

            for(int i = 0; i < this.tiros.Length; i++)
            {
                window.Draw(this.tiros[i]);
            }
            for(int i = 0; i < this.navesInimigas.Length; i++)
            {
                window.Draw(this.navesInimigas[i]);
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
            if(active[2] == true)
            {
                window.Draw(this.explosao2[cont2[2]]);

                this.cont2[2] += 1;
                if(this.cont2[2] == 7)
                {
                    this.cont2[2] = 0;
                    this.active[2] = false;
                }
            }
            if(active[3] == true)
            {
                window.Draw(this.explosao3[cont2[3]]);

                this.cont2[3] += 1;
                if(this.cont2[3] == 7)
                {
                    this.cont2[3] = 0;
                    this.active[3] = false;
                }
            }

        }
        public void destruir()
        {
            this.buffer1.Dispose();
            this.explosaoNaveIninimiga.Dispose();

            this.tirosInimigos.Dispose();
            this.buffer2.Dispose();
        }
    }
}