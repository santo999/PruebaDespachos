namespace Despachos.Domain.Entities;

public class Despacho
{
    public Guid Id { get; private set; }
    public string ReferenciaExterna { get; private set; } = default!;
    public Guid RepuestoId { get; private set; }
    public Repuesto? Repuesto { get; private set; }
    public int Cantidad { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    private Despacho()
    {
    }

    private Despacho(string referenciaExterna, Guid repuestoId, int cantidad, DateTime fechaRegistro)
    {
        ReferenciaExterna = referenciaExterna;
        RepuestoId = repuestoId;
        Cantidad = cantidad;
        FechaRegistro = fechaRegistro;
    }

    public static Despacho Crear(string referenciaExterna, Guid repuestoId, int cantidad)
    {
        if (string.IsNullOrWhiteSpace(referenciaExterna))
        {
            throw new ArgumentException("La referencia externa es obligatoria.");
        }
        if (cantidad <= 0)
        {
            throw new ArgumentException("La cantidad debe ser mayor que cero.");
        }

        return new Despacho(referenciaExterna.Trim(), repuestoId, cantidad, DateTime.UtcNow);
    }
}