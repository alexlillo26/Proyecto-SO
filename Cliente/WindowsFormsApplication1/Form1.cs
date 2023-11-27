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
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

        string respuestaservidor;

        private void AtenderServidor()
        {
            while (true)
            {
                //Recibimos mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                Console.WriteLine("Msg2 es " + msg2);

                //Separamos el vector con / para saber la petición y los datos
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');

                // Número de petición

                int codigo = Convert.ToInt32(trozos[0]);
                string mensaje = trozos[1].Split('\0')[0];
                string respuestaservidor; //Aquí recibirá del servidor un texto que determinará si el registro es correcto o no.



                switch (codigo)
                    {

                        case 1: 
                        



                        break;

                        case 2:   //registro

                        respuestaservidor = trozos[2].Split('\0')[0];
                        if (respuestaservidor == "SI")
                        {
                            MessageBox.Show("Cuenta creada satisfactoriamente, saludos " + trozos[1]);
                        }

                        else if (respuestaservidor == "NO")
                        {
                            MessageBox.Show("El nombre de usuario facilitado ya existe, prueba con otro que esté disponible");
                        }

                        else if (respuestaservidor == "ERROR")
                        {
                            MessageBox.Show("Ha ocurrido un error inesperado, prueba de intentarlo hacer más tarde");
                        }

                        break;

                        

                    }
                }
            }
        


                    


        private void conectar_Click(object sender, EventArgs e) //Perf
        {
            //Creamos un IPEndPoint con el ip del servidor y puerto del servidor 
            //al que deseamos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 50090);


            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);//Intentamos conectar el socket
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");

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
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();


        }

        private void registrarse_Click(object sender, EventArgs e)
        {

            if ((contrasena.Text != "") && (usuario.Text != ""))
            {
                string mensaje = "2/" + usuario.Text + '/' + contrasena.Text + '/';

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
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show("Estado del registro: " + mensaje);


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

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show("La consulta seleccionada y los puntos son: " + mensaje);
           
        }

        private void Consulta2_Click(object sender, EventArgs e)
        {
            //Envio respuesta
            string mensaje = "4/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            //Recibimos la respuesta del servidor
            byte[] msg2 = new byte[80];
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
            MessageBox.Show("Los puntos son: " + mensaje);
        }
    }
}
