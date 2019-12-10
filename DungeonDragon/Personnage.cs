using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonDragon
{
    public class Personnage
    {
        private string nom;
        private int ptsVie;
        private int ptsAttaque;
        private int ptsDefense;
        private bool modeDefense;

        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }

        public int PtsVie
        {
            get { return this.ptsVie; }
            set { this.ptsVie = value; }
        }

        public int PtsAttaque
        {
            get { return this.ptsAttaque; }
            set { this.ptsAttaque = value; }
        }

        public int PtsDefense
        {
            get { return this.ptsDefense; }
            set { this.ptsDefense = value; }
        }

        public bool ModeDefense
        {
            get { return this.modeDefense; }
            set { this.modeDefense = value; }
        }

        public Personnage(string nom, int ptsVie, int ptsAttaque, int ptsDefense)
        {
            this.nom = nom;
            this.ptsVie = ptsVie;
            this.ptsAttaque = ptsAttaque;
            this.ptsDefense = ptsDefense;
            this.modeDefense = false;
        }

        public Personnage()
        {
            this.nom = "";
            this.ptsVie = 0;
            this.ptsAttaque = 0;
            this.ptsDefense = 0;
            this.modeDefense = false;
        }

        public virtual void AfficherInfo()
        {
            Console.WriteLine($"*****************************************************");
            Console.WriteLine($"             {this.nom}");
            return;
        }

        public void SubirDegats(int nbrDegats)
        {
            this.PtsVie -= nbrDegats;
        }

        public void Attaquer()
        {
            throw new NotImplementedException();
        }

        public void Defendre()
        {
            throw new NotImplementedException();
        }
    }
}