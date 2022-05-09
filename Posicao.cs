using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
using System.Numerics;

namespace My_Game
{
    static class Posicao
    {
        public static Vector4 staticExplosao { get; set; }
        public static Vector4 staticPosPersonagem { get; set; }
        public static Vector4 []staticBalasPersonagem { get; set; }

        public static Vector4 []staticNavesInimigas { get; set; }
        public static Vector4 []staticBalasNaves { get; set; }

        public static bool RenderExplosao {get; set;}

        public static string path = "sounds/";
        public static int WIDTH { get;}
        public static int HEIGHT { get; }
        public static int pontos { get; set;}
        static Posicao(){
            pontos = 0;

            staticBalasPersonagem = new Vector4[128];

            staticNavesInimigas = new Vector4[160];

            staticBalasNaves = new Vector4[160];

            RenderExplosao = false;

            WIDTH = 1920;
            HEIGHT = 1080;

        }
        public static bool colisoes(Vector4 boxA, Vector4 boxB)
        {
            return (!(boxA.X > boxB.Z || boxA.Y > boxB.W || boxA.Z < boxB.X || boxA.W < boxB.Y));
        }
    }
}