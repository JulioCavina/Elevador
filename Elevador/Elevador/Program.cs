Logica logica = new Logica();

void MenuPrincipal()
{
    Console.Clear();
    Console.WriteLine("Bem vindo ao ELEVADOR!!!\n\nEscolha uma das opções abaixo:\n");
    Console.WriteLine("1. Iniciar elevador em andares aleatórios");
    Console.WriteLine("2. Escolher os andares para os elevadores");
    //Console.WriteLine("3. Como funciona este programa?");
    Console.WriteLine("3. Sair\n");
    string opcaoEscolhida = Console.ReadLine()!;

    switch (opcaoEscolhida)
    {
        case "1":
            //("1. Iniciar elevador em andares aleatórios");
            logica.GerarElevadorAleatorio();
            MenuPrincipal();
            break;
        case "2":
            //("2. Escolher os andares para os elevadores");
            logica.GerarElevadorNormal();
            MenuPrincipal();
            break;
        case "3":
            Console.WriteLine("\nObrigado por usar o programa!");
            break;
        default:
            Console.WriteLine($"\nOpção escolhida \"{opcaoEscolhida}\" não é válida...\nPressione qualquer tecla para tentar novamente.");
            Console.ReadKey();
            MenuPrincipal();
            break;
    }

}

MenuPrincipal();