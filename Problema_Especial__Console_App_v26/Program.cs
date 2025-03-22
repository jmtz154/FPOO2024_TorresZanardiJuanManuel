using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using ScottPlot;
using ScottPlot.Palettes;
using ScottPlot.Plottables;
using ScottPlot.Colormaps;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using HarfBuzzSharp;
using System.Net.NetworkInformation;
using System.Security.Cryptography;






class Program
{
    static void Main()
    {        
        string folder = "..\\..\\..\\..\\Registros\\";
        string folder0 = folder + "registros_prueba\\";
        string folder1 = folder + "registros_ayacucho\\";
        string folder2 = folder + "registros_laplata_av60\\";
        string folder3 = folder + "registros_laplata_rp06\\";
        string folder4 = folder + "registros_ranchos\\";
        string dataBase1 = folder + "registro_maswitch.db";
     

        // ACÁ CREO LAS ESTACIONES
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        Estacion est0 = new Estacion("RN00 Base Marambio", -64.241439, -56.625017);
        Estacion est1 = new Estacion("RP50 Ayacucho", -37.1258, -58.5592);
        Estacion est2 = new Estacion("LP Tiro Federal sentido LP", -34.8907, -57.9107); // 34°53'26.5"S 57°54'38.5"W
        Estacion est3 = new Estacion("LP RP06 Km0", -35.0498, -58.1190);                // 35°02'59.3"S 58°07'08.4"W
        Estacion est4 = new Estacion("RP20 Ranchos", -35.5183, -58.2964);
        Estacion est5 = new Estacion("Maschwitz_r09", -34.38872312812213, -58.745078680496185);

        // ACÁ CREO DIFERENTES EQUIPOS
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //EquipoVC vc0 = new EquipoVC(folder0);
        //EquipoVC vc1 = new EquipoVC(folder1);
        //EquipoVC vc2 = new EquipoVC(folder2);
        //EquipoVC vc3 = new EquipoVC(folder3);
        //EquipoVC vc4 = new EquipoVC(folder4);        
        //EquipoVR vr1 = new EquipoVR(dataBase1, "Radar 01");

        // ACÁ AGREGO LOS EQUIPOS A LAS ESTACIONES (y al mismo tiempo creo los equipos)
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        est0.AgregarEquipo(new EquipoVC(folder0));
        est1.AgregarEquipo(new EquipoVC(folder1));
        est2.AgregarEquipo(new EquipoVC(folder2));
        est3.AgregarEquipo(new EquipoVC(folder3));
        est4.AgregarEquipo(new EquipoVC(folder4));
        est5.AgregarEquipo(new EquipoVR(dataBase1, "Radar 01"));

        //==================================================================================================================================
        //==================================================================================================================================

        DateOnly startDate = new DateOnly(2024, 12, 05);
        DateOnly endDate = new DateOnly(2024, 12, 15);



        // IMPRIMIR TOTAL DE VEHICULOS
        //----------------------------------------------------------------------------------------------------------------------------------
        //est0.ImprimirTotalVehiculos(est0.ListaDeEquipos[0]);
        est1.ImprimirTotalVehiculos(est1.ListaDeEquipos[0]);
        //est2.ImprimirTotalVehiculos(est2.ListaDeEquipos[0]);
        //est3.ImprimirTotalVehiculos(est3.ListaDeEquipos[0]);
        //est4.ImprimirTotalVehiculos(est4.ListaDeEquipos[0]);
        //est5.ImprimirTotalVehiculos(est5.ListaDeEquipos[0]);

        // IMPRIMIR HISTORIAL
        //----------------------------------------------------------------------------------------------------------------------------------
        //est0.ImprimirHistorial(est0.ListaDeEquipos[0], 1, startDate, endDate);
        //est1.ImprimirHistorial(est1.ListaDeEquipos[0], 1, new DateOnly(2024, 12, 12), new DateOnly(2024, 12, 14));
        //est1.ImprimirHistorial(est1.ListaDeEquipos[0], 1);
        //est2.ImprimirHistorial(est2.ListaDeEquipos[0], 1);
        //est3.ImprimirHistorial(est3.ListaDeEquipos[0], 1);
        //est4.ImprimirHistorial(est4.ListaDeEquipos[0], 1);
        //est5.ImprimirHistorial(est5.ListaDeEquipos[0], 1);


        // GRAFICAR HISTOGRAMA
        //----------------------------------------------------------------------------------------------------------------------------------
        //est0.GraficoDeBarras(est0.ListaDeEquipos[0], 1, startDate, endDate);
        //est1.GraficoDeBarras(est1.ListaDeEquipos[0], 1);
        //est2.GraficoDeBarras(est2.ListaDeEquipos[0], 1);
        //est3.GraficoDeBarras(est3.ListaDeEquipos[0], 1);
        //est4.GraficoDeBarras(est4.ListaDeEquipos[0], 1);
        //est5.GraficoDeBarras(est5.ListaDeEquipos[0], 1);

        // EJ. Aceder y cambiar propiedad de via
        //----------------------------------------------------------------------------------------------------------------------------------        
        //Console.WriteLine($"Original:   Carril {est5.ListaDeEquipos[0].ListaDeVias[0].NumeroDeCarril} -> {est5.ListaDeEquipos[0].ListaDeVias[0].SentidoCirculacion}");
        //est5.ListaDeEquipos[0].ListaDeVias[0].SentidoCirculacion = "LALALALALALALALA";
        //Console.WriteLine($"Modificado: Carril {est5.ListaDeEquipos[0].ListaDeVias[0].NumeroDeCarril} -> {est5.ListaDeEquipos[0].ListaDeVias[0].SentidoCirculacion}");


        Console.ReadKey();
        //==================================================================================================================================
        //==================================================================================================================================

    }
}

class Estacion
{
    private List<Equipo> listaEquipos = new List<Equipo>();
    private (double latitud, double longitud) coordenadas;
    private string lugar;

    public List<Equipo> ListaDeEquipos { get { return listaEquipos; } }
    public double Latitud { get { return coordenadas.latitud; } }
    public double Longitud { get { return coordenadas.longitud; } }
    public string Lugar { get { return lugar; } }

    public Estacion(string _lugar, double _latitud, double _longitud)
    {
        this.lugar = _lugar;
        this.coordenadas.latitud = _latitud;
        this.coordenadas.longitud = _longitud;
    }

    public void AgregarEquipo(Equipo eq)
    {
        listaEquipos.Add(eq);
    }
    public void RemoverEquipo(Equipo eq)
    {
        listaEquipos.Remove(eq);
    }


    public void ImprimirTotalVehiculos(Equipo equipo, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        try
        {
            if (equipo.GetType() != typeof(EquipoVC) && equipo.GetType() != typeof(EquipoVR)) { throw new TipoDeEquipoInvalido(); }

            if (fechaInicial == null) fechaInicial = new DateOnly(1950, 01, 01);
            if (fechaFinal == null) fechaFinal = new DateOnly(2150, 01, 01);

            string equipoType = equipo.GetType().ToString();
            switch (equipoType)
            {
                case "EquipoVC":
                    EquipoVC equipoVC = (EquipoVC)equipo;
                    equipoVC.ImprimirTotalVehiculos((DateOnly)fechaInicial, (DateOnly)fechaFinal);
                    break;
                case "EquipoVR":
                    EquipoVR equipoVR = (EquipoVR)equipo;
                    equipoVR.ImprimirTotalVehiculos();
                    break;
            }
            Console.ReadKey();
        }
        catch (TipoDeEquipoInvalido ex)
        {
            Console.WriteLine($"===================================================================");
            Console.WriteLine($"¡¡ERROR en metodo 'ImprimirTotalVehiculos()' en la clase 'Equipo'!!");
            Console.WriteLine($"Tipo de Equipo invalido");
            Console.WriteLine($"===================================================================");
            Console.WriteLine($"{ex.Message}");
            Console.ReadKey();
            return; // Corta la ejecucion del metodo.

        }
        catch (Exception ex)
        {
            Console.WriteLine($"=====================================================================");
            Console.WriteLine($"¡¡La cagó el metodo 'ImprimirTotalVehiculos()' en la clase 'Equipo'!!");
            Console.WriteLine($"  ERROR: Excepción inesperada!");
            Console.WriteLine($"=====================================================================");
            Console.WriteLine($"{ex.Message}");
            Console.ReadKey();
            return; // Corta la ejecucion del metodo.
        }
    }
    public void ImprimirHistorial(Equipo equipo, int _via, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        try
        {
            if (equipo.GetType() != typeof(EquipoVC) && equipo.GetType() != typeof(EquipoVR)) { throw new TipoDeEquipoInvalido(); }

            if (fechaInicial == null) fechaInicial = new DateOnly(1950, 01, 01);
            if (fechaFinal == null) fechaFinal = new DateOnly(2150, 01, 01);

            string equipoType = equipo.GetType().ToString();
            switch (equipoType)
            {
                case "EquipoVC":
                    EquipoVC equipoVC = (EquipoVC)equipo;
                    equipoVC.ImprimirHistorial(_via, (DateOnly)fechaInicial, (DateOnly)fechaFinal);
                    break;
                case "EquipoVR":
                    EquipoVR equipoVR = (EquipoVR)equipo;
                    equipoVR.ImprimirHistorial(_via);
                    break;
            }
            Console.ReadKey();
        }
        catch (TipoDeEquipoInvalido ex)
        {
            Console.WriteLine($"============================================");
            Console.WriteLine($"¡¡ERROR!! Se cagó algo en 'ImprimirHistorial()' en la clase 'Estacion'");
            Console.WriteLine($"============================================");
            Console.WriteLine($"{ex.Message}");
            Console.ReadKey();
            return; // Corta la ejecucion del metodo.

        }
        catch (Exception ex)
        {
            Console.WriteLine($"================================");
            Console.WriteLine($"¡¡ERROR!! Se cagó algo en 'ImprimirHistorial()' en la clase 'Estacion'");
            Console.WriteLine($"================================");
            Console.WriteLine($"{ex.Message}");
            Console.ReadKey();
            return; // Corta la ejecucion del metodo.
        }
    }
    public void GraficoDeBarras(Equipo equipo, int direccion, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        try
        {
            if (equipo.GetType() != typeof(EquipoVC) && equipo.GetType() != typeof(EquipoVR)) { throw new TipoDeEquipoInvalido(); }
            if (direccion != 1 && direccion != 2) { throw new ParametroInvalido("direccion"); }

            if (fechaInicial == null) fechaInicial = new DateOnly(1950, 01, 01);
            if (fechaFinal == null) fechaFinal = new DateOnly(2150, 01, 01);

            string equipoType = equipo.GetType().ToString();
            switch (equipoType)
            {
                case "EquipoVC":
                    EquipoVC equipoVC = (EquipoVC)equipo;
                    equipoVC.GraficoDeBarras(direccion, this.Lugar, (DateOnly)fechaInicial, (DateOnly)fechaFinal);
                    break;
                case "EquipoVR":
                    EquipoVR equipoVR = (EquipoVR)equipo;
                    equipoVR.GraficoDeBarras(direccion, this.Lugar);
                    break;
            }
            //Console.ReadKey();
        }
        catch (TipoDeEquipoInvalido ex)
        {
            Console.WriteLine($"============================================");
            Console.WriteLine($"¡¡ERROR en metodo GraficoDeBarras()!!");
            Console.WriteLine($"============================================");
            Console.WriteLine($"{ex.Message}");
            Console.ReadKey();
            return; // Corta la ejecucion del metodo.

        }
        catch (Exception ex)
        {
            Console.WriteLine($"================================");
            Console.WriteLine($"  ERROR: Excepción inesperada!");
            Console.WriteLine($"================================");
            Console.WriteLine($"{ex.Message}");
            Console.ReadKey();
            return; // Corta la ejecucion del metodo.
        }
    }
}

abstract class Equipo
{
    protected string nombreEquipo;
    protected string descripcion;
    protected int numVias;
    protected List<DatosVia> listaVias = new List<DatosVia>();

    public List<DatosVia> ListaDeVias
    {
        get { return listaVias; }
        set { listaVias = value; }
    }

    public string NombreEquipo { get { return nombreEquipo; } set { nombreEquipo = value; } }
    public string Descripcion { get { return descripcion; } set { descripcion = value; } }
    public int NumVias { get { return numVias; } }

    public Equipo(string nombreEquipo = "NN", string descripcion = "", int numVias = 0) // CONSTRUCTOR
    {
        this.nombreEquipo = nombreEquipo;
        this.descripcion = descripcion;
        this.numVias = numVias;
    }

    public abstract void ImprimirTotalVehiculos(DateOnly? fechaInicial = null, DateOnly? fechaFinal = null);
    public abstract void ImprimirHistorial(int _via, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null);
    public abstract void GraficoDeBarras(int direccion, string? pngName = null, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null);
}

class EquipoVC : Equipo
{
    protected string carpeta;
    protected DateOnly _fechaMenor;
    protected DateOnly _fechaMayor;

    public EquipoVC(string carpeta, string? _nombreEquipo = null) : base() // CONTRUCTOR
    {
        this.carpeta = carpeta;
        this.nombreEquipo = "NN";
        this.descripcion = "X";
        this.numVias = 2;
        ObtenerPropiedadesEquipo();
        CargarVias();
        RangoDeFechasEnCarpeta();
        if (_nombreEquipo != null) { this.nombreEquipo = _nombreEquipo; }
    }

    private void ObtenerCoordenadasEquipo()
    {
        try
        {
            string archivo = Directory.GetFiles(carpeta, "*.txt").ElementAt(0);
            double latitud = double.Parse(File.ReadLines(archivo).ElementAt(2).Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[2]);
            double longitud = double.Parse(File.ReadLines(archivo).ElementAt(2).Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[3]);
            Console.WriteLine($"Latitud:  {latitud}");
            Console.WriteLine($"Longitud: {longitud}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"=====================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó en el metodo 'ObtenerCoordenadasEquipo()' de la clase 'EquipoVC'");
            Console.WriteLine($"=====================================================================================");
            Console.WriteLine($"{ex.Message}");
        }
    }

    private void ObtenerPropiedadesEquipo()
    {
        try
        {
            string archivo = Directory.GetFiles(carpeta, "*.txt").ElementAt(0);
            this.nombreEquipo = File.ReadLines(archivo).ElementAt(1).Substring(16).Trim();
            this.descripcion = File.ReadLines(archivo).ElementAt(3).Substring(16).Trim();
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"=================================");
            Console.WriteLine($"  ERROR: ¡La carpeta no existe!");
            Console.WriteLine($"=================================");
            Console.WriteLine($"{ex.Message}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"===================================================");
            Console.WriteLine($"  ERROR: ¡Verifique que la carpeta no este vacía!");
            Console.WriteLine($"===================================================");
            Console.WriteLine($"{ex.Message}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"=================================");
            Console.WriteLine($"  ERROR: ¡El archivo no existe!");
            Console.WriteLine($"=================================");
            Console.WriteLine($"{ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"================================");
            Console.WriteLine($"  ERROR: Excepción inesperada!");
            Console.WriteLine($"================================");
            Console.WriteLine($"{ex.Message}");
        }
    }

    private void CargarVias()
    {
        try
        {
            string archivo = Directory.GetFiles(carpeta, "*.txt").ElementAt(0); //tome el 1er archivo en la carpeta
            string via1Sentido = File.ReadLines(archivo).ElementAt(8).Substring(16).Trim();
            string via2Sentido = File.ReadLines(archivo).ElementAt(9).Substring(16).Trim();
            listaVias.Add(new DatosVia(1, via1Sentido)); //Constructor DatosVia(int carril, string sentido) 
            listaVias.Add(new DatosVia(2, via2Sentido));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"=====================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo 'CargarVias()' de la clase 'EquipoVC'");
            Console.WriteLine($"=====================================================================================");
            Console.WriteLine($"{ex.Message}");
        }
    }

    private void RangoDeFechasEnCarpeta() // Busca la fecha más baja y más alta en los archivos de la carpeta y las guarda en las variables privadas.
    {
        try
        {
            string[] listaTodosArchivos = Directory.GetFiles(carpeta, "*.txt");     // Obtener todos los archivos .txt en la carpeta
            List<DateOnly> listaFechaArchivos = new List<DateOnly>();               // Crea una lista de fechas
            foreach (string archivo in listaTodosArchivos)
            {
                DateOnly fecha = LeerFechaDeArchivo(archivo);                       // Lee la fecha de cada archivo
                listaFechaArchivos.Add(fecha);                                      // Guarda fecha de cada archivo en la lista de fechas
            }
            this._fechaMayor = listaFechaArchivos.Max();
            this._fechaMenor = listaFechaArchivos.Min();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"===========================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo '_RangoDeFechasEnCarpeta()' de la clase 'EquipoVC'");
            Console.WriteLine($"===========================================================================================");
            Console.WriteLine($"{ex.Message}");
        }
    }

    private DateOnly LeerFechaDeArchivo(string archivo) // Devuelve la fecha de un archivo especificado.
    {
        try
        {
            string[] ddMMyy = File.ReadLines(archivo).ElementAt(4).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[3].Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            DateOnly fecha = new DateOnly(int.Parse(ddMMyy[2]) + 2000, int.Parse(ddMMyy[1]), int.Parse(ddMMyy[0])); // año, mes, día
            return fecha;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"===========================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo 'LeerFechaDeArchivo()' de la clase 'EquipoVC'");
            Console.WriteLine($"===========================================================================================");
            Console.WriteLine($"{ex.Message}");
            return new DateOnly(0, 0, 0);
        }
    }

    private List<string> ListaArchivosEntreFechas(DateOnly fechaInicial, DateOnly fechaFinal) // Devuelve una lista con los archivos en un rango de fechas.
    {
        try
        {
            DateOnly _fechaXi = new DateOnly();
            DateOnly _fechaXf = new DateOnly();
            if (fechaInicial < this._fechaMenor) { _fechaXi = _fechaMenor; } else { _fechaXi = fechaInicial; }
            if (fechaFinal > this._fechaMayor) { _fechaXf = _fechaMayor; } else { _fechaXf = fechaFinal; }

            string[] listaTodosArchivos = Directory.GetFiles(carpeta, "*.txt");           // Obtener todos los archivos .txt en la carpeta
            List<string> listaRangoArchivos = new List<string>();
            foreach (string archivo in listaTodosArchivos)
            {
                DateOnly fecha = LeerFechaDeArchivo(archivo);
                if (fecha >= _fechaXi && fecha <= _fechaXf)
                {
                    listaRangoArchivos.Add(archivo);
                }
            }
            return listaRangoArchivos;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"===========================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo 'ListaArchivosEntreFechas()' de la clase 'EquipoVC'");
            Console.WriteLine($"===========================================================================================");
            Console.WriteLine($"{ex.Message}");
            return new List<string>();
        }
    }
    //-------------------------
    private List<int> ContarTotalesPorVias(DateOnly fechaInicial, DateOnly fechaFinal)
    {
        DateOnly _fechaXi = new DateOnly();
        DateOnly _fechaXf = new DateOnly();
        if (fechaInicial < this._fechaMenor) { _fechaXi = _fechaMenor; } else { _fechaXi = fechaInicial; }
        if (fechaFinal > this._fechaMayor) { _fechaXf = _fechaMayor; } else { _fechaXf = fechaFinal; }

        List<string> listaDeArchivos = ListaArchivosEntreFechas(_fechaXi, _fechaXf);

        int via1M = 0; int via2M = 0; int via1CM = 0; int via2CM = 0;

        foreach (string archivo in listaDeArchivos)
        {
            string fechaEnArchivo = LeerFechaDeArchivo(archivo).ToString("yyyy/MM/dd");
            List<string> lineasEnArchivo = File.ReadAllLines(archivo).Skip(25).ToList();

            for (int i = 0; i < lineasEnArchivo.Count; i++)
            {
                lineasEnArchivo[i] = fechaEnArchivo + " " + lineasEnArchivo[i];

                // vía 1 mano + vía 2 contramano
                if (lineasEnArchivo[i].Substring(24).Remove(1) == "1" && lineasEnArchivo[i].Substring(22).Remove(1) == "0") // Si es vía 1 y sentodo mano:(0).
                {
                    via1M += 1;
                }
                if (lineasEnArchivo[i].Substring(24).Remove(1) == "2" && lineasEnArchivo[i].Substring(22).Remove(1) == "1") // Si es vía 2 y sentodo contra mano:(1).
                {
                    via2CM += 1;
                }
                if (lineasEnArchivo[i].Substring(24).Remove(1) == "2" && lineasEnArchivo[i].Substring(22).Remove(1) == "0") // Si es vía 2 y sentodo mano:(0).
                {
                    via2M += 1;
                }
                if (lineasEnArchivo[i].Substring(24).Remove(1) == "1" && lineasEnArchivo[i].Substring(22).Remove(1) == "1") // Si es vía 1 y sentodo contra mano:(1).
                {
                    via1CM += 1;
                }
            }
        }
        List<int> subTotalesVias = new List<int>();
        subTotalesVias.Add(via1M);
        subTotalesVias.Add(via2CM);
        subTotalesVias.Add(via2M);
        subTotalesVias.Add(via1CM);
        return subTotalesVias;
    }
    private List<string> GenerarHistorialPorVia(int via, DateOnly fechaInicial, DateOnly fechaFinal)
    {
        DateOnly _fechaXi = new DateOnly();
        DateOnly _fechaXf = new DateOnly();
        if (fechaInicial < this._fechaMenor) { _fechaXi = _fechaMenor; } else { _fechaXi = fechaInicial; }
        if (fechaFinal > this._fechaMayor) { _fechaXf = _fechaMayor; } else { _fechaXf = fechaFinal; }

        string viaA_M = "0";
        string viaB_CM = "0";

        try
        {
            if (via == 1) { viaA_M = "1"; viaB_CM = "2"; }
            if (via == 2) { viaA_M = "2"; viaB_CM = "1"; }
            if (via != 1 && via != 2) { throw new OpcionDeViaInvalida(); }
        }
        catch (OpcionDeViaInvalida ex)
        {
            Console.WriteLine($"==========================================");
            Console.WriteLine($" ¡¡ERROR!! Parametro de entrada invalido!");
            Console.WriteLine($"==========================================");
            Console.WriteLine($"{ex.Message}");
        }

        List<string> listaDeArchivos = ListaArchivosEntreFechas(_fechaXi, _fechaXf);

        List<string> TotalSentidoViaA = new List<string>();

        foreach (string archivo in listaDeArchivos)
        {
            string fechaEnArchivo = LeerFechaDeArchivo(archivo).ToString("yyyy/MM/dd");
            List<string> lineasEnArchivo = File.ReadAllLines(archivo).Skip(25).ToList();

            for (int i = 0; i < lineasEnArchivo.Count; i++)
            {
                lineasEnArchivo[i] = fechaEnArchivo + " " + lineasEnArchivo[i];

                // vía 1 mano + vía 2 contramano
                if (lineasEnArchivo[i].Substring(24).Remove(1) == viaA_M && lineasEnArchivo[i].Substring(22).Remove(1) == "0") // Si es vía 1 y sentodo mano:(0).
                {
                    TotalSentidoViaA.Add(lineasEnArchivo[i]);
                }
                if (lineasEnArchivo[i].Substring(24).Remove(1) == viaB_CM && lineasEnArchivo[i].Substring(22).Remove(1) == "1") // Si es vía 2 y sentodo contra mano:(1).
                {
                    TotalSentidoViaA.Add(lineasEnArchivo[i]);
                }
            }
        }
        return TotalSentidoViaA;
    }
    private void DatosBarrasPromedioPorHora(int via, DateOnly fechaInicial, DateOnly fechaFinal, out List<double> posicionBarras_X, out List<double> alturaBarras_Y)
    {
        DateOnly _fechaXi = new DateOnly();
        DateOnly _fechaXf = new DateOnly();
        if (fechaInicial < this._fechaMenor) { _fechaXi = _fechaMenor; } else { _fechaXi = fechaInicial; }
        if (fechaFinal > this._fechaMayor) { _fechaXf = _fechaMayor; } else { _fechaXf = fechaFinal; }

        Dictionary<TimeOnly, int> conteoCadaHora = new Dictionary<TimeOnly, int>(); // Diccionario para contar las ocurrencias cada hora.
        TimeOnly keyHora = new TimeOnly(0, 0);
        for (int i = 0; i < 24; i++)                                                // Inicializar todas las horas del diccionario en 0.
        {
            conteoCadaHora[keyHora] = 69;
            keyHora = keyHora.AddHours(1);
        }

        List<string> TotalSentidoViaA = GenerarHistorialPorVia(via, fechaInicial, fechaFinal);

        foreach (string linea in TotalSentidoViaA)                              // Contar las ocurrencias en cada hora en "TotalSentidoViaA"            
        {
            TimeOnly horaDeLinea = new TimeOnly(int.Parse(linea.Substring(11, 2)), 0);     // Extrae la hora de la linea
            if (conteoCadaHora.ContainsKey(horaDeLinea))
            {
                conteoCadaHora[horaDeLinea]++;
            }
        }

        int rangoDeDias = _fechaXf.DayNumber - _fechaXi.DayNumber;
        alturaBarras_Y = new List<double>();
        posicionBarras_X = new List<double>();

        foreach (var item in conteoCadaHora)
        {
            alturaBarras_Y.Add(item.Value / rangoDeDias);
            posicionBarras_X.Add(item.Key.Hour + 0.5);
        }
        Console.WriteLine();
    }
    //-------------------------
    public override void ImprimirTotalVehiculos(DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        //-----------------------------------------------------------------------------
        DateOnly _fechaInicial;
        DateOnly _fechaFinal;
        if (fechaInicial == null) { _fechaInicial = new DateOnly(1950, 01, 01); } else { _fechaInicial = (DateOnly)fechaInicial; }
        if (fechaFinal == null) { _fechaFinal = new DateOnly(2150, 01, 01); } else { _fechaFinal = (DateOnly)fechaFinal; }
        //NOTA: esto lo tuve que poner porque si pongo en this.ContarTotalesVias() directamente "fechaInicial" se queja que no puede hacer la
        // conversión de DateOnly?() a DateOnly().
        // Detodas formas aunque "fechaInicial" no es null porque el metodo en "Estacion" le pone fecha si es null, como este metodo en publico
        // podria ser usado por fuera y generar un error. Ahora ya no.
        //-----------------------------------------------------------------------------

        List<int> subTotalesVias = this.ContarTotalesPorVias(_fechaInicial, _fechaFinal);

        int totalSentido1 = subTotalesVias[0] + subTotalesVias[1];
        int totalSentido2 = subTotalesVias[2] + subTotalesVias[3];

        Console.WriteLine("==================================================");
        Console.WriteLine($" {this.Descripcion} ; {this.NombreEquipo}");
        Console.WriteLine("==================================================");
        Console.WriteLine($"Vía {listaVias[0].NumeroDeCarril} ({listaVias[0].SentidoCirculacion})   -->  subTotal: {subTotalesVias[0]:N0}");
        Console.WriteLine($"Vía {listaVias[1].NumeroDeCarril} ({listaVias[1].SentidoCirculacion})   -->  subTotal: {subTotalesVias[2]:N0}");
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine($"                                     TOTAL: {(subTotalesVias[0] + subTotalesVias[2]):N0}");
        Console.WriteLine("==================================================");
        //Console.ReadKey();
    }
    public override void ImprimirHistorial(int _via, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        //-----------------------------------------------------------------------------
        DateOnly _fechaInicial;
        DateOnly _fechaFinal;
        if (fechaInicial == null) { _fechaInicial = new DateOnly(1950, 01, 01); } else { _fechaInicial = (DateOnly)fechaInicial; }
        if (fechaFinal == null) { _fechaFinal = new DateOnly(2150, 01, 01); } else { _fechaFinal = (DateOnly)fechaFinal; }
        //NOTA: esto lo tuve que poner porque si pongo en this.ContarTotalesVias() directamente "fechaInicial" se queja que no puede hacer la
        // conversión de DateOnly?() a DateOnly().
        // Detodas formas aunque "fechaInicial" no es null porque el metodo en "Estacion" le pone fecha si es null, como este metodo en publico
        // podria ser usado por fuera y generar un error. Ahora ya no.
        //-----------------------------------------------------------------------------

        List<string> listaHistorial = this.GenerarHistorialPorVia(_via, _fechaInicial, _fechaFinal);

        //--------------------------------------------------------------------
        {
            int lineasPorPagina = 9000; // Ajusta este valor según tu pantalla
            int contadorLineas = 0;
            foreach (string item in listaHistorial)
            {
                Console.WriteLine(item);
                contadorLineas++;

                if (contadorLineas % lineasPorPagina == 0)
                {
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadKey(); // Pausa la ejecución
                }
            }
        }
        //--------------------------------------------------------------------
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine($"Total de vehículos en la vía {listaVias[_via - 1].NumeroDeCarril} ({listaVias[_via - 1].SentidoCirculacion}): {listaHistorial.Count:N0}");
        Console.WriteLine("------------------------------------------------------------------");
        //Console.ReadKey();
    }
    public override void GraficoDeBarras(int direccion, string? pngName = null, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        //-----------------------------------------------------------------------------
        DateOnly _fechaInicial;
        DateOnly _fechaFinal;
        if (fechaInicial == null) { _fechaInicial = new DateOnly(1950, 01, 01); } else { _fechaInicial = (DateOnly)fechaInicial; }
        if (fechaFinal == null) { _fechaFinal = new DateOnly(2150, 01, 01); } else { _fechaFinal = (DateOnly)fechaFinal; }
        //NOTA: esto lo tuve que poner porque si pongo en this.ContarTotalesVias() directamente "fechaInicial" se queja que no puede hacer la
        // conversión de DateOnly?() a DateOnly().
        // Detodas formas aunque "fechaInicial" no es null porque el metodo en "Estacion" le pone fecha si es null, como este metodo en publico
        // podria ser usado por fuera y generar un error. Ahora ya no.
        //-----------------------------------------------------------------------------

        List<double> posicionBarras_X;
        List<double> alturaBarras_Y;
        this.DatosBarrasPromedioPorHora(direccion, _fechaInicial, _fechaFinal, out posicionBarras_X, out alturaBarras_Y);

        int yMax = (int)alturaBarras_Y.Max() * 105 / 100;

        // CREAR GRAFICO
        ScottPlot.Plot plt = new ScottPlot.Plot();
        var barPlot = plt.Add.Bars(posicionBarras_X, alturaBarras_Y);
        //var barPlot = plt.Add.Bars<double>(posicionBarras_X, alturaBarras_Y);

        // AJUSTAR LÍMITES
        plt.Axes.SetLimits(left: 0, right: 24, bottom: 0, top: yMax);
        //plt.Axes.Margins(bottom: 0, top: 0.01, right: 0.01, left: 0.01);

        // CONFIGURAR TITULO Y LABELS DE EJES
        plt.Title("Cantidad Media de Vehículos por Hora", 25);
        plt.XLabel("Hora del día", 16);
        plt.YLabel("Cantidad de vehículos", 16);

        // PROPIEDADES DE LAS BARRAS
        foreach (var bar in barPlot.Bars)
        {
            bar.Size = 0.9;
            bar.Label = bar.Value.ToString();
            bar.FillColor = Colors.Orange;  //DeepSkyBlue;
            bar.LineWidth = 1;
            bar.LineColor = bar.FillColor.Darken(0.5);
            //bar.FillHatchColor = bar.FillColor.Lighten(0.1);
        }
        barPlot.ValueLabelStyle.Bold = false;
        barPlot.ValueLabelStyle.FontSize = 10;

        // GUARDAR IMAGEN Y MOSTRAR
        string _pngName;
        if (pngName == null) { _pngName = "cucaracha.png"; } else { _pngName = pngName; }
        plt.SavePng($"{_pngName}.png", 1200, 900);
        string rutaImagen = $"{_pngName}.png";      // Asegúrate de que el archivo existe            
        Process.Start(new ProcessStartInfo() { FileName = rutaImagen, UseShellExecute = true });  // Comando para a
    }
}

class EquipoVR : Equipo
{
    private SQLiteConnection? _connection;
    private string archivoDB;

    public EquipoVR(string _archivoDB, string? _nombreEquipo = null)  // CONSTRUCTOR
    {
        this.archivoDB = _archivoDB;
        if (_nombreEquipo != null) { this.nombreEquipo = _nombreEquipo; } else { this.nombreEquipo = "NN"; }
        this.descripcion = "";
        this.numVias = 0;
        Crear_y_AbrirConeccion(archivoDB);
        ObtenerPropiedadesEquipo();
        if (this.numVias != 0) { CargarVias(); }
    }

    private void Crear_y_AbrirConeccion(string _RutayArchivo)
    {
        _connection = new SQLiteConnection($@"Data Source={_RutayArchivo};Version=3;");
        _connection.Open();
    }

    private void ObtenerPropiedadesEquipo()
    {
        try
        {
            string querryLeerTodo = @"SELECT * FROM equipo;";
            SQLiteCommand command = new SQLiteCommand(@querryLeerTodo, _connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.descripcion = reader.GetString(2);
                this.numVias = (int)reader.GetInt64(3);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"============================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo 'ObtenerPropiedadesEquipo()' de la clase 'EquipoVR'");
            Console.WriteLine($"============================================================================================");
            Console.WriteLine($"{ex.Message}");
        }
    }

    private void CargarVias()
    {
        try
        {
            string querryLeerTodo = @"SELECT * FROM equipo;";
            SQLiteCommand command = new SQLiteCommand(@querryLeerTodo, _connection);
            SQLiteDataReader reader = command.ExecuteReader();
            //listaVias = new List<DatosVia>();

            while (reader.Read())
            {
                for (int i = 0; i < numVias; i++)
                {
                    listaVias.Add(new DatosVia(i + 1, reader.GetString(4 + i)));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"=====================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo 'CargarVias()' de la clase 'EquipoVR'");
            Console.WriteLine($"=====================================================================================");
            Console.WriteLine($"{ex.Message}");
        }
    }

    private void ObtenerCoordenadasEquipo()
    {
        try
        {
            string querryLeerTodo = @"SELECT * FROM equipo;";
            SQLiteCommand command = new SQLiteCommand(@querryLeerTodo, _connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                double latitud = double.Parse(reader.GetString(1).Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                double longitud = double.Parse(reader.GetString(1).Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                Console.WriteLine($"Latitud:  {latitud}");
                Console.WriteLine($"Longitud: {longitud}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"=============================================================================================");
            Console.WriteLine($" ERROR: Algo se cagó al llegar al metodo  'ObtenerCoordenadasEquipo()' de la clase 'EquipoVR'");
            Console.WriteLine($"=============================================================================================");
            Console.WriteLine($"{ex.Message}");
        }
    }
    //-------------------------
    private List<double> ContarTotalesPorVias()
    {
        List<double> subTotalesVias = new List<double>();
        for (int i = 0; i < numVias; i++)  // Inicializar la lista con ceros para todas las vías
        {
            subTotalesVias.Add(0);
        }

        string query = @"
            SELECT via, COUNT(*) as cantidad 
            FROM datos 
            GROUP BY via 
            ORDER BY via;";

        SQLiteCommand command = new SQLiteCommand(query, _connection);
        using (SQLiteDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                int via = reader.GetInt32(0);
                double cantidad = reader.GetInt32(1);

                // Las vías comienzan en 1, pero los índices en 0
                if (via >= 1 && via <= subTotalesVias.Count)
                {
                    subTotalesVias[via - 1] = cantidad;
                }
            }
        }

        return subTotalesVias;
    }
    private List<string> GenerarHistorialPorVia(int _via)
    {
        List<string> historial = new List<string>();

        string query = $@"
                SELECT hora, via, velocidad, longitud, desviacion
                FROM datos
                WHERE via IN ({_via})
                ORDER BY hora ASC;";

        SQLiteCommand command = new SQLiteCommand(query, _connection);
        SQLiteDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            string fila = $"{reader.GetString(0)} {reader.GetInt32(1)} {reader.GetInt32(2)} {reader.GetInt32(3)} {reader.GetDouble(4)}";
            historial.Add(fila);
        }
        return historial;
    }
    private void DatosBarrasPromedioPorHora(int sentido, out List<double> posicionBarras_X, out List<double> alturaBarras_Y)
    {
        Dictionary<TimeOnly, int> conteoCadaHora = new Dictionary<TimeOnly, int>(); // Diccionario para contar las ocurrencias cada hora.
        TimeOnly keyHora = new TimeOnly(0, 0);
        for (int i = 0; i < 24; i++)                                                // Inicializar todas las horas del diccionario en 0.
        {
            conteoCadaHora[keyHora] = 0;
            keyHora = keyHora.AddHours(1);
        }

        // Construimos la condición WHERE según el sentido        
        string condicionVia = null;
        if (sentido == 1) { condicionVia = "via IN (1, 2, 3)"; }              // Sentido norte
        if (sentido == 2) { condicionVia = "via IN (4, 5, 6)"; }              // Sentido sur
        if (sentido == 0) { condicionVia = "via IN (1, 2, 3, 4, 5, 6)"; }     // Ambos sentidos
        if (sentido != 1 && sentido != 2 && sentido != 0) { condicionVia = "via IN (0)"; } // Null
        //WHERE via = 1  =>  WHERE via = {via}  =>  WHERE {condicionVia}

        string query = $@"
                SELECT 
                    SUBSTR(hora, 1, 2) as horaStr,
                    COUNT(*) as cantidad
                FROM datos 
                WHERE {condicionVia}
                GROUP BY SUBSTR(hora, 1, 2)
                ORDER BY horaStr;";
        SQLiteCommand command = new SQLiteCommand(query, _connection);
        SQLiteDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            string horaStr = reader.GetString(0);
            int cantidad = reader.GetInt32(1);
            TimeOnly hora = new TimeOnly(int.Parse(horaStr), 0);    // Convertimos la hora string a TimeOnly            
            conteoCadaHora[hora] = cantidad;                        // Actualizamos el conteo en el diccionario
        }

        alturaBarras_Y = new List<double>();
        posicionBarras_X = new List<double>();
        foreach (var item in conteoCadaHora)
        {
            alturaBarras_Y.Add(item.Value);
            posicionBarras_X.Add(item.Key.Hour + 0.5);
        }
    }
    //-------------------------
    public override void ImprimirTotalVehiculos(DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        //-----------------------------------------------------------------------------
        // "fechaInicial" y "fechaFinal" si valieran algo, no se usan. Estan por polimorfismo. Los metodos publicos de EquipoVC y EquipoVR
        // tienen los mismo parametros, los usen internamente o no.
        //-----------------------------------------------------------------------------
        //this.numVias = 4;
        List<double> subTotalesVias = this.ContarTotalesPorVias();
        Console.WriteLine("\n=======================================================");
        Console.WriteLine($" {this.Descripcion} ; {this.NombreEquipo}");
        Console.WriteLine("=======================================================");
        double subTotal1 = 0;
        int iFin = 0;
        for (int i = 0; i < numVias / 2; i++)
        {
            Console.WriteLine($"Vía {listaVias[i].NumeroDeCarril} ({listaVias[i].SentidoCirculacion})   -->  subTotal: {subTotalesVias[i]:N0}");
            subTotal1 = subTotal1 + subTotalesVias[i];
            iFin = i;
        }
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine($"                                     subTotal 1: {subTotal1:N0}");
        Console.WriteLine("-------------------------------------------------------");
        double subTotal2 = 0;
        for (int i = iFin + 1; i < numVias; i++)
        {
            Console.WriteLine($"Vía {listaVias[i].NumeroDeCarril} ({listaVias[i].SentidoCirculacion})   -->  subTotal: {subTotalesVias[i]:N0}");
            subTotal2 = subTotal2 + subTotalesVias[i];
        }
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine($"                                     subTotal 2: {subTotal2:N0}");
        Console.WriteLine("=======================================================");
        Console.WriteLine($"                                         TOTAL: {(subTotal1 + subTotal2):N0}");
        Console.WriteLine("=======================================================");
        //Console.ReadKey();
    }
    public override void ImprimirHistorial(int _via, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        //-----------------------------------------------------------------------------
        // "fechaInicial" y "fechaFinal" si valieran algo, no se usan. Estan por polimorfismo. Los metodos publicos de EquipoVC y EquipoVR
        // tienen los mismo parametros, los usen internamente o no.
        //-----------------------------------------------------------------------------

        List<string> listaHistorial = this.GenerarHistorialPorVia(_via);

        //--------------------------------------------------------------------
        {
            int lineasPorPagina = 9000; // Ajusta este valor según tu pantalla
            int contadorLineas = 0;
            foreach (string item in listaHistorial)
            {
                Console.WriteLine(item);
                contadorLineas++;

                if (contadorLineas % lineasPorPagina == 0)
                {
                    Console.WriteLine("Presiona Enter para continuar...");
                    Console.ReadKey(); // Pausa la ejecución
                }
            }
        }
        //--------------------------------------------------------------------
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine($"Total de vehículos en la vía {listaVias[_via - 1].NumeroDeCarril} ({listaVias[_via - 1].SentidoCirculacion}): {listaHistorial.Count:N0}");
        Console.WriteLine("------------------------------------------------------------------");
        //Console.ReadKey();
    }
    public override void GraficoDeBarras(int sentido, string? pngName = null, DateOnly? fechaInicial = null, DateOnly? fechaFinal = null)
    {
        //-----------------------------------------------------------------------------
        // "fechaInicial" y "fechaFinal" si valieran algo, no se usan. Estan por polimorfismo. Los metodos publicos de EquipoVC y EquipoVR
        // tienen los mismo parametros, los usen internamente o no.
        //-----------------------------------------------------------------------------

        List<double> posicionBarras_X;
        List<double> alturaBarras_Y;
        this.DatosBarrasPromedioPorHora(sentido, out posicionBarras_X, out alturaBarras_Y);

        int yMax = (int)alturaBarras_Y.Max() * 105 / 100;

        // CREAR GRAFICO
        ScottPlot.Plot plt = new ScottPlot.Plot();
        var barPlot = plt.Add.Bars(posicionBarras_X, alturaBarras_Y);
        //var barPlot = plt.Add.Bars<double>(posicionBarras_X, alturaBarras_Y);

        // AJUSTAR LÍMITES
        plt.Axes.SetLimits(left: 0, right: 24, bottom: 0, top: yMax);
        //plt.Axes.Margins(bottom: 0, top: 0.01, right: 0.01, left: 0.01);

        // CONFIGURAR TITULO Y LABELS DE EJES
        plt.Title("Cantidad Media de Vehículos por Hora", 25);
        plt.XLabel("Hora del día", 16);
        plt.YLabel("Cantidad de vehículos", 16);

        // PROPIEDADES DE LAS BARRAS
        foreach (var bar in barPlot.Bars)
        {
            bar.Size = 0.9;
            bar.Label = bar.Value.ToString();
            bar.FillColor = Colors.Orange;  //DeepSkyBlue;
            bar.LineWidth = 1;
            bar.LineColor = bar.FillColor.Darken(0.5);
            //bar.FillHatchColor = bar.FillColor.Lighten(0.1);
        }
        barPlot.ValueLabelStyle.Bold = false;
        barPlot.ValueLabelStyle.FontSize = 10;

        // GUARDAR IMAGEN Y MOSTRAR
        string _pngName;
        if (pngName == null) { _pngName = "cucaracha.png"; } else { _pngName = pngName; }
        plt.SavePng($"{_pngName}.png", 1200, 900);
        string rutaImagen = $"{_pngName}.png";      // Asegúrate de que el archivo existe
        Process.Start(new ProcessStartInfo() { FileName = rutaImagen, UseShellExecute = true });  // Comando para a
    }
}

class DatosVia
{
    private int numeroDeCarril;
    private string sentidoCirculacion;

    public int NumeroDeCarril { get { return numeroDeCarril; } set { numeroDeCarril = value; } }
    public string SentidoCirculacion { get { return sentidoCirculacion; } set { sentidoCirculacion = value; } }

    public DatosVia(int carril, string sentido)  // CONSTRUCTOR
    {
        this.numeroDeCarril = carril;
        this.sentidoCirculacion = sentido;
    }
}

public class OpcionDeViaInvalida : Exception
{
    public OpcionDeViaInvalida() : base("El parametro de entrada 'via' en SOLO puede ser '1' o '2'.") { } // Usado en clase "EquipoVC", metodo "GenerarHistorialPorDireccion()"    
}
public class TipoDeEquipoInvalido : Exception
{
    public TipoDeEquipoInvalido() : base("El parametro de entrada 'equipo' no es del tipo correcto." + "\nEl parametro 'equipo' debe ser de tipo EquipoVC o EquipoVR.") { } // Usado en clase "Estacion", metodo "ImprimirTotalVehiculos()"
}
public class ParametroNulo : Exception
{
    public ParametroNulo(string nombreParametro) : base($"El parametro de entrada '{nombreParametro}' es nulo.") { } // Usado en clase "Estacion", metodo "ImprimirTotalVehiculos()"
}
public class ParametroInvalido : Exception
{
    public ParametroInvalido(string parametro) : base($"El parametro de entrada '{parametro}' SOLO puede ser '1' o '2'.") { } // Usado en clase "Estacion", metodo "ImprimirHistorial()"
}
