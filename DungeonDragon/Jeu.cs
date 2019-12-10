using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DungeonDragon
{
    public class Jeu
    {
        private struct StructCarte
        {
            public int numCase;
            public string typeCase;
            public int idEquipement;
        }

        public bool gameOver = false;
        public bool quitter = false;
        public int caseActuelle = 0;
        private Dictionary<int, StructCarte> dictCartes = new Dictionary<int, StructCarte>();

        private static void AfficherChoixInvalide()
        {
            Console.WriteLine("Choix invalide!");
            Console.Write("Appuyez une touche pour continuer...");
            Console.ReadKey();
        }

        public bool ChargerFichier(string nomFichier)
        {
            string line;
            string[] lineSplit;
            StructCarte carte;

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
                    carte.numCase = int.Parse(lineSplit[0]);
                    carte.typeCase = lineSplit[1];
                    carte.idEquipement = int.Parse(lineSplit[2]);
                    dictCartes.Add(carte.numCase, carte);
                }
            }
            return true;
        }

        public void AfficherMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("***************************");
            Console.WriteLine("     Dungeon & Dragon      ");
            Console.WriteLine("***************************");
            Console.WriteLine("  1) Commencer une partie  ");
            Console.WriteLine("  2) Créateur de dungeon");
            Console.WriteLine("  3) Créateur de monstre");
            Console.WriteLine("  4) Créateur d'équipement");
            Console.WriteLine("\n  0) Quitter");
            Console.WriteLine("***************************");
            Console.Write("  Entrez votre choix: ");
        }

        public string[] CreerPersonnage()
        {
            string[] personnage = new string[2];
            bool quitter = false;

            while (!quitter)
            {
                string classe;
                string nom;

                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine(" Création de personnage");
                Console.WriteLine("************************");
                Console.WriteLine(" 1) Guerrier");
                Console.WriteLine(" Points de vie: 200");
                Console.WriteLine(" Point de magie: 100");
                Console.WriteLine(" Points d'attaque: 5");
                Console.WriteLine(" Points de défense: 4");
                Console.WriteLine("************************");
                Console.WriteLine(" 2) Magicien");
                Console.WriteLine(" Points de vie: 100");
                Console.WriteLine(" Point de magie: 200");
                Console.WriteLine(" Points d'attaque: 3");
                Console.WriteLine(" Points de défense: 6");
                Console.WriteLine("************************");
                Console.Write("\n Entrez-votre choix: ");
                classe = Console.ReadLine();
                if (classe == "1")
                {
                    personnage[0] = "Guerrier";
                    Console.Write(" Entrez le nom de votre héros: ");
                    nom = Console.ReadLine();
                    personnage[1] = nom;
                    quitter = true;
                }
                else if (classe == "2")
                {
                    personnage[0] = "Magicien";
                    Console.Write(" Entrez le nom de votre héros: ");
                    nom = Console.ReadLine();
                    personnage[1] = nom;
                    quitter = true;
                }
                else
                {
                    AfficherChoixInvalide();
                }
            }

            return personnage;
        }

        public void AfficherMenuJeu()
        {
            return;
        }

        public void AfficherMenuCombat()
        {
            Console.WriteLine("*****************************************************");
            Console.WriteLine("             Votre tour...");
            Console.WriteLine("             1) Attaquer");
            Console.WriteLine("             2) Défendre");
            Console.WriteLine("             3) Boire une potion");
            Console.Write("             Entrez-votre choix: ");

            return;
        }

        public int LancerDes(int nbrFaces, bool afficher)
        {
            int de;

            Random aleatoire = new Random();
            de = aleatoire.Next(1, nbrFaces + 1);
            if (afficher)
            {
                Console.WriteLine($"*****************************************************");
                Console.WriteLine($" Appuyez une touche pour lancer le dé à {nbrFaces} faces...");
                Console.ReadKey();
                Console.WriteLine($"             Vous avez lancé un {de}");
            }

            return de;
        }

        public string[] Avancer(int nbrCase)
        {
            string[] cartePigee = new string[2];

            caseActuelle += nbrCase;
            if (caseActuelle > dictCartes.Count)
            {
                caseActuelle = dictCartes.Count;
            }
            cartePigee[0] = dictCartes[caseActuelle].typeCase;
            cartePigee[1] = dictCartes[caseActuelle].idEquipement.ToString();

            return cartePigee;
        }

        public Heros Combat(Heros heros, Monstre monstre)
        {
            string choix;

            /*            heros.PtsVie = 10; // TODO - Remove, for short combat
                        heros.PtsMana = 10; // TODO - Remove, for short combat
            */
            while (monstre.PtsVie > 0 && heros.PtsVie > 0)
            {
                Console.Clear();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("                       COMBAT");
                heros.AfficherInfo();
                monstre.AfficherInfo();
                int ptsDegats;

                AfficherMenuCombat();

                choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":
                        heros.ModeDefense = false;
                        Console.WriteLine("*****************************************************");
                        Console.WriteLine("             1) Attaquer standard");
                        Console.WriteLine($"             2) Attaque spéciale (Cout: {heros.CoutAttaqueMagique} points de mana)");
                        Console.Write("             Entrez-votre choix: ");
                        choix = Console.ReadLine();

                        switch (choix)
                        {
                            case "1":
                                if (monstre.ModeDefense)
                                {
                                    ptsDegats = heros.PtsAttaque - monstre.PtsDefense;
                                }
                                else
                                {
                                    ptsDegats = heros.PtsAttaque;
                                }

                                break;

                            case "2":
                                if (heros.PtsMana >= heros.CoutAttaqueMagique)
                                {
                                    if (monstre.ModeDefense)
                                    {
                                        ptsDegats = heros.PtsAttaqueMagique - monstre.PtsDefense;
                                    }
                                    else
                                    {
                                        ptsDegats = heros.PtsAttaqueMagique;
                                    }
                                    heros.AttaqueSpeciale();
                                }
                                else
                                {
                                    Console.WriteLine($"{heros.Nom} n'a pas assez de points de mana.");
                                    ptsDegats = 0;
                                }
                                break;

                            default:
                                AfficherChoixInvalide();
                                ptsDegats = 0;
                                break;
                        }

                        if (ptsDegats < 0)
                        {
                            ptsDegats = 0;
                        }

                        monstre.SubirDegats(ptsDegats);
                        Console.WriteLine($"\n{heros.Nom} attaque et fait {ptsDegats} de dégats à {monstre.Nom}");
                        break;

                    case "2":
                        heros.ModeDefense = true;
                        Console.WriteLine($"\n{heros.Nom} se prépare à se défendre.");
                        break;

                    case "3":
                        if (heros.NbrPotion > 0)
                        {
                            Console.WriteLine("1) Regénéger points de vie");
                            Console.WriteLine("2) Regénéger points de mana");

                            Console.Write("\nEntrez-votre choix: ");
                            choix = Console.ReadLine();

                            switch (choix)
                            {
                                case "1":
                                    heros.RegenererPtsVie(50);
                                    break;

                                case "2":
                                    heros.RegenererPtsMana(50);
                                    break;

                                default:
                                    AfficherChoixInvalide();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vous n'avez plus de potion!");
                        }
                        break;

                    default:
                        AfficherChoixInvalide();
                        break;
                }

                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("*****************************************************");
                Console.WriteLine("                       COMBAT");
                heros.AfficherInfo();
                monstre.AfficherInfo();
                Console.WriteLine("Tour du monstre...");

                switch (LancerDes(2, false))
                {
                    case 1:
                        monstre.ModeDefense = false;

                        if (heros.ModeDefense)
                        {
                            ptsDegats = monstre.PtsAttaque - heros.PtsDefense;
                        }
                        else
                        {
                            ptsDegats = monstre.PtsAttaque;
                        }

                        if (ptsDegats < 0)
                        {
                            ptsDegats = 0;
                        }

                        heros.SubirDegats(ptsDegats);
                        Console.WriteLine($"\n{monstre.Nom} attaque et fait {ptsDegats} de dégats à {heros.Nom}");

                        break;

                    case 2:
                        Console.WriteLine($"\n{monstre.Nom} se prépare à se défendre.");
                        monstre.ModeDefense = true;
                        break;
                }

                Console.ReadKey();
            }

            return heros;
        }

        public void Sortir()
        {
            throw new NotImplementedException();
        }
    }
}