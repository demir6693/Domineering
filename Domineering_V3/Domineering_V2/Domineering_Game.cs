using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domineering_V2
{   

    public partial class Domineering_Game : Form
    {
        public static int n_pom;
        List<Plocica> trenutno_stanje = new List<Plocica>();
        public Domineering_Game(int n)
        {
            InitializeComponent();
            n_pom = n;
            int x_size = n_pom * (50 + 5) + 50;
            int y_size = n_pom * (50 + 5) + 50;
            this.Size = new Size(x_size, y_size);
        }

        private void Domineering_Game_Load(object sender, EventArgs e)
        {
            int top = 25;
            int left = 25;
            for (int i = 0; i < n_pom; i++)
            {
                for (int j = 0; j < n_pom; j++)
                {
                    Button button = new Button();
                    button.Height = 50;
                    button.Width = 50;
                    button.BackColor = Color.Blue;
                    string bt_name = j + "," + i;
                    button.Name = bt_name;
                    button.Text = "0";
                    button.ForeColor = Color.Blue;
                    button.Click += new EventHandler(this.Klik);

                    button.Left = left;
                    button.Top = top;
                    this.Controls.Add(button);
                    top += button.Height + 2;
                }
                left += 55;
                top = 25;
            }
        }

        void Klik(Object sender, EventArgs e)
        {
            Button click_btn = (Button)sender;

            if (dozvoljeno_stanje_H(click_btn.Name))
            {
                crvena(click_btn.Name);
                click_btn.BackColor = Color.Orange;
                click_btn.ForeColor = Color.Orange;
                click_btn.Text = "H";
                Trenutno_stanje();


            }
            else
            {
                MessageBox.Show("Nedozvoljen potez!");
            }
        }

        void crvena(String name)
        {
            int index_red = int.Parse(name[2].ToString()) + 1;
            string new_index = name[0].ToString() + name[1] + index_red.ToString() + "";

            this.Controls[new_index].BackColor = Color.Orange;
            this.Controls[new_index].ForeColor = Color.Orange;
            this.Controls[new_index].Text = "H";
        }

        bool dozvoljeno_stanje_H(string name)
        {
            bool stanje = true;

            int x = int.Parse(name[0].ToString());
            int y = int.Parse(name[2].ToString());
            int y_tmp = y + 1;

            if(!this.Controls[x+","+y].Text.Equals("0") || y == n_pom -1)
            {   
                    return false;                    
            }
            else if(y < n_pom -1)
            {
                if (!this.Controls[x + "," + y_tmp].Text.Equals("0"))
                    return false;
            }

            return stanje;
        }

        public void Trenutno_stanje()
        {
            List<Plocica> tabela_stanja = new List<Plocica>();

            for (int i = 0; i < n_pom; i++)
            {
                for (int j = 0; j < n_pom; j++)
                {
                    Plocica pl = new Plocica();

                    string index = (i + "," + j).ToString();
                    pl.igrac = this.Controls[index].Text;
                    pl.x = int.Parse(index[0].ToString());
                    pl.y = int.Parse(index[2].ToString());

                    tabela_stanja.Add(pl);
                }
            }
            trenutno_stanje = tabela_stanja;


            Partija prt = new Partija(trenutno_stanje, n_pom);
       
        }

        
    }
}
