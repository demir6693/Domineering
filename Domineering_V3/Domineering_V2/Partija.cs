using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domineering_V2
{
    class Partija
    {
        bool prvi_potez = true;
        bool potez = true;
        public static int _n;
        List<Plocica> tr_s = new List<Plocica>();
        static List<List<Plocica>> tmp = new List<List<Plocica>>();
        static List<List<Plocica>> poeni = new List<List<Plocica>>();

        public Partija(List<Plocica> ll, int n)
        {
            tr_s = ll;
            _n = n;



            //for(int dubina = 0; dubina < 4 - 1; dubina++)
            //{   
            //    if(prvi_potez) //max
            //    {
            //        tmp = Odigraj_V_prvi(ll);
            //        prvi_potez = false;
            //    }
            //    else if(potez) //min
            //    {
            //        tmp = Odigraj_H(tmp);
            //        potez = false;
            //    }
            //    else if(!potez) //max
            //    {
            //        tmp = Odigraj_V(tmp);
            //        potez = true;
            //    } 
            //}

            //prvi_potez = true;
            alpha_beta(tr_s, 2, int.MinValue, int.MaxValue, true).ToString();
            MessageBox.Show(tmp.Count.ToString());
            
        }

        public int alpha_beta(List<Plocica> tr,int dubina, int alpha, int beta, bool igrac)
        {
            if(pobednik(tr) != 0 || dubina == 0) //ako je terminalni cvor
            {              
                return vrednost_tr_stanja(tr, igrac); 
            }
            
            if (igrac)
            {
                if (prvi_potez)
                {
                    tmp = Odigraj_V_prvi(tr);
                    prvi_potez = false;
                }
                else
                    tmp = Odigraj_V(tmp);

                foreach (List<Plocica> ll in tmp)
                {
                    alpha = Math.Max(alpha, alpha_beta(ll, dubina - 1, alpha, beta, false));
                    if (alpha > beta)
                    {
                        break;
                    }
                }
                return alpha;
            }
            else
            {
                tmp = Odigraj_H(tmp);
                foreach (List<Plocica> ll in tmp)
                {
                    beta = Math.Min(beta, alpha_beta(ll, dubina - 1, alpha, beta, true));
                    if (alpha > beta)
                    {
                        break;
                    }
                }
                return beta;
            }
        }
        
        public List<List<Plocica>> Odigraj_V_prvi(List<Plocica> ll)
        {
            List<List<Plocica>> ll_vh = new List<List<Plocica>>();
            List<Plocica> nd = new List<Plocica>();
            Plocica p = new Plocica();

            for(int i = 0; i < ll.Count; i++)
            {
                if(i < _n * _n - _n)
                {
                    if(ll[i].igrac.Equals("0") && ll[i + _n].igrac.Equals("0"))
                    {
                        nd = new List<Plocica>();
                        for(int l = 0; l < ll.Count; l++)
                        {
                            p = new Plocica();
                            p.igrac = ll[l].igrac;
                            p.x = ll[l].x;
                            p.y = ll[l].y;

                            nd.Add(p);
                        }

                        nd[i].igrac = "V";
                        nd[i + _n].igrac = "V";

                        ll_vh.Add(nd);   
                    }
                }
            }

            return ll_vh;
        }
    
        public List<List<Plocica>> Odigraj_V(List<List<Plocica>> ll_vh)
        {
            List<List<Plocica>> ll_vh1 = new List<List<Plocica>>();
            List<Plocica> nd = new List<Plocica>();
            Plocica p = new Plocica();

            for (int i = 0; i < ll_vh.Count; i++)
            {
                for (int j = 0; j < ll_vh[i].Count; j++)
                {
                    if (j < _n * _n - _n)
                    {
                        if (ll_vh[i][j].igrac.Equals("0") && ll_vh[i][j + _n].igrac.Equals("0"))
                        {
                            nd = new List<Plocica>();
                            for (int l = 0; l < ll_vh[i].Count; l++)
                            {
                                p = new Plocica();
                                p.igrac = ll_vh[i][l].igrac;
                                p.x = ll_vh[i][l].x;
                                p.y = ll_vh[i][l].y;

                                nd.Add(p);
                            }

                            nd[j].igrac = "V";
                            nd[j + _n].igrac = "V";

                            ll_vh1.Add(nd);
                        }
                    }
                }
            }

            return ll_vh1;
        }

        public List<List<Plocica>> Odigraj_H(List<List<Plocica>> ll_vh)
        {
            List<List<Plocica>> ll_vh1 = new List<List<Plocica>>();
            List<Plocica> nd = new List<Plocica>();
            Plocica p = new Plocica();


            for (int i = 0; i < ll_vh.Count; i++)
            {
                for (int j = 0; j < ll_vh[i].Count; j++)
                {                 
                    if (j < ll_vh[i].Count - 1)
                    {
                        if (ll_vh[i][j].igrac.Equals("0") && ll_vh[i][j + 1].igrac.Equals("0") && ll_vh[i][j].x == ll_vh[i][j + 1].x)
                        {
                            nd = new List<Plocica>();
                            for (int l = 0; l < ll_vh[i].Count; l++)
                            {
                                p = new Plocica();
                                p.igrac = ll_vh[i][l].igrac;
                                p.x = ll_vh[i][l].x;
                                p.y = ll_vh[i][l].y;

                                nd.Add(p);
                            }

                            nd[j].igrac = "H";
                            nd[j + 1].igrac = "H";

                            ll_vh1.Add(nd);
                        }
                    }
                }                   
            }

            return ll_vh1;
        }

        public int vrednost_tr_stanja(List<Plocica> ll, bool igrac)
        {
            if(igrac)
            {
                return sl_polja_V(ll);
            }
            else
            {
                return sl_polja_H(ll);
            }
        }

        public int sl_polja_V(List<Plocica> ll)
        {
            int hr = 0;

            for (int i = 0; i < ll.Count; i++)
            {
                if (i < _n * _n - _n)
                {
                    if (ll[i].igrac.Equals("0") && ll[i + _n].igrac.Equals("0"))
                    {
                        hr++;
                    }
                }
            }

            return hr;
        }

        public int sl_polja_H(List<Plocica> ll)
        {
            int hr = 0;

            for (int i = 0; i < ll.Count; i++)
            {
                if (i < ll.Count - 1)
                {
                    if (ll[i].igrac.Equals("0") && ll[i + 1].igrac.Equals("0") && ll[i].x == ll[i + 1].x)
                    {
                        hr++;
                    }
                }
            }

            return hr;
        }

        public int pobednik(List<Plocica> ll_vh)
        {
            if(pobednik_V(ll_vh) == 1)
            {
                return 1;
            }
            else if(pobednik_H(ll_vh) == 2)
            {
                return 2;
            }
            else
                return 0;
            
        }

        public int pobednik_V(List<Plocica> ll_vh)
        {
            int rez = 1;

            for(int i = 0; i < ll_vh.Count; i++)
            {              
                if (i < ll_vh.Count - 1)
                {
                    if (ll_vh[i].igrac.Equals("0") && ll_vh[i + 1].igrac.Equals("0") && ll_vh[i].x == ll_vh[i + 1].x)
                    {
                        rez = 0;
                    }
                }                   
            }

            return rez;
        }

        public int pobednik_H(List<Plocica> ll_vh)
        {
            int rez = 2;

            for (int i = 0; i < ll_vh.Count; i++)
            {
                if (i < _n * _n - _n)
                {
                    if (ll_vh[i].igrac.Equals("0") && ll_vh[i + _n].igrac.Equals("0"))
                    {
                        rez = 0;
                    }
                }              
            }

            return rez;
        }
        


        void crtaj()
        {
            foreach(List<Plocica> tmp in tmp)
            {
                int i = 0;

                StringBuilder sb = new StringBuilder();
                foreach (Plocica pl in tmp)
                {
                    if (i % _n == 0)
                    {
                        sb.Append("\n");
                        i = 0;
                    }
                    i++;

                    sb.Append(pl.ToString());
                }
                MessageBox.Show(sb.ToString());
                sb.Clear();
            }            
        }

        void crtaj_one(List<Plocica> ll_vh)
        {
                int i = 0;

                StringBuilder sb = new StringBuilder();
                foreach(Plocica pl in ll_vh)
                {
                    if (i % _n == 0)
                    {
                        sb.Append("\n");
                        i = 0;
                    }
                    i++;

                    sb.Append(pl.ToString());
                }
                MessageBox.Show(sb.ToString());
                sb.Clear();           
        }
    }
}
