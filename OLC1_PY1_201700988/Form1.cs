
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
        parser_201709140 parser = new parser_201709140();
        private ArrayList listaErrores;
        private ArrayList listaTokens;
        private ArrayList listaTablas;

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
            buscarToolStripMenuItem.ForeColor = Color.White;
            listaTablas = new ArrayList();
            listaErrores = new ArrayList();
            listaTokens = new ArrayList();

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
                            writer.Close(); String[] ruta = saveFile.FileName.Split('\\');
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
                        parser.limpiar();
                        bool error = parser.parser(listaTokens);
                        listaErrores.AddRange(parser.getErrores());
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
                                //a.AppendText("\n");
                                //linea++;
                            }
                            while (linea < token.getFila())
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

                        for (int i = inicio + largo; i < contenido.Length; i++)
                        {
                            a.AppendText(contenido[i].ToString());
                        }

                        if (error)//Si no tiene errores
                        {
                            //Ejecuto codigo
                            crear();
                            insertar();
                            eliminar();
                            actualizar();
                            seleccionar();

                        }
                        else
                        {
                            MessageBox.Show("Corriga errores sintacticos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }





                        //Cargar las expresiones regulares

                    }
                }
            }
        }

        private void seleccionar()
        {
            foreach (Token token in listaTokens)
            {
                if (token.getToken() == 9)//Si encuentra la palabra seleccionar
                {

                }
            }
        }

        private void actualizar()
        {
            int posicion = 0;
            foreach (Token token in listaTokens)
            {
                if (token.getToken() == 16)//Si encuentra la palabra actualizar
                {
                    ArrayList listaActualizar = new ArrayList();
                    int nombreP = posicion + 1;
                    Token nombre = (Token)listaTokens[nombreP];
                    int par=0;
                    //Obtengo columnas a cambiar
                    for (int i = nombreP; i < listaTokens.Count; i++)
                    {
                        Token temp = (Token)listaTokens[i];
                        if (temp.getToken() ==22)//Parentesis que cierra TERMINA VALORES A CAMBIAR
                        {
                            par = i;
                            break;
                        }else if (temp.getToken()==24)
                        {
                            Token columna = (Token)listaTokens[i-1];
                            Token valor = (Token)listaTokens[i+1];
                            actualizar a = new actualizar(columna.getLexema(), valor.getLexema());
                            listaActualizar.Add(a);
                        }

                        

                    }

                    foreach (Tabla table in listaTablas)
                    {

                        
                        if (nombre.getLexema().ToUpper() == table.getNombre())
                        {
                            //Esta en la tabla que va a eliminar
                            Token te = (Token)listaTokens[par+1];
                            if (te.getToken() == 20)//Si es ; 
                            {
                                //Debo de cambiar todos los atributos a establecer (actualizar todo)
                                foreach(Registro re in table.getRegistros())
                                {
                                    foreach (actualizar ac in listaActualizar)
                                    {
                                        foreach (Atributo atri in re.getAtributo()) 
                                        {
                                            if (ac.getCol().ToUpper().Equals(atri.getNombre().ToUpper()))
                                            {
                                                atri.setAtribute(ac.getVal());
                                            }
                                        }
                                    }
                                }

                                
                            }
                            else
                            {
                                //Obetengo condiciones
                                ArrayList listaCondiciones = new ArrayList(); //Condiciones con || 



                                Condiciones condition = new Condiciones();//Agrego condicion para el && 


                                Condicion nueva = new Condicion();

                                //Consigo las condiciones
                                for (int i = par; i < listaTokens.Count; i++)
                                {
                                    Token tok = (Token)listaTokens[i];
                                    if (tok.getToken() == 20)//Si es ; 
                                    {
                                        listaCondiciones.Add(condition);
                                        //Agrego condiciones a la lista
                                        //Detengo ya termino de guardar condiciones
                                        break;
                                    }
                                    else if (tok.getToken() >= 23 && tok.getToken() <= 28)
                                    {
                                        //Creo la nueva condiciones
                                        Token columna = (Token)listaTokens[i - 1];
                                        Token valor = (Token)listaTokens[i + 1];
                                        if (valor.getToken() == 37)
                                        {
                                            nueva = new Condicion(table.getNombre().ToUpper(), columna.getLexema().ToUpper(), tok.getToken(), valor.getToken(), table.getNombre().ToUpper(), valor.getLexema().ToUpper());
                                            condition.addC(nueva);

                                        }
                                        else
                                        {

                                            nueva = new Condicion(table.getNombre().ToUpper(), columna.getLexema().ToUpper(), tok.getToken(), valorT(valor.getToken()), valor.getLexema());
                                            condition.addC(nueva);
                                        }


                                    }
                                    else if (tok.getToken() == 13 || tok.getToken() == 14)
                                    {
                                        if (tok.getToken() == 13)//Y
                                        {
                                            //condition.addC(nueva);
                                        }
                                        else if (tok.getToken() == 14)//O
                                        {
                                            listaCondiciones.Add(condition);
                                            condition = new Condiciones();

                                        }
                                        //Agrego 
                                        //Creo o agrego mas condiciones

                                    }

                                }
                                //Posicion del registro de la tabla

                                //Ya obtenbe condiciones ahora debo elimiar segun condiciones

                                //Si cumple modifico en el indice i
                                Console.WriteLine("");
                                foreach(Registro r in table.getRegistros())
                                {
                                    //Registro r = (Registro)table.getRegistros()[i];
                                    
                                    foreach (Condiciones cons in listaCondiciones)
                                    {

                                        foreach (Condicion con in cons.getCondiciones())
                                        {
                                            String columna = con.getColumna();
                                            int compara = con.getCompara();


                                            foreach (Atributo at in r.getAtributo())
                                            {
                                                if (at.getNombre().ToUpper().Equals(columna.ToUpper()))
                                                {
                                                    if (at.getTipo() == 0)//Entero
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if ((int)at.getValor() != (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if ((int)at.getValor() == (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if ((int)at.getValor() <= (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if ((int)at.getValor() < (int)con.getValor())
                                                            {
                                                                foreach (actualizar ac in listaActualizar)
                                                                {
                                                                    con.setEstado(true);
                                                                }
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if ((int)at.getValor() >= (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if ((int)at.getValor() > (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }
                                                    else if (at.getTipo() == 1)//Cadena
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if (!at.getValor().ToString().Equals(con.getValor().ToString()))
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if (at.getValor().ToString().Equals(con.getValor().ToString()))
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if (at.getValor().ToString().Length <= con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if (at.getValor().ToString().Length < con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if (at.getValor().ToString().Length >= con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if (at.getValor().ToString().Length > con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }
                                                    else if (at.getTipo() == 2)//flotante
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if ((double)at.getValor() != (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if ((double)at.getValor() == (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if ((double)at.getValor() <= (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if ((double)at.getValor() < (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if ((double)at.getValor() >= (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if ((double)at.getValor() > (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }
                                                    else if (at.getTipo() == 3)//fecha
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if ((DateTime)at.getValor() != (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if ((DateTime)at.getValor() == (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if ((DateTime)at.getValor() <= (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if ((DateTime)at.getValor() < (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if ((DateTime)at.getValor() >= (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if ((DateTime)at.getValor() > (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }


                                                }

                                            }
                                            if (cons.getEstado())
                                            {
                                                foreach (actualizar ac in listaActualizar)
                                                {
                                                    foreach (Atributo atri in r.getAtributo())
                                                    {
                                                        if (ac.getCol().ToUpper().Equals(atri.getNombre().ToUpper()))
                                                        {
                                                            atri.setAtribute(ac.getVal());
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        foreach (Condiciones c in listaCondiciones)
                                        {
                                            c.falso();
                                            c.setEstado(false);
                                        }

                                    }
                                   

                                }





                            }


                        }



                    }






                }
                posicion++;
            }
        }

        private void eliminar()
        {

            int posicion = 0;
            foreach (Token token in listaTokens)
            {
                if (token.getToken() == 15)//Si encuentra la palabra eliminar
                {
                    int nombreP = posicion + 2;
                    Token t = (Token)listaTokens[nombreP];

                    foreach (Tabla table in listaTablas)
                    {

                        Token te = (Token)listaTokens[nombreP + 1];
                        if (t.getLexema().ToUpper() == table.getNombre())
                        {
                            //Esta en la tabla que va a eliminar

                            if (te.getToken() == 20)//Si es ; 
                            {
                                //No tiene condiciones elimino todos los datos
                                table.eliminar();
                            }
                            else
                            {
                                //Obetengo condiciones
                                ArrayList listaCondiciones = new ArrayList(); //Condiciones con || 



                                Condiciones condition = new Condiciones();//Agrego condicion para el && 


                                Condicion nueva = new Condicion();

                                //Consigo las condiciones
                                for (int i = nombreP; i < listaTokens.Count; i++)
                                {
                                    Token tok = (Token)listaTokens[i];
                                    if (tok.getToken() == 20)//Si es ; 
                                    {
                                        listaCondiciones.Add(condition);
                                        //Agrego condiciones a la lista
                                        //Detengo ya termino de guardar condiciones
                                        break;
                                    }
                                    else if (tok.getToken() >= 23 && tok.getToken() <= 28)
                                    {
                                        //Creo la nueva condiciones
                                        Token columna = (Token)listaTokens[i - 1];
                                        Token valor = (Token)listaTokens[i + 1];
                                        if (valor.getToken() == 37)
                                        {
                                            nueva = new Condicion(table.getNombre().ToUpper(), columna.getLexema().ToUpper(), tok.getToken(), valor.getToken(), table.getNombre().ToUpper(), valor.getLexema().ToUpper());
                                            condition.addC(nueva);

                                        }
                                        else
                                        {

                                            nueva = new Condicion(table.getNombre().ToUpper(), columna.getLexema().ToUpper(), tok.getToken(), valorT(valor.getToken()), valor.getLexema());
                                            condition.addC(nueva);
                                        }


                                    }
                                    else if (tok.getToken() == 13 || tok.getToken() == 14)
                                    {
                                        if (tok.getToken() == 13)//Y
                                        {
                                            //condition.addC(nueva);
                                        }
                                        else if (tok.getToken() == 14)//O
                                        {
                                            listaCondiciones.Add(condition);
                                            condition = new Condiciones();

                                        }
                                        //Agrego 
                                        //Creo o agrego mas condiciones

                                    }

                                }
                                //Posicion del registro de la tabla

                                //Ya obtenbe condiciones ahora debo elimiar segun condiciones

                                //Si cumple elimino en el indice i
                                for (int i = 0; i < table.getRegistros().Count; i++)
                                {
                                    Registro r = (Registro)table.getRegistros()[i];
                                    bool elimino = false;
                                    foreach (Condiciones cons in listaCondiciones)
                                    {

                                        foreach (Condicion con in cons.getCondiciones())
                                        {
                                            String columna = con.getColumna();
                                            int compara = con.getCompara();
                                            
                                            
                                            foreach (Atributo at in r.getAtributo())
                                            {
                                                if (at.getNombre().ToUpper().Equals(columna.ToUpper()))
                                                {
                                                    if (at.getTipo() == 0)//Entero
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if ((int)at.getValor()!=(int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if ((int)at.getValor() == (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if ((int)at.getValor() <= (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if ((int)at.getValor() < (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if ((int)at.getValor() >= (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if ((int)at.getValor() > (int)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }
                                                    else if (at.getTipo() == 1)//Cadena
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if (!at.getValor().ToString().Equals(con.getValor().ToString()))
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if (at.getValor().ToString().Equals(con.getValor().ToString()))
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if (at.getValor().ToString().Length <= con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if (at.getValor().ToString().Length < con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if (at.getValor().ToString().Length >= con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if (at.getValor().ToString().Length > con.getValor().ToString().Length)
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }
                                                    else if (at.getTipo() == 2)//flotante
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if ((double)at.getValor() != (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if ((double)at.getValor() == (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if ((double)at.getValor() <= (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if ((double)at.getValor() < (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if ((double)at.getValor() >= (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if ((double)at.getValor() > (double)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }
                                                    else if (at.getTipo() == 3)//fecha
                                                    {
                                                        if (compara == 23)//Diferente
                                                        {
                                                            if ((DateTime)at.getValor() != (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 24)//igual
                                                        {
                                                            if ((DateTime)at.getValor() == (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 25)//menor o igual
                                                        {
                                                            if ((DateTime)at.getValor() <= (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 26)//menor
                                                        {
                                                            if ((DateTime)at.getValor() < (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 27)//mayor igual
                                                        {
                                                            if ((DateTime)at.getValor() >= (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                        else if (compara == 28)//mayor
                                                        {
                                                            if ((DateTime)at.getValor() > (DateTime)con.getValor())
                                                            {
                                                                con.setEstado(true);
                                                            }
                                                        }
                                                    }


                                                }

                                            }
                                        }


                                        if (cons.getEstado())
                                        {
                                            table.eliminar(i);
                                            elimino = true;
                                        }
                                    }
                                    if (elimino)
                                    {
                                        i--;
                                    }
                                    foreach (Condiciones c in listaCondiciones)
                                    {
                                        c.falso();
                                        c.setEstado(false);
                                    }
                                    
                                }



                                

                            }


                        }
                        


                    }




                    
                }
                //Posicion del token
                posicion++;
            }
        }


        private void insertar()
        {
            int posicion = 0;
            foreach (Token token in listaTokens)
            {
                if (token.getToken() == 6)//Si encuentra la palabra insertar
                {
                    int nombreP = posicion + 2;
                    Token t = (Token)listaTokens[nombreP];
                    foreach (Tabla table in listaTablas)
                    {

                        if (table.getNombre().Equals(t.getLexema().ToUpper()))
                        {
                            Registro caract = table.getCaracteristicas();
                            int atributo = 0;
                            Registro nuevo = new Registro();

                            for (int i = nombreP; i < listaTokens.Count; i++)
                            {
                                Token temp = (Token)listaTokens[i];
                                Token dato = (Token)listaTokens[i - 1];
                                Atributo cabeza = (Atributo)caract.getAtributo()[atributo];

                                if (temp.getToken() == 22)//Parentesis que cierra
                                {

                                    nuevo.addAtribute(new Atributo(cabeza.getNombre().ToUpper(), cabeza.getTipo(), dato.getLexema()));
                                    break;
                                }
                                else if (temp.getToken() == 19)//Si es coma creao atributos del registro caracteristica
                                {
                                    nuevo.addAtribute(new Atributo(cabeza.getNombre().ToUpper(), cabeza.getTipo(), dato.getLexema()));
                                    atributo++;
                                }



                            }
                            table.addRegistro(nuevo);

                            //Estoy en la tabla debo agregar un registro
                        }
                    }

                }
                posicion++;
            }
        }

        private void crear()
        {
            int posicion = 0;
            foreach (Token token in listaTokens)
            {

                if (token.getToken() == 4)//Si encuentra la palabra crear
                {
                    int nombreP = posicion + 2;
                    Token t = (Token)listaTokens[nombreP];
                    String name = t.getLexema().ToUpper();
                    Tabla nueva = new Tabla(name);
                    Console.WriteLine("-------------");
                    Console.WriteLine(name);
                    //Con este detecta el nombre
                    Registro caracteristica = new Registro();
                    for (int i = nombreP; i < listaTokens.Count; i++)
                    {
                        Token temp = (Token)listaTokens[i];
                        if (temp.getToken() == 22)//Parentesis que cierra
                        {
                            Token tipo = (Token)listaTokens[i - 1];
                            Token variable = (Token)listaTokens[i - 2];

                            caracteristica.addAtribute(new Atributo(variable.getLexema().ToUpper(), tipo.getToken()));
                            break;
                        }
                        else if (temp.getToken() == 19)//Si es coma creao atributos del registro caracteristica
                        {
                            Token tipo = (Token)listaTokens[i - 1];
                            Token variable = (Token)listaTokens[i - 2];

                            caracteristica.addAtribute(new Atributo(variable.getLexema().ToUpper(), tipo.getToken()));
                        }
                    }
                    nueva.setCaracteristica(caracteristica);
                    listaTablas.Add(nueva);

                }
                posicion++;
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
                        parser.limpiar();

                        parser.parser(listaTokens);
                        listaErrores.AddRange(parser.getErrores());

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
            reporteTablas("Memoria");
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




            Control controlBox;
            if (tabControl1.SelectedTab.HasChildren)
            {
                foreach (Control item in tabControl1.SelectedTab.Controls)
                {
                    controlBox = item;

                    if (controlBox is RichTextBox)
                    {
                        reporteErrores(tabControl1.SelectedTab.Name);


                    }
                }
            }
        }

        private void arbolDeDerivacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            arbol();
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
                        String contenido = a.Text;
                        String texto = a.SelectedText;
                        int inicio = a.Text.IndexOf(a.SelectedText);
                        int largo = a.SelectedText.Length;
                        String busqueda = Interaction.InputBox("Busqueda de palabra a reemplazar", "Busqueda", "Palabra", -1, -1);
                        if (contenido.Length == texto.Length || texto.Length == 0)
                        {
                            //Reemplaza en todo
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
                        else
                        {

                            //Remplazo en seleccionado


                            String nuevo = "";
                            if (contenido.Contains(busqueda))
                            {
                                a.Clear();
                                Console.WriteLine("Contenido: " + contenido);
                                Console.WriteLine("Seleccion:" + texto);
                                String remplazo = Interaction.InputBox("Palabra para reemplazar", "Reemplazo", "Palabra", -1, -1);
                                for (int i = 0; i < inicio; i++)
                                {
                                    nuevo += contenido[i].ToString();


                                }
                                texto = texto.Replace(busqueda, remplazo);

                                nuevo += texto;

                                for (int i = inicio + largo; i < contenido.Length; i++)
                                {
                                    nuevo += contenido[i].ToString();
                                }
                                a.Text = nuevo;


                            }
                            else
                            {
                                MessageBox.Show("No se encontro la palabra que busca", "Error al reemplazar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }




                        }







                    }
                }
            }
        }
        public static void QuickReplace(RichTextBox rtb, String word, String word2)
        {
            rtb.Text = rtb.Text.Replace(word, word2);
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

        public void reporteTablas(String archivo)
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
                string pathRep = pathFolder + "\\Tablas_" + archivo + ".html";
                StreamWriter repHtml = new StreamWriter(pathRep);


                //Escribir la tabla html
                repHtml.WriteLine("<!DOCTYPE html>");
                repHtml.WriteLine("<html>");
                repHtml.WriteLine("<head>");
                repHtml.WriteLine("<title>Tablas</title>");
                repHtml.WriteLine("<meta charset=\"utf-8\">");
                repHtml.WriteLine("<h1 style=\"text-align:center\">Universidad de San Carlos de Guatemala</h1>");
                repHtml.WriteLine("<h2 style=\"text-align: center\">Organizacion de lenguajes y compiladores 1</h2>");
                repHtml.WriteLine("<h4 style=\"text-align: center\">Oscar Armin Crisóstomo Ruiz - 201709140</h4>");
                repHtml.WriteLine("</head>");
                repHtml.WriteLine("<body>");
                repHtml.WriteLine("<center>");
                foreach (Tabla table in listaTablas)
                {
                    repHtml.WriteLine("<br>");
                    repHtml.WriteLine("<table border=\"1\" style=\"width:60%\">");
                    repHtml.WriteLine("<caption><h3>Tabla " + table.getNombre() + "</h3></caption>");
                    repHtml.WriteLine("<colgroup>");
                    repHtml.WriteLine("<col style=\"width: 10% \"/>");
                    repHtml.WriteLine("<col style=\"width: 20% \"/>");
                    repHtml.WriteLine("<col style=\"width: 70% \"/>");
                    repHtml.WriteLine("</colgroup>");
                    repHtml.WriteLine("<thead>");

                    repHtml.WriteLine("<tr>");
                    foreach (Atributo atr in table.getCaracteristicas().getAtributo())
                    {
                        repHtml.WriteLine("<th >" + atr.getNombre() + "</th>");
                    }

                    repHtml.WriteLine("</tr>");
                    repHtml.WriteLine("</thead>");
                    repHtml.WriteLine("<tfoot>");
                    repHtml.WriteLine("<tr>");
                    repHtml.WriteLine("<td colspan=\"" + table.getCaracteristicas().getAtributo().Count + "\">Fin tabla</td>");
                    repHtml.WriteLine("</tr>");
                    repHtml.WriteLine("</tfoot>");
                    repHtml.WriteLine("<tbody>");

                    foreach (Registro item in table.getRegistros())
                    {
                        repHtml.WriteLine("<tr>");
                        foreach (Atributo at in item.getAtributo())
                        {
                            repHtml.WriteLine("<td>" + at.getValor().ToString() + "</td>");

                        }
                        repHtml.WriteLine("</tr>");


                    }

                    repHtml.WriteLine("</tbody>");
                    repHtml.WriteLine("</table>");

                }

                repHtml.WriteLine("</center>");
                repHtml.WriteLine("</body>");
                repHtml.WriteLine("</html>");

                repHtml.Close();
                System.Diagnostics.Process.Start(pathRep);
            }
            catch (Exception er)
            {
                MessageBox.Show("Error al generar tablas " + er.ToString(), "Error con reporte lexico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void arbol()
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string pathFolder = pathDesktop + "\\Proyecto1\\Arbol_201709140\\";

            try
            {
                //Existe el directorio para el archivo, sino crearlo
                if (!Directory.Exists(pathFolder))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(pathFolder);
                }

                //Crear el reporte
                string pathRep = pathFolder + "\\arbol.dot";
                string image = pathFolder + "\\arbol.png";
                StreamWriter repHtml = new StreamWriter(pathRep);
                repHtml.WriteLine(parser.getDot());

                repHtml.Close();
                //dot -Tpng graph.dot -o graph.png 

                
                System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c dot -Tpng -o " + image +" " + pathRep);
                
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                //Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
                procStartInfo.CreateNoWindow = false;
                //Inicializa el proceso
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                //System.Diagnostics.Process.Start(image);
            }
            catch (Exception er)
            {
                MessageBox.Show("Error al generar tablas " + er.ToString(), "Error con reporte lexico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }


        private int valorT(int token)
        {
            if (token == 33)//Entero
            {
                return 0;
            }
            else if (token == 34)//Flotante
            {
                return 2;
            }
            else if (token == 35)//Candena
            {
                return 1;
            }
            else if (token == 36)//Fecha
            {
                return 3;
            }
            return 1;
        }

        private void buscarToolStripMenuItem_Click(object sender, EventArgs e)
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
                        String contenido = a.Text;
                        String texto = a.SelectedText;
                        int inicio = a.Text.IndexOf(a.SelectedText);
                        int largo = a.SelectedText.Length;
                        String busqueda = Interaction.InputBox("Busqueda de palabra a reemplazar", "Busqueda", "Palabra", -1, -1);

                        //Busca en todo
                        if (controlBox.Text.Contains(busqueda))
                        {
                            int index = 0;
                            String temp = a.Text;
                            a.Text = "";
                            a.Text = temp;
                            while (index < a.Text.LastIndexOf(busqueda))
                            {
                                a.Find(busqueda, index, a.Text.Length, RichTextBoxFinds.None);
                                a.SelectionBackColor = Color.FromArgb(12, 183, 242);
                                index = a.Text.IndexOf(busqueda, index) + 1;


                            }
                        }
                        else
                        {
                            //Mensaje que no se encontro la palabra
                            MessageBox.Show("No se encontro la palabra que busca", "No encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }




                    }
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Object ob;
            Object ob1;
            String a = parser.getDot();
            Console.WriteLine(a);
            
            DateTime fecha = Convert.ToDateTime("15/06/2020");
            DateTime fecha1 = Convert.ToDateTime("19/06/2020");

            ob = fecha;
            ob1 = fecha1;


            if ((DateTime)ob < (DateTime)ob1)
            {
                Console.WriteLine("---");
            }

            Console.WriteLine("---");
        }
    }
}
