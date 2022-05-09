using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace My_Game
{
     class Pontuacao
    {
        private Sprite[] estrelas;
        private float folha1 = -420;
        private float folha2 = -1920;
        private const int HEIGHT = 1080;
        private Text texto;
        public Pontuacao()
        {

            // estrelas
            Texturas stars = new Texturas("Space_stars.png");
            estrelas = new Sprite[2];
            for(int i = 0;i < estrelas.Length; i++)
            {
                estrelas[i] = new Sprite();
                estrelas[i].Texture = stars.texture;
                estrelas[i].Scale = new Vector2f(0.65f, 1f);

            }

            Font fonte = new Font("common/arial.ttf");
            texto = new Text("Pontuacão : ", fonte);
            texto.CharacterSize = 30;
            texto.FillColor = new Color(Color.Blue);
            texto.Position = new Vector2f(1520f, 1030f);
        }

        public void UpdateStars(){
            var vel = 5f;
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
            this.UpdateStars();

            window.Draw(this.estrelas[0]);
            window.Draw(this.estrelas[1]);

            this.texto.DisplayedString = "NAVES DESTRUIDAS : " + Posicao.pontos;
            window.Draw(this.texto );
        }
    }
}