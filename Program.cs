using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        int opcionPrincipal = 0;

        do
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("       COOPERATIVA AHORRO SEGURO");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Ejercicio 1 - Cajero automático");
            Console.WriteLine("2. Ejercicio 2 - Simulador de préstamo");
            Console.WriteLine("3. Salir");
            Console.Write("Seleccione una opción: ");

            if (!int.TryParse(Console.ReadLine(), out opcionPrincipal))
                opcionPrincipal = 0;

            switch (opcionPrincipal)
            {
                case 1:
                    string pinCorrecto = "1234";
                    int intentos = 3;
                    bool accesoPermitido = false;

                    while (intentos > 0 && !accesoPermitido)
                    {
                        Console.Write("Digite su PIN de cuatro dígitos: ");
                        string pinIngresado = Console.ReadLine();

                        if (pinIngresado == pinCorrecto)
                        {
                            accesoPermitido = true;
                            Console.WriteLine("PIN correcto. Bienvenido/a.");
                        }
                        else
                        {
                            intentos--;
                            Console.WriteLine("PIN incorrecto. Intentos restantes: " + intentos);
                        }
                    }

                    if (!accesoPermitido)
                    {
                        Console.WriteLine("La cuenta ha sido bloqueada por superar los tres intentos.");
                        break;
                    }

                    double saldo = 10000.00;
                    int cantidadDepositos = 0;
                    int cantidadRetiros = 0;
                    int opcionCajero = 0;

                    do
                    {
                        Console.WriteLine("\n---------- CAJERO AUTOMÁTICO ----------");
                        Console.WriteLine("1. Consultar saldo");
                        Console.WriteLine("2. Depositar");
                        Console.WriteLine("3. Retirar");
                        Console.WriteLine("4. Salir");
                        Console.Write("Seleccione una opción: ");

                        if (!int.TryParse(Console.ReadLine(), out opcionCajero))
                            opcionCajero = 0;

                        switch (opcionCajero)
                        {
                            case 1:
                                Console.WriteLine("Su saldo actual es: RD$ " + saldo.ToString("N2"));
                                break;

                            case 2:
                                Console.Write("Digite el monto que desea depositar: RD$ ");
                                double deposito;

                                if (double.TryParse(Console.ReadLine(), NumberStyles.Number,
                                    CultureInfo.CurrentCulture, out deposito) && deposito > 0)
                                {
                                    saldo += deposito;
                                    cantidadDepositos++;
                                    Console.WriteLine("Depósito realizado correctamente.");
                                    Console.WriteLine("Nuevo saldo: RD$ " + saldo.ToString("N2"));
                                }
                                else
                                {
                                    Console.WriteLine("Monto inválido. El depósito debe ser mayor que cero.");
                                }
                                break;

                            case 3:
                                Console.Write("Digite el monto que desea retirar: RD$ ");
                                double retiro;

                                if (double.TryParse(Console.ReadLine(), NumberStyles.Number,
                                    CultureInfo.CurrentCulture, out retiro))
                                {
                                    if (retiro <= 0)
                                        Console.WriteLine("El retiro debe ser mayor que cero.");
                                    else if (retiro > saldo)
                                        Console.WriteLine("No tiene suficiente saldo.");
                                    else if (retiro % 100 != 0)
                                        Console.WriteLine("El retiro debe ser múltiplo de RD$ 100.");
                                    else
                                    {
                                        saldo -= retiro;
                                        cantidadRetiros++;
                                        Console.WriteLine("Retiro realizado correctamente.");
                                        Console.WriteLine("Nuevo saldo: RD$ " + saldo.ToString("N2"));
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Debe escribir un monto numérico válido.");
                                }
                                break;

                            case 4:
                                Console.WriteLine("\nResumen de la cuenta:");
                                Console.WriteLine("Cantidad de depósitos: " + cantidadDepositos);
                                Console.WriteLine("Cantidad de retiros: " + cantidadRetiros);
                                Console.WriteLine("Saldo final: RD$ " + saldo.ToString("N2"));
                                break;

                            default:
                                Console.WriteLine("Opción no válida. Intente nuevamente.");
                                break;
                        }

                    } while (opcionCajero != 4);
                    break;

                case 2:
                    string repetirPrestamo;

                    do
                    {
                        double montoPrestamo = 0;
                        int plazoMeses = 0;
                        string categoria = "";
                        double tasaAnual = 0;

                        Console.WriteLine("\n---------- SIMULADOR DE PRÉSTAMO ----------");

                        bool montoValido = false;

                        while (!montoValido)
                        {
                            Console.Write("Digite el monto del préstamo (RD$ 5,000 a RD$ 500,000): RD$ ");

                            if (double.TryParse(Console.ReadLine(), NumberStyles.Number,
                                CultureInfo.CurrentCulture, out montoPrestamo)
                                && montoPrestamo >= 5000 && montoPrestamo <= 500000)
                                montoValido = true;
                            else
                                Console.WriteLine("Monto fuera del rango permitido o dato inválido. Intente otra vez.");
                        }

                        bool plazoValido = false;

                        while (!plazoValido)
                        {
                            Console.Write("Digite el plazo (de 6 a 60 meses): ");

                            if (int.TryParse(Console.ReadLine(), out plazoMeses)
                                && plazoMeses >= 6 && plazoMeses <= 60)
                                plazoValido = true;
                            else
                                Console.WriteLine("El plazo debe ser un número entre 6 y 60 meses.");
                        }

                        bool categoriaValida = false;

                        while (!categoriaValida)
                        {
                            Console.Write("Digite la categoría del socio (A, B o C): ");
                            categoria = (Console.ReadLine() ?? "").Trim().ToUpper();

                            if (categoria == "A" || categoria == "B" || categoria == "C")
                                categoriaValida = true;
                            else
                                Console.WriteLine("Categoría inválida. Solo se acepta A, B o C.");
                        }

                        if (categoria == "A")
                            tasaAnual = 0.12;
                        else if (categoria == "B")
                            tasaAnual = 0.15;
                        else
                            tasaAnual = 0.18;

                        double tasaMensual = tasaAnual / 12.0;
                        double factor = Math.Pow(1 + tasaMensual, plazoMeses);
                        double cuotaFija = montoPrestamo * (tasaMensual * factor) / (factor - 1);
                        cuotaFija = Math.Round(cuotaFija, 2);

                        double saldoPendiente = montoPrestamo;
                        double totalIntereses = 0;
                        double totalPagado = 0;

                        Console.WriteLine("\n========== TABLA DE AMORTIZACIÓN ==========");
                        Console.WriteLine("Mes\tCuota\t\tInterés\t\tAbono capital\tSaldo");
                        Console.WriteLine("---------------------------------------------------------------");

                        for (int mes = 1; mes <= plazoMeses; mes++)
                        {
                            double interes = Math.Round(saldoPendiente * tasaMensual, 2);
                            double abonoCapital = Math.Round(cuotaFija - interes, 2);
                            double cuotaMes = cuotaFija;

                            if (mes == plazoMeses)
                            {
                                abonoCapital = saldoPendiente;
                                cuotaMes = Math.Round(interes + abonoCapital, 2);
                            }

                            saldoPendiente = Math.Round(saldoPendiente - abonoCapital, 2);
                            totalIntereses += interes;
                            totalPagado += cuotaMes;

                            Console.WriteLine(mes + "\tRD$ " + cuotaMes.ToString("N2")
                                + "\tRD$ " + interes.ToString("N2")
                                + "\tRD$ " + abonoCapital.ToString("N2")
                                + "\tRD$ " + saldoPendiente.ToString("N2"));
                        }

                        Console.WriteLine("---------------------------------------------------------------");
                        Console.WriteLine("Monto solicitado: RD$ " + montoPrestamo.ToString("N2"));
                        Console.WriteLine("Tasa anual: " + (tasaAnual * 100).ToString("N0") + "%");
                        Console.WriteLine("Cuota fija mensual aproximada: RD$ " + cuotaFija.ToString("N2"));
                        Console.WriteLine("Total de intereses: RD$ " + totalIntereses.ToString("N2"));
                        Console.WriteLine("Total pagado: RD$ " + totalPagado.ToString("N2"));

                        Console.Write("\n¿Desea simular otro préstamo? (S/N): ");
                        repetirPrestamo = (Console.ReadLine() ?? "").Trim().ToUpper();

                        while (repetirPrestamo != "S" && repetirPrestamo != "N")
                        {
                            Console.Write("Respuesta no válida. Escriba S para sí o N para no: ");
                            repetirPrestamo = (Console.ReadLine() ?? "").Trim().ToUpper();
                        }

                    } while (repetirPrestamo == "S");
                    break;

                case 3:
                    Console.WriteLine("Gracias por utilizar la Cooperativa Ahorro Seguro.");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Seleccione 1, 2 o 3.");
                    break;
            }

        } while (opcionPrincipal != 3);
    }
}
