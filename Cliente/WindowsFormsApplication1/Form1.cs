using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        Thread atender;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Necesario para que los elementos de los formularios puedan ser
            //accedidos desde threads diferentes a los que los crearon
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
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
                string mensaje = mensaje = trozos[1].Split('\0')[0];
              

                switch (codigo)
                    {

                        case 1: //Registrarse

                      

                            break;

                        case 2:   //Iniciar Sesión

                        

                        break;

                        case 3: // Consulta 1

                        MessageBox.Show("El usuario es: " + mensaje);

                        break;

                        case 4: // Consulta 2

                        MessageBox.Show("Los puntos son: " + mensaje);
                        break;

                        case 5: // Recibimos notificación

                        label2.Text = mensaje;

                        break;
                    }
                }
            }
        


                    


        private void conectar_Click(object sender, EventArgs e) //Perf
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9052);


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

        private void desconectar_Click(object sender, EventArgs e)
        {
            //Mensaje de desconexión
            string mensaje = "0/";

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            atender.Abort();
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();


        }

        private void registrarse_Click(object sender, EventArgs e)
        {

            if ((contrasena.Text != "") && (usuario.Text != ""))
            {
                string mensaje = "3/" + usuario.Text + '/' + contrasena.Text;

                // Enviamos al servidor el nombre tecleado
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Arrancamos el thread que atenderá los mensajes del servidor
                ThreadStart ts = delegate
                {
                    AtenderServidor();
                };
                atender = new Thread(ts);
                atender.Start();


            }

            else
            {
                MessageBox.Show("No has introducido todos los datos necesarios para registrarte");
            }

        }


        private void iniciarsesion_Click(object sender, EventArgs e)
        {


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            
                if ((contrasena.Text != "") || (usuario.Text != ""))
                {
          
                    string mensaje = "2/" + usuario.Text + '/' + contrasena.Text;
                    // Enviamos al servidor el nombre tecleado
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);
                    //Arrancamos el thread que atenderá los mensajes del servidor
                    ThreadStart ts = delegate
                    {
                        AtenderServidor();
                    };
                    atender = new Thread(ts);
                    atender.Start();
                    
                    
                }
                else
                {
                    MessageBox.Show("No has introducido todos los datos necesarios para loguearte");
                }

             
        }

        private void Consulta1_Click(object sender, EventArgs e)
        {
            //Envio respuesta
            string mensaje = "3/";
            byte[]msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
           
        }

        private void Consulta2_Click(object sender, EventArgs e)
        {
            //Envio respuesta
            string mensaje = "4/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
