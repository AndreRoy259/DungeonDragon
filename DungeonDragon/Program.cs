using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDragon
{
    internal class Program
    {
        private const int NBR_DE_FACES_SUR_DE = 3;
        private const string FICHIER_EQUIPEMENTS = "equipement.csv";
        private const string FICHIER_MONSTRES = "monstre.csv";
        private const string FICHIER_CARTES = "carte.csv";

        public struct monstre
        {
            public int id;
            public string nom;
            public int ptsAttaque;
            public int ptsDefense;
            public int ptsVie;
        }

        public struct carte
        {
            public int numCase;
            public string typeCase;
            public int id;
        }

        public struct equipement
        {
            public int id;
            public string categorie;
            public string nom;
            public string classe;
            public int ptsAttDefVie;
        }

        private static Heros FaireCombat(Heros heros, Monstre monstre)
        {
            return heros;
        }

        private static void FaireUnePartie()
        {
            Jeu jeu = new Jeu();
            Equipement equipement = new Equipement();
            Monstre monstre = new Monstre();

            int error;
            string[] personnage = new string[2];
            string[] salle = new string[2];
            string nomHeros = "";
            int ptsVieHeros;
            int ptsAttaqueHeros;
            int ptsDefenseHeros;
            string classeHeros = "";
            int nbrPotionHeros;
            int ptsManaHeros;
            int lanceDe;
            int evenement;
            carte carteActuelle;
            List<string> equipementTrouve = new List<string>();

            if (!jeu.ChargerFichier(FICHIER_CARTES))
            {
                Console.WriteLine("Le fichier carte est manquant");
                Console.Write("Appuyez une touche pour continuer...");
                Console.ReadKey();
            }
            if (!monstre.ChargerFichier(FICHIER_MONSTRES))
            {
                Console.WriteLine("Le fichier monstre est manquant");
                Console.Write("Appuyez une touche pour continuer...");
                Console.ReadKey();
            }

            if (!equipement.ChargerFichier(FICHIER_EQUIPEMENTS))
            {
                Console.WriteLine("Le fichier equipement est manquant");
                Console.Write("Appuyez une touche pour continuer...");
                Console.ReadKey();
            }

            personnage = jeu.CreerPersonnage();
            classeHeros = personnage[0];
            nomHeros = personnage[1];
            nbrPotionHeros = 2;

            if (classeHeros == "Guerrier")
            {
                ptsVieHeros = 200;
                ptsManaHeros = 100;
                ptsAttaqueHeros = 5;
                ptsDefenseHeros = 4;
            }
            else if (classeHeros == "Magicien")
            {
                ptsVieHeros = 100;
                ptsManaHeros = 200;
                ptsAttaqueHeros = 3;
                ptsDefenseHeros = 6;
            }
            else
            {
                Console.WriteLine("Erreure lors de la création du personnage.");
                Console.Write("Appuyez une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Heros heros = new Heros(nomHeros, ptsVieHeros, ptsAttaqueHeros, ptsDefenseHeros, classeHeros, ptsManaHeros, nbrPotionHeros);

            while (!jeu.gameOver)
            {
                Console.Clear();
                heros.AfficherInfo();

                jeu.AfficherMenuJeu(); // TODO: Fonction vide pour l'instant
                lanceDe = jeu.LancerDes(NBR_DE_FACES_SUR_DE, true);
                salle = jeu.Avancer(lanceDe);
                carteActuelle.typeCase = salle[0];
                carteActuelle.id = int.Parse(salle[1]);
                Console.WriteLine("*****************************************************");
                Console.WriteLine($"\nVous vous aventurez dans la salle #{jeu.caseActuelle}");
                Console.WriteLine("Découvrons ce qu'elle contient...");
                Console.ReadKey();
                switch (carteActuelle.typeCase)
                {
                    case "vide":
                        Console.WriteLine("*****************************************************");
                        Console.Write("Soulagement, la pièce vide...");
                        Console.ReadKey();
                        break;

                    case "coffre":
                        Console.WriteLine("*****************************************************");
                        Console.Write("Que de chance! Un coffre... Ouvrez le...");
                        Console.ReadKey();
                        equipementTrouve = equipement.OuvrirCoffre(carteActuelle.id);
                        heros.Equiper(equipementTrouve);
                        Console.ReadKey();
                        break;

                    case "monstre":
                        Console.WriteLine("*****************************************************");
                        Console.WriteLine("Oh non! Un monstre!"); // TODO: Améliorer texte
                        Console.Write("Préparez-vous au combat...");
                        Console.ReadKey();
                        monstre.CreerMonstre(carteActuelle.id);
                        heros = jeu.Combat(heros, monstre);
                        if (heros.PtsVie <= 0)
                        {
                            Console.WriteLine("Vous etes mort!"); // TODO: Améliorer texte
                            Console.ReadKey();
                            jeu.gameOver = true;
                        }
                        Console.ReadKey();
                        break;

                    case "sortie":
                        Console.Write("Sortie!");
                        Console.ReadKey();
                        jeu.gameOver = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private static void AfficherChoixInvalide()
        {
            Console.WriteLine("\nChoix invalide!");
            Console.Write("Appuyez une touche pour continuer...");
            Console.ReadKey();
        }

        private static void AfficherEnConstruction()
        {
            Console.WriteLine("\nCette option en toujours en construction!");
            Console.Write("Appuyez une touche pour continuer...");
            Console.ReadKey();
        }

        private static void Main(string[] args)
        {
            Jeu jeu = new Jeu();
            string choix;

            while (!jeu.quitter)
            {
                jeu.AfficherMenuPrincipal();
                choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        FaireUnePartie();
                        break;

                    case "2":
                        AfficherEnConstruction();
                        break;

                    case "3":
                        AfficherEnConstruction();
                        break;

                    case "4":
                        AfficherEnConstruction();
                        break;

                    case "0":
                        jeu.quitter = true;
                        break;

                    default:
                        AfficherChoixInvalide();
                        break;
                }
            }
        }
    }
}