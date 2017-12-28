using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamVigenere
{
    class Program
    {
        static void Main(string[] args)
        {
            int indice = 0; int Plettre = 0; int Dlettre = 0;
            string colettre = ""; string test = ""; string phrase = "";
            int indiph = 0; int i = 0; int z = 0; int coz = 0;
            string Recupercogi; string alpha = "abcdefghijklmnopqrstuvwxyz";
            //string cogito = "cogito";
            Console.Write("Votre mot de passe : ");
            string cogito = Console.ReadLine();

            Console.Write("Phrase à crypter : ");
            phrase = Console.ReadLine();

            while (indiph < phrase.Length)
            {
                Recupercogi = cogito[coz].ToString();

                while (alpha[z].ToString() != Recupercogi)
                {
                    z = z + 1;
                }
                test = phrase[indiph].ToString();

                if (test == " ")
                {
                    indice = indiph + 1;
                    colettre = colettre + " ";
                }
                else
                {
                    while (alpha[i].ToString() != test)
                    {
                        i = i + 1;
                    }
                    i = i + z;
                    if (i >= 26)
                    {
                        i = i - 26;
                    }
                    colettre = colettre + alpha[i].ToString();
                    z = 0;
                    i = 0;
                    indiph = indiph + 1;

                    coz = coz + 1;
                    if (coz >= 6)
                    {
                        coz = 0;
                    }
                }
            }
            Console.WriteLine("Message crypté : " + colettre);

            Console.ReadKey();
        }
    }
}
