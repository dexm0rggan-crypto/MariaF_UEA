using System;

namespace AgendaClinica
{
    // REGISTRO (STRUCT)
    public struct TurnoInfo
    {
        public string Consultorio;
        public string Estado;
    }

    // CLASE PACIENTE
    public class Paciente
    {
        private string nombre = "";
        private string cedula = "";
        private DateTime fechaTurno;
        private string especialidad = "";
        private TurnoInfo infoTurno;

        public void SetNombre(string valor)
        {
            nombre = valor;
        }

        public void SetCedula(string valor)
        {
            cedula = valor;
        }

        public void SetFechaTurno(DateTime valor)
        {
            fechaTurno = valor;
        }

        public void SetEspecialidad(string valor)
        {
            especialidad = valor;
        }

        public void SetInfoTurno(TurnoInfo valor)
        {
            infoTurno = valor;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public string GetCedula()
        {
            return cedula;
        }

        public DateTime GetFechaTurno()
        {
            return fechaTurno;
        }

        public string GetEspecialidad()
        {
            return especialidad;
        }

        public TurnoInfo GetInfoTurno()
        {
            return infoTurno;
        }
    }

    // CLASE AGENDA
    public class AgendaTurnos
    {
        // VECTOR
        private Paciente[] listaPacientes = new Paciente[100];
        private int cantidadPacientes = 0;

        // MATRIZ
        private string[,] horarios =
        {
            { "Medicina General", "08:00" },
            { "Pediatría", "09:00" },
            { "Odontología", "10:00" },
            { "Cardiología", "11:00" }
        };

        public void MostrarHorarios()
        {
            Console.WriteLine("\n--- HORARIOS DISPONIBLES ---");

            for (int i = 0; i < horarios.GetLength(0); i++)
            {
                Console.WriteLine(horarios[i, 0] + " - " + horarios[i, 1]);
            }
        }

        public void AgregarTurno(Paciente paciente)
        {
            if (cantidadPacientes < listaPacientes.Length)
            {
                listaPacientes[cantidadPacientes] = paciente;
                cantidadPacientes++;

                Console.WriteLine("\nTurno agregado correctamente.");
            }
            else
            {
                Console.WriteLine("\nNo hay espacio disponible.");
            }
        }

        public void MostrarTurnos()
        {
            if (cantidadPacientes == 0)
            {
                Console.WriteLine("\nNo existen turnos registrados.");
                return;
            }

            Console.WriteLine("\n--- TURNOS REGISTRADOS ---");

            for (int i = 0; i < cantidadPacientes; i++)
            {
                Paciente p = listaPacientes[i];

                Console.WriteLine("\n-------------------------");
                Console.WriteLine("Nombre: " + p.GetNombre());
                Console.WriteLine("Cédula: " + p.GetCedula());
                Console.WriteLine("Fecha: " + p.GetFechaTurno().ToShortDateString());
                Console.WriteLine("Especialidad: " + p.GetEspecialidad());
                Console.WriteLine("Consultorio: " + p.GetInfoTurno().Consultorio);
                Console.WriteLine("Estado: " + p.GetInfoTurno().Estado);
            }
        }

        public void BuscarPorCedula(string cedula)
        {
            bool encontrado = false;

            for (int i = 0; i < cantidadPacientes; i++)
            {
                if (listaPacientes[i].GetCedula() == cedula)
                {
                    Paciente p = listaPacientes[i];

                    Console.WriteLine("\n--- PACIENTE ENCONTRADO ---");
                    Console.WriteLine("Nombre: " + p.GetNombre());
                    Console.WriteLine("Cédula: " + p.GetCedula());
                    Console.WriteLine("Especialidad: " + p.GetEspecialidad());
                    Console.WriteLine("Consultorio: " + p.GetInfoTurno().Consultorio);

                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Console.WriteLine("\nNo se encontró ningún paciente.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AgendaTurnos agenda = new AgendaTurnos();
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("\n===== AGENDA DE TURNOS CLÍNICA =====");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Mostrar turnos");
                Console.WriteLine("3. Buscar por cédula");
                Console.WriteLine("4. Mostrar horarios");
                Console.WriteLine("5. Salir");

                Console.Write("\nSeleccione una opción: ");
                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":

                        Paciente nuevo = new Paciente();

                        Console.Write("Nombre: ");
                        nuevo.SetNombre(Console.ReadLine() ?? "");

                        Console.Write("Cédula: ");
                        nuevo.SetCedula(Console.ReadLine() ?? "");

                        Console.Write("Fecha del turno (AAAA-MM-DD): ");
                        DateTime fecha;

                        if (DateTime.TryParse(Console.ReadLine(), out fecha))
                        {
                            nuevo.SetFechaTurno(fecha);
                        }
                        else
                        {
                            nuevo.SetFechaTurno(DateTime.Today);
                        }

                        Console.Write("Especialidad: ");
                        nuevo.SetEspecialidad(Console.ReadLine() ?? "");

                        TurnoInfo info = new TurnoInfo();

                        Console.Write("Consultorio: ");
                        info.Consultorio = Console.ReadLine() ?? "";

                        info.Estado = "Agendado";

                        nuevo.SetInfoTurno(info);

                        agenda.AgregarTurno(nuevo);

                        break;

                    case "2":
                        agenda.MostrarTurnos();
                        break;

                    case "3":

                        Console.Write("Ingrese la cédula: ");
                        string cedula = Console.ReadLine() ?? "";

                        agenda.BuscarPorCedula(cedula);

                        break;

                    case "4":
                        agenda.MostrarHorarios();
                        break;

                    case "5":

                        salir = true;
                        Console.WriteLine("\nPrograma finalizado.");

                        break;

                    default:
                        Console.WriteLine("\nOpción inválida.");
                        break;
                }
            }
        }
    }
}