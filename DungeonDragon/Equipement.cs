using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DungeonDragon
{
    public class Equipement
    {
        public struct StructEquipement
        {
            public int id;
            public string categorie;
            public string nom;
            public string classe;
            public int ptsAttDefVie;
        }

        private Dictionary<int, StructEquipement> dictEquipements = new Dictionary<int, StructEquipement>();

        private int id;
        private string categorie;
        private string nom;
        private string classe;
        private int ptsAttDefVie;

        public bool ChargerFichier(string nomFichier)
        {
            string line;
            string[] lineSplit;
            StructEquipement equipement;

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
                    equipement.id = int.Parse(lineSplit[0]);
                    equipement.categorie = lineSplit[1];
                    equipement.nom = lineSplit[2];
                    equipement.classe = lineSplit[3];
                    equipement.ptsAttDefVie = int.Parse(lineSplit[4]);
                    dictEquipements.Add(equipement.id, equipement);
                }
            }
            return true;
        }

        public List<string> OuvrirCoffre(int contenuCoffre)
        {
            List<string> descEquipement = new List<string>();

            descEquipement.Add(dictEquipements[contenuCoffre].categorie);
            descEquipement.Add(dictEquipements[contenuCoffre].nom);
            descEquipement.Add(dictEquipements[contenuCoffre].classe);
            descEquipement.Add(dictEquipements[contenuCoffre].ptsAttDefVie.ToString());

            Console.WriteLine($"\nVous avez trouvé: {descEquipement[1]}");

            return descEquipement;
        }
    }
}