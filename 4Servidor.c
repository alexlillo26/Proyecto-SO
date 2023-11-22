#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>

int contador;

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

MYSQL *conn;
MYSQL_RES *resultado;
MYSQL_ROW row;

int Registro(char nombre[25], char contrasena[20])
{
	
	int err;
	//Busca si ya hay un usuario con ese nombre
	char consulta[100];
	strcpy(consulta, "SELECT nombre FROM jugador WHERE nombre='");
	strcat(consulta, nombre);
	strcat(consulta, "'");
	err = mysql_query(conn, consulta);
	if (err!=0)
		return -1;
	
	else {
		resultado = mysql_store_result(conn);
		row =  mysql_fetch_row(resultado);
		//Se registra si no hay nadie con el mismo nombre
		if (row==NULL) {
			
			err=mysql_query(conn, "SELECT FROM jugador WHERE id=(SELECT max(id) FROM jugador);");
			if (err!=0){
				return -1;
			}
			resultado = mysql_store_result(conn);
			row =  mysql_fetch_row(resultado);
			printf("%s\n",row[0]);
			sprintf(consulta,"INSERT INTO jugador VALUES ('%s','%s');", nombre, contrasena);
			printf("%s\n",consulta);
			err= mysql_query(conn, consulta);
			if (err!=0)
				return -1;
			else
				return 0; 
		}
		else 
			return -1;

	}
}	
			
			

void *AtenderCliente (void *socket)
{
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	
	//int socket_conn = * (int *) socket;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	int ResultadoConsulta1;
	int ResultadoConsulta2;
	
	
	int terminar =0;
	// Entramos en un bucle para atender todas las peticiones de este cliente
	//hasta que se desconecte
	while (terminar ==0)
	{
		// Ahora recibimos la petici?n
		ret=read(sock_conn,peticion, sizeof(peticion));
		printf ("Recibido\n");
		
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		peticion[ret]='\0';
		
		
		printf ("Peticion: %s\n",peticion);
		
		// vamos a ver que quieren
		char *p = strtok( peticion, "/");
		int codigo =  atoi (p);
		

		if (codigo ==0) //petici?n de desconexi?n
			terminar=1;
		else if (codigo == 2){
			char usuario[80];
			char contrasena[80];
			p = strtok(NULL, "/");
			strcpy (usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (contrasena, p); //Conseguimos la contrasena
			
			int res = Registro(usuario, contrasena);
			sprintf(respuesta,"2/%d", res);
			write(sock_conn, respuesta, strlen(respuesta));
		}
		
			
		else if (codigo == 3)
		{
			conn = mysql_init(NULL);
			if (conn == NULL) 
			{
				// Manejar error de inicialización de MySQL
				printf ("Error al crear la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
			}
			
			conn = mysql_real_connect(conn, "localhost", "root", "mysql", "juego", 0, NULL, 0);
			if (conn == NULL) 
			{
				// Manejar error de conexión a la base de datos
				printf ("Error al inicializar la conexion: %u %s\n", 
						mysql_errno(conn), mysql_error(conn));
			}
			
			char consulta1[512];
			strcpy(consulta1, "SELECT jugador.usuario FROM (jugador, partidas) WHERE jugador.edad < '19' AND partidas.puntosganador > '40'");
			
			ResultadoConsulta1 = mysql_query(conn, consulta1);
			if (ResultadoConsulta1 != 0) 
			{
				// Manejar error de consulta
			}
			
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado);
			if (row == NULL) 
			{
				// Manejar caso de que no hayan resultados
				// Enviar un mensaje al cliente indicando que no hay resultados
				char respuesta[512];
				sprintf(respuesta, "%d/No hay usuarios con menos de 19 años ni partidas donde los puntos del ganador sean más de 40", codigo);
				write(sock_conn, respuesta, strlen(respuesta));
			} 
			else 
			{
				// Procesar resultados y enviar al cliente
				char respuesta[512];
				sprintf(respuesta, "3/%s", row[0]);
				write(sock_conn, respuesta, strlen(respuesta));
			}
			mysql_free_result(resultado);
			mysql_close(conn);
			
			
		}
			
		else if (codigo == 4)
		{
			conn = mysql_init(NULL);
			if (conn == NULL) 
			{
				// Manejar error de inicialización de MySQL
			}
			
			conn = mysql_real_connect(conn, "localhost", "root", "mysql", "juego", 0, NULL, 0);
			if (conn == NULL) 
			{
				// Manejar error de conexión a la base de datos
			}
			
			char consulta1[512];
			strcpy(consulta1, "SELECT jugador.puntos FROM (jugador, partidas) WHERE partidas.ganador = 'luis44' AND partidas.duracion < '5'");
			
			ResultadoConsulta1 = mysql_query(conn, consulta1);
			if (ResultadoConsulta1 != 0) 
			{
				// Manejar error de consulta
			}
			
			resultado = mysql_store_result(conn);
			row = mysql_fetch_row(resultado);
			if (row == NULL) 
			{
				// Manejar caso de que no hayan resultados
				// Enviar un mensaje al cliente indicando que no hay resultados
				char respuesta[512];
				sprintf(respuesta, "4/No estan disponibles los puntos de Luis");
				write(sock_conn, respuesta, strlen(respuesta));
			} 
			else 
			{
				// Procesar resultados y enviar al cliente
				char respuesta[512];
				int puntos = atoi(row[0]);
				sprintf(respuesta, "4/%d", puntos);
				write(sock_conn, respuesta, strlen(respuesta));
			}
			mysql_free_result(resultado);
			mysql_close(conn);
			
			
			
		}
		
		
		if ((codigo ==1) || (codigo==2) || (codigo==3) || (codigo==4))
		{
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			contador = contador +1;
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			// notificar a todos los clientes conectados
			char notificacion[20];
			sprintf (notificacion, "5/%d",contador);
			int j;
			for (j=0; j< i; j++)
				write (sockets[j],notificacion, strlen(notificacion));
			
		}
			
			
			
			
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}


int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(50090);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	contador =0;
	
	pthread_t thread;
	i=0;
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		sockets[i] =sock_conn;
		//sock_conn es el socket que usaremos para este cliente
		
		// Crear thead y decirle lo que tiene que hacer
		
		pthread_create (&thread, NULL, AtenderCliente,&sockets[i]);
		i=i+1;
		
	}
	

	
}
