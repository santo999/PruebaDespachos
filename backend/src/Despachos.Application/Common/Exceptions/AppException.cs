namespace Despachos.Application.Common.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string message) : base(message)
    {
    }
}

public sealed class RepuestoNoEncontradoException : AppException
{
    public Guid RepuestoId { get; }

    public RepuestoNoEncontradoException(Guid repuestoId)
        : base($"El repuesto con id {repuestoId} no existe.")
    {
        RepuestoId = repuestoId;
    }
}

public sealed class CantidadInvalidaException : AppException
{
    public int CantidadSolicitada { get; }

    public CantidadInvalidaException(int cantidadSolicitada)
        : base($"La cantidad solicitada ({cantidadSolicitada}) debe ser mayor que cero.")
    {
        CantidadSolicitada = cantidadSolicitada;
    }
}

public sealed class StockInsuficienteException : AppException
{
    public String RepuestoId { get; }
    public int CantidadSolicitada { get; }
    public int CantidadDisponible { get; }

    public StockInsuficienteException(String nombreRepuesto, int cantidadSolicitada, int cantidadDisponible)
        : base($"Stock insuficiente para el repuesto {nombreRepuesto}: solicitado {cantidadSolicitada}, disponible {cantidadDisponible}.")
    {
        RepuestoId = nombreRepuesto;
        CantidadSolicitada = cantidadSolicitada;
        CantidadDisponible = cantidadDisponible;
    }
}

public sealed class ReferenciaExternaDuplicadaException : AppException
{
    public string ReferenciaExterna { get; }

    public ReferenciaExternaDuplicadaException(string referenciaExterna)
        : base($"La referencia externa '{referenciaExterna}' ya fue procesada.")
    {
        ReferenciaExterna = referenciaExterna;
    }
}
