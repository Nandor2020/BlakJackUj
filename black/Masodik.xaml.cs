using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace black
{
    /// <summary>
    /// Interaction logic for Masodik.xaml
    /// </summary>
    public partial class Masodik : Window
    {
        int sajato, hazo, tet, lap = 0;
        int penzo = 2000;
        Random r = new Random();
        int[] hazlap = { 0, 0, 0, 0, 0, 0, 0 };
        int[] sajatlap = { 0, 0, 0, 0, 0, 0, 0 };
        int i = 2;
        int x = 1;
        int y = 2;
        public void DoubleEna()
        {
            Double.IsEnabled = true;
            if (penzo - tet < 0)
            {
                Double.IsEnabled = false;
            }
        }
        public void ujlap()
        {
            lap = r.Next(1, 14);
            if (lap == 1)
            {
                //ász
                lap = 1;
            }
            else if (lap > 10)
            {
                lap = 10;
            }
        }
        public void huszonegy(int win)
        {
            string text = "";
            if (win == 0)
            {
                text = "Nyertél :)";
                penzoL.Content = penzo += tet * 2;
            }
            else if (win == 1)
            {
                text = "Vesztettél :(";
            }
            else if (win == 2)
            {
                text = "Push :|";
                penzoL.Content = penzo += tet;
            }
            else if (win == 3)
            {
                text = "Háznak BlackJack";
            }
            else if (win == 4)
            {
                text = "Neked BlackJack";
                penzoL.Content = penzo += tet * tet/2;
            }
            MessageBox.Show(text);

            /*MainWindow subWindow = new MainWindow();
            subWindow.Show();
            MasodikW.Close();*/
            Double.Visibility = Visibility.Collapsed;
            Hit.Visibility = Visibility.Collapsed;
            Stand.Visibility = Visibility.Collapsed;
            submit.Visibility = Visibility.Visible;
            bet.Visibility = Visibility.Visible;
            tet1.Visibility = Visibility.Visible;
            hazkez.Visibility = Visibility.Collapsed;
            sajatkez.Visibility = Visibility.Collapsed;
            bet.Text = "";
            sajat.Content = "";
            sajato = 0;
            penz.Content = "";
            hazo = 0;
            i = 2;
            x = 1;
            y = 2;
            haz.Content = "";
            tet = 0;
            if (penzo == 0 && tet == 0)
            {
                MessageBox.Show("Kifogytál a pénzből :/");
                MasodikW.Close();
            }
        }
        public void submit_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                lap = r.Next(1, 14);
                if (lap > 10)
                {
                    lap = 10;
                }
                sajatlap[i] = lap;
            }
            sajato = sajatlap[0] + sajatlap[1];
            sajat.Content = sajatlap[0] + "+" + sajatlap[1];

            //ház
            for (int i = 0; i < 7; i++)
            {
                lap = r.Next(1, 14);
                if (lap > 10)
                {
                    lap = 10;
                }
                hazlap[i] = lap;
            }

            hazo = hazlap[0] + hazlap[1];
            haz.Content = hazlap[0];
            tet1.Visibility = Visibility.Collapsed;
            submit.Visibility = Visibility.Collapsed;
            bet.Visibility = Visibility.Collapsed;
            Double.Visibility = Visibility.Visible;
            Hit.Visibility = Visibility.Visible;
            Stand.Visibility = Visibility.Visible;
            tetL.Visibility = Visibility.Visible;
            hazkez.Visibility = Visibility.Visible;
            sajatkez.Visibility = Visibility.Visible;
            try
            {
                tet = Convert.ToInt32(bet.Text);
                penz.Content = tet;
                penzoL.Content = penzo -= tet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (hazlap[0] == 1 && hazlap[1] == 10 || hazlap[0] == 10 && hazlap[1] == 1)
            {
                haz.Content += "+" + hazlap[1];
                huszonegy(3);
            }
            if (sajatlap[0] == 1 && sajatlap[1] == 10 || sajatlap[0] == 10 && sajatlap[1] == 1)
            {
                huszonegy(4);
            }
            DoubleEna();
        }

        private void bet_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (bet.Text != "")
            {
                try
                {
                    tet = Convert.ToInt32(bet.Text);
                    if (penzo - tet < 0)
                    {
                        submit.IsEnabled = false;
                    }
                    else
                    {
                        submit.IsEnabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    submit.IsEnabled = false;
                }
            }
        }

        public Masodik()
        {
            InitializeComponent();
            penzoL.Content = penzo;
            tetL.Visibility = Visibility.Collapsed;
        }

        private void Split_Click(object sender, RoutedEventArgs e)
        {
            DoubleEna();
        }

        private void Stand_Click(object sender, RoutedEventArgs e)
        {
            DoubleEna();
            if (x == 1)
            {
                haz.Content += "+" + hazlap[x];
                x++;
            }
            if (hazo < 17)
            {
                do
                {
                    hazo += hazlap[y];
                    haz.Content += "+" + hazlap[y];
                    y++;
                    if (hazo > 21)
                    {
                        huszonegy(0);
                    }
                } while (y < 7 && x != 1 && hazo < 17);
            }
            if (hazo > 21)
            {
                huszonegy(1);
            }
            if (hazo > 16)
            {
                if (hazo > sajato)
                {
                    huszonegy(1);
                }
                if (sajato > hazo)
                {
                    huszonegy(0);
                }
                else if (hazo == sajato && x != 1)
                {
                    huszonegy(2);
                }
            }
            Double.IsEnabled = false;
        }

        private void Hit_Click(object sender, RoutedEventArgs e)
        {
            DoubleEna();
            do
            {
                sajato += sajatlap[i];
                sajat.Content += "+" + sajatlap[i];
                i++;
                if (sajato > 21)
                {
                    huszonegy(1);
                }
            } while (i > 7);
            Double.IsEnabled = false;
        }
        private void Double_Click(object sender, RoutedEventArgs e)
        {
            DoubleEna();
            sajato += sajatlap[2];
            sajat.Content += "+" + sajatlap[2];
            penzo -= tet;
            tet = tet * 2;
            penzoL.Content = penzo;
            penz.Content = tet;
            if (sajato > 21)
            {
                huszonegy(1);
            }
            else
            {
                if (x == 1)
                {
                    haz.Content += "+" + hazlap[x];
                    x++;
                }
                if (hazo < 17)
                {
                    do
                    {
                        hazo += hazlap[y];
                        haz.Content += "+" + hazlap[y];
                        y++;
                        if (hazo > 21)
                        {
                            huszonegy(0);
                        }
                    } while (y < 7 && x != 1 && hazo < 17);
                }
                if (hazo > 21)
                {
                    huszonegy(1);
                }
                else if (hazo > 16)
                {
                    if (hazo > sajato)
                    {
                        huszonegy(1);
                    }
                    if (sajato > hazo)
                    {
                        huszonegy(0);
                    }
                    else if (hazo == sajato && x != 1)
                    {
                        huszonegy(2);
                    }
                }
            }
            Double.IsEnabled = false;
        }
    }
}
