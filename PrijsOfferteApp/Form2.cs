using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Font = iTextSharp.text.Font;

namespace PrijsOfferteApp
{
    public partial class Form2 : Form
    {



        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                    "AttachDbFilename= {OfferteDatabase.mdf};" +
                                    "Integrated Security=True;" +
                                    "Connect Timeout=30");

        SqlCommand cmd;


        public Form2()
        {
            InitializeComponent();
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
GeneratePdf();
        }

        public void GeneratePdf()
        {
            string outFile = Environment.CurrentDirectory + $"/{tb_titel.Text}.pdf";
            Document doc = new Document();

            PdfWriter.GetInstance(doc, new FileStream (outFile, FileMode.Create));
            doc.Open();

            BaseColor blue = new BaseColor(0, 75, 155);
            BaseColor gris = new BaseColor(240, 240, 240);
            BaseColor blanc = new BaseColor(0, 75, 155);


            Font info_style = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            Font title_style = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            Paragraph p1 = new Paragraph(my_bedrijfn.Text , info_style);
            p1.Alignment = Element.ALIGN_LEFT;
            doc.Add(p1);
            Paragraph p2 = new Paragraph("BE " + my_tva.Text , info_style);
            p2.Alignment = Element.ALIGN_LEFT;
            doc.Add(p2);
            Paragraph p3 = new Paragraph(my_adres.Text , info_style);
            p3.Alignment = Element.ALIGN_LEFT;
            doc.Add(p3);
            Paragraph p4 = new Paragraph(my_email.Text, info_style);
            p4.Alignment = Element.ALIGN_LEFT;
            doc.Add(p4);
            Paragraph p5 = new Paragraph(my_tel.Text + "\n\n", info_style);
            p5.Alignment = Element.ALIGN_LEFT;
            doc.Add(p5);

            Paragraph p6 = new Paragraph("Klant: " + "\n" + klant_bedrijfn.Text, info_style);
            p6.Alignment = Element.ALIGN_RIGHT;
            doc.Add(p6);
            Paragraph p7 = new Paragraph("BE " + klant_tva.Text , info_style);
            p7.Alignment = Element.ALIGN_RIGHT;
            doc.Add(p7);
            Paragraph p8 = new Paragraph(klant_adres.Text , info_style);
            p8.Alignment = Element.ALIGN_RIGHT;
            doc.Add(p8);
            Paragraph p9 = new Paragraph(klant_email.Text , info_style);
            p9.Alignment = Element.ALIGN_RIGHT;
            doc.Add(p9);
            Paragraph p10 = new Paragraph(klant_tel.Text + "\n\n", info_style);
            p10.Alignment = Element.ALIGN_RIGHT;
            doc.Add(p10);

            Paragraph p11 = new Paragraph(tb_titel.Text + "\n\n", title_style);
            p11.Alignment = Element.ALIGN_CENTER;
            doc.Add(p11);

            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;

            AddCelli("Werk", title_style, blue, table);
            AddCelli("Prijs", title_style, blue, table);
            AddCelli("Kwantiteit", title_style, blue, table);

            string[] details = new string[12];
            if (String.IsNullOrEmpty(blabla.Text))
            {
                details[0] = "";
                details[1] = "";
                details[2] = "";
            }
            else
            {
                details[0] = tb_1_werk.Text;
                details[1] = tb_1_prijs.Text + "€";
                details[2] = tb_1_kw.Text + tb_1_eenh.Text;
                
            }

            if (String.IsNullOrEmpty(tb_2_werk.Text))
            {
                details[3] = "";
                details[4] = "";
                details[5] = "";
            }
            else
            {

                details[3] = tb_2_werk.Text;
                details[4] = tb_2_prijs.Text + "€";
                details[5] = tb_2_kw.Text + tb_2_eenh.Text;
                
            }

            if (String.IsNullOrEmpty(tb_3_werk.Text))
            {
                details[6] = "";
                details[7] = "";
                details[8] = "";
            }
            else
            {
                details[6] = tb_3_werk.Text;
                details[7] = tb_3_prijs.Text + "€";
                details[8] = tb_3_kw.Text + tb_3_eenh.Text;
                
            }

            if (String.IsNullOrEmpty(tb_4_werk.Text))
            {
                details[9] = "";
                details[10] = "";
                details[11] = "";
            }
            else
            {
                details[9] = tb_4_werk.Text;
                details[10] = tb_4_prijs.Text + "€";
                details[11] = tb_4_kw.Text + tb_4_eenh.Text;
               
            }

            foreach (string info in details)
            {
                PdfPCell cell = new PdfPCell(new Phrase(info));
                cell.BackgroundColor = gris;
                cell.Padding = 7;
                cell.BorderColor = gris;
                table.AddCell(cell);
            }


            doc.Add(table);

            doc.Add(new Phrase("\n\n"));

            float tot_prijs_1 = 0;
            float tot_prijs_2 = 0;
            float tot_prijs_3 = 0;
            float tot_prijs_4 = 0;

            

            if (String.IsNullOrEmpty(tb_1_prijs.Text))
            {

                tot_prijs_1 = 0;
            }
            else
            {
                tot_prijs_1 = float.Parse(tb_1_prijs.Text) * float.Parse(tb_1_kw.Text);
            }

            if (String.IsNullOrEmpty(tb_2_prijs.Text))
            {
                
                tot_prijs_2 = 0;
            }
            else
            {
                tot_prijs_2 = float.Parse(tb_2_prijs.Text) * float.Parse(tb_2_kw.Text);
            }

            if (String.IsNullOrEmpty(tb_3_prijs.Text))
            {

                tot_prijs_3 = 0;
            }
            else
            {
                tot_prijs_3 = float.Parse(tb_3_prijs.Text) * float.Parse(tb_3_kw.Text);
            }

            if (String.IsNullOrEmpty(tb_4_prijs.Text))
            {

                tot_prijs_4 = 0;
            }
            else
            {
                tot_prijs_4 = float.Parse(tb_4_prijs.Text) * float.Parse(tb_4_kw.Text);
            }
          

            float tot_prijs = tot_prijs_1 + tot_prijs_2 + tot_prijs_3 + tot_prijs_4;

            Paragraph p12 = new Paragraph("Totaal: " + tot_prijs + "€" + "\n\n", title_style);
            p12.Alignment = Element.ALIGN_RIGHT;
            doc.Add(p12);

            doc.Close();
            Process.Start(@"cmd.exe", @"/c" + outFile);


        }
        public void AddCelli(string tit, Font font, BaseColor color, PdfPTable tab)
        {
            PdfPCell cell1 = new PdfPCell(new Phrase(tit, font));
            cell1.BackgroundColor = color;
            cell1.Padding = 7;
            cell1.BorderColor = color;
            tab.AddCell(cell1);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            float tot_prijs_1 = 0;
            float tot_prijs_2 = 0;
            float tot_prijs_3 = 0;
            float tot_prijs_4 = 0;



            if (String.IsNullOrEmpty(tb_1_prijs.Text) || String.IsNullOrEmpty(tb_1_kw.Text))
            {

                tot_prijs_1 = 0;
            }
            else
            {
                tot_prijs_1 = float.Parse(tb_1_prijs.Text) * float.Parse(tb_1_kw.Text);
            }

            if (String.IsNullOrEmpty(tb_2_prijs.Text))
            {

                tot_prijs_2 = 0;
            }
            else
            {
                tot_prijs_2 = float.Parse(tb_2_prijs.Text) * float.Parse(tb_2_kw.Text);
            }

            if (String.IsNullOrEmpty(tb_3_prijs.Text))
            {

                tot_prijs_3 = 0;
            }
            else
            {
                tot_prijs_3 = float.Parse(tb_3_prijs.Text) * float.Parse(tb_3_kw.Text);
            }

            if (String.IsNullOrEmpty(tb_4_prijs.Text))
            {

                tot_prijs_4 = 0;
            }
            else
            {
                tot_prijs_4 = float.Parse(tb_4_prijs.Text) * float.Parse(tb_4_kw.Text);
            }


            float tot_prijs = tot_prijs_1 + tot_prijs_2 + tot_prijs_3 + tot_prijs_4;


            if (my_bedrijfn.Text == "")
            {
                MessageBox.Show("vul jouw bedrijfnaam in.");
            }
            else if (klant_bedrijfn.Text == "")
            {
                MessageBox.Show("vul klant bedrijfnaam in.");
            }
            else if (tb_titel.Text == "")
            {
                MessageBox.Show("vul een titel offerte in.");
            }
            else if (tb_1_prijs.Text == "" || tb_1_kw.Text == "")
            {
                MessageBox.Show("vul minstens een werk in.");
            }
            else
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("insert into Offertes values(@BedrijfNaam,@KlantBedrijfNaam,@TitelOfferte,@TotaalBedrag)", connection);
                    cmd.Parameters.AddWithValue("@BedrijfNaam", my_bedrijfn.Text);
                    cmd.Parameters.AddWithValue("@KlantBedrijfNaam", klant_bedrijfn.Text);
                    cmd.Parameters.AddWithValue("@TitelOfferte", tb_titel.Text);
                    cmd.Parameters.AddWithValue("@TotaalBedrag", tot_prijs);


                    SqlCommand cmd1 = new SqlCommand("insert into Bedrijven values(@BedrijfNaam,@NrTva,@Adres,@Email,@NrTel)", connection);
                    cmd1.Parameters.AddWithValue("@BedrijfNaam", my_bedrijfn.Text);
                    cmd1.Parameters.AddWithValue("@NrTva", my_tva.Text);
                    cmd1.Parameters.AddWithValue("@Adres", my_adres.Text);
                    cmd1.Parameters.AddWithValue("@Email", my_email.Text);
                    cmd1.Parameters.AddWithValue("@NrTel", my_tel.Text);

                    SqlCommand cmd2 = new SqlCommand("insert into Klanten values(@BedrijfNaam,@NrTva,@Adres,@Email,@NrTel)", connection);
                    cmd2.Parameters.AddWithValue("@BedrijfNaam", klant_bedrijfn.Text);
                    cmd2.Parameters.AddWithValue("@NrTva", klant_tva.Text);
                    cmd2.Parameters.AddWithValue("@Adres", klant_adres.Text);
                    cmd2.Parameters.AddWithValue("@Email", klant_email.Text);
                    cmd2.Parameters.AddWithValue("@NrTel", klant_tel.Text);





                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        private void my_tva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void my_tel_TextChanged(object sender, EventArgs e)
        {

        }

        private void my_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void klant_tva_TextChanged(object sender, EventArgs e)
        {

        }

        private void klant_tva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void klant_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_1_prijs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_1_kw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_2_prijs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_2_kw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_3_prijs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_3_kw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_4_prijs_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void tb_4_kw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }
    }
}
