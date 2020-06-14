
using OLC1_PY1_201700988.Analizadores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using OLC1_PY1_201700988.Objetos;

namespace OLC1_PY1_201700988
{
    public partial class Form1 : Form
    {
        int caracter = 0;
        scanner_201709140 scanner = new scanner_201709140();
        private ArrayList listaErrores;
        private ArrayList listaTokens;

        public Form1()
        {
            InitializeComponent();
            archivoToolStripMenuItem.ForeColor = Color.White;
            nuevoToolStripMenuItem1.ForeColor = Color.White;
            ayudaToolStripMenuItem.ForeColor = Color.White;
            abrirToolStripMenuItem1.ForeColor = Color.White;
            guardarComoToolStripMenuItem1.ForeColor = Color.White;
            guardarToolStripMenuItem1.ForeColor = Color.White;
            herramientasToolStripMenuItem.ForeColor = Color.White;
            acercaDeToolStripMenuItem.ForeColor = Color.White;
            manualTecnicoToolStripMenuItem.ForeColor = Color.White;
            salirToolStripMenuItem.ForeColor = Color.White;
            manualDeUsuarioToolStripMenuItem.ForeColor = Color.White;
            ejecutarToolStripMenuItem.ForeColor = Color.White;
            cargarTablasToolStripMenuItem.ForeColor = Color.White;
            verTablasToolStripMenuItem.ForeColor = Color.White;
            mostrarToolStripMenuItem.ForeColor = Color.White;
            tokensToolStripMenuItem.ForeColor = Color.White;
            erroresToolStripMenuItem.ForeColor = Color.White;
            arbolDeDerivacionToolStripMenuItem.ForeColor = Color.White;
            consultaYRemplazoToolStripMenuItem.ForeColor = Color.White;


            menuStrip1.Renderer = new ToolStripProfessionalRenderer(new MyColorTable());


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }



        private class MyColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color ImageMarginGradientBegin
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }


            public override Color ImageMarginGradientMiddle
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color ImageMarginRevealedGradientEnd
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color MenuItemBorder
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color MenuBorder
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color MenuItemSelected
            {
                get
                {
                    return Color.FromArgb(100, 100, 100);
                }
            }

            public override Color MenuStripGradientBegin
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color MenuStripGradientEnd
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get
                {
                    return Color.FromArgb(100, 100, 100);
                }
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get
                {
                    return Color.FromArgb(100, 100, 100);
                }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }

            public override Color MenuItemPressedGradientEnd
            {
                get
                {
                    return Color.FromArgb(70, 70, 70);
                }
            }
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsMethod();
        }

        private void saveAsMethod()
        {
            Control controlBox;
            string rutaString = "";

            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {

                        SaveFileDialog saveFile = new SaveFileDialog()
                        {
                            Title = "Seleccione la ruta",
                            Filter = "Archivo Compiladores 1 | *.oacr",
                            FileName = tabControl1.SelectedTab.Text,
                            AddExtension = true,
                        };

                        var result = saveFile.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            StreamWriter writer = new StreamWriter(saveFile.FileName);
                            writer.Write(controlBox.Text);
                            writer.Close();

                            String[] ruta = saveFile.FileName.Split('\\');
                            rutaString = saveFile.FileName;
                            tabControl1.SelectedTab.ToolTipText = saveFile.FileName;

                            tabControl1.SelectedTab.Text = ruta[ruta.Length - 1];
                        }
                    }
                }
            }
        }

        private void saveMethod()
        {
            Control controlBox;
            Control richText = null;

            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {
                        richText = item;
                    }


                }
                StreamWriter writer = new StreamWriter(tabControl1.SelectedTab.ToolTipText);
                writer.Write(richText.Text);
                writer.Close();
            }
            else
            {
                MessageBox.Show("Error al guardar el archivo", "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void getFile()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            openFile.Filter = "Archivo Compiladores 1(*.oacr)|*.oacr|Tablas SQL (*.sqle)|*.sqle";
            openFile.FilterIndex = 2;
            openFile.RestoreDirectory = true;

            var result = openFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                StreamReader read = new StreamReader(openFile.FileName);

                Control controlBox;

                if (tabControl1.SelectedTab.HasChildren)
                {
                    foreach (Control item in tabControl1.SelectedTab.Controls)
                    {
                        controlBox = item;

                        if (controlBox is RichTextBox)
                        {
                            controlBox.Text = read.ReadToEnd();
                            read.Close();
                        }
                    }
                }

                String[] ruta = openFile.FileName.Split('\\');
                tabControl1.SelectedTab.Text = ruta[ruta.Length - 1];
                tabControl1.SelectedTab.ToolTipText = openFile.FileName;

            }

        }

        private void addTabPage()
        {
            int numPage = tabControl1.TabPages.Count + 1;
            TabPage newPage = new TabPage("Untitled_" + numPage);

            RichTextBox textBox = new RichTextBox();
            textBox.SetBounds(58, 0, 695, 432);
            textBox.BackColor = Color.FromArgb(170, 170, 170);
            textBox.ForeColor = Color.Black;
            textBox.Font = new Font("Tahoma", 10, FontStyle.Regular);
            textBox.WordWrap = false;

            PictureBox num = new PictureBox();
            num.SetBounds(0, 0, 58, 427);
            num.BackColor = Color.FromArgb(50, 50, 50);
            num.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);

            newPage.Controls.Add(textBox);
            newPage.Controls.Add(num);

            tabControl1.TabPages.Add(newPage);
            tabControl1.SelectedTab = newPage;
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is PictureBox)
                    {
                        controlBox.Refresh();
                    }
                }
            }
            //pictureBox1.Refresh();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            pintarNumeros(e);
        }

        private void pintarNumeros(PaintEventArgs e)
        {
            caracter = 0;
            int altura = 1;
            PictureBox numeros = null;
            RichTextBox aux = null;

            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {
                        aux = (RichTextBox)item;
                    }

                    if (controlBox is PictureBox)
                    {
                        numeros = (PictureBox)item;
                    }
                }
            }

            if (aux.Lines.Length > 0)
            {
                for (int i = 0; i < aux.Lines.Length; i++)
                {
                    e.Graphics.DrawString((i + 1).ToString(), aux.Font, Brushes.SkyBlue, numeros.Width - (e.Graphics.MeasureString((i + 1).ToString(), aux.Font).Width + 10), altura);
                    caracter += aux.Lines[i].Length + 1;
                    altura = aux.GetPositionFromCharIndex(caracter).Y;
                }
            }
            else
            {
                e.Graphics.DrawString("1", aux.Font, Brushes.SkyBlue, numeros.Width - (e.Graphics.MeasureString("1", aux.Font).Width + 10), altura);
            }
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void nuevoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            addTabPage();
        }

        public void open()
        {
            Control controlBox;
            int posPage = 0;
            bool createPage = true;

            tabControl1.SelectedIndex = posPage;
            foreach (TabPage paginaActual in tabControl1.TabPages)
            {
                if (paginaActual.ToolTipText.Equals(""))
                {
                    //--------
                    if (tabControl1.SelectedTab.HasChildren)
                    {
                        foreach (Control item in tabControl1.SelectedTab.Controls)
                        {
                            controlBox = item;

                            if (controlBox is RichTextBox)
                            {
                                if (controlBox.Text.Equals(""))
                                {
                                    createPage = false;
                                    break;
                                }
                            }
                        }
                    }
                    //--------
                }
                if (createPage)
                {
                    posPage++;
                }
                tabControl1.SelectedIndex = posPage;
            }

            if (createPage)
            {
                addTabPage();
            }

            getFile();
        }

        private void abrirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            open();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void guardarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.ToolTipText.Equals(""))
            {
                saveAsMethod();
            }
            else
            {
                saveMethod();
            }
        }

        private void guardarComoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveAsMethod();
        }

        private void borrarExpresionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void acercaDeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Oscar Armin Crisostomo Ruiz \n 201709140", "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void manualTecnicoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manualDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ejecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {
                        //Realizar analisis lexico
                        RichTextBox a = (RichTextBox)controlBox;
                        //ANALISIS LEXICO
                        listaTokens = scanner.scannerMethod(a.SelectedText);
                        listaErrores = scanner.getErrores();
                        //ANALISIS SINTACTICP


                        //CountryList1.AddRange(CountryList2);
                        Console.WriteLine(a.SelectedText.Length);
                        Console.WriteLine(a.Text.IndexOf(a.SelectedText));
                        //Pinto segun lo escaneado
                        int inicio = a.Text.IndexOf(a.SelectedText);
                        int largo = a.SelectedText.Length;
                        String contenido = a.Text;
                        a.Clear();
                        Console.WriteLine(contenido);
                        int linea = 0;
                        for (int i = 0; i < inicio; i++)
                        {
                            a.AppendText(contenido[i].ToString());
                            if (contenido[i] == 13 || contenido[i] == 10 || contenido[i] == 11)
                            {
                                linea++;
                            }


                        }

                        foreach (Token token in listaTokens)
                        {
                            if (linea == token.getFila())
                            {
                                a.AppendText("\n");
                                linea++;
                            }

                            if (token.getToken() == 36)//FECHAS
                            {
                                a.SelectionColor = Color.Orange;
                                a.AppendText(token.getLexema() + " ");

                            }
                            else if(token.getToken() == 31 || token.getToken() == 32)//COMENTARIOS
                            {
                                a.SelectionColor = Color.Gray;
                                a.AppendText(token.getLexema()+" ");
                            }
                            else if (token.getToken() == 35)//CADENAS
                            {
                                a.SelectionColor = Color.Green;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 37)//IDENTIFICADORES
                            {
                                a.SelectionColor = Color.FromArgb(101, 67, 33); 
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 33 || token.getToken() == 34)//Numeros
                            {
                                a.SelectionColor = Color.Blue;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 5 || token.getToken() == 6 || token.getToken() == 15 || token.getToken() == 16)//PALABRAS RESSERVADAS
                            {
                                a.SelectionColor = Color.Purple;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 23 || token.getToken() == 24 || token.getToken() ==25 || token.getToken() == 26 || token.getToken() == 27 || token.getToken() == 28 || token.getToken() == 29)
                            {
                                a.SelectionColor = Color.Red;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else
                            {
                                a.SelectionColor = Color.Black;
                                a.AppendText(token.getLexema() + " ");

                            }

                            a.SelectionColor = Color.Black;
                        }

                        for (int i = inicio + largo; i < contenido.Length; i++)
                        {
                            a.AppendText(contenido[i].ToString());
                        }


                        //Ejecuto codigo




                        //Cargar las expresiones regulares

                    }
                }
            }
        }

        private void cargarTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {
                        //Realizar analisis lexico
                        RichTextBox a = (RichTextBox)controlBox;
                        //ANALISIS LEXICO
                        listaTokens = scanner.scannerMethod(a.Text);
                        listaErrores = scanner.getErrores();
                        //ANALISIS SINTACTICP


                        //CountryList1.AddRange(CountryList2);
                        
                        //Pinto segun lo escaneado
                        
                        
                        int linea = 0;

                        a.Clear();
                        foreach (Token token in listaTokens)
                        {
                            if (linea == token.getFila())
                            {
                                a.AppendText("\n");
                                linea++;
                            }

                            if (token.getToken() == 36)//FECHAS
                            {
                                a.SelectionColor = Color.Orange;
                                a.AppendText(token.getLexema() + " ");

                            }
                            else if (token.getToken() == 31 || token.getToken() == 32)//COMENTARIOS
                            {
                                a.SelectionColor = Color.Gray;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 35)//CADENAS
                            {
                                a.SelectionColor = Color.Green;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 37)//IDENTIFICADORES
                            {
                                a.SelectionColor = Color.FromArgb(101, 67, 33);
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 33 || token.getToken() == 34)//Numeros
                            {
                                a.SelectionColor = Color.Blue;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 5 || token.getToken() == 6 || token.getToken() == 15 || token.getToken() == 16)//PALABRAS RESSERVADAS
                            {
                                a.SelectionColor = Color.Purple;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else if (token.getToken() == 23 || token.getToken() == 24 || token.getToken() == 25 || token.getToken() == 26 || token.getToken() == 27 || token.getToken() == 28 || token.getToken() == 29)
                            {
                                a.SelectionColor = Color.Red;
                                a.AppendText(token.getLexema() + " ");
                            }
                            else
                            {
                                a.SelectionColor = Color.Black;
                                a.AppendText(token.getLexema() + " ");

                            }

                            a.SelectionColor = Color.Black;
                        }

                      


                        //Ejecuto codigo




                        //Cargar las expresiones regulares

                    }
                }
            }
        }

        private void verTablasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tokensToolStripMenuItem_Click(object sender, EventArgs e)
        {




            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {

                        scanner.reporteLexico(tabControl1.SelectedTab.Name);

                    }
                }
            }
        }

        private void erroresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void arbolDeDerivacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {

                        RichTextBox a = (RichTextBox)controlBox;
                        a.SelectionColor = Color.Red;


                        Console.WriteLine(a.SelectedText);




                        //Pinto segun lo escaneado

                        //Ejecuto codigo




                        //Cargar las expresiones regulares

                    }
                }
            }
        }

        private void consultaYRemplazoToolStripMenuItem_Click(object sender, EventArgs e)
        {




            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {

                        RichTextBox a = (RichTextBox)controlBox;


                        String busqueda = Interaction.InputBox("Busqueda de palabra a reemplazar", "Busqueda", "Palabra", -1, -1);

                        if (controlBox.Text.Contains(busqueda))
                        {
                            //remplaza

                            String remplazo = Interaction.InputBox("Palabra para reemplazar", "Reemplazo", "Palabra", -1, -1);

                            QuickReplace(a, busqueda, remplazo);

                            MessageBox.Show("Remplazo satisfactorio", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);




                        }
                        else
                        {
                            //Mensaje que no se encontro la palabra
                            MessageBox.Show("No se encontro la palabra que busca", "Error al reemplazar", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }

                    }
                }
            }
        }
        public static void QuickReplace(RichTextBox rtb, String word, String word2)
        {
            rtb.Text = rtb.Text.Replace(word, word2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {

                        RichTextBox a = (RichTextBox)controlBox;
                        a.AppendText("Alo");




                        //Pinto segun lo escaneado

                        //Ejecuto codigo




                        //Cargar las expresiones regulares

                    }
                }
            }
        }
        public void reporteErrores(String archivo)
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string pathFolder = pathDesktop + "\\Proyecto1\\" + archivo + "_201709140\\";

            try
            {
                //Existe el directorio para el archivo, sino crearlo
                if (!Directory.Exists(pathFolder))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(pathFolder);
                }

                //Crear el reporte
                string pathRep = pathFolder + "\\Errores_" + archivo + ".html";
                StreamWriter repHtml = new StreamWriter(pathRep);


                //Escribir la tabla html
                repHtml.WriteLine("<!DOCTYPE html>");
                repHtml.WriteLine("<html>");
                repHtml.WriteLine("<head>");
                repHtml.WriteLine("<title>Analisis_" + archivo + "</title>");
                repHtml.WriteLine("<meta charset=\"utf-8\">");
                repHtml.WriteLine("<h1 style=\"text-align:center\">Universidad de San Carlos de Guatemala</h1>");
                repHtml.WriteLine("<h2 style=\"text-align: center\">Organizacion de lenguajes y compiladores 1</h2>");
                repHtml.WriteLine("<h4 style=\"text-align: center\">Oscar Armin Crisóstomo Ruiz - 201709140</h4>");
                repHtml.WriteLine("</head>");
                repHtml.WriteLine("<body>");
                repHtml.WriteLine("<center>");
                repHtml.WriteLine("<table border=\"1\" style=\"width:60%\">");
                repHtml.WriteLine("<caption><h3>Reporte de analisis lexico</h3></caption>");
                repHtml.WriteLine("<colgroup>");
                repHtml.WriteLine("<col style=\"width: 10% \"/>");
                repHtml.WriteLine("<col style=\"width: 20% \"/>");
                repHtml.WriteLine("<col style=\"width: 70% \"/>");
                repHtml.WriteLine("</colgroup>");
                repHtml.WriteLine("<thead>");
                repHtml.WriteLine("<tr>");
                repHtml.WriteLine("<th >Tipo</th>");
                repHtml.WriteLine("<th >Error</th>");
                repHtml.WriteLine("<th>Descripcion</th>");
                repHtml.WriteLine("<th>Linea</th>");
                repHtml.WriteLine("<th>Columna</th>");
                repHtml.WriteLine("</tr>");
                repHtml.WriteLine("</thead>");
                repHtml.WriteLine("<tfoot>");
                repHtml.WriteLine("<tr>");
                repHtml.WriteLine("<td colspan=\"5\">Fin de Reporte</td>");
                repHtml.WriteLine("</tr>");
                repHtml.WriteLine("</tfoot>");
                repHtml.WriteLine("<tbody>");

                foreach (Error item in listaErrores)
                {
                    repHtml.WriteLine("<tr>");
                    repHtml.WriteLine("<td>" + item.getTipo() + "</td>");
                    repHtml.WriteLine("<td>" + item.getError() + "</td>");
                    repHtml.WriteLine("<td>" + item.getDescipcion() + "</td>");
                    repHtml.WriteLine("<td>" + item.getFila().ToString() + "</td>");
                    repHtml.WriteLine("<td>" + item.getColumna().ToString() + "</td>");
                    repHtml.WriteLine("</tr>");
                }

                repHtml.WriteLine("</tbody>");
                repHtml.WriteLine("</table>");
                repHtml.WriteLine("</center>");
                repHtml.WriteLine("</body>");
                repHtml.WriteLine("</html>");

                repHtml.Close();
                System.Diagnostics.Process.Start(pathRep);
            }
            catch (Exception er)
            {
                MessageBox.Show("Error al generar el reporte " + er.ToString(), "Error con reporte lexico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
