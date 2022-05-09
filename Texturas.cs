using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;

namespace My_Game
{
    class Texturas
    {
        public Texture texture;
        private string path = "Sprites/";
        
        public Texturas(string file){

            texture = new Texture(path + file);
            
        }
    }
}