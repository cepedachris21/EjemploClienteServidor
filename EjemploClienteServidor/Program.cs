using System;

using System.Collections.Generic;

using System.Net;

using System.Net.Sockets;

using System.Text;



namespace ServidorProducto

{

    class Program

    {

        static List<string> productos = new List<string>();



        static void Main(string[] args)

        {

            TcpListener servidor = new TcpListener(IPAddress.Any, 5000);

            servidor.Start();

            Console.WriteLine("Servidor iniciado en el puerto 5000...");



            while (true)

            {

                TcpClient cliente = servidor.AcceptTcpClient();

                NetworkStream stream = cliente.GetStream();



                byte[] buffer = new byte[1024];

                int bytesLeidos = stream.Read(buffer, 0, buffer.Length);

                string mensaje = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);



                string respuesta = "";



                if (mensaje.StartsWith("AGREGAR:"))

                {

                    string nuevoProducto = mensaje.Substring(8).Trim();

                    productos.Add(nuevoProducto);

                    respuesta = $"Producto '{nuevoProducto}' agregado.";

                }

                else if (mensaje == "VER")

                {

                    respuesta = productos.Count == 0

                        ? "No hay productos."

                        : string.Join(", ", productos);

                }

                else

                {

                    respuesta = "Comando no reconocido.";

                }



                byte[] datosRespuesta = Encoding.UTF8.GetBytes(respuesta);

                stream.Write(datosRespuesta, 0, datosRespuesta.Length);



                cliente.Close();

            }

        }

    }

}