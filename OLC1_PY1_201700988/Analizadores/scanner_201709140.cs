
using OLC1_PY1_201700988.Objetos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLC1_PY1_201700988.Analizadores
{
    class scanner_201709140
    {
        private ArrayList listaTokens;
        private ArrayList listaErrores;
        private int estado;
        private string aux = "";



        public ArrayList scannerMethod(string entrada)
        {
            entrada += "#";
            listaErrores = new ArrayList();
            listaTokens = new ArrayList();
            estado = 0;
            aux = "";

            int fila = 0;
            int columna = 0;

            char caracter;
            for (int i = 0; i < entrada.Length; i++)
            {
                caracter = entrada[i];
                switch (estado)
                {
                    case 0:
                        if (caracter == '*')//*
                        {
                            columna++;
                            estado = 1;

                        }
                        else if (caracter == ',')//,
                        {
                            columna++;
                            estado = 2;
                        }
                        else if (caracter == ';')//;
                        {
                            columna++;
                            estado = 3;
                        }
                        else if (caracter == '(')//(
                        {
                            columna++;
                            estado = 4;
                        }
                        else if (caracter == ')')//)
                        {
                            columna++;
                            estado = 5;
                        }
                        else if (caracter == '!')//!
                        {
                            columna++;
                            estado = 6;
                        }
                        else if (caracter == '<')//<
                        {
                            columna++;
                            estado = 8;
                        }
                        else if (caracter == '>')//>
                        {
                            columna++;
                            estado = 10;
                        }
                        else if (caracter == '=')
                        {
                            columna++;
                            estado = 12;
                        }
                        else if (caracter == '.')
                        {
                            columna++;
                            estado = 13;
                        }
                        else if (caracter == '-')
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 14;
                        }
                        else if (caracter == '/')
                        {
                            columna++;
                            aux += caracter.ToString();
                            estado = 16;
                        }
                        else if (Char.IsDigit(caracter)) //digito
                        {
                            columna++;
                            aux += caracter.ToString();
                            estado = 20;
                        }
                        else if (caracter == '"')
                        {
                            columna++;
                            estado = 23;
                            aux += caracter.ToString();
                        }
                        else if (caracter == 39)//Comilla simple
                        {
                            columna++;
                            aux += caracter.ToString();
                            estado = 25;
                        }
                        else if (Char.IsLetter(caracter))//Letra
                        {
                            columna++;
                            aux += caracter.ToString();

                            estado = 37;

                        }
                        else if (caracter == 13 || caracter == 10 || caracter == 11)
                        {
                            fila++;
                        }
                        else if (caracter == ' ')
                        {
                            columna++;
                        }
                        else
                        {

                            listaErrores.Add(new Error("Error Lexico", "Caracter no pertenece al lenguaje", caracter.ToString(), fila, columna));
                            columna++;

                        }
                        break;
                    case 1:
                        listaTokens.Add(new Token(18, "*", "ASTERISCO", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 2:
                        listaTokens.Add(new Token(19, ",", "COMA", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 3:
                        listaTokens.Add(new Token(20, ";", "PUNTO Y COMA", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 4:
                        listaTokens.Add(new Token(21, "(", "PARENTESIS ABRE", fila, columna));
                        estado = 0;
                        i--;
                        break;

                    case 5:
                        listaTokens.Add(new Token(22, ")", "PARENTESIS CIERRA", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 6:
                        if (caracter == '=')
                        {
                            estado = 7;
                            columna++;
                        }
                        else
                        {
                            listaTokens.Add(new Token(29, "!", "NOT", fila, columna));


                            estado = 0;
                            i--;
                        }
                        break;
                    case 7:
                        listaTokens.Add(new Token(23, "!=", "DIFERENTE", fila, columna));
                        i--;
                        estado = 0;
                        break;
                    case 8:
                        if (caracter == '=')
                        {
                            estado = 9;
                            columna++;
                        }
                        else
                        {
                            listaTokens.Add(new Token(26, "<", "MENOR QUE", fila, columna));
                            estado = 0;
                            i--;
                        }

                        break;
                    case 9:
                        listaTokens.Add(new Token(25, "<=", "MENOR O IGUAL", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 10:
                        if (caracter == '=')
                        {
                            estado = 11;
                            columna++;
                        }
                        else
                        {
                            listaTokens.Add(new Token(28, ">", "MAYOR QUE", fila, columna));
                            estado = 0;
                            i--;
                        }

                        break;
                    case 11:
                        listaTokens.Add(new Token(27, ">=", "MAYOR O IGUAL", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 12:
                        listaTokens.Add(new Token(24, "=", "IGUAL", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 13:
                        listaTokens.Add(new Token(30, ".", "PUNTO", fila, columna));
                        estado = 0;
                        i--;
                        break;
                    case 14:
                        if (caracter == '-')
                        {
                            estado = 15;
                            aux += caracter.ToString();
                            columna++;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "Caracter no pertenece al lenguaje", "-", fila, columna));
                            i--;
                            aux = "";
                            estado = 0;
                        }
                        break;
                    case 15:
                        if (caracter == 13 || caracter == 10 || caracter == 11)
                        {
                            listaTokens.Add(new Token(31, aux, "COMENTARIO UNILINEA", fila, columna));
                            fila++;
                            estado = 0;
                            aux = "";


                        }
                        else
                        {
                            aux += caracter.ToString();
                        }
                        break;
                    case 16:
                        if (caracter == '*')
                        {
                            estado = 17;
                            aux += caracter.ToString();
                            columna++;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "Caracter no pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;
                        }
                        break;
                    case 17:
                        if (caracter == '*')
                        {
                            aux += caracter.ToString();
                            estado = 18;
                            columna++;
                        }
                        else if (caracter == 10 || caracter == 11 || caracter == 13)
                        {
                            aux += caracter.ToString();
                            fila++;
                        }
                        else
                        {
                            aux += caracter.ToString();
                            columna++;
                        }
                        break;
                    case 18:
                        if (caracter == '/')
                        {
                            estado = 19;
                            columna++;
                            aux += caracter.ToString();
                        }
                        else if (caracter == '*')
                        {
                            columna++;
                            aux += caracter.ToString();
                        }
                        else
                        {
                            aux += caracter.ToString();

                            columna++;
                            estado = 17;
                        }
                        break;
                    case 19:
                        listaTokens.Add(new Token(32, aux, "COMENTARIO MULTILINEA", fila, columna));
                        aux = "";
                        estado = 0;
                        i--;
                        break;
                    case 20:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;

                        }
                        else if (caracter == '.')
                        {
                            estado = 21;
                            aux += caracter.ToString();
                            columna++;

                        }
                        else
                        {
                            listaTokens.Add(new Token(33, aux, "ENTERO", fila, columna));
                            aux = "";
                            estado = 0;
                            i--;
                        }
                        break;
                    case 21:
                        if (caracter == '.')
                        {
                            estado = 22;
                            aux += caracter.ToString();
                            columna++;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "Caracter no pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;
                        }
                        break;
                    case 22:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;

                        }
                        else
                        {
                            listaTokens.Add(new Token(34, aux, "FLOTANTE", fila, columna));
                            estado = 0;
                            aux = "";
                            i--;
                        }
                        break;
                    case 23:
                        if (caracter == '"')
                        {
                            estado = 24;
                            columna++;
                            aux += caracter.ToString();
                        }
                        else
                        {
                            aux += caracter.ToString();
                            columna++;
                        }
                        break;
                    case 24:
                        listaTokens.Add(new Token(35, aux, "CADENA", fila, columna));
                        aux = "";
                        estado = 0;
                        i--;
                        break;
                    case 25:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 26:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 27;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 27:

                        if (caracter == '/')
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 28;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 28:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 29;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 29:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 30;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 30:

                        if (caracter == '/')
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 31;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 31:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 32;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 32:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 33;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 33:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 34;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;

                    case 34:
                        if (Char.IsDigit(caracter))
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 35;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 35:
                        if (caracter == 39)
                        {
                            aux += caracter.ToString();
                            columna++;
                            estado = 36;
                        }
                        else
                        {
                            listaErrores.Add(new Error("Error Lexico", "No pertenece al lenguaje", aux, fila, columna));
                            i--;

                            aux = "";
                            estado = 0;

                        }
                        break;
                    case 36:
                        listaTokens.Add(new Token(36, aux, "FECHA", fila, columna));
                        aux = "";
                        estado = 0;
                        i--;
                        break;
                    case 37:
                        if (Char.IsLetterOrDigit(caracter) || caracter == '_')
                        {
                            aux += caracter.ToString();
                            columna++;

                        }
                        else
                        {
                            String temp = "";
                            temp = aux.ToUpper();
                            if (temp.Equals("ENTERO"))
                            {
                                listaTokens.Add(new Token(0, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("CADENA"))
                            {
                                listaTokens.Add(new Token(1, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("FLOTANTE"))
                            {
                                listaTokens.Add(new Token(2, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("FECHA"))
                            {
                                listaTokens.Add(new Token(3, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("CREAR"))
                            {
                                listaTokens.Add(new Token(4, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("TABLA"))
                            {
                                listaTokens.Add(new Token(5, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("INSERTAR"))
                            {
                                listaTokens.Add(new Token(6, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("EN"))
                            {
                                listaTokens.Add(new Token(7, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("VALORES"))
                            {
                                listaTokens.Add(new Token(8, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("SELECCIONAR"))
                            {
                                listaTokens.Add(new Token(9, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("COMO"))
                            {
                                listaTokens.Add(new Token(10, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("DE"))
                            {
                                listaTokens.Add(new Token(11, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("DONDE"))
                            {
                                listaTokens.Add(new Token(12, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("Y"))
                            {
                                listaTokens.Add(new Token(13, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("O"))
                            {
                                listaTokens.Add(new Token(14, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("ELIMINAR"))
                            {
                                listaTokens.Add(new Token(15, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("ACTUALIZAR"))
                            {
                                listaTokens.Add(new Token(16, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else if (temp.Equals("ESTABLECER"))
                            {
                                listaTokens.Add(new Token(17, aux, "PALABRA RESERVADA", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }
                            else
                            {
                                listaTokens.Add(new Token(37, aux, "IDENTIFICADOR", fila, columna));
                                estado = 0;
                                aux = "";
                                i--;
                            }

                        }
                        break;



                }
            }



            return listaTokens;

        }

        public void reporteLexico(String archivo)
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
                string pathRep = pathFolder + "\\Tokens_" + archivo + ".html";
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
                repHtml.WriteLine("<th >Token</th>");
                repHtml.WriteLine("<th >Lexema</th>");
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

                foreach (Token item in listaTokens)
                {
                    repHtml.WriteLine("<tr>");
                    repHtml.WriteLine("<td>" + item.getToken() + "</td>");
                    repHtml.WriteLine("<td>" + item.getLexema() + "</td>");
                    repHtml.WriteLine("<td>" + item.getDescrp() + "</td>");
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

        public ArrayList getErrores()
        {
            return listaErrores;
        }

        

        private void limpiarVariables()
        {
            aux = "";
            estado = 0;



        }
    }
}
