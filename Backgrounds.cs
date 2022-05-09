using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace My_Game
{
     class Background
    {
        private Sprite []nebulosas;
        private Sprite[] estrelas;
        private Sprite fundo;
        private byte tom = 50;
        private float folha1 = -420;
        private float folha2 = -1920;
        private float folhaU1 = -420;
        private float folhaU2 = -1920;
        private const int HEIGHT = 1080;
        public Background()
        {
            // Texturas odysseia = new Texturas("odysseia_space.png");
            Texturas odysseia = new Texturas("Scene-1.jpg");
            fundo = new Sprite();
            fundo.Color = new Color(255, 255, 255, 100);
            fundo.Texture = odysseia.texture;
            // fundo.Scale = new Vector2f(0.65f, 0.8f);

            // Nebulosas
            Texturas background = new Texturas("universo.png");
            nebulosas = new Sprite[2];
            for(int i = 0;i < nebulosas.Length; i++)
            {
                nebulosas[i] = new Sprite();
                nebulosas[i].Texture = background.texture;
                nebulosas[i].Color = new Color(255, 255, 255, 120);
                nebulosas[i].Scale = new Vector2f(0.65f, 1f);
            }


            // estrelas
            Texturas stars = new Texturas("Space_stars.png");
            estrelas = new Sprite[2];
            for(int i = 0;i < estrelas.Length; i++)
            {
                estrelas[i] = new Sprite();
                estrelas[i].Texture = stars.texture;
                estrelas[i].Scale = new Vector2f(0.65f, 1f);

            }

        }
        private int cont = 0;
        public void brilhoUniverse()
        {
            if(cont < 100)
            {
                this.tom += 1;
            }
            if(cont > 100)
            {
                this.tom -= 1;
            }
            if(cont == 200)
            {
                cont = 0;
            }
            this.fundo.Color = new Color(255, 255, 255, this.tom);
            cont++;
        }
        public void UpdateNubulosas(){
            var vel = 0.2f;
            this.nebulosas[0].Position = new Vector2f(0f, folhaU1);
            this.folhaU1 += vel;

            this.nebulosas[1].Position = new Vector2f(0f, folhaU2);
            this.folhaU2 += vel;
            
            if(folhaU1 > HEIGHT) {
                this.folhaU1 = -1920;
            }
            if(folhaU2 > HEIGHT) {
                this.folhaU2 = -1920;
            }


        }
        public void UpdateStars(){
            var vel = 1f;
            this.estrelas[0].Position = new Vector2f(0f, folha1);
            this.folha1 += vel;

            this.estrelas[1].Position = new Vector2f(0f, folha2);
            this.folha2 += vel;
            
            if(folha1 > HEIGHT) {
                this.folha1 = -1920;
            }
            if(folha2 > HEIGHT) {
                this.folha2 = -1920;
            }
        }
        public void draw(RenderTarget window)
        {
            brilhoUniverse();
            window.Draw(this.fundo);

            this.UpdateStars();
            this.UpdateNubulosas();

            window.Draw(this.nebulosas[0]);
            window.Draw(this.nebulosas[1]);

            window.Draw(this.estrelas[0]);
            window.Draw(this.estrelas[1]);
        }
    }
}