

class Elevador
{

    public Elevador(int id, int andarAtual, string movimento, int andarDesejadoDescarga, int andarDesejadoCarga)
    {
        Id = id;
        AndarAtual = andarAtual;
        Status = "Disponível";
        Movimento = movimento;
        AndarDesejadoDescarga = andarDesejadoDescarga;
        AndarDesejadoCarga = andarDesejadoCarga;
    }

    public string Status { get; set; }
    public int Id { get; }
    public string Movimento { get; set; }

    public int AndarAtual { get; set; }

    public int AndarDesejadoDescarga { get; set; }

    public int AndarDesejadoCarga { get; set; }

    public void MostrarElevador()
    {
        if (AndarDesejadoDescarga == 10000)
        {
            Console.WriteLine($"Elevador {Id}:\nStatus: {Status}\nAndar Atual: {AndarAtual}\nMovimento: {Movimento}\nAndar Desejado: Nenhum\n");

        }
        else
        {
            Console.WriteLine($"Elevador {Id}:\nStatus: {Status}\nAndar Atual: {AndarAtual}\nMovimento: {Movimento}\nAndar Desejado: {AndarDesejadoDescarga}\n");
        }
    }


    public void CalcularTurnoCarga()
    {
        if (AndarDesejadoCarga == 10000)
        {
            CalcularTurnoDescarga();
        }
        else if (Movimento == "Descendo")
        {
            AndarAtual--;
            if (AndarAtual == AndarDesejadoCarga)
            {
                Movimento = "Portas Abertas";
                AndarDesejadoCarga= 10000;
            }

        }
        else if (Movimento == "Subindo")
        {
            AndarAtual++;
            if (AndarAtual == AndarDesejadoCarga)
            {
                Movimento = "Portas Abertas";
                AndarDesejadoCarga = 10000;
            }
        }
    }

    public void CalcularTurnoDescarga()
    {

        if (Movimento=="Portas Abertas")
        {
            if (AndarAtual > AndarDesejadoDescarga)
            {
                Movimento = "Descendo";
            }
            else if (AndarAtual < AndarDesejadoDescarga)
            {
                Movimento = "Subindo";
            }
            else
            {
                Movimento = "Parado";
                Status = "Disponível";
                AndarDesejadoDescarga = 10000;
            }
        }
        else if ( Movimento == "Descendo")
        {
            AndarAtual--;
            if (AndarAtual == AndarDesejadoDescarga)
            {
                
                Movimento = "Portas Abertas";
            }

        }
        else if ( Movimento == "Subindo")
        {
            AndarAtual++;
            if (AndarAtual == AndarDesejadoDescarga)
            {
                Movimento = "Portas Abertas";
            }
        }

    }


}