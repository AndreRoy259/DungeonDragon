using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonDragon
{
    public struct structEquipement
    {
        public string categorie;
        public string nom;
        public string classe;
        public int ptsAttDefVie;
    }

    public class Heros : Personnage
    {
        private string classe;
        private int ptsMana;
        private int nbrPotion;
        private int ptsAttaqueMagique;
        private int coutAttaqueMagique;

        public int PtsAttaqueMagique
        {
            get { return ptsAttaqueMagique; }
        }

        public int CoutAttaqueMagique
        {
            get { return coutAttaqueMagique; }
        }

        public int NbrPotion
        {
            get { return this.nbrPotion; }
            set { this.nbrPotion = value; }
        }

        public int PtsMana
        {
            get { return this.ptsMana; }
            set { this.ptsMana = value; }
        }

        public Heros(string nom, int ptsVie, int ptsAttaque, int ptsDefense, string classe, int ptsMana, int nbrPotion) : base(nom, ptsVie, ptsAttaque, ptsDefense)
        {
            this.classe = classe;
            this.ptsMana = ptsMana;
            this.nbrPotion = nbrPotion;
            switch (this.classe)
            {
                case "Guerrier":
                    ptsAttaqueMagique = 10;
                    coutAttaqueMagique = 20;
                    break;

                case "Magicien":
                    ptsAttaqueMagique = 10;
                    coutAttaqueMagique = 20;
                    break;

                default:
                    ptsAttaqueMagique = 0;
                    coutAttaqueMagique = 0;
                    break;
            }
        }

        public void RegenererPtsVie(int nbrPoints)
        {
            this.PtsVie += nbrPoints;
            this.nbrPotion--;
            Console.WriteLine($"La potion vous à redonnée {nbrPoints} points de vie...");
        }

        public void RegenererPtsMana(int nbrPoints)
        {
            this.ptsMana += nbrPoints;
            this.nbrPotion--;
            Console.WriteLine($"La potion vous à redonnée {nbrPoints} points de mana...");
        }

        public void AttaqueSpeciale()
        {
            this.ptsMana -= this.coutAttaqueMagique;
        }

        public override void AfficherInfo()
        {
            base.AfficherInfo();
            Console.WriteLine($"*****************************************************");
            Console.WriteLine($"             Classe: {this.classe}");
            Console.WriteLine($"             Points de vie: {this.PtsVie}");
            Console.WriteLine($"             Points de mana: {this.ptsMana}");
            Console.WriteLine($"             Points d'attaque: {this.PtsAttaque}");
            Console.WriteLine($"             Points de defense: {this.PtsDefense}");
            Console.WriteLine($"             Nombre de potion: {this.nbrPotion}\n");
        }

        public bool Equiper(List<string> equipementAPorter)
        {
            structEquipement equipement;

            equipement.categorie = equipementAPorter[0];
            equipement.nom = equipementAPorter[1];
            if (equipementAPorter[2] == "mage")
            {
                equipement.classe = "magicien";
            }
            else
            {
                equipement.classe = equipementAPorter[2];
            }
            equipement.ptsAttDefVie = int.Parse(equipementAPorter[3]);

            if (equipement.classe.ToLower() == this.classe.ToLower())
            {
                switch (equipement.categorie)
                {
                    case "arme":
                        this.PtsAttaque = equipement.ptsAttDefVie;
                        Console.WriteLine($"Vous avez équipé l'arme, votre attque sera maintenant de {equipement.ptsAttDefVie}...");
                        break;

                    case "armure":
                        this.PtsDefense = equipement.ptsAttDefVie;
                        Console.WriteLine($"Vous avez équipé l'armure, votre défense sera maintenant de {equipement.ptsAttDefVie}...");
                        break;
                }
            }
            else if (equipement.classe == "general")
            {
                this.nbrPotion++;
            }
            else
            {
                Console.WriteLine($"Vous ne pouvez pas équiper cette objet car elle est réservé au {equipement.classe}...");
                return false;
            }

            return true;
        }
    }
}