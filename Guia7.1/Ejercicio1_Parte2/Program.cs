using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_Parte2
{
    internal class Program
    {

        #region variables y  métodos del dominio del problema
        static int CantidadAccesos = 0;
        static double Recaudacion = 0;

        static void RegistrarAcceso(string id)
        {
            if (BusquedaAccesosPorIdentificador(id)<0)//sino lo encontró lo agrega
            {
                IndentificadoresAcceso[Contador++] = id;
            }
            CantidadAccesos++;
        }

        #region propio del ticket
        static double ValorBase = 0;
        static double ValorBasePorVehiculo = 0;
        static double PorDiasAplicado = 0;
        static double SubTotal = 0;
        static double ValorIVA = 0;
        static double SubTotalConIva = 0;
        static double EcoImpuesto = 0;
        static double TotalAPagar = 0;
        static double Dias = 0;
        static void IniciarNuevoTicket()
        {
            ValorBase = 0;
        }

        static void RegistrarVehículoEnTicket(int tipo, int cantidad)
        {
            ValorBasePorVehiculo = TarifaPorTipoVehiculo(tipo) * cantidad;
            ValorBase += ValorBasePorVehiculo;
        }

        static void CalcularTotalAPagar(int dias)
        {
            Dias = dias;
            PorDiasAplicado = PorcentajeAplicadoPorDia(dias);
            SubTotal = ValorBase * PorDiasAplicado / 100;
            ValorIVA = SubTotal * 21 / 100;
            SubTotalConIva = SubTotal + ValorIVA;
            EcoImpuesto = SubTotalConIva * 15 / 100;//es así! jaja

            TotalAPagar = SubTotalConIva + EcoImpuesto;

            Recaudacion += TotalAPagar;
        }

        static double TarifaPorTipoVehiculo(int tipo)
        {
            double valor = 0;
            switch (tipo)
            {
                case 1://sin vehículo
                    {
                        valor = 100;
                    }
                    break;
                case 2://moto
                    {
                        valor = 800;
                    }
                    break;
                case 3://auto
                    {
                        valor = 1000;
                    }
                    break;
                case 4: //camioneta
                    {
                        valor = 1500;
                    }
                    break;
                case 5: //bugy
                    {
                        valor = 5000;
                    }
                    break;
                case 6:
                    {
                        valor = 1200;
                    }
                    break;
            }
            return valor;
        }

        static double PorcentajeAplicadoPorDia(int dias)
        {
            double valor = 0;
            switch (dias)
            {
                case 1:
                    {
                        valor = 100;
                    }
                    break;
                case 2:
                    {
                        valor = 120;
                    }
                    break;
                case 3:
                    {
                        valor = 220;
                    }
                    break;
                case 4:
                    {
                        valor = 320;
                    }
                    break;
                case 5 - 10:
                    {
                        valor = 380;
                    }
                    break;
            }
            return valor;
        }
        #endregion
        #endregion

        #region contrato con el ministerio - escalando el sistema
        static string[] IndentificadoresAcceso = new string[100];
        static int Contador = 0;

        static int BusquedaAccesosPorIdentificador(string identificador)
        {
            int idx = -1;
            int n = 0;
            while (n< Contador && idx==-1)
            {
                if (IndentificadoresAcceso[n] == identificador)
                {
                    idx = n;
                }
            }
            return idx;
        }

        static void OrdenarAccesos()
        {
            for (int n = 0; n < Contador-1; n++)
            {
                for (int m = n+1; m < Contador; m++)
                {
                    if (IndentificadoresAcceso[n].CompareTo(IndentificadoresAcceso[m]) > 0)
                    {
                        string aux = IndentificadoresAcceso[n];
                        IndentificadoresAcceso[n] = IndentificadoresAcceso[m];
                        IndentificadoresAcceso[m] = aux;
                    }
                }
            }
        }
        #endregion


        #region métodos para la vista
        /*
           relacionados con las impresiones en pantalla y capturas de datos de la entrada del usuario
        */
        static void MostrarVistaMenu()
        {
            Console.Clear();

            Console.WriteLine(" \t\t Sistema de control de acceso \n\n");

            Console.WriteLine("\t1-  Verificar acceso");
            Console.WriteLine("\t2-  Imprimir Recaudación");
            Console.WriteLine("\t3-  Cantidad de CantidadAccesos");
            Console.WriteLine("\t4-  Listado de accesos");
            Console.WriteLine("\t5-  Busqueda Por Identificador");

            Console.WriteLine("\tOtro-  Cerrar");
        }

        static void MostrarVerificarAcceso()
        {
            Console.Clear();
            Console.WriteLine(" \t\t Verificación de acceso\n\n");


            Console.WriteLine(" Tiene Ticket valido 0:No - otro:Sí");
            bool valido = Convert.ToInt32(Console.ReadLine()) > 0;

            Console.WriteLine(" Ingrese el identificador");
            string id = Console.ReadLine();
            RegistrarAcceso(id);

            if (valido == false)
            {
                MostrarSolicitudYGeneracionTicket();
            }

            Console.WriteLine("Presione una tecla para volver al menú principal");
            Console.ReadKey();
        }

        static void MostrarSolicitudYGeneracionTicket()
        {
            Console.Clear();
            Console.WriteLine(" \t\t Generación de ticket\n\n");

            IniciarNuevoTicket();

            Console.WriteLine($" -------------------");
            Console.WriteLine(" Ingrese el tipo de vehículo -1 para cortar");
            int tipo = Convert.ToInt32(Console.ReadLine());
            int registrados = 0;
            while (tipo > -1)
            {
                Console.WriteLine(" - Cantidad de vehículos del tipo elegido");
                int cantidad = Convert.ToInt32(Console.ReadLine());

                RegistrarVehículoEnTicket(tipo, cantidad);

                Console.WriteLine($" * Valor Base (vehi:{tipo}/cant:{cantidad}) {ValorBasePorVehiculo,40:f2}");

                Console.WriteLine(" Ingrese el tipo de vehículo -1 para cortar");
                tipo = Convert.ToInt32(Console.ReadLine());

                registrados += cantidad;
            }
            Console.WriteLine($" -------------------");

            if (registrados > 0)
            {
                Console.WriteLine(" Cantidad de días de validez del ticket:");
                int dias = Convert.ToInt32(Console.ReadLine());

                CalcularTotalAPagar(dias);

                Console.WriteLine($" -------------------");
                string descripcionDias = $"Por Cant. Días ({Dias}) $";
                Console.WriteLine($"{descripcionDias,25}{SubTotal,40:f2}");
                Console.WriteLine($"{" IVA: (21%) ",25}{ValorIVA,40:f2}");
                Console.WriteLine($" -------------------");
                Console.WriteLine($"{" Con IVA  ",25}{SubTotalConIva,40:f2}");
                Console.WriteLine($"{" Eco: (15%) ",25}{EcoImpuesto,40:f2}");
                Console.WriteLine($" -------------------");
                Console.WriteLine($"{" Total a Pagar: $ ",25}{TotalAPagar,40:f2}");
            }
            else
            {
                Console.WriteLine($" No se registraron vehículos! ");
            }

            Console.WriteLine("Presione una tecla para volver al menú principal");
            Console.ReadKey();
        }

        static void MostrarImprimirRecaudacion()
        {
            Console.Clear();
            Console.WriteLine(" \t\t Consulta de Recaudación\n\n");

            Console.WriteLine(" \t\t\t\t Recaudación Total: {0:f2}\n\n", Recaudacion);

            Console.WriteLine("Presione una tecla para volver al menú principal");
            Console.ReadKey();
        }

        static void MostrarCantidadDeAccesos()
        {
            Console.Clear();
            Console.WriteLine(" \t\t Consulta de cantidad de Accesos\n\n");

            Console.WriteLine(" \t\t\t\t Cantidad de CantidadAccesos: {0}\n\n", CantidadAccesos);

            Console.WriteLine("Presione una tecla para volver al menú principal");
            Console.ReadKey();
        }

        static void MostrarBusquedaPorIdentificador()
        {
            Console.Clear();
            Console.WriteLine(" \t\t Búsqueda de accesos por identificador\n\n");

            Console.WriteLine(" \t\t\t\t Ingrese el identificaor requerido:\n\n");
            string id = Console.ReadLine();
            
            int idx=BusquedaAccesosPorIdentificador(id);

            if (idx > -1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" \t\t\t\t Registro encontrado\n\n\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" \t\t\t\t Registro no encontrado\n\n\n");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Presione una tecla para volver al menú principal");
            Console.ReadKey();
        }

        public static void MostrarListadoAccesos()
        {
            Console.Clear();
            Console.WriteLine(" \t\t Listado de accesos\n\n");

            OrdenarAccesos();
            for (int n = 0; n < Contador; n++)
            {
                Console.WriteLine("{0}", IndentificadoresAcceso[n]);
            }

            Console.WriteLine("Presione una tecla para volver al menú principal");
            Console.ReadKey();
        }
        #endregion


        static void Main(string[] args)
        {
            int op = 0;

            MostrarVistaMenu();
            op = Convert.ToInt32(Console.ReadLine());

            while (op != 0)
            {
                switch (op)
                {
                    case 1:
                        {
                            MostrarVerificarAcceso();
                        }
                        break;
                    case 2:
                        {
                            MostrarImprimirRecaudacion();
                        }
                        break;
                    case 3:
                        {
                            MostrarCantidadDeAccesos();
                        }
                        break;
                    case 4:
                        {
                            MostrarListadoAccesos();
                        }
                        break;
                    case 5:
                        {
                            MostrarBusquedaPorIdentificador();
                        }
                        break;

                    default:
                        {
                            op = 0;
                        }
                        break;
                }

                if (op != 0)
                {
                    MostrarVistaMenu();
                    op = Convert.ToInt32(Console.ReadLine());
                }
            }
        }
    }
}
