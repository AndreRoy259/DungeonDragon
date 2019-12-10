using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DungeonDragon
{
    public class Monstre : Personnage
    {
        public struct StructMonstre
        {
            public int id;
            public string nom;
            public int ptsAttaque;
            public int ptsDefense;
            public int ptsVie;
        }

        private Dictionary<int, StructMonstre> dictMonstres = new Dictionary<int, StructMonstre>();

        public Monstre(string nom, int ptsVie, int ptsAttaque, int ptsDefense) : base(nom, ptsVie, ptsAttaque, ptsDefense)
        {
            throw new NotImplementedException();
        }

        public Monstre()
        {
            this.Nom = "";
            this.PtsVie = 0;
            this.PtsAttaque = 0;
            this.PtsDefense = 0;
        }

        public bool ChargerFichier(string nomFichier)
        {
            string line;
            string[] lineSplit;
            StructMonstre monstre;

            if (!File.Exists(nomFichier))
            {
                return false;
            }

            using (StreamReader fluxLecture = File.OpenText(nomFichier))
            {
                while (!fluxLecture.EndOfStream)
                {
                    line = fluxLecture.ReadLine();
                    lineSplit = line.Split(',');
                    monstre.id = int.Parse(lineSplit[0]);
                    monstre.nom = lineSplit[1];
                    monstre.ptsAttaque = int.Parse(lineSplit[2]);
                    monstre.ptsDefense = int.Parse(lineSplit[3]);
                    monstre.ptsVie = int.Parse(lineSplit[4]);
                    dictMonstres.Add(monstre.id, monstre);
                }
            }
            return true;
        }

        public void CreerMonstre(int typeMonstre)
        {
            this.Nom = dictMonstres[typeMonstre].nom;
            this.PtsVie = dictMonstres[typeMonstre].ptsVie;
            this.PtsAttaque = dictMonstres[typeMonstre].ptsAttaque;
            this.PtsDefense = dictMonstres[typeMonstre].ptsDefense;

            return;
        }

        public override void AfficherInfo()
        {
            Console.WriteLine("*****************************************************");
            Console.WriteLine("                         VS");
            Console.WriteLine("*****************************************************");
            Console.WriteLine($"             {this.Nom}");
            Console.WriteLine("*****************************************************");
            Console.WriteLine($"             Points de vie: {this.PtsVie}");
            Console.WriteLine($"             Points d'attaque: {this.PtsAttaque}");
            Console.WriteLine($"             Points de defense: {this.PtsDefense}\n");
            return;
        }
    }
}