using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GridOfCells
{
    public partial class Form1 : Form
    {
        //int jugador = 0;
        int fila, columna;
        Socket server;
        Thread atender;
        int count;

        

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que los crearon

            matriz.DefaultCellStyle.BackColor = Color.Black;
            

        }
       


        private bool turnoJugador1 = true;

        private void CambiarTurno()
        {
            
            // Cambia el turno al otro jugador
             turnoJugador1 = !turnoJugador1;

           
        }
       

      

     
        private bool[,] casillaPintada;

        private void matriz_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int contador = 0;
            fila = e.RowIndex;
            columna = e.ColumnIndex;

           
                if (casillaPintada[fila, columna] == false)
                {
                    // Pinta la casilla
                    matriz.Rows[fila].Cells[columna].Style.BackColor = (turnoJugador1) ? Color.Red : Color.Blue;

                   

                    // Marca la casilla como pintada
                    casillaPintada[fila, columna] = true;


                   


                    // Cambia el turno al siguiente jugador
                    CambiarTurno();
                }
                else
                {
                    // La casilla ya está pintada, manejar según sea necesario
                    MessageBox.Show("La casilla ya está ocupada.");
                }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int filas = matriz.RowCount;
            int columnas = matriz.ColumnCount;
            casillaPintada = new bool[filas, columnas];
            matriz.ColumnCount = 15;
            matriz.RowCount = 15;
            matriz.ColumnHeadersVisible = false;
            matriz.RowHeadersVisible = false;
            matriz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            
        }
        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                //Separamos el vector con / para saber la petición y los datos
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                // Número de petición
                int codigo = Convert.ToInt32(trozos[0]);


                if (codigo == 0)
                {
                    string mensaje = trozos[1].Split('\0')[0];
                    //Iniciar Sesión
                    string respuestaservidor = respuestaservidor = trozos[2].Split('\0')[0];

                    if (respuestaservidor == "SI")
                    {
                        MessageBox.Show("Bienvenido de nuevo " + mensaje);
                    }

                    else if (respuestaservidor == "NO")
                    {
                        MessageBox.Show("Error al iniciar sesión, prueba a regístrarte");
                    }
                    else if (respuestaservidor == "ERROR")
                    {
                        MessageBox.Show("Ha ocurrido un error inesperado, prueba de intentarlo hacer más tarde");
                    }
                }

                else if (codigo == 1)
                {
                    string mensaje = trozos[1].Split('\0')[0];
                    //Iniciar Sesión
                    string respuestaservidor = respuestaservidor = trozos[2].Split('\0')[0];

                    if (respuestaservidor == "SI")
                    {
                        MessageBox.Show("Bienvenido de nuevo " + mensaje);
                    }

                    else if (respuestaservidor == "NO")
                    {
                        MessageBox.Show("Error al iniciar sesión, prueba a regístrarte");
                    }
                    else if (respuestaservidor == "ERROR")
                    {
                        MessageBox.Show("Ha ocurrido un error inesperado, prueba de intentarlo hacer más tarde");
                    }

                }
                else if (codigo == 2)
                {
                    string mensaje = trozos[1].Split('\0')[0];
                    //Registrarse
                    string respuestaservidor = respuestaservidor = trozos[2].Split('\0')[0];

                    if (respuestaservidor == "SI")
                    {
                        MessageBox.Show("Cuenta creada satisfactoriamente, saludos " + mensaje);
                    }

                    else if (respuestaservidor == "NO")
                    {
                        MessageBox.Show("El nombre de usuario facilitado ya existe, prueba con otro que esté disponible");
                    }

                    else if (respuestaservidor == "ERROR")
                    {
                        MessageBox.Show("Ha ocurrido un error inesperado, prueba de intentarlo hacer más tarde");
                    }
                }
                else if (codigo == 3)
                {
                    string mensaje = trozos[1].Split('\0')[0];
                    // Consulta 1
                    MessageBox.Show("El usuario es: " + mensaje + " y el código " + codigo);
                }
                else if (codigo == 4)
                {
                    string mensaje = trozos[1].Split('\0')[0];
                    // Consulta 2
                    MessageBox.Show("Los puntos son: " + mensaje);
                }
                else if (codigo == 5)
                {

                    int num = Convert.ToInt32(trozos[1]);
                    string mensaje;

                    if ((mensaje = trozos[2].Split('\0')[0]) != null)
                    {
                        mensaje = trozos[2].Split('\0')[0];

                        label2.Text = ""; // Limpia el contenido actual del label

                        for (int i = 0; i < num; i++)
                        {
                            int j = i + 2;
                            label2.Text += trozos[j] + "\n"; // Concatena el texto y agrega un salto de línea
                        }
                    }

                    else
                        label2.Text = "ERROR";

                }

                else if (codigo == 6)
                {

                    int resultado = Convert.ToInt32(trozos[1]);

                    if (resultado == 0)
                    {
                        MessageBox.Show("Invitación enviada correctamente");
                    }

                    else if (resultado == -1)
                    {
                        MessageBox.Show("No hay ningú￺n jugador con ese nombre de usuario");
                    }


                    else if (resultado == -2)
                    {
                        MessageBox.Show("Ese usuario no está conectado actualmente");
                    }

                    else if (resultado == -3)
                    {
                        MessageBox.Show("No has iniciado sesión correctamente");
                    }

                    else if (resultado == -4)
                    {
                        MessageBox.Show("Error. Inténtalo de nuevo más tarde");
                    }

                }

                else if (codigo == 7)
                {
                    //Trozos[1] = invitado
                    //Trozos[2] = anfitrión

                    string invitado = trozos[1].Split('\0')[0];
                    string anfitrion = trozos[2].Split('\0')[0];

                    //MessageBox.Show(invitado + " has sido invitado a jugar por, " + anfitrion);

                    MessageBoxButtons botones = MessageBoxButtons.YesNo;
                    DialogResult invitacion = MessageBox.Show(invitado + ", ¿Quieres jugar con " + anfitrion + "?", " Invitación ", botones);
                    if (invitacion == DialogResult.Yes)
                    {
                        string mensaje = "9/" + invitado + '/' + anfitrion + '/' + " ACEPTO LA INVITACION " + '/';
                        // Enviamos al servidor el nombre tecleado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                    }

                    else
                    {
                        string mensaje = "9/" + invitado + '/' + anfitrion + '/' + " RECHAZO LA INVITACION " + '/';
                        // Enviamos al servidor el nombre tecleado
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);
                    }
                }

                else if (codigo == 8)
                {
                    string mensaje = trozos[1].Split('\0')[0];
                    //Iniciar Sesión
                    string respuestaservidor = respuestaservidor = trozos[2].Split('\0')[0];

                    if (respuestaservidor == "SI")
                    {
                        MessageBox.Show("Disfruta de la partida " + mensaje);
                        matriz.DefaultCellStyle.BackColor = Color.White;
                    }

                    else if (respuestaservidor == "NO")
                    {
                        MessageBox.Show("No has iniciado sesión");
                    }
                    else if (respuestaservidor == "ERROR")
                    {
                        MessageBox.Show("Ha ocurrido un error inesperado, prueba de intentarlo hacer más tarde");
                    }

                }

                else if (codigo == 9)
                {

                    int resultado = Convert.ToInt32(trozos[1]);

                    if (resultado == 0)
                    {
                        MessageBox.Show("Decisión enviada correctamente");
                    }

                    else if (resultado == -1)
                    {
                        MessageBox.Show("No hay ningú￺n jugador con ese nombre de usuario");
                    }


                    else if (resultado == -2)
                    {
                        MessageBox.Show("Ha ocurrido un error. Inténtalo más tarde");
                    }


                }

                else if (codigo == 10)
                {
                    string anfitrion = trozos[1].Split('\0')[0];
                    string invitado = trozos[2].Split('\0')[0];
                    string decision = trozos[3].Split('\0')[0];

                    MessageBox.Show(anfitrion + ", " + invitado + "ha decidido lo siguiente: " + decision);
                }


            }
        }


        private void desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/" + usuario.Text + '/';

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();


        }


       



        private void registrarse_Click_1(object sender, EventArgs e)
        {

            if ((contrasena.Text != "") && (usuario.Text != ""))
            {
                string mensaje = "2/" + usuario.Text + '/' + contrasena.Text + '/';
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            else
            {
                MessageBox.Show("No has introducido todos los datos necesarios para registrarte");
            }
        }

        private void iniciarsesion_Click_1(object sender, EventArgs e)
        {
            if ((contrasena.Text != "") || (usuario.Text != ""))
            {
                string mensaje = "1/" + usuario.Text + '/' + contrasena.Text;
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                MessageBox.Show("No has introducido todos los datos necesarios para loguearte");
            }
        }

        private void conectar_Click(object sender, EventArgs e)
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 50092);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

                //pongo en marcha el thread que atenderá los mensajes del servidor
                ThreadStart ts = delegate { AtenderServidor(); };
                atender = new Thread(ts);
                atender.Start();

            }
            catch (SocketException ex)
            {
                //Si hay excepcion imprimimos error y salimos del programa con return 
                MessageBox.Show("No he podido conectar con el servidor");
                return;
            }
        }

        private void desconectar_Click_1(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/" + usuario.Text + '/';

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void Consulta1_Click(object sender, EventArgs e)
        {
            //Envio respuesta
            string mensaje = "3/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Consulta2_Click(object sender, EventArgs e)
        {
            //Envio respuesta
            string mensaje = "4/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void invitar_Click_1(object sender, EventArgs e)
        {
            if (this.invitado.Text != "")
            {
                string mensaje = "6/" + this.invitado.Text + '/' + this.usuario.Text + '/' + this.contrasena.Text + '/';
                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

            else
            {
                MessageBox.Show("No has introducido ningún usuario");
            }
        }

        private void jugar_Click(object sender, EventArgs e)
        {
            string mensaje = "8/" + usuario.Text + '/' + contrasena.Text;
            // Enviamos al servidor el nombre tecleado
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }
    }

}
