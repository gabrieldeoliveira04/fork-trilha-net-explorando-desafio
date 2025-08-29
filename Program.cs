using System.Text;
using DesafioProjetoHospedagem.Models;

Console.OutputEncoding = Encoding.UTF8;

List<Reserva> reservas = new List<Reserva>();

string opcao;
do
{
    Console.Clear();
    Console.WriteLine("=== Sistema de Reserva de Hotel ===");
    Console.WriteLine("1 - Cadastrar nova reserva");
    Console.WriteLine("2 - Listar reservas");
    Console.WriteLine("3 - Sair");
    Console.Write("Escolha uma opção: ");
    opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            CadastrarReserva(reservas);
            break;
        case "2":
            ListarReservas(reservas);
            break;
        case "3":
            Console.WriteLine("Encerrando o sistema...");
            break;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }

    if (opcao != "3")
    {
        Console.WriteLine("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }

} while (opcao != "3");


// ===== MÉTODOS AUXILIARES =====

static void CadastrarReserva(List<Reserva> reservas)
{
    Console.Clear();
    Console.WriteLine("=== Cadastro de Reserva ===");

    // Dados da suíte
    Console.Write("Digite o tipo da suíte: ");
    string tipoSuite = Console.ReadLine();

    Console.Write("Digite a capacidade da suíte: ");
    int capacidade = int.Parse(Console.ReadLine());

    Console.Write("Digite o valor da diária: R$ ");
    decimal valorDiaria = decimal.Parse(Console.ReadLine());

    Suite suite = new Suite(tipoSuite, capacidade, valorDiaria);

    // Dados da reserva
    Console.Write("Digite a quantidade de dias da reserva: ");
    int diasReservados = int.Parse(Console.ReadLine());

    Reserva reserva = new Reserva(diasReservados);
    reserva.CadastrarSuite(suite);

    // Cadastro de hóspedes
    List<Pessoa> hospedes = new List<Pessoa>();
    Console.WriteLine($"A suíte suporta até {capacidade} hóspedes.");
    for (int i = 0; i < capacidade; i++)
    {
        Console.Write($"Digite o nome do hóspede {i + 1} (ou ENTER para parar): ");
        string nome = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nome))
            break;

        Console.Write($"Digite o sobrenome do hóspede {i + 1} (opcional): ");
        string sobrenome = Console.ReadLine();

        hospedes.Add(new Pessoa(nome, sobrenome));
    }

    try
    {
        reserva.CadastrarHospedes(hospedes);
        reservas.Add(reserva);

        Console.WriteLine("\nReserva cadastrada com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao cadastrar reserva: {ex.Message}");
    }
}

static void ListarReservas(List<Reserva> reservas)
{
    Console.Clear();
    Console.WriteLine("=== Lista de Reservas ===");

    if (reservas.Count == 0)
    {
        Console.WriteLine("Nenhuma reserva cadastrada.");
        return;
    }

    int index = 1;
    foreach (var reserva in reservas)
    {
        Console.WriteLine($"\nReserva {index++}:");
        Console.WriteLine($" - Suíte: {reserva.Suite.TipoSuite}");
        Console.WriteLine($" - Hóspedes ({reserva.ObterQuantidadeHospedes()}):");

        foreach (var h in reserva.Hospedes)
            Console.WriteLine($"   * {h.NomeCompleto}");

        Console.WriteLine($" - Dias reservados: {reserva.DiasReservados}");
        Console.WriteLine($" - Valor total: R$ {reserva.CalcularValorDiaria():0.00}");
    }
}


