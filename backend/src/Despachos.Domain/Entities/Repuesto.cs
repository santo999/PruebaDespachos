namespace Despachos.Domain.Entities;

public class Repuesto
{
    public Guid Id { get; private set; }

    public string Sku { get; private set; } = default!;

    public string Nombre { get; private set; } = default!;

    public int CantidadDisponible { get; private set; }

    private Repuesto()
    {
    }

    public Repuesto(string sku, string nombre, int cantidadInicial)
    {

        if (string.IsNullOrWhiteSpace(sku))
        {
            throw new ArgumentException("El SKU es obligatorio.");

        }
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre es obligatorio.");

        }
        if (cantidadInicial < 0)
        {
            throw new ArgumentException("La cantidad inicial no puede ser negativa.");
        }

        Sku = sku.Trim();
        Nombre = nombre.Trim();
        CantidadDisponible = cantidadInicial;
    }

    public void DescontarStock(int cantidad)
    {

        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad a descontar debe ser mayor que cero.");
        }

        if (CantidadDisponible < cantidad)
        {
            throw new InvalidOperationException("No hay stock suficiente para descontar.");
        }

        CantidadDisponible -= cantidad;
    }
}
