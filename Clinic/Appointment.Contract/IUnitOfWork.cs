namespace Appointment.Contract;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}