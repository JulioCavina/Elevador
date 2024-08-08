class Logica
{
    private string fraseInicial = "";

    List<Elevador> listaElevador = new List<Elevador>();



    public void GerarElevadorAleatorio()
    {
        fraseInicial = "1. Iniciar elevador em andares aleatórios";
        Console.Clear();
        listaElevador.Clear();
        Console.WriteLine(fraseInicial);
        Console.WriteLine("\nQuantos elevadores estarão ativos simultâneamente?\n");
        string escolhaElevadores = Console.ReadLine()!;
        if (int.TryParse(escolhaElevadores, out int numeroElevadores) && numeroElevadores > 0)
        {
            Random random = new Random();
            int menValor = -3;
            int maiValor = 21;
            int contador = 0;
            while (contador < numeroElevadores)
            {
                int randomNumber = random.Next(menValor, maiValor);
                Elevador elevador = new Elevador(contador + 1, randomNumber, "Parado", 10000, 10000);
                listaElevador.Add(elevador);
                Console.WriteLine();
                contador++;
            }


            IniciarTurnos();

        }
        else
        {
            Console.WriteLine("\nOpção inválida!\nPressione qualquer tecla para retornar ao menu principal.");
            Console.ReadKey();
        }
    }

    public void GerarElevadorNormal()
    {
        fraseInicial = "2. Elevadores com andaresescolhidos";
        Console.Clear();
        listaElevador.Clear();
        Console.WriteLine(fraseInicial);
        Console.WriteLine("\nQuantos elevadores estarão ativos simultâneamente?\n");
        string escolhaElevadores = Console.ReadLine()!;
        if (int.TryParse(escolhaElevadores, out int numeroElevadores) && numeroElevadores > 0)
        {
            for (int i = 0; i < numeroElevadores; i++)
            {
                Console.WriteLine($"\nEm qual andar o {i + 1}° elevador estará?\n");
                string escolhaAndar = Console.ReadLine()!;
                if (int.TryParse(escolhaAndar, out int numeroAndar) && numeroAndar > -4 && numeroAndar < 21)
                {
                    Elevador elevador = new Elevador(i + 1, numeroAndar, "Parado", 10000, 10000);
                    listaElevador.Add(elevador);
                }
                else
                {
                    Console.WriteLine("\nOpção inválida!\nPressione qualquer tecla para retornar ao menu principal.");
                    Console.ReadKey();
                    return;
                }
            }
            IniciarTurnos();

        }
        else
        {
            Console.WriteLine("\nOpção inválida!\nPressione qualquer tecla para retornar ao menu principal.");
            Console.ReadKey();
        }
    }


    public void IniciarTurnos()
    {
        int numeroUsuario = 10000;
        int numeroDestino = 10000;
        string opcaoEscolhida = "";

        while (opcaoEscolhida != "s")
        {
            int contadorOcupado = 0;
            Console.Clear();
            Console.WriteLine(fraseInicial);
            Console.WriteLine("\nCaso deseje sair, digite \"s\"");
            Console.WriteLine("Para passar o turno, digite \"p\"\n");
            foreach (Elevador elevador in listaElevador)
            {
                if (elevador.Status == "Ocupado")
                {
                    contadorOcupado++;
                }
            }
            if (contadorOcupado == listaElevador.Count)
            {
                Console.WriteLine("*Aviso*\nTodos elevadores ocupados, nenhum elevador receberá seus pedidos!\n");
            }

            int contador = 0;
            opcaoEscolhida = "";
            while (contador != 2 && opcaoEscolhida != "s" && opcaoEscolhida != "p")
            {
                if (contador != 0)
                {
                    Bronca();
                }

                foreach (Elevador elevador in listaElevador)
                {
                    elevador.MostrarElevador();
                }

                contador = 1;
                Console.WriteLine("\nEm qual andar o usuário está? (entre -3 até 20)\n");
                opcaoEscolhida = Console.ReadLine()!;
                if (int.TryParse(opcaoEscolhida, out numeroUsuario) && numeroUsuario > -4 && numeroUsuario < 21)
                {
                    contador = 2;
                }
            }
            contador = 0;
            while (contador != 2 && opcaoEscolhida != "s" && opcaoEscolhida != "p")
            {
                if (contador != 0)
                {
                    Bronca();
                }
                contador = 1;
                Console.WriteLine("\nPara qual andar o usuário deseja chegar? (entre -3 até 20)\n");
                opcaoEscolhida = Console.ReadLine()!;
                if (int.TryParse(opcaoEscolhida, out numeroDestino) && numeroDestino > -4 && numeroDestino < 21)
                {
                    contador = 2;
                }
            }
            if (opcaoEscolhida == "s")
            {
                Console.WriteLine("\nSaindo da simulação.\nPressione qualquer tecla para retornar ao menu principal.");
                Console.ReadKey();
                return;
            }
            else if (opcaoEscolhida == "p") { }
            else if (numeroUsuario == numeroDestino)
            {
                Console.WriteLine("\nO usuário já se encontra no andar desejado!\nPrecione qualquer tecla para tentar novamente.");
                Console.ReadKey();
            }
            else
            {
                ChecarElevadoresMesmoAndar(numeroUsuario, numeroDestino);

            }

            foreach (Elevador elevador in listaElevador)
            {
                elevador.CalcularTurnoCarga();
            }

        }

        Console.ReadKey();

    }

    public void ChecarElevadoresMesmoAndar(int numeroUsuario, int numeroDestino)
    {
        int idElevadorMesmoAndar = 0;
        string usuarioEsperando = "sim";
        foreach (Elevador elevador in listaElevador)
        {
            if (elevador.Status == "Disponível")
            {

                if (elevador.AndarAtual == numeroUsuario)
                {
                    idElevadorMesmoAndar = elevador.Id;
                    if (usuarioEsperando == "sim")
                    {
                        elevador.Status = "Ocupado";
                        elevador.AndarDesejadoDescarga = numeroDestino;
                        usuarioEsperando = "não";

                        if (numeroUsuario > numeroDestino)
                        {
                            elevador.Movimento = "Descendo";
                        }
                        else
                        {
                            elevador.Movimento = "Subindo";
                        }
                        Console.WriteLine($"\nElevador de ID: {elevador.Id} está no andar! {elevador.Movimento} para o andar {numeroDestino}!");
                    }
                }

            }

        }
        if (idElevadorMesmoAndar == 0)
        {
            ChecarProximidade(numeroUsuario, numeroDestino);
        }
        Console.ReadKey();
    }


    public void ChecarProximidade(int numeroUsuario, int numeroDestino)
    {
        int deltaAndar = 0;
        int deltaEscolhido = 10000;
        int idElevadorProximo = 0;
        foreach (Elevador elevador in listaElevador)
        {
            if (elevador.Status == "Disponível")
            {
                deltaAndar = Math.Abs(numeroUsuario - elevador.AndarAtual);

                if (deltaAndar < deltaEscolhido)
                {
                    deltaEscolhido = deltaAndar;
                    idElevadorProximo = elevador.Id;
                }
            }
        }

        Console.WriteLine($"\nO elevador q vai vir será de ID: {idElevadorProximo}, que está à {deltaEscolhido} andares de distância!");

        int contador = 1;

        foreach (Elevador elevador in listaElevador)
        {
            if (contador == idElevadorProximo)
            {
                elevador.Status = "Ocupado";
                elevador.AndarDesejadoDescarga = numeroDestino;
                elevador.AndarDesejadoCarga = numeroUsuario;

                if (elevador.AndarAtual > numeroUsuario)
                {
                    elevador.Movimento = "Descendo";
                }
                else
                {
                    elevador.Movimento = "Subindo";
                }

            }
            contador++;
        }

    }

    public void Bronca()
    {
        Console.WriteLine("\nOpção inválida! Favor tentar novamente.\n");

    }

}  