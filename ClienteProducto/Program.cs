using System;

using System.Net.Sockets;

using System.Text;



namespace ClienteProducto

{

    class Program

    {

        static void Main(string[] args)

        {

            Console.WriteLine("Cliente de productos iniciado.");

            while (true)

            {

                Console.WriteLine("\nSeleccione una opción:");

                Console.WriteLine("1. Agregar producto");

                Console.WriteLine("2. Ver productos");

                Console.WriteLine("3. Salir");



                string opcion = Console.ReadLine();



                if (opcion == "1")

                {

                    Console.Write("Ingrese el nombre del producto: ");

                    string producto = Console.ReadLine();

                    EnviarMensaje($"AGREGAR:{producto}");

                }

                else if (opcion == "2")

                {

                    EnviarMensaje("VER");

                }

                else if (opcion == "3")

                {

                    break;

                }

                else

                {

                    Console.WriteLine("Opción no válida.");

                }

            }

        }



        static void EnviarMensaje(string mensaje)

        {

            try

            {

                TcpClient cliente = new TcpClient("127.0.0.1", 5000);

                NetworkStream stream = cliente.GetStream();



                byte[] datos = Encoding.UTF8.GetBytes(mensaje);

                stream.Write(datos, 0, datos.Length);



                byte[] buffer = new byte[1024];

                int bytesLeidos = stream.Read(buffer, 0, buffer.Length);

                string respuesta = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);



                Console.WriteLine("Respuesta del servidor: " + respuesta);



                cliente.Close();

            }

            catch (Exception ex)

            {

                Console.WriteLine("Error al conectar con el servidor: " + ex.Message);

            }

        }

    }

}