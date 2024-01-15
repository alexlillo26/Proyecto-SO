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

typedef struct {
	char nombre [20];
	int socket;
} Conectado;

typedef struct {
	Conectado conectados [100];
	int num;
} ListaConectados;

ListaConectados miLista;


//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

MYSQL *conn;
MYSQL_RES *resultado;
MYSQL_ROW row;



int Registro(char Usuario[80], char Contrasena[80], char Respuesta[512]) {
	pthread_mutex_lock(&mutex);
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char ConsultaResultante[1250];
	int ResultadoConsulta;
	
	// Consulta para verificar si el usuario ya existe
	strcpy(ConsultaResultante, "SELECT jugador.usuario FROM jugador WHERE jugador.usuario = '");
	strcat(ConsultaResultante, Usuario);
	strcat(ConsultaResultante, "'");
	ResultadoConsulta = mysql_query(conn, ConsultaResultante);
	if (ResultadoConsulta != 0)
	{
		printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
	}
	
	else
	{
		// Recogemos el resultado de la consulta en una tabla virtual MySQL
		resultado = mysql_store_result(conn);
		
		// Recogemos el resultado de la primera fila
		row = mysql_fetch_row(resultado);
		
		// Si no encuentra ningÃºn usuario con ese nombre
		if (row == NULL)
		{
			mysql_free_result(resultado);
			// Consulta para contar el nÃºmero total de jugadores
			strcpy(ConsultaResultante, "SELECT COUNT(*) FROM jugador");
			ResultadoConsulta = mysql_query(conn, ConsultaResultante);
			
			if (ResultadoConsulta != 0) {
				printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
				sprintf(Respuesta, "2/%s/ERROR", Usuario);
			} else {
				// Recogemos el resultado de la consulta en una tabla virtual MySQL
				resultado = mysql_store_result(conn);
				row = mysql_fetch_row(resultado);
				int NumeroJugadoresInicial = atoi(row[0]);
				
				// Incrementamos el nÃºmero de jugadores
				NumeroJugadoresInicial++;
				// Volvemos a construir la consulta de inserciÃ³n
				memset(ConsultaResultante, 0, strlen(ConsultaResultante));
				sprintf(ConsultaResultante, "INSERT INTO jugador (ID, usuario, contrasena) VALUES (%d, '%s', '%s')", NumeroJugadoresInicial, Usuario, Contrasena);
				// Ejecutamos la consulta para insertar el nuevo jugador
				ResultadoConsulta = mysql_query(conn, ConsultaResultante);
				if (ResultadoConsulta == 0) {
					// Ã‰xito en la inserciÃ³n
					pthread_mutex_unlock(&mutex);
					return 0;
				}
				else 
				{
					// Error al insertar datos en la base
					printf("Error al introducir datos en la base %u %s\n", mysql_errno(conn), mysql_error(conn));
					sprintf(Respuesta, "2/%s/ERROR", Usuario);
				}
			}
		} else
			{
			// Si se encuentra un usuario con ese nombre
			printf("Ya hay un usuario registrado con ese usuario");
			sprintf(Respuesta, "2/%s/NO", Usuario);
			pthread_mutex_unlock(&mutex);
			return -2;
			}
	}
	
	pthread_mutex_unlock(&mutex);
	return -1;
}

int Login(char Usuario[80], char Contrasena[80], char Respuesta[512]) 
{
	pthread_mutex_lock(&mutex);
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	char ConsultaResultante[1250];
	int ResultadoConsulta;
	// Consulta para verificar si el usuario ya existe
	strcpy(ConsultaResultante, "SELECT jugador.usuario FROM jugador WHERE jugador.usuario = '");
	strcat(ConsultaResultante, Usuario);
	strcat (ConsultaResultante,"' AND jugador.contrasena = '");
	strcat (ConsultaResultante, Contrasena);
	strcat (ConsultaResultante,"'");
	
	
	ResultadoConsulta = mysql_query(conn, ConsultaResultante);
	if (ResultadoConsulta != 0) {
		printf("Error al consultar datos de la base %u %s\n", mysql_errno(conn), mysql_error(conn));
		pthread_mutex_unlock(&mutex);
	} 
	else
	{
		// Recogemos el resultado de la consulta en una tabla virtual MySQL
		resultado = mysql_store_result(conn);
		
		// Recogemos el resultado de la primera fila
		row = mysql_fetch_row(resultado);
	
		// Si no encuentra ningun usuario con ese nombre
		if (row == NULL)
		{
			
			printf("Inicio de sesion fallido, regístrate\n");
			sprintf(Respuesta, "1/%s/NO", Usuario);
			return -1;
		}
		else
		{
			// Si se encuentra un usuario con ese nombre
			printf("Inicio de sesion exitoso");
			//Pon (&miLista, Usuario, sock_conn);
			sprintf(Respuesta, "1/%s/OK", Usuario);
			pthread_mutex_unlock(&mutex);
			return 0;
		
		}
	}
	pthread_mutex_unlock(&mutex);
	return -2;	
}


int Pon (ListaConectados *lista, char nombre[20], int socket) {
	// AÃ±ade nuevo conectado. Retorna 0 si OK y -1 si la lista
	// ya estaba llena y no lo ha podido aÃ±adir
	if (lista->num == 100)
		return -1;
	else {
		strcpy (lista->conectados[lista->num].nombre, nombre);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
}

int DamePosicion (ListaConectados *lista, char nombre[20]) {
	// Devuelve la posicion en la lista o -1 si no estÃ¡ en la lista
	int i=0;
	int encontrado = 0;
	while ((i < lista->num) && !encontrado)
	{
		if (strcmp(lista->conectados[i].nombre,nombre) ==0)
			encontrado =1;
		if (!encontrado)
			i=i+1;
	}
	if (encontrado)
		return i;
	else
		return -1;
}

int Elimina (ListaConectados *lista, char nombre[20]) {
	// Retorna 0 si elimina y -1 si ese usuario no estÃ¡ en la lista
	
	int pos = DamePosicion (lista, nombre);
	if (pos == -1)
		return -1;
	else {
		int i;
		for (i=pos; i < lista->num-1; i++)
		{
			lista->conectados[i] = lista->conectados[i+1];
			strcpy (lista->conectados[i].nombre, lista->conectados[i+1].nombre);
			//lista->conectados[i].socket = lista->conectados[i+1].socket;
		}
		lista->num--;
		return 0;
	}
}

void DameConectados (ListaConectados *lista, char conectados[300]) {
	// Pone en conectados los nombres de todos los conectados separados por /.
	// Primero pone el numero de conectados. Ejemplo: "3/Juan/Maria/Pedro"
	sprintf (conectados,"%d", lista->num);
	int i;
	for (i=0; i< lista->num; i++)
		sprintf (conectados, "%s/%s", conectados, lista->conectados[i].nombre);
}


int DameSocket (ListaConectados *lista, char nombre[20])
{ // Devuelve el socket o -1 si no está en la lista
	int i = 0;
	int encontrado = 0;
	while ((i<lista->num) && !encontrado)
	{
		if (strcmp(lista->conectados[i].nombre, nombre) == 0)
			encontrado = 1;
		if (!encontrado)
			i = i+1;
	}
	if (encontrado)
		return lista->conectados[i].socket;
	else
		return -1;
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
		
		if (codigo != 0)
		{
			
			// INICIALITZACIONS
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
		}

		if (codigo ==0){ //petici?n de desconexi?n
			
		char usuario[80];
		p = strtok(NULL, "/");
		strcpy (usuario, p); // Ya tenemos el usuario
		char notificacion[20];
			
			pthread_mutex_lock(&mutex);
			int eliminado = Elimina (&miLista, usuario);
			pthread_mutex_unlock(&mutex);
			
			if (eliminado == 0){
			
			char misConectados [300];
			DameConectados (&miLista, misConectados);
			printf ("Resultado: %s\n", misConectados);
			
			//char *s = strtok (misConectados, "/");
			//int n = atoi (s);
			//printf ("NUM CONECTADOS: %d\n", n);
			// notificar a todos los clientes conectados
			int j;
			sprintf (notificacion, "5/%s", misConectados);
			printf ("Notificacion: %s\n", notificacion);
			
			for (j=0; j< i; j++)
				write (sockets[j],notificacion, strlen(notificacion));

			terminar=1;
			}
			
			else
				printf("Ese usuario no esta en la lista");
			
		}	
		
		
		else if (codigo == 1) //Iniciar sesion
		{
			
			char usuario[80];
			char contrasena[80];
			char respuesta[80];
			p = strtok(NULL, "/");
			strcpy (usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (contrasena, p); //Conseguimos la contrasena
			char notificacion[20];
			
			int res = Login(usuario, contrasena, respuesta);
			
			
			if(res == 0)
			{
				
				sprintf(respuesta, "1/%s/SI", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				
				Pon (&miLista, usuario, sock_conn);
				char misConectados [300];
				DameConectados (&miLista, misConectados);
				printf ("Resultado: %s\n", misConectados);
				
				// notificar a todos los clientes conectados
				char notificacion[20];
				int j;
				sprintf (notificacion, "5/%s", misConectados);
				printf ("Notificacion: %s\n", notificacion);
				
				for (j=0; j< i; j++)
					write (sockets[j],notificacion, strlen(notificacion));
			
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (res == -1)
			{
				sprintf(respuesta,"1/%s/NO", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (res == -2)
			{
				sprintf(respuesta,"1/%s/ERROR", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			
			
		}
		
		
		else if (codigo == 2){
			
		
			
			char usuario[80];
			char contrasena[80];
			char respuesta[80];
			p = strtok(NULL, "/");
			strcpy (usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (contrasena, p); //Conseguimos la contrasena
			
			
			
			int res = Registro(usuario, contrasena, respuesta);
			
			
			if(res == 0)
			{
				sprintf(respuesta, "2/%s/SI", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
			
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (res == -1)
			{
				sprintf(respuesta,"2/%s/ERROR", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (res == -2)
			{
				sprintf(respuesta,"2/%s/NO", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			
			pthread_mutex_lock( &mutex ); //No me interrumpas ahora
			contador = contador +1;
			pthread_mutex_unlock( &mutex); //ya puedes interrumpirme
			//notificar a todos los clientes conectados
			
		
			
		}
		
			
		else if (codigo == 3)
		{
		
			
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
		else if (codigo == 6) //Invitación
		{
			//Invitado es la persona que a la que va destinada la invitacion
			char invitado[200];
			
			//Persona que invita
			char anfitrion[200];
			char password[200];
			char respuesta[200];
			
			char misConectados[500];
			char notificacion[200];
			
			p = strtok(NULL, "/");
			strcpy (invitado, p);
			
			p = strtok(NULL, "/");
			strcpy (anfitrion, p);
			
			p = strtok(NULL, "/");
			strcpy (password, p);
			
			
			
			int reslogin = Login(anfitrion, password, respuesta);
			printf("Resultado:  %d", reslogin);
			
			if(reslogin == 0)
			{
				DameConectados(&miLista, misConectados);
				char *s = strtok(misConectados, "/");
				int numconectados =  atoi (s);
				printf("Num conectados: %d\n", numconectados);
				
				int encontrado = 0;
				int i = 0;
				
				while((i<numconectados) && (encontrado == 0))
				{
					char conectado [20];
					s = strtok(NULL, "/");
					strcpy (conectado, s);
					printf("Conectado: %s\n ", conectado);
					//Mira mediante strcmp si el invitado está conectado
					if(strcmp(conectado, invitado) == 0)
					{
						encontrado = 1;
					}
					else
					   i++;
				}
				
				if(encontrado == 1)
				{
					int res = DameSocket (&miLista, invitado);
					
					printf("Socket de %s, %d", invitado, res);
					
					if (res == -1)
					{	
						//No hay nadie con ese usuario en la lista
						strcpy (respuesta, "6/-1");
						write (sock_conn, respuesta, strlen (respuesta));
					}
					else
					{
						//Respuesta a la persona que hace la invitacion
						strcpy (respuesta, "6/0");
						write (sock_conn, respuesta, strlen (respuesta));
						
						
						//Notificacion para el invitado, incluye su nombre y el del anfitrion
						sprintf (notificacion, "7/%s/%s", invitado, anfitrion);
						printf("notificacion %s ", notificacion);
						write (res, notificacion, strlen (notificacion));
						mysql_close(conn);
						
						
					}
				}
				else
				{
					sprintf(respuesta,"6/-2");
					write(sock_conn, respuesta, strlen(respuesta));
					mysql_free_result(resultado);
					mysql_close(conn);
				}
				
				
				
				
			}
			
			else if(reslogin == -1)
			{
				sprintf(respuesta,"6/-3");
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (reslogin == -2)
			{
				sprintf(respuesta,"6/-4");
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			
			
		}
		
		else if (codigo == 8)
		{
			char usuario[80];
			char contrasena[80];
			char respuesta[80];
			p = strtok(NULL, "/");
			strcpy (usuario, p); // Ya tenemos el usuario
			p = strtok(NULL, "/");
			strcpy (contrasena, p); //Conseguimos la contrasena
			char notificacion[20];
			
			int res = Login(usuario, contrasena, respuesta);
			
			
			if(res == 0)
			{
				
				sprintf(respuesta, "8/%s/SI", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (res == -1)
			{
				sprintf(respuesta,"8/%s/NO", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			else if (res == -2)
			{
				sprintf(respuesta,"8/%s/ERROR", usuario);
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
		}
		
		else if (codigo == 9) //Respuesta a la invitación
		{
			//Invitado es la persona que a la que va destinada la invitacion
			char invitado[200];
			//Persona que invita
			char anfitrion[200];
			//Resuesta del invitado a la invitacion
			char decision[200];
			//Respuesta que mandará al cliente
			char respuesta[200];
			
			char misConectados[500];
			char notificacion[200];
			
			p = strtok(NULL, "/");
			strcpy (invitado, p);
			
			p = strtok(NULL, "/");
			strcpy (anfitrion, p);
			
			p = strtok(NULL, "/");
			strcpy (decision, p);
			
			DameConectados(&miLista, misConectados);
			char *s = strtok(misConectados, "/");
			int numconectados =  atoi (s);
			printf("Num conectados: %d\n", numconectados);
			
			int encontrado = 0;
			int i = 0;
			
			while((i<numconectados) && (encontrado == 0))
			{
				char conectado [20];
				s = strtok(NULL, "/");
				strcpy (conectado, s);
				printf("Conectado: %s\n ", conectado);
				//Mira mediante strcmp si el invitado está conectado
				if(strcmp(conectado, anfitrion) == 0)
				{
					encontrado = 1;
				}
				else
				   i++;
			}
			
			if(encontrado == 1)
			{
				int res = DameSocket (&miLista, anfitrion);
				
				printf("Socket de %s, %d", anfitrion, res);
				
				if (res == -1)
				{	
					//No hay nadie con ese usuario en la lista
					strcpy (respuesta, "9/-1");
					write (sock_conn, respuesta, strlen (respuesta));
				}
				else
				{
					//Respuesta a la persona que hace la invitacion
					strcpy (respuesta, "9/0");
					write (sock_conn, respuesta, strlen (respuesta));
					
					
					//Notificacion para el invitado, incluye su nombre y el del anfitrion
					sprintf (notificacion, "10/%s/%s/%s", anfitrion, invitado, decision);
					printf("notificacion %s ", notificacion);
					write (res, notificacion, strlen (notificacion));
					mysql_close(conn);
					
					
				}
			}
			else
			{
				//Error
				sprintf(respuesta,"9/-2");
				write(sock_conn, respuesta, strlen(respuesta));
				mysql_free_result(resultado);
				mysql_close(conn);
			}
			
		}
		
	}
			
	
	// Se acabo el servicio para este cliente
	close(sock_conn); 
}

int main(int argc, char *argv[])
{
	ListaConectados miLista;
	miLista.num =0;
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	

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
	serv_adr.sin_port = htons(50092);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	//contador =0;
	
	pthread_t thread;
	i=0;
	for (;;)
	{
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
