
class Program
{
    static void Main(string[] args)
    {
        // Diccionario: nombreEquipo -> conjunto de jugadores
        Dictionary<string, HashSet<string>> torneo = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
        int opcion;

        do
        {
            Console.WriteLine("\n--- Menú Torneo de Fútbol ---");
            Console.WriteLine("1. Registrar equipo");
            Console.WriteLine("2. Agregar jugador a un equipo");
            Console.WriteLine("3. Mostrar equipos y jugadores");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    RegistrarEquipo(torneo);
                    break;

                case 2:
                    AgregarJugadorAEquipo(torneo);
                    break;

                case 3:
                    MostrarTorneo(torneo);
                    break;

                case 0:
                    Console.WriteLine("Saliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

        } while (opcion != 0);
    }

    static void RegistrarEquipo(Dictionary<string, HashSet<string>> torneo)
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string nombreEquipo = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(nombreEquipo))
        {
            Console.WriteLine("El nombre del equipo no puede estar vacío.");
            return;
        }

        if (!torneo.ContainsKey(nombreEquipo))
        {
            torneo[nombreEquipo] = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Console.WriteLine($"Equipo '{nombreEquipo}' registrado con éxito.");
        }
        else
        {
            Console.WriteLine("Ese equipo ya existe en el torneo.");
        }
    }

    static void AgregarJugadorAEquipo(Dictionary<string, HashSet<string>> torneo)
    {
        Console.Write("Ingrese el nombre del equipo: ");
        string equipoJugador = (Console.ReadLine() ?? string.Empty).Trim();

        if (!torneo.ContainsKey(equipoJugador))
        {
            Console.WriteLine("Ese equipo no existe.");
            return;
        }

        Console.Write("Ingrese el nombre del jugador: ");
        string nombreJugador = (Console.ReadLine() ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(nombreJugador))
        {
            Console.WriteLine("El nombre del jugador no puede estar vacío.");
            return;
        }

        if (torneo[equipoJugador].Add(nombreJugador))
        {
            Console.WriteLine($"Jugador '{nombreJugador}' agregado al equipo '{equipoJugador}'.");
        }
        else
        {
            Console.WriteLine("Ese jugador ya está en el equipo.");
        }
    }

    static void MostrarTorneo(Dictionary<string, HashSet<string>> torneo)
    {
        Console.WriteLine("\n--- Equipos y Jugadores ---");
        if (torneo.Count == 0)
        {
            Console.WriteLine("Aún no hay equipos registrados.");
            return;
        }

        foreach (var kvp in torneo)
        {
            Console.WriteLine($"\nEquipo: {kvp.Key}");
            if (kvp.Value.Count == 0)
            {
                Console.WriteLine("  No hay jugadores registrados.");
            }
            else
            {
                foreach (var jugador in kvp.Value)
                {
                    Console.WriteLine($"  - {jugador}");
                }
            }
        }
    }
}
